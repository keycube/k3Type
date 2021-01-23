using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	private List<string> words;
	
	public PlayerController[] player;
	public Enemy enemyPrefab;
	public Projectile projectilePrefab;
	public Vector3 spawnValues;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private int enemies;

	public StatsController statsController;
	public LogsController logsController;

	private int waiveNumber;
	public Text textWaive;
	public bool waiveSuccess;
	private float waiveEnemySpeed;

	
	void Start () 
	{
		words = Utils.GetWords();
		Utils.Shuffle(words);

		waiveSuccess = true;
		waiveNumber = 0;		

		StartCoroutine(SpawnWaves());
		player[0].OnKeyPressed += PlayerController_OnKeyPressedZero;
		player[1].OnKeyPressed += PlayerController_OnKeyPressedOne;
		player[2].OnKeyPressed += PlayerController_OnKeyPressedTwo;

		logsController.Append("startGame");
	}

	void Update()
    {
		/*
        if (Input.GetKeyDown(("a")))
        {
			Debug.Log("newPlayer0");
			logsController.Append("newPlayer,0");
        }
		if (Input.GetKeyDown(("t")))
        {
			Debug.Log("newPlayer1");
			logsController.Append("newPlayer,1");
        }
		if (Input.GetKeyDown(("p")))
        {
			Debug.Log("newPlayer2");
			logsController.Append("newPlayer,2");
        }
		*/
    }

	public void Lose() 
	{
		logsController.Append("loseWaive," + waiveNumber.ToString());
		enemies -= 1;
		waiveSuccess = false;
		textWaive.text = "X (" + waiveNumber.ToString() + ")" ;
	}

	IEnumerator SpawnWaves()
	{
		Debug.Log("TestSpawnWaves");

		if (waiveSuccess) {
			waiveNumber += 1;
		} else {
			waiveSuccess = true;
			waiveNumber = 1;
			enemyCount = 2;
			waiveEnemySpeed = 0f;
		}

		logsController.Append("spawnWaves," + waiveNumber.ToString());

		textWaive.text = waiveNumber.ToString();
		
		Utils.Shuffle(words);

		enemyCount += 1;
		waiveEnemySpeed += 0.05f;

		enemies = 0;
		yield return new WaitForSeconds(waveWait);
		
		int pR = 0;
		for (int i = 0; i < enemyCount; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
			statsController.UpdateCubesGenerated(1);
			enemy.Spawn(words[i].ToLower(), player[0].gameObject.transform.position, waiveEnemySpeed);
			enemies += 1;
			pR += 1;
			if (pR > 2)
			{
				pR = 0;
			}
			yield return new WaitForSeconds(spawnWait);
		}
	}

	public void StartSpawn() 
	{
		statsController.Save();
		StartCoroutine(SpawnWaves());
	}

	private void PlayerController_OnKeyPressedZero(string letter)
	{
		PlayerOnKeyPressed(0, letter);
	}

	private void PlayerController_OnKeyPressedOne(string letter)
	{
		PlayerOnKeyPressed(1, letter);
	}

	private void PlayerController_OnKeyPressedTwo(string letter)
	{
		PlayerOnKeyPressed(2, letter);
	}

	private void PlayerOnKeyPressed(int playerNumber, string letter)
	{
		//statsController.UpdateKeysPressed(1);

		bool isLetterCorrect = false;

		if (player[playerNumber].enemyFocus != null && player[playerNumber].enemyFocus.name != "")
		{
			if (player[playerNumber].enemyFocus.gameObject.name.Length > 0)
			{
				if (player[playerNumber].enemyFocus.gameObject.name[0] == letter.ToLower()[0])
				{
					player[playerNumber].gameObject.transform.LookAt(player[playerNumber].enemyFocus.transform.position);
					Enemy enemy = player[playerNumber].enemyFocus.GetComponent<Enemy>();
					Projectile projectile = Instantiate(projectilePrefab, player[playerNumber].gameObject.transform.position, Quaternion.identity);
					statsController.UpdateCubesGenerated(1);
					projectile.SetTarget(player[playerNumber].enemyFocus.transform, enemy.getOriginWord());
					projectile.SetStatController(statsController);
					isLetterCorrect = true;
					if (enemy.ReduceWord())
					{
						statsController.UpdateWordsTyped(1);
						enemies -= 1;
						Debug.Log("enemies = " + enemies.ToString());
						if (enemies <= 0)
						{
							statsController.Save();
							StartCoroutine(SpawnWaves());
						}							
					}

					player[playerNumber].UpdateSpeed();
				}
			}
		}
		else
		{
			if (transform.childCount > 0)
			{
				foreach (Transform transformChild in transform)
				{
					if (transformChild.gameObject.name.Length > 0)
					{
						if (transformChild.gameObject.name[0] == letter.ToLower()[0])
						{
							logsController.Append(playerNumber.ToString() + ",newWord," + transformChild.gameObject.name);
							player[playerNumber].gameObject.transform.LookAt(transformChild.transform.position);
							Enemy enemy = transformChild.GetComponent<Enemy>();
							Projectile projectile = Instantiate(projectilePrefab, player[playerNumber].gameObject.transform.position, Quaternion.identity);
							statsController.UpdateCubesGenerated(1);
							projectile.SetTarget(transformChild.transform, enemy.getOriginWord());
							projectile.SetStatController(statsController);
							enemy.ReduceWord();
							isLetterCorrect = true;

							player[playerNumber].enemyFocus = transformChild;
							enemy.text.faceColor = player[playerNumber].colorFocus;

							player[playerNumber].StartWord();
														
							break;
						}
					}
				}
			}
		}

		if (!isLetterCorrect)
		{
			GetComponent<AudioSource>().Play();
		}

		player[playerNumber].updateAccuary(isLetterCorrect);
	}
}

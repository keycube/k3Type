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

	public Text textWordsTyped;
	public Text textKeysPressed;
	public Text textCubeGenerated;
	public Text textBestWPM;

	private int wordsTyped;
	private int keysPressed;
	private int cubeGenerated;
	private float bestWPM;

	void Start () 
	{
		wordsTyped = 0;
		keysPressed = 0;
		cubeGenerated = 0;
		bestWPM = 0.0f;

		textWordsTyped.text = wordsTyped.ToString();
		textKeysPressed.text = keysPressed.ToString();
		textCubeGenerated.text = cubeGenerated.ToString();
		textBestWPM.text = bestWPM.ToString("F2");

		words = Utils.GetWords();
		Utils.Shuffle(words);

		StartCoroutine(SpawnWaves());
		player[0].OnKeyPressed += PlayerController_OnKeyPressedZero;
		player[1].OnKeyPressed += PlayerController_OnKeyPressedOne;
		player[2].OnKeyPressed += PlayerController_OnKeyPressedTwo;
	}

	IEnumerator SpawnWaves()
	{
		Utils.Shuffle(words);

		enemyCount += 2;

		enemies = 0;
		yield return new WaitForSeconds(waveWait);
		
		int pR = 0;
		for (int i = 0; i < enemyCount; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
			enemy.Spawn(words[i].ToLower(), player[pR].gameObject.transform.position);
			enemies += 1;
			pR += 1;
			if (pR > 2)
			{
				pR = 0;
			}			
			yield return new WaitForSeconds(spawnWait);
		}
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
					projectile.SetTarget(player[playerNumber].enemyFocus.transform, enemy.getOriginWord());
					isLetterCorrect = true;
					if (enemy.ReduceWord())
					{
						enemies -= 1;
						if (enemies <= 0)
							StartCoroutine(SpawnWaves());
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
							player[playerNumber].gameObject.transform.LookAt(transformChild.transform.position);
							Enemy enemy = transformChild.GetComponent<Enemy>();
							Projectile projectile = Instantiate(projectilePrefab, player[playerNumber].gameObject.transform.position, Quaternion.identity);
							projectile.SetTarget(transformChild.transform, enemy.getOriginWord());
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

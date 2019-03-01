using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private List<string> words;
	
	public PlayerController player;
	public Enemy enemyPrefab;
	public Projectile projectilePrefab;
	public Vector3 spawnValues;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private Color colorFocus = new Color(0.95f, 0.7f, 0.75f);
	public Transform enemyFocus;
	private int enemies;

	void Start () 
	{
		words = Utils.GetWords();
		Utils.Shuffle(words);

		StartCoroutine(SpawnWaves());
		player.OnKeyPressed += PlayerController_OnKeyPressed;
	}

	IEnumerator SpawnWaves()
	{
		Utils.Shuffle(words);

		enemies = 0;
		yield return new WaitForSeconds(waveWait);
		
		for (int i = 0; i < enemyCount; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
			enemy.Spawn(words[i].ToLower(), player.gameObject.transform.position);
			enemies += 1;
			yield return new WaitForSeconds(spawnWait);
		}
	}

	private void PlayerController_OnKeyPressed(string letter)
	{
		bool isLetterCorrect = false;

		if (enemyFocus != null && enemyFocus.name != "")
		{
			if (enemyFocus.gameObject.name.Length > 0)
			{
				if (enemyFocus.gameObject.name[0] == letter.ToLower()[0])
				{
					player.gameObject.transform.LookAt(enemyFocus.transform.position);
					Enemy enemy = enemyFocus.GetComponent<Enemy>();
					Projectile projectile = Instantiate(projectilePrefab, player.gameObject.transform.position, Quaternion.identity);
					projectile.SetTarget(enemyFocus.transform.position, enemy.getOriginWord());
					isLetterCorrect = true;
					if (enemy.ReduceWord()) 
					{	
						enemies -= 1;
						if (enemies <= 0)
							StartCoroutine(SpawnWaves());
					}
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
							player.gameObject.transform.LookAt(transformChild.transform.position);
							Enemy enemy = transformChild.GetComponent<Enemy>();
							Projectile projectile = Instantiate(projectilePrefab, player.gameObject.transform.position, Quaternion.identity);
							projectile.SetTarget(transformChild.transform.position, enemy.getOriginWord());
							enemy.ReduceWord();
							isLetterCorrect = true;

							enemyFocus = transformChild;
							enemy.text.faceColor = colorFocus;
														
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
	}
}

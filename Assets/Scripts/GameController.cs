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

	void Start () 
	{
		words = Utils.GetWords();
		Utils.Shuffle(words);

		StartCoroutine(SpawnWaves());
		player.OnKeyPressed += PlayerController_OnKeyPressed;
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while(true)
		{
			for (int i = 0; i < enemyCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
				enemy.Spawn(words[i], player.gameObject.transform.position);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			Utils.Shuffle(words);
		}
	}

	private void PlayerController_OnKeyPressed(string letter)
	{
		bool isLetterCorrect = false;
		if (transform.childCount > 0)
		{
			foreach (Transform transformChild in transform)
			{
				if (transformChild.gameObject.name.Length > 0)
				{
					if (transformChild.gameObject.name[0] == letter.ToLower()[0])
					{
						Enemy enemy = transformChild.GetComponent<Enemy>();
						Projectile projectile = Instantiate(projectilePrefab, player.gameObject.transform.position, Quaternion.identity);
						projectile.SetTarget(transformChild.transform.position, enemy.getOriginWord());
						enemy.ReduceWord();
						isLetterCorrect = true;
						break;
					}
				}
			}
		}

		if (!isLetterCorrect)
		{
			Debug.Log("Incorrect Letter");
		}
	}
}

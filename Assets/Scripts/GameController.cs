using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private string[] words = { "a", "is", "the", "from", "hello", "typing", "glasgow", "keyboard", "computing", "technology", "interactive", "practitioner", "multicultural", "interconnected", "world", "key", "cube", "chi", "human", "community", "friendship", "researcher", "designer" };
	
	public PlayerController playerController;
	public GameObject player;
	public Enemy enemyPrefab;
	public Projectile projectilePrefab;
	public Vector3 spawnValues;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	void Start () 
	{
		StartCoroutine(SpawnWaves());
		playerController.OnKeyPressed += PlayerController_OnKeyPressed;
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
				enemy.Spawn(words[i], player.transform.position);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	private void PlayerController_OnKeyPressed(string letter)
	{
		if (transform.childCount > 0)
		{
			foreach (Transform transformChild in transform)
			{
				if (transformChild.gameObject.name.Length > 0)
				{
					if (transformChild.gameObject.name[0] == letter.ToLower()[0])
					{
						Debug.Log(transformChild.gameObject.name);
						Enemy enemy = transformChild.GetComponent<Enemy>();
						Projectile projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
						projectile.SetTarget(transformChild.transform.position, enemy.getOriginWord());
						enemy.ReduceWord();
						break;
					}
				}
			}
		}
	}
}

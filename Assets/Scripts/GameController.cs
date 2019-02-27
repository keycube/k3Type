﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private string[] words = { "a", "is", "the", "from", "hello", "typing", "glasgow", "keyboard", "computing", "technology", "interactive", "practitioner", "multicultural", "interconnected", "world", "key", "cube", "chi", "human", "community", "friendship", "researcher", "designer" };
	
	public GameObject player;
	public Enemy enemyPrefab;
	public Vector3 spawnValues;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	void Start () 
	{
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while(true)
		{
			for (int i = 0; i < enemyCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
				enemy.Spawn(words[i], player.transform.position);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
}

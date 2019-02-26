using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public Vector3 spawnValues;

	void Start () 
	{
		SpawnWaves();
	}

	private void SpawnWaves()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
	}
}

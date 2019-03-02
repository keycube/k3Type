using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {

	public Transform cube;

	public TextMeshPro text;

	private float movementSpeed;

	private float rotationSpeed;
	
	private Vector3 target;

	private string word;

	private string originWord;

	private int rotationDirection;
	public float slowTiming;

	void Update ()
	{
		transform.Translate((target - transform.position).normalized * movementSpeed * Time.deltaTime);
		
		float rotation = rotationSpeed * Time.deltaTime;
		cube.Rotate(rotation, rotation, rotation);
	}

	public void Spawn(string word, Vector3 target)
	{
		originWord = word;
		this.word = word;
		gameObject.name = word;
		text.text = word;

		rotationDirection = Random.Range(0, 2) * 2 - 1;
		UpdateFromWord(word.Length);
		
		this.target = target;
	}


	public bool ReduceWord()
	{
		if (gameObject.name.Length <= 1)
		{
			gameObject.name = "";
		}
		else
		{
			gameObject.name = gameObject.name.Substring(1);
		}
		text.text = gameObject.name;
		
		return gameObject.name == "";
	}

	public string getOriginWord()
	{
		return originWord;
	}

	private void UpdateFromWord(int wordLength) 
	{
		cube.localScale = new Vector3(wordLength * 0.2f, wordLength * 0.2f, wordLength * 0.2f);
		movementSpeed = 1.0f / Mathf.Sqrt(wordLength);
		rotationSpeed = 1.0f / wordLength * 100f * rotationDirection;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile")
		{
			if (other.name == originWord) 
			{
				if (word.Length <= 1)
				{
					Destroy(gameObject);
				} 
				else
				{
					word = word.Substring(1);
					UpdateFromWord(word.Length);
					StartCoroutine(slowForXSecond(slowTiming));
				}
				Destroy(other.gameObject);
			}
		}
	}

	IEnumerator slowForXSecond(float time)
	{
		float temp = movementSpeed;
		movementSpeed *= 0.25f;
		yield return new WaitForSeconds(time);
		movementSpeed = temp;
		yield return null;
	}
}
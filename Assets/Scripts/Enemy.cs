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

	void Update ()
	{
		transform.Translate((target - transform.position).normalized * movementSpeed * Time.deltaTime);
		
		float rotation = rotationSpeed * Time.deltaTime;
		cube.Rotate(rotation, rotation, rotation);
	}

	public void Spawn(string word, Vector3 target)
	{
		UpdateFromWord(word);

		this.target = target;
	}


	private void UpdateFromWord(string word) 
	{
		this.word = word;
		text.text = word;
		cube.localScale = new Vector3(word.Length * 0.2f, word.Length * 0.2f, word.Length * 0.2f);
		movementSpeed = 1.0f / Mathf.Sqrt(word.Length);
		rotationSpeed = 1.0f / word.Length * 100f * (Random.Range(0,2)*2-1);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile")
		{
			if (word.Length <= 1)
			{
				Destroy(gameObject);
			} else 
			{
				UpdateFromWord(word.Substring(1));
			}
			Destroy(other.gameObject);
		}
	}
}

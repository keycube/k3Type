using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform cube;

	public float movementSpeed;

	public float rotationSpeed;
	
	private Vector3 target;

	void Update ()
	{
		transform.Translate((target - transform.position).normalized * movementSpeed * Time.deltaTime);
		
		float rotation = rotationSpeed * Time.deltaTime;
		cube.Rotate(rotation, rotation, rotation);
	}

	public void SetTarget(Vector3 target)
	{
		this.target = target;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float rotationSpeed;
	public float movementSpeed;
	private float degrees;
	private Vector3 target;

	void Update ()
	{
		transform.LookAt(target);
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

		degrees += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(degrees, degrees, degrees);	
	}

	public void SetTarget(Vector3 target)
	{
		this.target = target;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}

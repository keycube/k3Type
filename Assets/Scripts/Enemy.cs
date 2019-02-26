using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float rotationSpeed;

	private float degrees;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		degrees += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(degrees, degrees, degrees);	
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

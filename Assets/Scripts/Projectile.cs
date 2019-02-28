using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public Vector3 target;

	public GameObject explosion;

	public float movementSpeed;

	public float rotationSpeed;

	private float degrees;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(target);
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		
		// Because transform.Rotate(...) have a strange effect after transform.LookAt(...)
		degrees += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(degrees, degrees, degrees);
	}

	
	public void SetTarget(Vector3 target, string targetName) 
	{
		this.target = target;
		gameObject.name = targetName;
	}


	void OnDestroy()
	{
		Instantiate(explosion, transform.position, Quaternion.identity);
	}
}

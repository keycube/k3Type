using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public Transform target;

	public float movementSpeed;

	public float rotationSpeed;

	private float z;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(target);
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		
		// Rotate around Z axis, because simple transform.Rotate(...) have a strange effect after transform.LookAt(...)
		z += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, z);
	}

	
	public void SetTarget(Transform target) 
	{
		this.target = target;
	}
}

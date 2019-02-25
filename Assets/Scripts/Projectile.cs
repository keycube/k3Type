using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public Transform target;

	public float speed;

	private float z;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(target);
		// Rotate around Z axis, because simple transform.Rotate(...) have a strange effect after transform.LookAt(...)
		z += Time.deltaTime * 150f * speed;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, z);

		Vector3 direction = target.position - transform.position;
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}

	
	public void SetTarget(Transform target) 
	{
		this.target = target;
	}
}

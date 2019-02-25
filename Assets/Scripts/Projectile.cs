using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public Transform target;

	public float speed;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(target);
		Vector3 direction = target.position - transform.position;
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}
}

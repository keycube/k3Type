using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public Projectile prefabProjectile;

	public Transform target;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A))
        {
            Fire();
        }
	}


	private void Fire()
	{
		Projectile projectile = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
		projectile.SetTarget(target);
	}
}

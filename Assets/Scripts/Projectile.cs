using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public Transform target;

	public GameObject explosion;

	[Range(1f, 20f)]
	public float movementSpeed;

	public float rotationSpeed;

	private float degrees;
	
	private StatsController statsController;

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

	
	public void SetTarget(Transform target, string targetName) 
	{
		this.target = target;
		gameObject.name = targetName;
	}

	
	public void SetStatController(StatsController sc)
	{
		statsController = sc;
	}

	void OnDestroy()
	{
		if (statsController != null)
			statsController.UpdateCubesGenerated(9);
		Instantiate(explosion, transform.position, Quaternion.identity);
	}
}

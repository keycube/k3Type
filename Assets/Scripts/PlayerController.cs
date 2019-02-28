using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour 
{
	public event Action<string> OnKeyPressed = delegate { };
	
	private int a = (int) KeyCode.A;
	private int z = (int) KeyCode.Z;

	void Update()
	{	
		for (int i = a; i <= z; i++)
		{
    		if (Input.GetKeyDown((KeyCode)i))
			{
				if (OnKeyPressed != null)
				{
					OnKeyPressed(((KeyCode)i).ToString());
				}
			}
		}
	}
}
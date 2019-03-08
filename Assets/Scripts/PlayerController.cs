using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerController : MonoBehaviour 
{
	public event Action<string> OnKeyPressed = delegate { };
	public TextMeshPro textAccury;
	public TextMeshPro textSpeed;
	
	private int a = (int) KeyCode.A;
	private int z = (int) KeyCode.Z;
	private float keyPressCount = 0f;
	private float keyPressSuccessCount = 0f; 
	private float keyPressAccuracy = 0f;
	
	private float timeTemp;
	private float timeTotal;

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

	public void updateAccuary(bool b)
	{
		keyPressCount += 1f;
		if (b)
		{
			keyPressSuccessCount += 1f;
		}
		keyPressAccuracy = keyPressSuccessCount / keyPressCount * 100f;
		textAccury.text = keyPressAccuracy.ToString("F2") + "%";

		if (timeTotal != 0f)
			textSpeed.text = (keyPressSuccessCount / timeTotal * 12f).ToString("F2") + " wpm"; // * 12 = * 60 / 5 (cps to wpm)
	}

	public void UpdateSpeed()
	{
		timeTotal += (Time.time - timeTemp);
		timeTemp = Time.time;
	}

	public void StartWord()
	{
		timeTemp = Time.time;
	}
}
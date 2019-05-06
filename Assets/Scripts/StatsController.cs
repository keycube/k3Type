using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour {

	public Text textWordsTyped;
	public Text textKeysPressed;
	public Text textCubeGenerated;
	//public Text textBestWPM;

	private int wordsTyped;
	private int keysPressed;
	private int cubeGenerated;
	//private float bestWPM;

	// Use this for initialization
	void Start ()
	{
		wordsTyped = PlayerPrefs.GetInt("wordsTyped", 0);
		keysPressed = PlayerPrefs.GetInt("keysPressed", 0);
		cubeGenerated = PlayerPrefs.GetInt("cubeGenerated", 0);
		//bestWPM = PlayerPrefs.GetFloat("bestWPM", 0f);
		
		textWordsTyped.text = wordsTyped.ToString();
		textKeysPressed.text = keysPressed.ToString();
		textCubeGenerated.text = cubeGenerated.ToString();
		//textBestWPM.text = bestWPM.ToString("F2");
	}

	public void Save() 
	{
		PlayerPrefs.SetInt("wordsTyped", wordsTyped);
		PlayerPrefs.SetInt("keysPressed", keysPressed);
		PlayerPrefs.SetInt("cubeGenerated", cubeGenerated);
		//PlayerPrefs.SetFloat("bestWPM", bestWPM);
		PlayerPrefs.Save();
	}


	public int UpdateWordsTyped(int increment)
	{
		wordsTyped += increment;
		textWordsTyped.text = wordsTyped.ToString();
		return wordsTyped;
	}

	public int UpdateKeysPressed(int increment)
	{
		keysPressed += increment;
		textKeysPressed.text = keysPressed.ToString();
		return keysPressed;
	}

	public int UpdateCubesGenerated(int increment)
	{
		cubeGenerated += increment;
		textCubeGenerated.text = cubeGenerated.ToString();
		return cubeGenerated;
	}

	/* 
	public float UpdateBestWPM(float value)
	{
		bestWPM = value;
		textBestWPM.text = bestWPM.ToString("F2");
		return bestWPM;
	}
	*/
}

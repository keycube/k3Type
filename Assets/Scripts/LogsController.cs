using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogsController : MonoBehaviour {

	private string pathFileName;

	public string customFileName;

	// Use this for initialization
	void Start () {
		// path + custom name + date + extension
		pathFileName = string.Format("{0}{1}{2}{3}",  "./", customFileName, DateTime.Now.ToString("yyyyMMdd"), ".log");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Append(string data)
	{
		System.IO.File.AppendAllText(pathFileName, string.Format("{0},{1}\n", DateTime.Now.ToString("HH:mm:ss.fff"), data));
	}

	public void AddKeyPress(int playerNumber, string keyCode) 
	{
		Append(string.Format("{1},{2}", playerNumber, keyCode));
	}
	
	public void StartWord(int playerNumber, string keyCode) 
	{
		Append(string.Format("{1},{2}", playerNumber, keyCode));
	}
}

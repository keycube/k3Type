using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.IO.Ports;
using System.Threading;

public class PlayerController : MonoBehaviour
{
	public event Action<string> OnKeyPressed = delegate { };
    public TextMeshPro textAccury;
    public TextMeshPro textSpeed;

    private int a = (int)KeyCode.A;
    private int z = (int)KeyCode.Z;
    private float keyPressCount = 0f;
    private float keyPressSuccessCount = 0f;
    private float keyPressAccuracy = 0f;

    private float timeTemp;
    private float timeTotal;

    public string port = "COM4";
    public int baudrate = 9600;
    private SerialPort serialPort;
    private bool _continue = false;
	Dictionary<string, bool> keyState = new Dictionary<string, bool>();

	public Transform enemyFocus;
	public Color colorFocus;

	public int playerNumber;

	public StatsController statsController;

    public LogsController logsController;

    void Start()
    {
		InitKeyState();

        Thread readThread = new Thread(Read);

        serialPort = new SerialPort(port, baudrate);
        serialPort.ReadTimeout = 500;
        serialPort.WriteTimeout = 500;
        serialPort.Open();
        _continue = true;


        readThread.Start();
    }

	private void InitKeyState() 
	{
		// blue
		keyState.Add("+u0", false); //
		
		keyState.Add("+u1", false);
		keyState.Add("+u2", false);
		keyState.Add("+u3", false);

		keyState.Add("+u4", false); //

		keyState.Add("+u5", false);
		keyState.Add("+u6", false);
		keyState.Add("+u7", false);

		keyState.Add("+u8", false); //

		keyState.Add("+u9", false);
		keyState.Add("+uA", false);
		keyState.Add("+uB", false);

		keyState.Add("+uC", false); //

		keyState.Add("+uD", false);
		keyState.Add("+uE", false);
		keyState.Add("+uF", false);

		// yellow
		keyState.Add("+y0", false);
		keyState.Add("+y1", false);
		keyState.Add("+y2", false);
		keyState.Add("+y3", false);
		keyState.Add("+y4", false);
		keyState.Add("+y5", false);
		keyState.Add("+y6", false);
		keyState.Add("+y7", false);
		keyState.Add("+y8", false);
		keyState.Add("+y9", false);
		keyState.Add("+yA", false);

		keyState.Add("+yB", false); //
		keyState.Add("+yC", false); //
		keyState.Add("+yD", false); //
		keyState.Add("+yE", false); //
		keyState.Add("+yF", false); //

		// red
		keyState.Add("+r0", false); //

		keyState.Add("+r1", false);
		keyState.Add("+r2", false);

		keyState.Add("+r3", false); //
		keyState.Add("+r4", false); //
		keyState.Add("+r5", false); //
		keyState.Add("+r6", false); //

		keyState.Add("+r7", false);

		keyState.Add("+r8", false); //
		keyState.Add("+r9", false); //
		keyState.Add("+rA", false); //

		keyState.Add("+rB", false);

		keyState.Add("+rC", false); //
		keyState.Add("+rD", false); //
		keyState.Add("+rE", false); //
		keyState.Add("+rF", false); //

		// green
		keyState.Add("+g0", false); //
		keyState.Add("+g1", false); //
		keyState.Add("+g2", false); //
		keyState.Add("+g3", false); //
		keyState.Add("+g4", false); //
		keyState.Add("+g5", false); //
		keyState.Add("+g6", false); //
		keyState.Add("+g7", false); //
		keyState.Add("+g8", false); //
		keyState.Add("+g9", false); //
		keyState.Add("+gA", false); //
		keyState.Add("+gB", false); //
		keyState.Add("+gC", false); //
		keyState.Add("+gD", false); //
		keyState.Add("+gE", false); //
		keyState.Add("+gF", false); //

		// white
		keyState.Add("+w0", false); //
		keyState.Add("+w1", false); //
		keyState.Add("+w2", false); //
		keyState.Add("+w3", false); //
		keyState.Add("+w4", false); //
		keyState.Add("+w5", false); //
		keyState.Add("+w6", false); //
		keyState.Add("+w7", false); //
		keyState.Add("+w8", false); //
		keyState.Add("+w9", false); //
		keyState.Add("+wA", false); //
		keyState.Add("+wB", false); //
		keyState.Add("+wC", false); //
		keyState.Add("+wD", false); //
		keyState.Add("+wE", false); //
		keyState.Add("+wF", false); //
	}

    public void Read()
    {
        while (_continue)
        {
            try
            {
                string message = serialPort.ReadTo(".");
                UnityMainThreadDispatcher.Instance().Enqueue(ThisWillBeExecutedOnTheMainThread(message));
            }
            catch (TimeoutException)
            {

            }
        }
    }


    public IEnumerator ThisWillBeExecutedOnTheMainThread(string message)
    {
		Debug.Log(message);
		String s = ConvertKeycubeToLetter(message);
		if (s != "")
		{
			Debug.Log(s);
			if (OnKeyPressed != null)
        	{
	            OnKeyPressed(s);
        	}
		}
        
        yield return null;
    }


    private string ConvertKeycubeToLetter(string code)
    {
		if (!keyState.ContainsKey(code))
		{
			Debug.Log("Do not contains Key");
			return "";
		}		

		if (keyState[code] == true) // if true it means we just release the key, therefore nothing to do
		{
			Debug.Log("Release Key");
            //logsController.AddKeyPress(playerNumber, code, 0);
            logsController.Append(playerNumber.ToString() + "," + code + ",0");
			keyState[code] = false;
			return "";
		}

        //logsController.AddKeyPress(playerNumber, code, 1);
        logsController.Append(playerNumber.ToString() + "," + code + ",1");

		keyState[code] = true;
        switch (code)
        {
			// blue
            case "+u1":
                return "Z";
            case "+u2":
                return "A";
            case "+u3":
                return "Q";

            case "+u5":
                return "X";
            case "+u6":
                return "S";
            case "+u7":
                return "W";

            case "+u9":
                return "C";
            case "+uA":
                return "D";
            case "+uB":
                return "E";

            case "+uD":
                return "V";
            case "+uE":
                return "F";
            case "+uF":
                return "R";

			// yellow
			case "+y0":
                return "U";
            case "+y1":
                return "I";
            case "+y2":
                return "O";
            case "+y3":
                return "P";
            case "+y4":
                return "J";
            case "+y5":
                return "K";
            case "+y6":
                return "L";
            case "+y7":
                return " ";
            case "+y8":
                return "B";
            case "+y9":
                return "N";
            case "+yA":
                return "M";

            // red
			case "+r1":
                return "G";
            case "+r2":
                return "T";

            case "+r7":
                return "Y";

            case "+rB":
                return "H";
        }
        return "";
    }


    public void WriteToArduino(string message)
    {
        serialPort.WriteLine(message);
        serialPort.BaseStream.Flush();
    }


    void Update()
    {
        /* 
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
        */
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
            textSpeed.text = (keyPressSuccessCount / timeTotal * 12f).ToString("F2") + " awpm"; // * 12 = * 60 / 5 (cps to wpm)
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

	public void ResetStat()
	{
		keyPressCount = 0f;
		keyPressSuccessCount = 0f;
		keyPressAccuracy = 0f;
		textAccury.text = "- %";

		timeTotal = 0f;
		textSpeed.text = "- awpm";

		statsController.UpdateCubesGenerated(19);
		statsController.Save();

		WriteToArduino("1");
		StartCoroutine(StopVibrationAfterTime(1f));
	}

	IEnumerator StopVibrationAfterTime(float time)
 	{
    	yield return new WaitForSeconds(time);
     	WriteToArduino("0");
 	}
}
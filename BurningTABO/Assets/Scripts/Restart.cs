﻿using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void restart()
	{
		Debug.Log ("reset");
		Application.LoadLevel("TitleScene");
	}
}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelComplete : MonoBehaviour {

	Timer timer;
	Text score;

	// Use this for initialization
	void Start () {
		timer = GameObject.Find("TimerText").GetComponent<Timer>();
		score = GameObject.Find ("ScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		timer.Pause ();
		score.text = "Score: " + timer.timerString;
		timer.Reset ();
	}
}
﻿using UnityEngine;
using System.Collections;
using System;

public class GenericStack : LambdaItemScript {

	public string what;
	// Use this for initialization
	private static string templateStr = "Stack <b>{0}</b>";
	void Start () {
		var lwhat = (LambdaGrid.LambdaCube)Enum.Parse(typeof(LambdaGrid.LambdaCube), what);
		lambdaBehavior = new LambdaBehavior(i => i.Stack(lwhat));
		// Override desc in GUI
		if(String.IsNullOrEmpty(description)) {
			OverrideString();
		}
		lambdaBehavior.desc = description;
	}

	void OverrideString() {
        description = String.Format(templateStr, what);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

﻿using UnityEngine;
using System.Collections;

public class JudgementSystem : MonoBehaviour {
    public int correctItemId;
    public string correctStatement;

    public int playerItemChoice;
    public string playerStatementChoice;

    public GameObject correctAnswerBox;
    public GameObject wrongAnswerBox;

    private DialogueManager dMan;

    // Use this for initialization
    void Start () {
        dMan = FindObjectOfType<DialogueManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setItemID(int id ) {
        playerItemChoice = id;
    }

    public void setStatement(string statement) {
        playerStatementChoice = statement;
    }

    public void judge() {

        if ((correctItemId == playerItemChoice) && (correctStatement.Trim().Equals(playerStatementChoice.Trim())))
        {
            //correct answer
            dMan.getActiveNPC().GetComponent<DialogHolder>().setAndShowDialogue(correctAnswerBox);
        }
        else {
            dMan.getActiveNPC().GetComponent<DialogHolder>().setAndShowDialogue(wrongAnswerBox);
        }
    }

    public void showButton() {
        this.gameObject.SetActive(true);
    }
}
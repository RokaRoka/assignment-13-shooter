using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private RestartControl restartControl;

    public GameObject gameOverUI;

    //The things to change the ticking state of

    private bool isGameOver = false;

	// Use this for initialization
	void Start () {
        restartControl = GetComponent<RestartControl>();
        restartControl.RestartingGame += OnRestartingGame;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PlayerDeath()
    {
        //play game over UI
        isGameOver = true;
        SwitchGameOverUI(isGameOver);
    }

    private void SwitchGameOverUI(bool state)
    {
        gameOverUI.SetActive(state);
    }

    protected virtual void OnRestartingGame(object source, EventArgs e)
    {
        isGameOver = false;
        SwitchGameOverUI(isGameOver);
    }
}

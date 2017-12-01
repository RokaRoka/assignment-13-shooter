using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartControl : MonoBehaviour
{

	public delegate void RestartGameEventHandler(object source, EventArgs e);

	public event RestartGameEventHandler RestartingGame;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			OnRestartingGame();
		}	
	}

	protected virtual void OnRestartingGame()
	{
		if (RestartingGame != null)
			RestartingGame(this, EventArgs.Empty);
	}

}

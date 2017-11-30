using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	//Player movement ref
	private PlayerMovement playerMovement;

	//public delegate void
	public delegate void PlayerInputEventHandler(object source, EventArgs args);

	//shooting event
	public event PlayerInputEventHandler PlayerFired;

	//player shoot input delay
	private float shootInputDelay = 0.1f;
	private float shootInputCountdown = 0;

	private void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();

	}

	// Update is called once per frame
	private void Update () {
		InputDelayTick();
		CheckInput ();
	}

	private void InputDelayTick()
	{
		if (shootInputCountdown > 0)
			shootInputCountdown -= Time.deltaTime;
	}

	private void CheckInput()
	{
		Vector2 delta = Vector2.zero;

		if (Input.GetKey(KeyCode.UpArrow))
			delta.y += 1;
		if (Input.GetKey(KeyCode.DownArrow))
			delta.y -= 1;
		if (Input.GetKey(KeyCode.RightArrow))
			delta.x += 1;
		if (Input.GetKey(KeyCode.LeftArrow))
			delta.x -= 1;
		if (delta.x != 0 || delta.y != 0)
			playerMovement.MovePlayer(delta);

		if (Input.GetKeyDown (KeyCode.Space) || shootInputCountdown <= 0) {
			//player shoot
		}
	}

	protected virtual void OnPlayerFired()
	{
		if (PlayerFired != null)
			PlayerFired(this, EventArgs.Empty);
	}
}

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Game manager restart control
	private RestartControl restartControl;
	
	GameObject player;
	Rigidbody2D rb;

	public float maxVelocity = 15;
	public float acceleration = 10;
	private Vector2 currentSpeed = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
		restartControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartControl>();
		restartControl.RestartingGame += OnRestartingGame;
		rb = GetComponent<Rigidbody2D>();
	}

	public void MovePlayer(Vector2 moveDelta)
	{
		currentSpeed = rb.velocity;

		currentSpeed += (acceleration * Time.deltaTime * (rb.drag * 2)) * moveDelta;

		currentSpeed.x = Mathf.Clamp(currentSpeed.x, maxVelocity * -1f, maxVelocity);
		currentSpeed.y = Mathf.Clamp(currentSpeed.y, maxVelocity * -1f, maxVelocity);
		//currentSpeed = Vector2.ClampMagnitude(currentSpeed, maxVelocity);

		rb.velocity = currentSpeed;
	}

	protected virtual void OnRestartingGame(object source, EventArgs e)
	{
		currentSpeed = Vector2.zero;
		rb.velocity = Vector2.zero;
		transform.position = Vector3.down;
	}

    private void OnDestroy()
    {
        restartControl.RestartingGame -= OnRestartingGame;
    }
}

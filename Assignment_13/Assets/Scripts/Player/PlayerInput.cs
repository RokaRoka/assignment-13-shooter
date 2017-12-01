using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	//Player movement ref
	private PlayerMovement playerMovement;

    private ProjectileManager projMan;

    private FactionScript factionScript;

	//public delegate void
	public delegate void PlayerInputEventHandler(object source, ProjectileEventArgs args);

	//shooting event
	public event PlayerInputEventHandler PlayerFired;

	//player shoot input delay
	private float shootInputDelay = 0.07f;
	private float shootInputCountdown = 0;

    private void Awake()
    {
        projMan = GameObject.FindGameObjectWithTag("ProjectileManager").GetComponent<ProjectileManager>();
        projMan.SubscribeToPlayerEvent(gameObject);
    }

    private void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
        factionScript = GetComponent<FactionScript>();
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

		if (Input.GetKey (KeyCode.Space) && shootInputCountdown <= 0) {
			//player shoot
			shootInputCountdown = shootInputDelay;
			OnPlayerFired();
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int newfaction;
            if (factionScript.faction == 2)
                newfaction = 0;
            else
                newfaction = factionScript.faction + 1;
            factionScript.DetermineFaction(newfaction);
        }
	}

	protected virtual void OnPlayerFired()
	{
		if (PlayerFired != null)
			PlayerFired(this, new ProjectileEventArgs() { spawnPosition = transform.position});
	}
}

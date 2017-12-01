using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	//Enemy Shot Delegate
	
	public delegate void EnemyFiringEventHandler (object source, ProjectileEventArgs e);

	public event EnemyFiringEventHandler EnemyFired;

	private int faction = 0;
	
	//spawn position
	private Vector3 origin;
	
	public AnimationCurve animExponentialSlope;
	//Enemy movement timing
	private float t = 0;
	private float xDistance = 2;
	private float yDistance = 2;

	private float frequency = 2;
	
	//hit stun
	private float hit_t = 0;
	private float hitStunTime = 2f;
	
	//Enemy shots per cycle
	
	
	// Use this for initialization
	private void Start ()
	{
		origin = transform.position;
		faction = GetComponent<FactionScript>().faction;
	}
	
	// Update is called once per frame
	void Update () {
		if (faction == 0)
		{
			EnemyNormalMove();
		}
		else
		{
			EnemyCurvedMove();
		}
		Tick();
	}

	private void EnemyNormalMove()
	{
		Vector3 movementFromOrigin = Vector3.zero;
		movementFromOrigin.x = Mathf.Sin(Mathf.PI * t) * xDistance;
		
		transform.position = origin + movementFromOrigin;
	}
	
	private void EnemyCurvedMove()
	{
		Vector3 movementFromOrigin = Vector3.zero;

		//animExponentialSlope.Evaluate(t/2f);
		
		movementFromOrigin.x = Mathf.Sin(Mathf.PI * t) * xDistance;
		movementFromOrigin.y = -1 * Mathf.Sin(Mathf.PI * t) * yDistance;

		transform.position = origin + movementFromOrigin;
	}

	private void Tick()
	{
		if (hit_t > 0)
		{
			hit_t += Time.deltaTime * (frequency/4);
		}
		else
		{
			t += Time.deltaTime * (frequency/4);
			if (t >= 2f)
			{
				t -= 2f;
			}	
		}
	}

	public void AddHitStun()
	{
		hit_t = hitStunTime;
	}
	
	protected virtual void OnEnemyFired()
	{
		if (EnemyFired != null)
			EnemyFired(this, new ProjectileEventArgs() { spawnPosition = transform.position});
	}
}

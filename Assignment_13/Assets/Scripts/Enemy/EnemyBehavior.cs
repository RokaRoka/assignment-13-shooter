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

    //player
    private GameObject player;

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

    public bool isTicking = true;

    //Enemy shots per cycle
    private float shot_t = 0;
    public float shotFrequency = 1.5f;
    
    // Use this for initialization
    private void Start ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
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
		if (isTicking)
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
            shot_t += Time.deltaTime;
            if (shot_t >= shotFrequency)
            {
                shot_t -= shotFrequency;
                if (player == null) return;
                Vector2 direction = player.transform.position - transform.position;
                EnemyFired(this, new ProjectileEventArgs { spawnPosition = transform.position, spawnRotation = Quaternion.LookRotation(Vector3.forward, direction), shipFaction = faction });
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

    private void OnDestroy()
    {
        EnemyFired = null;
    }

}

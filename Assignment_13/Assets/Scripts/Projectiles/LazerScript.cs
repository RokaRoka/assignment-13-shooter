using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{

	private GameObject player;
	
	private Rigidbody2D rb;

	public float lazerDamage = 4f;
	
	private float travelSpeed = 18f;

	private float maxTravelTime = 1f;
	private float t = 0;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = travelSpeed * Vector2.up;
		t += Time.deltaTime;
		if (t > maxTravelTime) {
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			Debug.Log("Enemy Collision :O");
			
			float damage = 0;
			
			if (other.gameObject.GetComponent<FactionScript>().CompareFaction(GetPlayerFaction()))
			{
				damage = lazerDamage;
			}
			else
			{
				damage = lazerDamage * 1.75f;
			}
			other.transform.GetComponent<EnemyHealth>().DealDamage(damage);
		}
	}

	private int GetPlayerFaction()
	{
		return player.GetComponent<FactionScript>().faction;
	}
}

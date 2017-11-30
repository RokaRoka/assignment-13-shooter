using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour {

	private Rigidbody2D rb;

	private float travelSpeed = 18f;

	private float maxTravelTime = 1f;
	private float t = 0;

	// Use this for initialization
	void Start () {
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
}

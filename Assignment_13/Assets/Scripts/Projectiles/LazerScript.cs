using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour {

	private float maxTravelTime = 5f;
	private float t = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > maxTravelTime) {
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//Enemy health amount
	public float healthValue = 24f;

	public void DealDamage(float value)
	{
		healthValue -= value;
		if (healthValue <= 0)
			Destroy(gameObject);
	}

}

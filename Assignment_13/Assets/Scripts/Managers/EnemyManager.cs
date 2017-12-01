using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	//projectile script reference
	public ProjectileManager projMan;
	
	//Enemy prefabs array
	public GameObject[] enemyPrefabsArray = new GameObject[3];
	
	//Enemy spawn positions
	public GameObject[] enemySpawnPositions;
	
	//Spawn timer
	private float t = 0;

	private float spawnFreq = 2f;
	
	// Update is called once per frame
	private void Update () {
		CheckSpawn();
		Tick();
	}

	private void CheckSpawn()
	{
		if (t >= spawnFreq)
		{
			int randomSpawnInt = UnityEngine.Random.Range(0, enemySpawnPositions.Length);
			SpawnEnemy(0, enemySpawnPositions[randomSpawnInt].transform.position);
		}
	}
	
	private void Tick()
	{
		t += Time.deltaTime;
	}
	
	private void SpawnEnemy(int faction, Vector3 startPosition)
	{
		GameObject newEnemy = Instantiate(enemyPrefabsArray[faction], startPosition, Quaternion.AngleAxis(180f, Vector3.forward));
		newEnemy.GetComponent<EnemyBehavior>().EnemyFired += projMan.OnEnemyFired;
	}

	IEnumerator SpawnEnemyGroup(int faction, Vector3 startPosition)
	{
		
		yield return null;
	}
	
}

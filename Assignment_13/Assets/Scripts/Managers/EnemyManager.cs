using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	private RestartControl restartControl;
	
	//projectile script reference
	public ProjectileManager projMan;
	
	//Enemy prefabs array
	public GameObject[] enemyPrefabsArray = new GameObject[3];
	
	//Enemy spawn positions
	public GameObject[] enemySpawnPositions;

	private int enemyCount = 0;
	private int enemyMax = 10;
	private GameObject[] enemyArray;
	
	//Spawn timer
	private float t = 0;

	private float spawnFreq = 1.25f;

	private void Start()
	{
		restartControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartControl>();
		restartControl.RestartingGame += OnRestartingGame;

		enemyArray = new GameObject[enemyMax];
	}
	
	// Update is called once per frame
	private void Update () {
		CheckSpawn();
		Tick();
	}

	private void CheckSpawn()
	{
		if (t >= spawnFreq + enemyCount)
		{
			if (enemyCount < enemyMax)
			{
				int randomFactionInt = UnityEngine.Random.Range(0, 3);
				int randomSpawnInt = UnityEngine.Random.Range(0, enemySpawnPositions.Length);
				SpawnEnemy(randomFactionInt, enemySpawnPositions[randomSpawnInt].transform.position);
			}

			t -= spawnFreq;
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
		enemyCount++;
	}

	IEnumerator SpawnEnemyGroup(int faction, Vector3 startPosition)
	{
		
		yield return null;
	}

	protected virtual void OnRestartingGame(object source, EventArgs e)
	{
		t = 0;
		foreach (var gObj in enemyArray)
		{
			Destroy(gObj);
		}
	}
	
}

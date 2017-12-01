using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEventArgs : EventArgs {
	public Vector2 spawnPosition { get; set; }
	public Quaternion spawnRotation { get; set; }
	public int shipFaction { get; set; }
}

public class ProjectileManager : MonoBehaviour {

	//Event holders
	private GameObject player;

	//Projectile prefabs
	public GameObject playerLazer;

	public GameObject[] enemyShotArray;

	protected virtual void OnPlayerFired(object source, ProjectileEventArgs e)
	{
		FirePlayerProjectile(e.spawnPosition);
	}

	private void FirePlayerProjectile(Vector2 initPos)
	{
		GameObject lazer = Instantiate(playerLazer, initPos, Quaternion.identity);
	}
	
	public virtual void OnEnemyFired(object source, ProjectileEventArgs e) 
	{
		FireEnemyProjectile(e.spawnPosition, e.spawnRotation, e.shipFaction);
	}

	private void FireEnemyProjectile(Vector2 initPos, Quaternion initRot, int faction)
	{
        Debug.Log(faction);
        GameObject enemyBullet = Instantiate(enemyShotArray[faction], initPos, initRot);
	}

    public void SubscribeToPlayerEvent(GameObject player)
    {
        //Like, Comment, and Subscribe to events
        player.GetComponent<PlayerInput>().PlayerFired += OnPlayerFired;
    }

}

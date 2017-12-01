using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour {

    private GameOver gameOverScript;

    private Rigidbody2D rb;

    public float travelSpeed = 10f;

    private float maxTravelTime = 2f;
    private float t = 0;

    // Use this for initialization
    void Start()
    {
        gameOverScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = travelSpeed * transform.up;
        t += Time.deltaTime;
        if (t > maxTravelTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerCore"))
        {
            //Kill Player
            Destroy(other.transform.parent.gameObject);
            gameOverScript.PlayerDeath();
        }
    }

}

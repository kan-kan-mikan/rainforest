using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public GameObject player;
    public GameObject respawn;
    public float respawntimer = 1.5f;
    float deathtime;
    bool dead;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dead = false;
    }

    void Update()
    {
        if(dead && Time.time > deathtime + respawntimer)
        {
            Debug.Log("Respawning");
            dead = false;
            player.transform.position = respawn.transform.position;
            player.GetComponent<BoxCollider2D>().enabled = true;
            player.GetComponent<Player>().enabled = true;
            player.GetComponentInChildren<Shooting>().enabled = true;
            Player.velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyProjectile" || coll.gameObject.tag == "Out of Bounds")
        {
            Debug.Log("Dead");
            dead = true;
            deathtime = Time.time;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Player>().enabled = false;
            player.GetComponentInChildren<Shooting>().enabled = false;
        }
    }

}

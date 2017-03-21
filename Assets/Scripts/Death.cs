using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public GameObject respawn;
    public float respawntimer = 1.5f;
    float deathtime;
    bool dead;

    void Start()
    {
        dead = false;
    }

    void Update()
    {
        if(dead && Time.time > deathtime + respawntimer)
        {
            Debug.Log("Respawning");
            dead = false;
            gameObject.transform.position = respawn.transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Player>().enabled = true;
            gameObject.GetComponentInChildren<Shooting>().enabled = true;
            Player.velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Projectile" || coll.gameObject.tag == "Out of Bounds")
        {
            Debug.Log("Dead");
            dead = true;
            deathtime = Time.time;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponentInChildren<Shooting>().enabled = false;
        }
    }

}

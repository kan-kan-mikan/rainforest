using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	private Rigidbody2D rigidEnemy;
	private GameObject Player;

    Vector2 velocity; 

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        rigidEnemy = gameObject.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{ 
        rigidEnemy.velocity = (Vector2.left * 5);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.name == "Player")
        {

			Destroy (coll.gameObject);

		}
        if(coll.gameObject.tag == "Projectile")
        {

            Destroy(gameObject);

        }
	}
}

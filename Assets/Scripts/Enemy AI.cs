
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	public float acceleration = 4f;
	public float maxSpeed = 8f;
	public float gravity = 5f;
	public GameObject Enemy; 
	public Rigidbody2D rigidEnemy;
	public GameObject Playera; 

	Rect box;

	Vector2 velocity; 

	Vector2 walkAmount;

	void Start()
	{
		GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GameObject>();
		GameObject Playera = GameObject.FindGameObjectWithTag("Playera").GetComponent<GameObject>();
	}

	void FixedUpdate()
	{ 

		rigidEnemy = Enemy.GetComponent<Rigidbody2D>(); 
		rigidEnemy.velocity = (Vector2.left * 30);
		rigidEnemy.freezeRotation = true;

		float newVelocityX = velocity.x;

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Playera") {

			Destroy (coll.gameObject);

		}
	}
}

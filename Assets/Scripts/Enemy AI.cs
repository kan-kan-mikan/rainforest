using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	public float acceleration = 4f;
	public float maxSpeed = 8f;
	public float gravity = 5f;
	public GameObject Enemy; 
	public GameObject Playera; 
	public bool destroy;

	Rect box;

	Vector2 velocity; 

	bool isVisible = true;

	Vector2 walkAmount;

	void Start()
    {
		GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GameObject>();
		GameObject Playera = GameObject.FindGameObjectWithTag("Playera").GetComponent<GameObject>();
	}

	void FixedUpdate()
	{ 
		if(destroy == true)
        {
			Playera.SetActive(false);
		}

		if (destroy == false)
        {
			Playera.SetActive(true);
		}

		walkAmount.x = acceleration * maxSpeed * Time.deltaTime;
		transform.Translate(walkAmount);

		float newVelocityX = velocity.x;

		if (isVisible == true)
        { 
			newVelocityX += acceleration;
			newVelocityX = Mathf.Clamp (newVelocityX, -maxSpeed, maxSpeed);
		}

		velocity = new Vector2(newVelocityX, velocity.y);

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.name == "Playera")
        {
			destroy = true;
		}
        else
        {
			destroy = false;
		}
	}

	void LateUpdate()
	{
		transform.Translate (velocity * Time.deltaTime);
	}

}
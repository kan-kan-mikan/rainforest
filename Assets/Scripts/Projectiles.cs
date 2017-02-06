using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour
{

	public GameObject Enemy; 
	public bool destroyaa;

	void Start(){

		GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GameObject>();
	}

	void FixedUpdate()
	{ 
		if(destroyaa == true) {
			Enemy.SetActive (false);
			Destroy (gameObject);  //Destroys the bullet when it hits the enemy
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Enemy") {       // If bullet collides with enemy, destroyaa is true
			destroyaa = true;
		} 
	}
}

using UnityEngine;
using System.Collections;

public class EnemyProjectiles : MonoBehaviour
{
	public GameObject Playera; 

	void Start(){

		GameObject Playera = GameObject.FindGameObjectWithTag("Playera").GetComponent<GameObject>();
	}
		
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Playera") {       // If bullet collides with player, the Player is destroyed
			Destroy (col.gameObject);
			Destroy (gameObject);  //Destroys the bullet when it hits the player
		} 
	}
}

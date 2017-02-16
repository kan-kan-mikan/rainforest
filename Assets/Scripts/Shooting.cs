using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	//Make child gameobject of Player and attach this script to it

	public GameObject bullet; 
	public GameObject bulletClone;
	public Rigidbody2D bulletPrefab;

	void FixedUpdate ()
	{

		if(Input.GetButtonDown("Shoot"))
		{
			bulletClone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
			bulletPrefab = bulletClone.GetComponent<Rigidbody2D>(); 
			bulletPrefab.velocity = (Vector2.right * 250); //Take the rigidbody of the bullet clone and add force to it
			Destroy(bulletClone, 2); //Destroys the bullets after 2 seconds
		}
	}		
}

using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	//Make child gameobject of Player and attach this script to it

	public GameObject bullet; 
	private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;

    void FixedUpdate()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			bulletClone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
			bulletPrefab = bulletClone.GetComponent<Rigidbody2D>(); 
			bulletPrefab.velocity = (Vector2.right * 10); //Take the rigidbody of the bullet clone and add force to it
		}
	}

}

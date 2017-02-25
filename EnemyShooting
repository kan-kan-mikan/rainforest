using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
	public GameObject enemyBullet; 
	public GameObject enemybulletClone;
	public Rigidbody2D enemybulletPrefab;
	public GameObject Playera;

	void Start ()
	{
		if (Playera.transform.position.x < transform.position.x)
		{
			
		StartCoroutine (shootTiming ());
		
		}
	}


	IEnumerator shootTiming()
	{
		while (enabled) {
			if (Playera != null) {
				yield return new WaitForSeconds (1.5f);
				enemybulletClone = Instantiate (enemyBullet, transform.position, Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
				enemybulletPrefab = enemybulletClone.GetComponent<Rigidbody2D> (); 
				enemybulletPrefab.velocity = (Vector2.left * 100); //Take the rigidbody of the bullet clone and add force to it
				Destroy (enemybulletClone, 1.5f); //Destroys the bullets after 1 second
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	//Make child gameobject of Player and attach this script to it

	public GameObject bullet; 
	private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;
    private Vector2 dir;

    private bool faceRight;

    public float fireRate = 0.05f;
    private float fireTime = 0f;

    void Start()
    {
        dir = Vector2.right; //default facing position is always right unless that gets changed somehow
        faceRight = true;
    }

    void FixedUpdate() //spaghetti code written 2 days before the game was due, please excuse bad code practices
    {
        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) //fire right and up while on ground
        {
            dir = new Vector2(1, 1);
            faceRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0) //fire left and up while on ground
        {
            dir = new Vector2(-1, 1);
            faceRight = false;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) //fire right and down while on ground
        {
            dir = new Vector2(1, -1);
            faceRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) //fire left and down while on ground
        {
            dir = new Vector2(-1, -1);
            faceRight = false;
        }

        else if (Input.GetAxis("Horizontal") > 0) //fire right while on ground
        {
            dir = Vector2.right;
            faceRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0) //fire left while on ground
        {
            dir = Vector2.left;
            faceRight = false;
        }
        else if (Input.GetAxis("Vertical") > 0) //fire up while on ground
        {
            dir = Vector2.up;
        }

        else if (faceRight && Input.GetAxis("Vertical") < 0 && Player.grounded) //fire right while prone on ground
        {
            dir = Vector2.right;
            faceRight = true;
        }
        else if (!faceRight && Input.GetAxis("Vertical") < 0 && Player.grounded) //fire left while prone on ground
        {
            dir = Vector2.left;
            faceRight = false;
        }
        else if (Input.GetAxis("Vertical") < 0 && !Player.grounded) //jumping in air, aiming down
        {
            dir = Vector2.down;
        }

        else //aim in last facing direction in case of no input
        {
            if(faceRight)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;
            }
        }
    }

    void Update()
    { 

        if (Input.GetButtonDown("Fire1") && Time.time > fireTime)
		{
            fireTime = Time.time + fireRate;
			bulletClone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
			bulletPrefab = bulletClone.GetComponent<Rigidbody2D>(); 
			bulletPrefab.velocity = (dir * 10); //Take the rigidbody of the bullet clone and add force to it
		}
	}

}
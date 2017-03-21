using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	//Make child gameobject of Player and attach this script to it

	public GameObject bullet; 
	private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;
    private Vector2 dir;

    private SpriteRenderer playersprite;
    private Sprite[] sprites;
    private bool faceRight;

    public float fireRate = 0.2f;
    private float fireTime = 0f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        playersprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/PNG/characters/axeloak");
        dir = Vector2.right; //default facing position is always right unless that gets changed somehow
        faceRight = true;
    }

    void FixedUpdate() //spaghetti code written 2 days before the game was due, please excuse bad code practices
    {
        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) //fire right and up while on ground
        {
            dir = new Vector2(1, 1);
            faceRight = true;
            playersprite.sprite = sprites[3];
            x = 1.2f;
            y = 1.2f;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0) //fire left and up while on ground
        {
            dir = new Vector2(-1, 1);
            faceRight = false;
            playersprite.sprite = sprites[3];
            x = -1.2f;
            y = 1.2f;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) //fire right and down while on ground
        {
            dir = new Vector2(1, -1);
            faceRight = true;
            playersprite.sprite = sprites[2];
            x = 1.2f;
            y = -1.2f;
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) //fire left and down while on ground
        {
            dir = new Vector2(-1, -1);
            faceRight = false;
            playersprite.sprite = sprites[2];
            x = -1.2f;
            y = -1.2f;
        }

        else if (Input.GetAxis("Horizontal") > 0) //fire right while on ground
        {
            dir = Vector2.right;
            faceRight = true;
            playersprite.sprite = sprites[1];
            x = 1.5f;
            y = -0.1f;
        }
        else if (Input.GetAxis("Horizontal") < 0) //fire left while on ground
        {
            dir = Vector2.left;
            faceRight = false;
            playersprite.sprite = sprites[1];
            x = -1.5f;
            y = -0.1f;
        }
        else if (faceRight && Input.GetAxis("Vertical") > 0) //fire up while on ground and facing right
        {
            dir = Vector2.up;
            playersprite.sprite = sprites[3];
            x = .8f;
            y = 1.2f;
        }
        else if (!faceRight && Input.GetAxis("Vertical") > 0) //fire up while on ground and facing left
        {
            dir = Vector2.up;
            playersprite.sprite = sprites[3];
            x = -0.8f;
            y = 1.2f;
        }

        else if (faceRight && Input.GetAxis("Vertical") < 0 && Player.grounded) //fire right while prone on ground
        {
            dir = Vector2.right;
            faceRight = true;
            playersprite.sprite = sprites[1];
            x = 1.5f;
            y = -0.7f;
        }
        else if (!faceRight && Input.GetAxis("Vertical") < 0 && Player.grounded) //fire left while prone on ground
        {
            dir = Vector2.left;
            faceRight = false;
            playersprite.sprite = sprites[1];
            x = -1.5f;
            y = -0.7f;
        }
        else if (faceRight && Input.GetAxis("Vertical") < 0 && !Player.grounded) //jumping in air, aiming down and facing right
        {
            dir = Vector2.down;
            playersprite.sprite = sprites[2];
            x = 0.8f;
            y = -1.2f;
        }
        else if (!faceRight && Input.GetAxis("Vertical") < 0 && !Player.grounded) //jumping in air, aiming down and facing left
        {
            dir = Vector2.down;
            playersprite.sprite = sprites[2];
            x = -0.8f;
            y = -1.2f;
        }

        else //aim in last facing direction in case of no input
        {
            if(faceRight)
            {
                dir = Vector2.right;
                x = 1.5f;
                y = -0.1f;
            }
            else
            {
                dir = Vector2.left;
                x = -1.5f;
                y = -0.1f;
            }
        }
    }

    void LateUpdate()
    { 
        if (Input.GetButtonDown("Fire1") && Time.time > fireTime)
		{
            fireTime = Time.time + fireRate;
			bulletClone = Instantiate (bullet, transform.position + new Vector3(x,y), Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
			bulletPrefab = bulletClone.GetComponent<Rigidbody2D>(); 
			bulletPrefab.velocity = (dir * 10); //Take the rigidbody of the bullet clone and add force to it
		}
	}

}
using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //Make child gameobject of Player and attach this script to it

    public AudioSource sound;
    public AudioClip pew;

	public GameObject bullet; 
	private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;
    private Vector2 dir;

    private SpriteRenderer playersprite;
    private Sprite[] walksprites;
    private Sprite[] jumpsprites;
    private bool faceRight;
    private int curwalk = 1;
    private float spriteChangeRate = 0.05f;
    private float spriteChangeTime = 0f;

    public float fireRate = 0.05f;
    private float fireTime = 0f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        playersprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        walksprites = Resources.LoadAll<Sprite>("Sprites/PNG/characters/axeloak");
        jumpsprites = Resources.LoadAll<Sprite>("Sprites/PNG/characters/axeloak/Jump");
        dir = Vector2.right; //default facing position is always right unless that gets changed somehow
        faceRight = true;
    }

    void FixedUpdate() //spaghetti code, please excuse bad code practices
    {
        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) //fire right and up
        {
            dir = new Vector2(1, 1);
            faceRight = true;
            x = 1.2f;
            y = 1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[3 + curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[3];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[2];
            } 
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0) //fire left and up
        {
            dir = new Vector2(-1, 1);
            faceRight = false;
            x = -1.2f;
            y = 1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[3 + curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[3];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[2];
            }
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) //fire right and down
        {
            dir = new Vector2(1, -1);
            faceRight = true;
            x = 1.2f;
            y = -1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[0];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[1];
            }
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) //fire left and down
        {
            dir = new Vector2(-1, -1);
            faceRight = false;
            x = -1.2f;
            y = -1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[0];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[1];
            }
        }

        else if (Input.GetAxis("Horizontal") > 0) //fire right
        {
            dir = Vector2.right;
            faceRight = true;
            x = 1.5f;
            y = -0.1f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[6+curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[6];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[0];
            }
        }
        else if (Input.GetAxis("Horizontal") < 0) //fire left
        {
            dir = Vector2.left;
            faceRight = false;
            x = -1.5f;
            y = -0.1f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[6 + curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[6];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[0];
            }
        }
        else if (faceRight && Input.GetAxis("Vertical") > 0) //fire up and facing right
        {
            dir = Vector2.up;
            x = .8f;
            y = 1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[3 + curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[3];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[2];
            }
        }
        else if (!faceRight && Input.GetAxis("Vertical") > 0) //fire up and facing left
        {
            dir = Vector2.up;
            x = -0.8f;
            y = 1.2f;

            if (Player.grounded)
            {
                if (Player.velocity.x != 0)
                {
                    playersprite.sprite = walksprites[3 + curwalk];
                }
                else
                {
                    playersprite.sprite = walksprites[3];
                }
            }
            else
            {
                playersprite.sprite = jumpsprites[2];
            }
        }
        else if (!Player.grounded && faceRight && Input.GetAxis("Vertical") < 0) //fire down and facing right while in air
        {
            dir = Vector2.down;
            x = 0.8f;
            y = -1.2f;

            playersprite.sprite = jumpsprites[1];
        }
        else if (!Player.grounded && !faceRight && Input.GetAxis("Vertical") < 0) //fire down and facing left while in air
        {
            dir = Vector2.down;
            x = -0.8f;
            y = -1.2f;

            playersprite.sprite = jumpsprites[1];
        }
        else //aim in last facing direction in case of no input
        {
            if(faceRight)
            {
                dir = Vector2.right;
                x = 1.5f;
                y = -0.1f;

                if (Player.grounded)
                {
                    if (Player.velocity.x != 0)
                    {
                        playersprite.sprite = walksprites[6 + curwalk];
                    }
                    else
                    {
                        playersprite.sprite = walksprites[6];
                    }
                }
                else
                {
                    playersprite.sprite = jumpsprites[0];
                }
            }
            else
            {
                dir = Vector2.left;
                x = -1.5f;
                y = -0.1f;

                if (Player.grounded)
                {
                    if (Player.velocity.x != 0)
                    {
                        playersprite.sprite = walksprites[6 + curwalk];
                    }
                    else
                    {
                        playersprite.sprite = walksprites[6];
                    }
                }
                else
                {
                    playersprite.sprite = jumpsprites[0];
                }
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
            sound.clip = pew;
            sound.Play();
		}

        if (Player.grounded && Time.time > spriteChangeTime)
        {
            spriteChangeTime = Time.time + spriteChangeRate;

            curwalk++;
            
            if(curwalk > 2)
            {
                curwalk = 0;
            }
        }
	}

}
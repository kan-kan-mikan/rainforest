using UnityEngine;
using System.Collections;

public class Stroheim : MonoBehaviour
{

    public GameObject bullet;
    private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;
    Vector2 dir;
    float angle;

    GameObject Player;
    public static bool activated;

    bool faceRight;
    private SpriteRenderer enemysprite;
    private Sprite[] sprites;

    public float fireRate = .2f;
    private float fireTime = 0f;
    private int burstcount = 0;

    private float x = 0f;
    private float y = 0f;

    public static int hitcount = 0;

    void Start()
    {
        enemysprite = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/PNG/characters/stroheim");
        activated = false;
        faceRight = false;
        Player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        //-----PLAYER FACING DIRECTION-----\\

        if (faceRight) //flip character to face right if aiming right
        {
            transform.localScale = new Vector2(-1.7f, 1.7f);
        }
        else if (!faceRight) //flip character to face left if aiming left
        {
            transform.localScale = new Vector2(1.7f, 1.7f);
        }

        //-----AIMING-----\\

        dir = Player.transform.InverseTransformDirection(Player.transform.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(angle); //outputs angle between enemy and player as values between -180 and 180 with right being 0 degrees

        if (angle > -22 && angle <= 23)
        {
            dir = Vector2.right;
            faceRight = true;
            enemysprite.sprite = sprites[0];
            x = 1.5f;
            y = -0.1f;
        }
        else if (angle > 23 && angle <= 67)
        {
            dir = new Vector2(1, 1);
            faceRight = true;
            enemysprite.sprite = sprites[2];
            x = 1.2f;
            y = 1.2f;
        }
        else if (angle > 67 && angle <= 113)
        {
            dir = Vector2.up;
            enemysprite.sprite = sprites[2];

            if (faceRight)
            {
                x = 0.8f;
                y = 1.2f;
            }
            else
            {
                x = -0.8f;
                y = 1.2f;
            }

        }
        else if (angle > 113 && angle <= 157)
        {
            dir = new Vector2(-1, 1);
            faceRight = false;
            enemysprite.sprite = sprites[2];
            x = -1.2f;
            y = 1.2f;
        }
        else if (angle > 157 || angle <= -158)
        {
            dir = Vector2.left;
            faceRight = false;
            enemysprite.sprite = sprites[0];
            x = -1.5f;
            y = -0.1f;
        }
        else if (angle > -158 && angle <= -112)
        {
            dir = new Vector2(-1, -1);
            faceRight = false;
            enemysprite.sprite = sprites[1];
            x = -1.2f;
            y = -1.2f;
        }
        else if (angle > -68 && angle <= -22)
        {
            dir = new Vector2(1, -1);
            faceRight = true;
            enemysprite.sprite = sprites[1];
            x = 1.2f;
            y = -1.2f;
        }
    }

    void Update()
    {
        if (burstcount == 10)
        {
            fireTime += 3;
            burstcount = 0;
        }

        if (activated && Time.time > fireTime)
        {
            fireTime = Time.time + fireRate;
            bulletClone = Instantiate(bullet, transform.position + new Vector3(x, y), Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
            bulletPrefab = bulletClone.GetComponent<Rigidbody2D>();
            bulletPrefab.velocity = (dir.normalized) * 10; //Take the rigidbody of the bullet clone and add force to it
            burstcount++;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (activated && coll.gameObject.tag == "Projectile")
        {
            hitcount++;

            if (hitcount > 10)
            {
                Destroy(gameObject);
            }  
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Activator Wall")
        {
            activated = true;
        }
    }

}
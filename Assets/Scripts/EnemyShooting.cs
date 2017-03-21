using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{

    public GameObject bullet;
    private GameObject bulletClone;
    private Rigidbody2D bulletPrefab;
    Vector2 dir;
    float angle;

    GameObject Player;
    bool activated;
    bool faceRight;

    public float fireRate = 0.05f;
    private float fireTime = 0f;
    private int burstcount = 0;

    void Start()
    {
        activated = false;
        faceRight = false;
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
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
        Debug.Log(angle); //outputs angle between enemy and player as values between -180 and 180 with right being 0 degrees

        if(angle > -23 && angle <= 22)
        {
            dir = Vector2.right;
        }
        else if(angle > -67 && angle <= -23)
        {
            dir = new Vector2(1, 1);
        }
        else if(angle > -113 && angle <= -67)
        {
            dir = Vector2.up;
        }
        else if(angle > -157 && angle <= -137)
        {
            dir = new Vector2(-1, 1);
        }
        else if(angle > 158 && angle <= -157)
        {
            dir = Vector2.left;
        }
        else if(angle > 112 && angle <= 158)
        {
            dir = new Vector2(-1, -1);
        }
        else if(angle > 68 && angle <= 112)
        {
            dir = Vector2.down;
        }
        else if(angle > 22 && angle <= 68)
        {
            dir = new Vector2(1, -1);
        }
    }

    void LateUpdate()
    {
        if(burstcount > 3)
        {
            fireTime += 1;
        }

        if (Time.time > fireTime)
        {
            fireTime = Time.time + fireRate;
            bulletClone = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject; //Instantiate the bullet as a GameObject
            bulletPrefab = bulletClone.GetComponent<Rigidbody2D>();
            bulletPrefab.velocity = (dir * 10); //Take the rigidbody of the bullet clone and add force to it
            burstcount++;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
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
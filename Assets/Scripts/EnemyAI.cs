using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public static float acceleration = 5f;
    public static float maxSpeed = 5f;
    public float gravity = 1f;
    public float maxfall = 12f;
    public float jump = 18f;

    Rect box;

    public static Vector2 velocity;

    public static bool grounded = false;
    public bool falling = false;

    int horizontalRays = 6;
    int verticalRays = 4;
    float margin = 0.1f;

    bool activated;

    void Start()
    {
        activated = false;
    }

    void FixedUpdate()
	{

        //-----GRAVITY-----\\

        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        box = new Rect(
            boxCol.bounds.min.x,
            boxCol.bounds.min.y,
            boxCol.bounds.size.x,
            boxCol.bounds.size.y
            );

        if (!grounded)
        {
            //add gravity to y speed with terminal velocity being maxfall
            velocity = new Vector2(velocity.x, Mathf.Max(velocity.y - gravity, -maxfall));
        }

        if (velocity.y < 0)
        {
            falling = true;
        }

        if (grounded || falling) //doesn't check if moving up in the air
        {
            Vector2 startPoint = new Vector2(box.xMin + margin, box.center.y);
            Vector2 endPoint = new Vector2(box.xMax - margin, box.center.y);

            //conditional operator checks if grounded true then use margin if false use movement distance
            float distance = box.height / 2 + (grounded ? margin : Mathf.Abs(velocity.y * Time.deltaTime));

            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 0f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 1f / 3f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 2f / 3f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 1f), Vector2.down * distance, Color.red, 0, false);

            bool connected = false;

            for (int i = 0; i < verticalRays; i++)
            {
                float lerpAmount = (float)i / (float)(verticalRays - 1);
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

                RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.down, distance);

                if (hitInfo.collider != null && hitInfo.collider != boxCol) //if raycast hits something that isn't the player
                {
                    //Debug.Log("Hit Collider: " + hitInfo.collider);
                    connected = true;
                    grounded = true;
                    falling = false;
                    transform.Translate(Vector2.down * (hitInfo.distance - box.height / 2));
                    velocity = new Vector2(velocity.x, 0);
                    break;
                }
            }

            if (!connected) //if raycast doesn't hit anything
            {
                grounded = false;
            }

        }

        if(grounded)
        {
            velocity = new Vector2(-5, 0);
        }

    }

    void LateUpdate()
    {
        //applies movement with Time.deltaTime being time since last frame
        if(activated)
        {
            transform.Translate(velocity * Time.deltaTime);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
	{
        if(activated && (coll.gameObject.tag == "Projectile" || coll.gameObject.tag == "Out of Bounds"))
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
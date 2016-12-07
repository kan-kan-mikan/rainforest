using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float acceleration = 4f;
    public float maxSpeed = 150f;
    public float gravity = 6f;
    public float maxfall = 200f;
    public float jump = 200f;

    int layerMask;

    Rect box;

    Vector2 velocity;

    bool grounded = false;
    bool falling = false;

    int horizontalRays = 6;
    int verticalRays = 4;
    int margin = 2;

    void Start()
    {
        layerMask = LayerMask.NameToLayer("Collisions");
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
            Vector2 startPoint = new Vector2(box.xMax - margin, box.center.y);
            Vector2 endPoint = new Vector2(box.xMin + margin, box.center.y);

            //conditional operator checks if grounded true then use margin if false use movement distance
            float distance = box.height / 2 + (grounded ? margin : Mathf.Abs(velocity.y * Time.deltaTime));

            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 0f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 1f / 3f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 2f / 3f), Vector2.down * distance, Color.red, 0, false);
            Debug.DrawRay(Vector2.Lerp(startPoint, endPoint, 1f), Vector2.down * distance, Color.red, 0, false);

            bool connected = false;

            for (int i = 0; i < verticalRays; i++)
            {
                float lerpAmount = (float)i / (float) (verticalRays - 1);
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

                RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.down, distance);

                if (hitInfo.collider != null && hitInfo.collider != boxCol) //if raycast hits something that isn't the player
                {
                    Debug.Log("Hit Collider: " + hitInfo.collider);
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

        //-----HORIZONTAL MOVEMENT-----\\

        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        float newVelocityX = velocity.x;

        if (horizontalAxis != 0) //add movement according to input
        {
            newVelocityX += acceleration * horizontalAxis;
            newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed, maxSpeed);
        }
        else if (velocity.x != 0) //apply deceleration due to no input
        {
            int modifier = velocity.x > 0 ? -1 : 1;
            newVelocityX += acceleration * modifier;
        }

        velocity = new Vector2(newVelocityX, velocity.y);

        if (velocity.x != 0)
        {
            Vector2 startPoint = new Vector2(box.center.x, box.yMin + margin);
            Vector2 endPoint = new Vector2(box.center.x, box.yMax - margin);

            float sideRayLength = box.width / 2 + Mathf.Abs(newVelocityX * Time.deltaTime);
            Vector2 direction = newVelocityX > 0 ? Vector2.right : Vector2.left;

            for (int i = 0; i < horizontalRays; i++)
            {
                float lerpAmount = (float)i / (float)(horizontalRays - 1);
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

                RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, sideRayLength);

                if (hitInfo.collider != null && hitInfo.collider != boxCol)
                {
                    transform.Translate(direction * (hitInfo.distance - box.width / 2));
                    velocity = new Vector2(0, velocity.y);
                    break;
                }
            }

        }

        //-----JUMPING-----\\

        /*
        if (grounded && Input.GetButtonDown("Jump"))
        {
            velocity = new Vector2(velocity.x, velocity.y + jump * Time.deltaTime);
        }
        */

        bool inputJump = Input.GetButton("Jump");
        bool lastInput = false;
        float jumpPressedTime = 0;
        float jumpPressLeeway = 0.1f;

        if(inputJump && !lastInput)
        {
            jumpPressedTime = Time.time;
        }
        else if(!inputJump)
        {
            jumpPressedTime = 0;
        }

        if(grounded && Time.time - jumpPressedTime < jumpPressLeeway)
        {
            velocity = new Vector2(velocity.x, velocity.y + jump * Time.deltaTime);
            jumpPressedTime = 0;
        }

        lastInput = inputJump;

    }

    void LateUpdate()
    {
        //applies movement with Time.deltaTime being time since last frame
        transform.Translate(velocity * Time.deltaTime);
    }

}
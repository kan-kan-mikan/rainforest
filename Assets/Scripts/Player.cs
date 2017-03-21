using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{

    public AudioSource music;
    public AudioSource sound;
    public AudioClip win;
    private float wintime;
    private bool won;

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
                    //Debug.Log("Hit Collider: " + hitInfo.collider);
                    transform.Translate(direction * (hitInfo.distance - box.width / 2));
                    velocity = new Vector2(0, velocity.y);
                    break;
                }
            }

        }

        //-----ONE WAY PLATFORMS-----\\

        /*
            Currently more or less nonfunctional (at least not as intended)
            
            To use, set a platform as having being on the SoftTop or SoftBottom layer.
            As the name suggests, a SoftTop allows dropping down but not going back up.
            A SoftBottom allows for jumping up but not going back down.
        */

        if (grounded || velocity.y > 0)
        {
            float upRayLength = grounded ? margin : velocity.y * Time.deltaTime;

            bool connection = false;
            int lastConnection = 0;
            Vector2 min = new Vector2(box.xMin + margin, box.center.y);
            Vector2 max = new Vector2(box.xMax - margin, box.center.y);
            RaycastHit2D[] upRays = new RaycastHit2D[verticalRays];

            for (int i = 0; i < verticalRays; i++)
            {
                Vector2 start = Vector2.Lerp(min, max, i / verticalRays);
                Vector2 end = start + Vector2.up * (upRayLength + box.height / 2);
                upRays[i] = Physics2D.Linecast(start, end, Raylayers.upRay);
                if (upRays[i].fraction > 0)
                {
                    connection = true;
                    lastConnection = i;
                }
            }

            if (connection)
            {
                velocity = new Vector2(velocity.x, 0);
                transform.position += Vector3.up * (upRays[lastConnection].point.y - box.yMax);
            }
        }

        //-----JUMPING-----\\

        bool inputJump = Input.GetButton("Jump");
        bool lastInput = false;
        float jumpPressedTime = 0;
        float jumpPressLeeway = 0.1f;

        if (inputJump && !lastInput)
        {
            jumpPressedTime = Time.time;
        }
        else if (!inputJump)
        {
            jumpPressedTime = 0;
        }

        if (grounded && Time.time - jumpPressedTime < jumpPressLeeway)
        {
            velocity = new Vector2(velocity.x, jump);
            jumpPressedTime = 0;
            grounded = false;
        }

        lastInput = inputJump;

        //-----PLAYER FACING DIRECTION-----\\

        if (Input.GetAxis("Horizontal") > 0) //flip character to face right if moving right
        {
            transform.localScale = new Vector2(1.7f, 1.7f);
        }
        else if (Input.GetAxis("Horizontal") < 0) //flip character to face left if moving left
        {
            transform.localScale = new Vector2(-1.7f, 1.7f);
        }

    }

    void LateUpdate()
    {
        //applies movement with Time.deltaTime being time since last frame
        transform.Translate(velocity * Time.deltaTime);

        if (won && Time.time > wintime + 2)
        {
            wintime = 0;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Mountain")
            {
                SceneManager.LoadScene("Forest");
            }
            else if (scene.name == "Forest")
            {
                SceneManager.LoadScene("City");
            }
            else if (scene.name == "City")
            {
                SceneManager.LoadScene("Cyberpunk");
            }
            else if (scene.name == "City")
            {
                SceneManager.LoadScene("Industrial");
            }
            else if (scene.name == "Industrial")
            {
                SceneManager.LoadScene("Level Select");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!won && collision.gameObject.tag == "End of Stage")
        {
            won = true;
            sound.clip = win;
            sound.Play();
            wintime = Time.time;
            music.Stop();
        }
    }

}
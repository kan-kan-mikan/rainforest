using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    public float acceleration = Player.acceleration;
    public float maxSpeed = Player.maxSpeed;

    Vector2 velocity = Player.velocity;
    float newVelocityX;

    float horizontalAxis;
    BoxCollider2D boxcol;
    BoxCollider2D frontWall;
    BoxCollider2D endWall;

    bool cameraFrozen;

    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
        frontWall = GameObject.FindWithTag("Front Wall").GetComponent<BoxCollider2D>();
        endWall = GameObject.FindWithTag("CameraFreezer").GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (!boxcol.IsTouching(GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>()))
        {
            newVelocityX = 0f;
        }
    }

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (boxcol.IsTouching(GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>()))
        {
            Debug.Log("Player hit camera wall");
            newVelocityX = velocity.x;

            if (horizontalAxis == 1) //add movement according to input
            {
                newVelocityX += acceleration * horizontalAxis;
                newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed, maxSpeed);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.IsTouching(endWall))
        {
            Debug.Log("Camera Frozen");

            cameraFrozen = true;
        }
    }

    void LateUpdate()
    {
        if(cameraFrozen)
        {
            newVelocityX = 0f;
        }

        velocity = new Vector2(newVelocityX, velocity.y);

        //applies movement with Time.deltaTime being time since last frame
        transform.Translate(velocity * Time.deltaTime);
    }

}
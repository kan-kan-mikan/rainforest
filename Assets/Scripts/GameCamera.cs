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

    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
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

        velocity = new Vector2(newVelocityX, velocity.y);
    }

    void LateUpdate()
    {
        //applies movement with Time.deltaTime being time since last frame
        transform.Translate(velocity * Time.deltaTime);
    }

}
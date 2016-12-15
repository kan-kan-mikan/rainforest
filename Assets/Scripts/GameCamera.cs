using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    public float acceleration = Player.acceleration;
    public float maxSpeed = Player.maxSpeed;

    Vector2 velocity = new Vector2(0f, 0f);
    float newVelocityX;

    float horizontalAxis;

    void Start()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
    }
   
    void FixedUpdate()
    {

        BoxCollider2D boxcol = GetComponent<BoxCollider2D>();

        if(boxcol.IsTouching( GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>() ) )
        {
            newVelocityX = velocity.x;
        }

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

        if (GetComponent<BoxCollider2D>() == true)
        {
            velocity = new Vector2(newVelocityX, velocity.y);
        }

    }

    void LateUpdate()
    {
        //applies movement with Time.deltaTime being time since last frame
        transform.Translate(velocity * Time.deltaTime);
    }

}
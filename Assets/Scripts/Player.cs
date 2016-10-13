using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float maxSpeed = 200f;
    public float speed = 200f;
    public float jumpPower = 100f;

    public bool onGround;

    private Rigidbody2D player;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {

        player = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

	}

    void Update ()
    {

        anim.SetBool("On Ground", onGround); //changes bool parameter in Animator based on onGround value
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); //changes speed parameter in Animator to magnitude of speed

        if ((Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D))) //stop players from moving if movement keys are not pressed
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if(Input.GetAxis("Horizontal") < -0.1f) //flip sprite to the left when moving left
        {

            transform.localScale = new Vector3(-1, 1, 1);

        }

        if (Input.GetAxis("Horizontal") > 0.1f) //flip sprite to the right when moving right
        {

            transform.localScale = new Vector3(1, 1, 1);

        }

    }
	
	void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        player.AddForce((Vector2.right * speed) * horizontal); //floaty moving with acceleration

        if (player.velocity.x < -maxSpeed) //cap speed in the negative direction
        {

            player.velocity = new Vector2(-maxSpeed, player.velocity.y);

        }

        if (player.velocity.x > maxSpeed) //cap speed in the positive direction
        {

            player.velocity = new Vector2(maxSpeed, player.velocity.y);

        }

    }
}

using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{

    private Player player;

    void Start ()
    {

        player = gameObject.GetComponentInParent<Player>();

    }

    void OnTriggerStay2D(Collider2D col)
    {

        player.onGround = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {

        player.onGround = false;

    }

}

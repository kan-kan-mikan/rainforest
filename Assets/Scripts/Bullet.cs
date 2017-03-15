using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Bullet Hit Enemy"); //Off screen bullet killing zones are counted as enemies for ease of purpose
            Destroy(gameObject);
        }

    }

}

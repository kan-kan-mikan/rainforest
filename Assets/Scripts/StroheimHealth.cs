using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroheimHealth : MonoBehaviour
{

    public AudioSource hitsound;
    public AudioClip hit;

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (Stroheim.activated && coll.gameObject.tag == "Projectile")
        {
            Stroheim.hitcount++;

            hitsound.clip = hit;
            hitsound.Play();

            if (Stroheim.hitcount > 10)
            {
                Destroy(GameObject.Find("Stroheim"));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroheimHealth : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (Stroheim.activated && coll.gameObject.tag == "Projectile")
        {
            Stroheim.hitcount++;

            if (Stroheim.hitcount > 10)
            {
                Destroy(gameObject);
            }
        }
    }
}

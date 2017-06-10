using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroheimDeath : MonoBehaviour
{

    public AudioSource sound;
    public AudioClip death;

    void Update()
    {
        if (Stroheim.hitcount == 10)
        {
            sound.clip = death;
            sound.Play();
            Stroheim.hitcount++;
        }
    }
}

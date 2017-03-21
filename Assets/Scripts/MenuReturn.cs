using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuReturn : MonoBehaviour
{

    public AudioSource sound;
    public AudioClip end1, end2, end3, end4, end5;
    int random;
    bool StartSelected = true;
    GameObject cube1;
    GameObject cube2;

    public void Start()
    {
        random = Random.Range(1, 6);

        if (random == 1)
        {

            sound.clip = end1;

        }
        if (random == 2)
        {

            sound.clip = end2;

        }
        if (random == 3)
        {

            sound.clip = end3;

        }
        if (random == 4)
        {

            sound.clip = end4;

        }
        if (random == 5)
        {

            sound.clip = end5;

        }

        sound.Play();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        cube1 = GameObject.Find("Start Menu Cube");
        cube2 = GameObject.Find("Start Menu Cube 2");
    }

    public void OnMouseUp()
    {

        SceneManager.LoadScene("Mountain");

    }

    public void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            StartSelected = true;
            cube1.transform.position = new Vector3(400, 150, -50f);
            cube2.transform.position = new Vector3(750, 150, -50f);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            StartSelected = false;
            cube1.transform.position = new Vector3(400, 70, -50f);
            cube2.transform.position = new Vector3(750, 70, -50f);
        }
        if (Input.GetAxis("Fire1") > 0)
        {
            if (StartSelected)
            {
                SceneManager.LoadScene("Level Select");
            }
            if (!StartSelected)
            {
                Application.Quit();
            }
        }
    }

}

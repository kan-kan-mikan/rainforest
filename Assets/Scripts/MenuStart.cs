using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuStart : MonoBehaviour
{

    bool StartSelected = true;
    GameObject cube1;
    GameObject cube2;

    public void Start()
    {

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
        if(Input.GetAxis("Vertical") > 0)
        {
            StartSelected = true;
            cube1.transform.position = new Vector3(400, 190, -50f);
            cube2.transform.position = new Vector3(750, 190, -50f);
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            StartSelected = false;
            cube1.transform.position = new Vector3(400, 120, -50f);
            cube2.transform.position = new Vector3(750, 120, -50f);
        }
        if(Input.GetAxis("Fire1") > 0)
        {
            if (StartSelected)
            {
                SceneManager.LoadScene("Mountain");
            }
            if(!StartSelected)
            {
                Application.Quit();
            }
        }
    }

}

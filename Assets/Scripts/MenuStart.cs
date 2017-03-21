using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuStart : MonoBehaviour {

    public void Start()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void OnMouseUp()
    {

        SceneManager.LoadScene("Mountain");
        
    }

}

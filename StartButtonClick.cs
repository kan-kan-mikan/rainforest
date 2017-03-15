using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//A Canvas and Button needs to be made. Add to the On Click () function and attach this script. 
//Go to build settings and add the scene that will begin the game. 
//When you click the Start button, the scene put in the scenes menu will load. 

public class StartButtonClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
}

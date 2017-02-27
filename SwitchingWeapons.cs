using UnityEngine;
using System.Collections;

public class SwitchingWeapons : MonoBehaviour {

	public GameObject Shotgun;
	public GameObject Rifle;
	public GameObject Missle;

	void Start () {
		
		Rifle.SetActive (true);
		Shotgun.SetActive (false);
		Missle.SetActive (false);
	}

	void Update () { //The script will cycle from the rifle, to the shotgun, to the missle launcher, and back to the rifle. 
	
		if (Rifle.activeInHierarchy) {

			if (Input.GetKey ("return")) {
				Rifle.SetActive (false);
				Shotgun.SetActive (true);
				Missle.SetActive (false);

			}
		}
			
		else if (Shotgun.activeInHierarchy) {

			if (Input.GetKey ("return")) {
				Rifle.SetActive (false);
				Shotgun.SetActive (false);
				Missle.SetActive (true);

			}
		}

		else if (Missle.activeInHierarchy) {

			if (Input.GetKey ("return")) {
				Rifle.SetActive (true);
				Shotgun.SetActive (false);
				Missle.SetActive (false);

			}
		}
	}
}

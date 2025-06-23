using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeCloseScript : MonoBehaviour {

	public GameObject safeOpen;
	public GameObject eyeball;

	void Start(){
		if (PlayerPrefs.HasKey ("GF Laboratory Password Done")) {
			Instantiate (safeOpen, transform.position, Quaternion.identity);
			Instantiate (eyeball, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}

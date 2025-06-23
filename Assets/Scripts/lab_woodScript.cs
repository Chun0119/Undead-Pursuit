using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lab_woodScript : MonoBehaviour {

	public GameObject safeClose;

	void Start(){
		if (PlayerPrefs.HasKey ("Key All Input")) {
			Instantiate (safeClose, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	void OnMouseDown(){
		Application.LoadLevel ("Wood");
	}
}

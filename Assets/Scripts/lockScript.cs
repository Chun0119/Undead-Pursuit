using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockScript : MonoBehaviour {

	public string key;
	public string level;
	public GameObject[] locked;

	public GameObject ladder;
	Vector2 location = new Vector2 (4.634f, 0.152f);
	Vector3 scale = new Vector3 (2.0714f, 3.0564f, 2.6494f);

	Inventory inventory;

	void Start(){
		if (PlayerPrefs.HasKey (level + " Can Access")) {
			for (int i = 0; i < locked.Length; i++) {
				Destroy (locked [i]);
			}
			if (level == "2F Roof Top") {
				GameObject newObject = (GameObject)Instantiate (ladder, location, Quaternion.identity);
				newObject.transform.localScale = scale;
			}
		} else if (PlayerPrefs.HasKey (level + " Eyeball")) {
			Destroy (locked [0]);
		}
	}

	void OnMouseDown() {
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		if(inventory.selectedToUse.itemName == key){
			if (key != "Eyeball") {
				PlayerPrefs.SetInt (level + " Can Access", 1);
			} else {
				PlayerPrefs.SetInt (level + " Eyeball", 1);
			}
			for (int i = 0; i < locked.Length; i++) {
				Destroy (locked[i]);
			}
			if (level == "2F Roof Top") {
				GameObject newObject = (GameObject)Instantiate (ladder, location, Quaternion.identity);
				newObject.transform.localScale = scale;
			}
		}
	}
}

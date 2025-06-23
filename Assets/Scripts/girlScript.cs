using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlScript : MonoBehaviour {
	
	Inventory inventory;

	void Start() {
		if (PlayerPrefs.HasKey ("Girl Followed")) {
			Destroy (gameObject);
		}
	}

	void OnMouseDown() {
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		if (inventory.selectedToUse.itemName == "Candy") {
			PlayerPrefs.SetInt ("Girl Followed", 1);
			Destroy (gameObject);
		}
	}
}

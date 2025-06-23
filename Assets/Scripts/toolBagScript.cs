using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolBagScript : MonoBehaviour {

	private Inventory inventory;

	void Start(){
		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.C)) {
			openInventory ();
		}
	}

	void OnMouseDown(){
		openInventory ();
	}

	void openInventory(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		inventory.showInventory = !inventory.showInventory;
		if (inventory.createBlock == false && inventory.showInventory == true) {
			inventory.createBlock = true;
		}
		for (int i = 0; i < inventory.selected.Count; i++) {
			inventory.selected [i] = new Item ();
		}
	}
}

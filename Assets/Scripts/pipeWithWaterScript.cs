using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeWithWaterScript : MonoBehaviour {

	public GameObject pipe;
	public Vector2 location;
	public Vector3 scale;
	Inventory inventory;

	void Start() {
	}

	void OnMouseDown() {
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		if (inventory.selectedToUse.itemName == "Bucket without Water") {
			inventory.AddItem ("Bucket with Water");
			inventory.RemoveItem ("Bucket without Water");
			inventory.selectedToUse = new Item ();
		}
		GameObject newObject = (GameObject)Instantiate (pipe, location, Quaternion.identity);
		newObject.transform.localScale = scale;
		Destroy(gameObject);
	}
}

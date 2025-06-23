using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkInventory : MonoBehaviour {

	public GameObject Object;
	public string key;
	public Vector2 location;
	public Vector3 scale;
	private Inventory inventory;
	BoxCollider2D newCollider;

	void Start(){
		newCollider = GetComponent<BoxCollider2D> ();
		newCollider.enabled = true;
		if (key == "Torch with Battery" && PlayerPrefs.HasKey ("Light On") || 
			(key == "Full Photo" && PlayerPrefs.HasKey("Girl Out"))) {
			GameObject newObject = (GameObject)Instantiate (Object, location, Quaternion.identity);
			newObject.transform.localScale = scale;
			Destroy (gameObject);
		}
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}

	void OnMouseDown(){
		if (inventory.selectedToUse.itemName == key) {
			GameObject newObject = (GameObject)Instantiate (Object, location, Quaternion.identity);
			newObject.transform.localScale = scale;
			if (key == "Full Photo") {
				PlayerPrefs.SetInt ("Girl Out", 1);
				Destroy (gameObject);
			} else {
				newCollider.enabled = false;
			}
		}
	}

}

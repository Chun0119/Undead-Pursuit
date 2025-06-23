using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodScript : MonoBehaviour {

	public GameObject keyholeWithKey;
	public string key;
	Inventory inventory;
	string[] AllKey = new string[7];
	bool allInput = true;
	Vector2[] location = new Vector2[7];
	Vector3[] scale = new Vector3[7];

	void Start(){
		AllKey[0] = "Key to Kitchen";
		AllKey[1] = "Key to Laboratory";
		AllKey[2] = "Key to Master Bedroom";
		AllKey[3] = "Key to GF";
		AllKey[4] = "Key to Entrance";
		AllKey[5] = "Key to Toilet";
		AllKey[6] = "Key to Girl Bedroom";
		location[0] = new Vector2 (-6.5f, 1.5f);
		location[1] = new Vector2 (0f, -2f);
		location[2] = new Vector2 (2.3f, 1.5f);
		location[3] = new Vector2 (-4.5f, -2f);
		location[4] = new Vector2 (6.5f, 1.5f);
		location[5] = new Vector2 (-2f, 1.5f);
		location[6] = new Vector2 (4.5f, -2f);
		scale [0] = new Vector3 (1.82673f, 1.82673f, 1.82673f); 
		scale [1] = new Vector3 (1.89092f, 1.89092f, 1.89092f); 
		scale [2] = new Vector3 (1.87098f, 1.87098f, 1.87098f); 
		scale [3] = new Vector3 (1.74982f, 1.74982f, 1.74982f); 
		scale [4] = new Vector3 (1.82673f, 1.82673f, 1.82673f); 
		scale [5] = new Vector3 (1.86704f, 1.86704f, 1.86704f); 
		scale [6] = new Vector3 (1.87963f, 1.87963f, 1.87963f); 

		allInput = true;

		if (PlayerPrefs.HasKey (key + " input")) {
			GameObject newObject = Instantiate (keyholeWithKey, location[findIndex()], Quaternion.identity);
			newObject.transform.localScale = scale [findIndex ()];
			Destroy (gameObject);
		}
	}

	int findIndex(){
		for (int i = 0; i < 7; i++) {
			if (AllKey [i] == key) {
				return i;
			}
		}
		return 8;
	}

	void CheckAll(){
		for (int i=0; i<7; i++){
			if (!PlayerPrefs.HasKey (AllKey [i] + " input")) {
				allInput = false;
				break;
			}
		}
		if (allInput) {
			PlayerPrefs.SetInt ("Key All Input", 1);
			Application.LoadLevel ("GF Laboratory");
		}
		allInput = true;
	}

	void OnMouseDown(){
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		if (inventory.selectedToUse.itemName == key) {
			GameObject newObject = Instantiate (keyholeWithKey, location[findIndex()], Quaternion.identity);
			newObject.transform.localScale = scale [findIndex ()];
			PlayerPrefs.SetInt (key + " input", 1);
			CheckAll();
			Destroy (gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour {

	public GameObject[] monster = new GameObject[6];
	GameObject blackScene;

	Vector2[] location;
	Vector3[] scale;
	Inventory inventory;

	void Start() {
		blackScene = GameObject.Find ("Black Scene");
		location = new Vector2[6];
		scale = new Vector3[6];
		location[0] = new Vector2(-2.2f, -0.55f);
		scale[0] = new Vector3(0.6f, 0.6f, 0.6f);
		location[1] = new Vector2(4.382f, -3.49f);
		scale[1] = new Vector3(1.187f, 1.187f, 1.187f);
		location[2] = new Vector2(1.66f, -3.37f);
		scale[2] = new Vector3(1f, 1f, 1f);
		location[3] = new Vector2(-5.23f, -2.23f);
		scale[3] = new Vector3(1f, 1f, 1f);
		location[4] = new Vector2(2.42f, -0.11f);
		scale[4] = new Vector3(0.51f, 0.51f, 0.51f);
		location[5] = new Vector2(-4.65f, 3.64f);
		scale[5] = new Vector3(0.4417f, 0.4417f, 0.4417f);

		if (CheckingAvailability ()) {
			Destroy (blackScene);
			for (int i = 0; i < 6; i++) {
				GameObject newObject = (GameObject)Instantiate (monster[i], location [i], Quaternion.identity);
				newObject.transform.localScale = scale [i];
			}
			Destroy (gameObject);
		}
	}

	public bool CheckingAvailability(){
		if (PlayerPrefs.HasKey ("Light On")) {
			return true;
		}
		return false;
	}

	void OnMouseDown() {
		Destroy (blackScene);
		for (int i = 0; i < 6; i++) {
			GameObject newObject = (GameObject)Instantiate (monster[i], location [i], Quaternion.identity);
			newObject.transform.localScale = scale [i];
			newObject.transform.position = new Vector3 (location[i].x, location[i].y, -8f);
		}
		PlayerPrefs.SetInt ("Light On", 1);
		Destroy(gameObject);
	}
}

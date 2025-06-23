using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterPopupScript : MonoBehaviour {

	public GameObject monster;
	public Vector2 location;
	public Vector3 scale;
	public string key;
	monsterScript newMonster;
	BoxCollider2D newCollider;

	public bool CheckingAvailability(){
		if (PlayerPrefs.HasKey (key + " Pressed")) {
				return true;
		}
		return false;
	}

	void OnMouseDown() {
		if (CheckingAvailability() == false) {
			GameObject newObject = (GameObject)Instantiate (monster, location, Quaternion.identity);
			newObject.transform.localScale = scale;
			newObject.transform.position = new Vector3 (location.x, location.y, -8f);
			PlayerPrefs.SetInt (key + "Pressed", 1);
			newMonster = newObject.GetComponent<monsterScript> ();
			newMonster.key = key + " Monster";
			newCollider = newObject.GetComponent<BoxCollider2D> ();
			newCollider.size = new Vector2 (13.72683f, 7.872506f);
		}
	}
}

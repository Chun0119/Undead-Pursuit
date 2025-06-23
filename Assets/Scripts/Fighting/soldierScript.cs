using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldierScript : MonoBehaviour {

	Character character;

	void Start(){
		character = GameObject.Find ("Character").GetComponent<Character> ();	
	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			if (character.holding.soldierID == 0) {
				character.RemoveSoldier (gameObject.name);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			if (character.holding.soldierID == 0) {
				character.HoldLastSoldier (gameObject.name);
			} else {
				character.PlaceLastSoldier (gameObject.name);
			}
		}
	}

}

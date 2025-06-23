using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyRowScript : MonoBehaviour {

	Character character;

	void Start(){
		character = GameObject.Find ("Character").GetComponent<Character> ();	
	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			if (character.holding.soldierObject == null) {
				character.HoldLastSoldier (gameObject.name);
			} else {
				character.PlaceLastSoldier (gameObject.name);
			}
		}
	}
}

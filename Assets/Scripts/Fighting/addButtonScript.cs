using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addButtonScript : MonoBehaviour {

	Character character;

	void Start(){
		character = GameObject.Find ("Character").GetComponent<Character> ();	
	}

	void OnMouseDown(){
		character.RandomAddSoldier();
	}
}

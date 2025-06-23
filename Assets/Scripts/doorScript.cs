using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

	public string level;
	public AudioClip sound;
	public string key;
	public bool password;
	private Inventory inventory;

	public void Start(){
		if (level == "GF Girl Study Room" || level == "GF Laboratory") {
			if (PlayerPrefs.HasKey ("GF Living Room Monster")) {
				GetComponent<BoxCollider2D>().enabled = true;
			} else {
				GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	public bool CheckingAvailability(){
		if (PlayerPrefs.HasKey (level + " Can Access")) {
			return true;
		}
		return false;

	}

	bool isUnlock(){
		if (!((level == "1F Storeroom" && !GameObject.Find ("Fire")) ||
			(level == "GF Dining Room" && !GameObject.Find ("Iron Lock")) ||
			(level == "GF Living Room" && PlayerPrefs.HasKey("1F Storeroom Password Done")) ||
			(level == "GF Entrance" && !GameObject.Find ("Eyeball Recognition System")))) {
			return false;
		}
		return true;
	}

	void OnMouseDown() {
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		if (level != "GF Entrance") {
			if (CheckingAvailability ()) { //Can access
				loadNextLevel ();
			} else if (!password && key != "Locked" && inventory.selectedToUse.itemName == key) { //no password, no lock, need key
				loadNextLevel ();
			} else if (key != "Locked" && isUnlock () && inventory.selectedToUse.itemName == key) { //need / no need password, need / no lock, need key
				loadNextLevel ();
			} else if (!password && key == "Locked" && isUnlock ()) { //no password, need lock, no key
				loadNextLevel ();
			}
		} else {
			if ((isUnlock () && inventory.selectedToUse.itemName == key) || CheckingAvailability()) {
				loadNextLevel ();
			}
		}
	}

	void loadNextLevel(){
		//GetComponent<AudioSource>().PlayOneShot(sound);
		//waitForSound ();
		Application.LoadLevel(level);
		if (!CheckingAvailability ()) {
			PlayerPrefs.SetInt (level + " Can Access", 1);
		}
	}


	IEnumerator waitForSound(){
		while(GetComponent<AudioSource>().isPlaying) {
			yield return null;
		}
	}
}

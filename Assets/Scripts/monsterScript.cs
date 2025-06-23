using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class monsterScript : MonoBehaviour {
	
	public string level;
	public AudioClip sound;
	public string key;
	public static string PreviousLevel;
	public GameObject deadbody;
	public AudioClip[] newTrack = new AudioClip[2];
	private audioScript AudioScript;

	void Start() {
		GetComponent<AudioSource>().PlayOneShot(sound);
		CheckingAvailability ();
	}

	void Awake(){
		if (key == "Ghost") {
			DontDestroyOnLoad (gameObject);
		}
	}
	void CheckingAvailability(){
		if (key == "GF Laboratory Monster") {
			if (PlayerPrefs.HasKey(key) && !PlayerPrefs.HasKey ("Password Paper")) {
				GameObject newObject = Instantiate (deadbody, new Vector2 (1.33f, -2.83f), Quaternion.Euler(new Vector3 (0f, 0f, -239f)));
				newObject.transform.localScale = new Vector3 (0.825871f, 0.825871f, 0.825871f);
			}
		}
		if (PlayerPrefs.HasKey (key)) {
			Destroy (gameObject);
		}
	}

	void OnMouseDown() {
		if (key != "" && key != null){
			PlayerPrefs.SetInt(key, 1);
		}
		Inventory inventory = GameObject.Find ("Inventory").GetComponent<Inventory>();
		if(key == "1F Toilet Monster"){
			inventory.AddItem("Key to Master Bedroom");
		}
		if(key == "Ghost"){
			inventory.AddItem("Key to Out");
		}
		if (key == "GF Living Room Monster") {
			inventory.AddItem ("Photo Piece");
		}
		if (key == "Chest Monster"){
			inventory.AddItem("Photo Piece 2");
		}
		if(key == "2F Roof Top Monster"){
			inventory.AddItem("Photo Piece 3");
		}
		if(key == "GF Toilet Monster"){
			inventory.AddItem("Photo Piece 4");
		}
		if(key == "GF Toilet Monster 2"){
			inventory.AddItem("Photo Piece 5");
		}
		if(key == "GF Toilet Monster 3"){
			inventory.AddItem("Photo Piece 6");
		}
		PreviousLevel = SceneManager.GetActiveScene ().name;
		Application.LoadLevel(level);
	}

	public void BackWard(){
		AudioScript = FindObjectOfType<audioScript> ();
		if (newTrack [0] != null && newTrack [1] != null) {
			if (PreviousLevel == "1F Toilet" || PreviousLevel == "1F Master Bedroom" || PreviousLevel == "1F Living Room" ||
			    PreviousLevel == "1F Game Room" || PreviousLevel == "2F Roof Top") {
				AudioScript.changeBGM (newTrack [0]);
			} else {
				AudioScript.changeBGM (newTrack [1]);
			}
		}
		Application.LoadLevel (PreviousLevel);
	}

}

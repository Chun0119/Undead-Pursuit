using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBGM : MonoBehaviour {
	public AudioClip newTrack;
	private audioScript AudioScript;
	public bool locked;

	// Use this for initialization
	void Start () {
		AudioScript = FindObjectOfType<audioScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown() {
		if (newTrack != null && !locked) {
			AudioScript.changeBGM(newTrack);
		}
		if (newTrack != null && locked && PlayerPrefs.HasKey("GF Living Room Can Access")){
			AudioScript.changeBGM(newTrack);
		}
	}
}

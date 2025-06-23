using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour {
	public AudioSource BGM;
	public AudioSource sound;

    void Start() {

    }
		
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update() {

    }

	public void changeBGM(AudioClip music) {
		BGM.Stop ();
		BGM.clip = music;
		BGM.Play ();
	}
		
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueScript : MonoBehaviour {

	int currentIndex;
	public static bool end = false;
	public static GameObject endObject = null;

	void Start()
	{
		currentIndex = 0;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			NextDialogue ();
		}
		if (currentIndex < GetComponent<Dialogue> ().text.Length) {
			GameObject.Find ("Text").GetComponent<Text> ().text = GetComponent<Dialogue> ().text [currentIndex];
		}
	}

	void OnMouseDown(){
		NextDialogue ();
	}

	void NextDialogue(){
		if (currentIndex + 1 >= GetComponent<Dialogue> ().text.Length) {
			GetComponent<Dialogue> ().CloseDialogue ();
			if (end) {
				GetComponent<Dialogue> ().CreateObjectAtEnd (endObject);
			}
			currentIndex = 0;
		} else {
			currentIndex ++;
		}
	}

}

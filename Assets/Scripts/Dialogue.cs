using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour{

	public string[] text;
	public Font font;
	public GameObject dialogueBox;
	public GameObject textArea;

	public void CloseDialogue(){
		Destroy (dialogueBox);
		Destroy(textArea);
	}
		
	public void CreateObjectAtEnd(GameObject endObject){
		Vector2 location = new Vector2();
		Vector3 scale = new Vector3();
		if (endObject.name == "Full Photo") {
			location = new Vector2 (0f, -1f);
			scale = new Vector3 (1.2009f, 1.2009f, 1.2009f);
		} else {
			location = new Vector2 (0f, 0.29f);
			scale = new Vector3 (1.75681f, 1.75681f, 1.75681f);
		}
		GameObject newObject = Instantiate (endObject, location, Quaternion.identity);
		newObject.transform.localScale = scale;
	}
		
}

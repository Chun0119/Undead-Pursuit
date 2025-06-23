using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour {

	public GameObject textArea;
	public Dialogue newDialogue;
	public string name;
	public bool door;
	public bool needKey;
	public string key;
	public string level;
	public string[] displayText;
	public bool auto = false;
	public bool end = false;
	public GameObject endObject;
	Vector2 location = new Vector2(0f, -3.56f);
	Vector3 scale = new Vector3(2.5939f, 1f, 1f);
	Inventory inventory;
	Dialogue newDialogue2;

	public void CreateDialogue(){
		newDialogue2 = Instantiate(newDialogue, location, Quaternion.identity);
		newDialogue2.textArea = Instantiate(textArea, new Vector2(0f, 0f), Quaternion.identity);
		newDialogue2.text = displayText;
		newDialogue2.dialogueBox.transform.localScale = scale;
		newDialogue2.dialogueBox.transform.position = new Vector3 (location.x, location.y, -9f);
		if (name != "" && name != null) {
			PlayerPrefs.SetInt (name, 1);
		}
	}

	public bool CheckingAvailability(){
		for (int i = 0; i < 30; i++) {
			if (PlayerPrefs.HasKey (name)) {
				return true;
			}
		}
		return false;
	}

	void Start(){
		if (CheckingAvailability () == false) {
			if (auto == true) {
				CreateDialogue ();
			}
		}
		dialogueScript.end = end;
		dialogueScript.endObject = endObject;
	}

	void Update(){
	}

	void OnMouseDown(){
		if (CheckingAvailability () == false) {
			if (auto == false) {
				if (!door && !needKey) {
					CreateDialogue ();
				} else {
					inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
					if (door && !PlayerPrefs.HasKey (level + " Can Access") && inventory.selectedToUse.itemName != key) {
						CreateDialogue ();
					}
					if (needKey && inventory.selectedToUse.itemName != key) {
						CreateDialogue ();
					}
				}
			}
		}
	}
}

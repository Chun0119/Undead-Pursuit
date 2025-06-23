using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpPageScript : MonoBehaviour {

	bool showHelpPage;
	Vector3 scale;
	public Texture2D[] helpPage;
	int currentIndex = 0;
	public GameObject block;
	public bool createBlock;
	public Texture2D close;

	void Start(){
		showHelpPage = false;
	}

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.H)) {
			currentIndex = 0;
			showHelpPage = !showHelpPage;
			if (createBlock == false && showHelpPage == true) {
				createBlock = true;
			}
		}
		if (createBlock) {
			GameObject newObject = Instantiate (block, new Vector2 (0f, 0f), Quaternion.identity);
			newObject.transform.position = new Vector3 (0f, 0f, -9.5f);
			createBlock = false;
		} else {
			if (showHelpPage == false) {
				if (GameObject.Find ("Block(Clone)")) {
					Destroy (GameObject.Find ("Block(Clone)"));
				}
			}
		}
	}

	void OnGUI(){
		Event currentEvent = Event.current;
		scale.x = Screen.width / 952f;
		scale.y = Screen.height / 536f;
		scale.z = 1f;
		Matrix4x4 svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
		if (showHelpPage) {
			Vector2 pagePosition = new Vector2 (-75f, 20f);
			Vector2 pageSize = new Vector2 (1100, 500);
			Rect pageRect = new Rect (pagePosition, pageSize);
			GUI.DrawTexture (pageRect, helpPage[currentIndex], ScaleMode.ScaleToFit);
			if (pageRect.Contains (Event.current.mousePosition)) {
				if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown) {
					if (currentIndex + 1 >= helpPage.Length) {
						showHelpPage = false;
						createBlock = false;
					} else {
						currentIndex++;
					}
				}
			}
			if (GUI.Button (new Rect (850, 25, 50, 50), close)) {
				showHelpPage = false;
			}
		}
		GUI.matrix = svMat;
	}
}

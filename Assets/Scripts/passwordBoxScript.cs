using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class passwordBoxScript : MonoBehaviour {

	public GameObject[] number = new GameObject[10];
	public Vector2 location;
	public Vector3 scale;

	public static string PreviousScene;
	public static string correctPassword;
	public string zoom;

	static string password;
	static Vector2 objectLocation;
	BoxCollider2D collider;

	void Awake() {
		PreviousScene = SceneManager.GetActiveScene ().name;
		collider = GetComponent<BoxCollider2D> ();
		if (PlayerPrefs.HasKey (PreviousScene + " Password Done")) {
			collider.enabled = false;
		} else {
			collider.enabled = true;
		}
		Reset ();
	}
		
	public void Reset() {
		objectLocation = location;
		password = "";
	}

	public void CreateNumberImage(int num) {
		if (password.Length < 4 || password == "") {
			GameObject newObject = (GameObject)Instantiate (number [num], objectLocation, Quaternion.identity);
			newObject.name = "Password " + password.Length;
			newObject.transform.localScale = scale;
			objectLocation.x += 1.2f;
			password += num;
			Debug.Log (password);
		}
		if (password.Length == 4) {
			CheckCorrectPassword ();
		}
	}

	public void CheckCorrectPassword() {
		if (password == correctPassword) {
			PlayerPrefs.SetInt (PreviousScene + " Password Done", 1);
			if (PreviousScene == "GF Dining Room") {
				PlayerPrefs.SetInt ("GF Toilet Can Access", 1);
			}
			Application.LoadLevel (PreviousScene);
			PreviousScene = "";
			correctPassword = "";
		} else {
			for (int i = 0; i < 4; i++) {
				Destroy (GameObject.Find("Password " + i));
			}
		}
		Reset ();
	}
		
	public void Backward() {
		Application.LoadLevel (PreviousScene);
	}

	void OnMouseDown() {
		if (PreviousScene == "1F Storeroom") {
			correctPassword = "1506";
		}
		if (PreviousScene == "GF Dining Room") {
			correctPassword = "3200";
		}
		if (PreviousScene == "GF Laboratory") {
			correctPassword = "7159";
		}
		Application.LoadLevel(zoom);
	}
}

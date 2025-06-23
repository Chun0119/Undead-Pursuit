using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class becomeGhostScript : MonoBehaviour {

	public GameObject ghost;
	public Vector2 location;
	public Vector3 scale;
	BoxCollider2D newCollider;

	void Start()
	{
		if (PlayerPrefs.HasKey ("Ghost Out")) {
			GameObject newObject = (GameObject)Instantiate (ghost, location, Quaternion.identity);
			newObject.transform.localScale = scale;
			newObject.transform.position = new Vector3 (location.x, location.y, -8f);
			newCollider = newObject.GetComponent<BoxCollider2D> ();
			newCollider.size = new Vector2 (13.72683f, 7.872506f);
			Destroy (gameObject);
		}
	}

	void OnMouseDown(){
		GameObject newObject = (GameObject)Instantiate (ghost, location, Quaternion.identity);
		newObject.transform.localScale = scale;
		newObject.transform.position = new Vector3 (location.x, location.y, -8f);
		newCollider = newObject.GetComponent<BoxCollider2D> ();
		newCollider.size = new Vector2 (13.72683f, 7.872506f);
		PlayerPrefs.SetInt ("Ghost Out", 1);
		Destroy (gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeScript : MonoBehaviour {

	public GameObject water;
	public Vector2 location;
	public Vector3 scale;

	void Start() {
	}

	void OnMouseDown() {
		GameObject newObject = (GameObject)Instantiate (water, location, Quaternion.identity);
		newObject.transform.localScale = scale;
		Destroy(gameObject);
	}
}

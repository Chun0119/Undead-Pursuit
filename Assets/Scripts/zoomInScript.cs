using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomInScript : MonoBehaviour {

	public float camSize, camSizeLimit, timeLerp, timeLerpValue;
	public Vector3 camPosition, camPositionLimit;
	public GameObject theEnd;
	public AudioClip sound;
	void Start(){
		StartCoroutine (createTheEnd ());
	}

	void Update () {
		camSize = Camera.main.orthographicSize;
		timeLerpValue = timeLerp * Time.deltaTime;
		camPosition = Camera.main.transform.position;
		if (camPosition.x < camPositionLimit.x) {
			Camera.main.transform.position = Vector3.Lerp (camPosition, camPositionLimit, timeLerpValue);
		}
		if (camSize > camSizeLimit) {
			Camera.main.orthographicSize = Mathf.Lerp (camSize, camSizeLimit, timeLerpValue);
		}
	}

	IEnumerator createTheEnd(){
		yield return new WaitForSeconds (3);
		GetComponent<AudioSource>().PlayOneShot(sound);
		Instantiate (theEnd, new Vector2 (0f, 0f), Quaternion.identity);
	}
}

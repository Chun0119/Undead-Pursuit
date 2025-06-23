using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireScript : MonoBehaviour {

	public float speed = 6f;
	Monster monster;

	void Start(){
		GetComponent<Rigidbody2D> ().velocity = new Vector3(speed, 0f, 0f);
		monster = GameObject.Find ("Monster").GetComponent<Monster> ();	
	}

	void Update(){
		if (gameObject.transform.position.x > 7) {
			int atk = gameObject.name [5] - 48;
			monster.DestroyBloodBar ();
			monster.blood = monster.blood - atk;
			monster.DrawBloodBar ();
			Destroy (gameObject);
		}
	}
}

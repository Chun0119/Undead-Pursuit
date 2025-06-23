using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	Monster monster;

	void Start(){	
		monster = GameObject.Find ("Monster").GetComponent<Monster> ();	
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.tag == "Character Fire") {
			int row = gameObject.name [19] - 48;
			int column = gameObject.name [21] - 48;
			int atk = obj.name [5] - 48;
			int _atk = obj.name [5] - 48;
			atk = atk - monster.formation [row, column].hp;
			monster.AttackBy (_atk, row, column);
			if (atk < 0) {
				monster.MoveUp (column);
				Destroy (obj.gameObject);
			} else if (atk == 0) {
				Destroy (obj.gameObject);
				monster.Die (row, column);
				Destroy (gameObject);
				monster.MoveUp (column);
			} else {
				obj.name = "Fire " + atk;
				monster.Die (row, column);
				Destroy (gameObject);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int blood;
	public GameObject bloodBar;
	public Soldier[,] formation;
	int row, column;
	PuzzleGameManager GM;
	int remainSoldier;
	public Sprite ghost;

	void Start(){
		GM = GameObject.FindGameObjectWithTag ("Puzzle Game Manager").GetComponent<PuzzleGameManager> ();
		if (GameObject.Find ("Ghost(Clone)")) {
			blood = 10;
			GetComponent<SpriteRenderer> ().sprite = ghost;
			GameObject.Find ("Ghost(Clone)").GetComponent<BoxCollider2D> ().enabled = false;
			transform.localScale = new Vector3 (2.91f, 2.91f, 2.91f);
			remainSoldier = 15;
		} else {
			blood = 5;
			remainSoldier = 12;
		}
		row = GM.Row;
		column = GM.Column;
		formation = new Soldier[row,column];
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				formation [i, j] = new Soldier ();
			}
		}
		GM.RandomAdd (formation, ref remainSoldier, 7);
		DrawFormation();
		DrawBloodBar ();
	}

	IEnumerator StartAgain(){
		yield return new WaitForSeconds (2);
		remainSoldier++;
		GM.RandomAdd (formation, ref remainSoldier, 7);
		DrawFormation ();
		StartCoroutine (ChangeAgain());
	}

	IEnumerator ChangeAgain(){
		yield return new WaitForSeconds (1);
		GM.changeTurn ("Player's");
	}

	void Update(){
		if (GM.turn == "Enemy's") {
			GM.changeTurn ("Enemy Moving");
			StartCoroutine(StartAgain ());
		}
		if (blood <= 0) {
			monsterScript win = GameObject.Find ("Backward").GetComponent<monsterScript> ();
			if (GameObject.Find ("Ghost(Clone)")) {
				GameObject.Find ("Ghost(Clone)").GetComponent<BoxCollider2D> ().enabled = true;
				Destroy (GameObject.Find ("Ghost(Clone)"));
			}
			win.BackWard ();
		}
	}

	public void DestroyBloodBar(){
		for (int i = 0; i < blood; i++) {
			if (GameObject.Find ("Monster Blood Bar " + i)) {
				Destroy (GameObject.Find ("Monster Blood Bar " + i));
			}
		}
		DrawFormation ();
	}

	public void DrawBloodBar(){
		for (int i = 0; i < blood; i++) {
			GameObject newObject = Instantiate(bloodBar, new Vector2((float)(i*-0.5+6), 4.2f), Quaternion.identity);
			newObject.name = "Monster Blood Bar " + i;
		}

	}

	void DrawFormation(){
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				if (GameObject.Find ("Monster Formation [" + i + "," + j + "]")) {
					Destroy (GameObject.Find ("Monster Formation [" + i + "," + j + "]"));
				}
				if (formation [i, j].soldierID != 0) {
					Vector2 location = new Vector2 ((float)(i + 1) * 1.1f, (float)3.25 - j * 1.3f);
					GameObject newObject = Instantiate (formation [i, j].soldierObject, location, Quaternion.identity);
					newObject.name = "Monster Formation [" + i + "," + j + "]";
					newObject.transform.position = new Vector3 (location.x, location.y, -8f);
				}
			}
		}
	}

	public void AttackBy(int atk, int r, int c){
		formation [r, c].hp = formation [r, c].hp - atk;
	}

	public void Die(int r, int c){
		formation [r, c] = new Soldier ();
	}

	public void MoveUp(int c){
		int r = -1;
		for (int i = 0; i < row; i++) {
			if (formation [i, c].soldierID != 0) {
				r = i;
				break;
			}
		}
		if (r != -1) {
			for (int i = 0; i < row; i++) {
				if (i + r < row) {
					formation [i, c].Clone (formation [r + i, c]);
				} else {
					formation [i, c] = new Soldier ();
				}
			}
		}
		DrawFormation ();
	}
}

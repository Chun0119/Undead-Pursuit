using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public Soldier[,] formation;
	int row, column;
	PuzzleGameManager GM;
	int move, remainSoldier;
	public Soldier holding;
	int tempColumn;
	public GameObject emptyRow;
	public GameObject block;
	GameObject holdingObject;
	Text remainingSoldier, remainingMove;

	void Start(){
		GM = GameObject.FindGameObjectWithTag ("Puzzle Game Manager").GetComponent<PuzzleGameManager> ();
		remainingMove = GameObject.Find ("Move").GetComponent<Text> ();
		remainingSoldier = GameObject.Find ("Soldier").GetComponent<Text> ();
		row = GM.Row;
		column = GM.Column;
		move = 2;
		remainSoldier = 20;
		holding = new Soldier ();
		formation = new Soldier[row,column];
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				formation [i, j] = new Soldier ();
			}
		}
		GM.RandomAdd (formation, ref remainSoldier, -1);
		DrawFormation();
		DrawEmptyRow ();
	}

	IEnumerator StartAgain(){
		yield return new WaitForSeconds (2);
		GM.NewTurn (formation, ref remainSoldier);
		DrawFormation();
		Destroy (GameObject.Find ("Block Player"));
	}

	void Update(){
		if (move == 0) {
			GM.changeTurn ("Enemy's");
			GameObject newObject = Instantiate (block, new Vector2 (0f, 0f), Quaternion.identity);
			newObject.transform.position = new Vector3 (0f, 0f, -9f);
			newObject.name = "Block Player";
			move = 2;
		}
		if (GM.turn == "Player's") {
			GM.changeTurn ("Player Moving");
			StartCoroutine(StartAgain ());
		}
		if (holdingObject != null) {
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition.z = 0f;
			holdingObject.transform.position = mousePosition;
		}
		remainingSoldier.text = "No. of Soldiers can be added: " + remainSoldier;
		remainingMove.text = "No. of Moves remaining: " + move;
	}
		
	void DrawEmptyRow(){
		for (int j = 0; j < column; j++) {
			Vector2 location = new Vector2 (0f, (float)2.6 - j * 1.3f);
			GameObject newObject = Instantiate (emptyRow, location, Quaternion.identity);
			newObject.name = "Character Formation [x," + j + "]";
			newObject.transform.position = new Vector3 (location.x, location.y, -7f);
		}
	}

	void DrawFormation(){
		GM.FormWeaponUnit (formation);
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				if (GameObject.Find ("Character Formation [" + i + "," + j + "]")) {
					Destroy (GameObject.Find ("Character Formation [" + i + "," + j + "]"));
				}
				if (GameObject.Find ("Character Attack Unit [" + i + "," + j + "]")) {
					Destroy (GameObject.Find ("Character Attack Unit [" + i + "," + j + "]"));
				}
				if (GameObject.Find ("Character Attack Turn [" + i + "," + j + "]")) {
					Destroy (GameObject.Find ("Character Attack Turn [" + i + "," + j + "]"));
				}
				if (formation [i, j].soldierID != 0) {
					Vector2 location = new Vector2 ((float)(i + 1) * -1.1f, (float)3.25 - j * 1.3f);
					GameObject newObject = Instantiate (formation [i, j].soldierObject, location, Quaternion.identity);
					newObject.name = "Character Formation [" + i + "," + j + "]";
					newObject.transform.position = new Vector3 (location.x, location.y, -8f);
					if (formation [i, j].type != Soldier.soldierType.soldier) {
						DrawUnit (formation[i,j], i, j);
					}
				}
			}
		}
	}

	void DrawUnit(Soldier unit, int row, int column){
		Vector2 location = new Vector2 ((float)0.4 + (row + 1) * -1.1f, (float)3.6 - column * 1.3f);
		if (unit.atk != 0) {
			GameObject attackObject = Instantiate (GM.atkObj [unit.atk - 1], location, Quaternion.identity);
			attackObject.name = "Character Attack Unit [" + row + "," + column + "]";
			Vector2 location2 = new Vector2 ((float)0.4 + (row + 1) * -1.1f, (float)2.95 - column * 1.3f);
			GameObject roundObject = Instantiate (GM.roundObj [unit.turn - 1], location2, Quaternion.identity);
			roundObject.name = "Character Attack Turn [" + row + "," + column + "]";
		}
	}

	public void RandomAddSoldier(){
		if (remainSoldier > 0) {
			GM.RandomAdd (formation, ref remainSoldier, -1);
			DrawFormation ();
			move--;
			remainSoldier = 0;
		} else {
			Debug.Log ("No Soldier To Add");
		}
	}

	public void RemoveSoldier(string name){
		int r = name [21] - 48;
		int c = name [23] - 48;
		GM.RemoveSoldier (r, c, formation);
		DrawFormation ();
		remainSoldier++;
		move--;
	}

	public void HoldLastSoldier(string name){
		int r = 4;
		int c = name [23] - 48;
		for (int i = 1; i < row; i++) {
			if (formation [i, c].soldierID == 0) {
				r = i - 1;
				break;
			}
		}
		if (formation [r, c].type == Soldier.soldierType.soldier) {
			holding.Clone(formation [r, c]);
			holdingObject = Instantiate (holding.soldierObject, new Vector2 (0f, 0f), Quaternion.identity);
			holdingObject.name = "Holding";
			holdingObject.GetComponent<BoxCollider2D> ().enabled = false;
			tempColumn = c;
			GM.RemoveSoldier (r, c, formation);
			DrawFormation ();
		}
	}

	public void PlaceLastSoldier(string name){
		int c = name [23] - 48;
		for (int i = 0; i < row; i++) {
			if (formation [i, c].soldierID == 0) {
				formation [i, c].Clone(holding);
				Destroy (GameObject.Find ("Holding"));
				holdingObject = null;
				holding = new Soldier ();
				DrawFormation ();
				if (tempColumn != c) {
					move--;
				}
				break;
			}
		}
	}
}

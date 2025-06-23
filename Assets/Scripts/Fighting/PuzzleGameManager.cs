using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour {

	public GameObject playerTurnBox;
	public GameObject enemyTurnBox;
	public string turn;
	public int Row, Column;
	soldierDatabase database;
	public GameObject[] atkObj;
	public GameObject[] roundObj;
	public GameObject fire;

	void Start(){
		turn = "First Player's";
		Row = 5;
		Column = 6;
		StartCoroutine (DrawTurnBox ());
		database = GameObject.FindGameObjectWithTag ("Soldier Database").GetComponent<soldierDatabase> ();
	}

	IEnumerator DrawTurnBox(){
		GameObject turnBox;
		if (turn == "Player's" || turn == "First Player's") {
			turnBox = playerTurnBox;
		} else {
			turnBox = enemyTurnBox;
		}
		GameObject newObject = Instantiate (turnBox, new Vector2 (0f, 0f), Quaternion.identity);
		yield return new WaitForSeconds (2);
		Destroy (newObject);
	}

	public void changeTurn(string whoseTurn){
		turn = whoseTurn;
		if (turn == "Player's" || turn == "Enemy's") {
			StartCoroutine (DrawTurnBox ());
		}
	}

	public void AddSoldierToFormation(Soldier soldier, Soldier[,] formation, int column){
		for (int i = 0; i < Row; i++) {
			if (formation [i, column].soldierID == 0) {
				formation [i, column].Clone(soldier);
				break;
			}
		}
	}

	bool Check(int num, int column, Soldier[,] formation){
		int row = -1;
		for (int i = 0; i < Row; i++) {
			if (formation [i, column].soldierID == 0) {
				row = i;
				break;
			}
		}
		if (row == -1) {
			return false;
		} else if (row >= 2) {
			if (formation [row - 1, column].soldierID == num && formation [row - 2, column].soldierID == num) {
				return false;
			}
		}
		return true;
	}

	public void RandomAdd(Soldier[,] formation, ref int remainSoldier, int type){
		while (remainSoldier > 0){
			int num = Random.Range (1, 5);
			int column = Random.Range (0, Column);
			int id;
			if (type == -1) {
				id = (num + type) * 2;
			} else {
				id = num + type;
			}
			if (Check (database.soldiers [id].soldierID, column, formation)) {
				AddSoldierToFormation (database.soldiers [id], formation, column);
				remainSoldier--;
			}
		}
	}

	public void RemoveSoldier(int row, int column, Soldier[,] formation){
		for (int i = row; i < Row - 1; i++) {
			formation [i, column].Clone(formation [i + 1, column]);
		}
		formation [Row - 1, column] = new Soldier ();
	}

	public void FormWeaponUnit(Soldier[,] formation){
		for (int j = 0; j < Column; j++) {
			for (int i = 0; i < Row - 2; i++) {
				if (formation [i, j].type == Soldier.soldierType.soldier) {
					if (formation [i, j].soldierID == formation [i + 1, j].soldierID &&
						formation [i + 2, j].soldierID == formation [i + 1, j].soldierID) {
						formation [i, j].Clone(database.soldiers [formation[i,j].soldierID * 2 - 1]);
						formation [i + 1, j].Clone(database.soldiers [formation[i+1,j].soldierID * 2 - 1]);
						formation [i + 2, j].Clone(database.soldiers [formation[i+2,j].soldierID * 2 - 1]);
						formation [i + 1, j].atk = 0;
						formation [i + 2, j].atk = 0;
						for (int k = 0; k < 3; k++) {
							ChangePosition (formation, i + 2, j);
						}
					}
				}
			}
		}
	}

	void ChangePosition(Soldier[,] formation, int row, int column){
		Soldier temp = new Soldier ();
		temp.Clone(formation [row, column]);
		formation [row, column] = new Soldier ();
		for (int i = 0; i < row; i++) {
			formation [row - i, column].Clone(formation [row - i - 1, column]);
		}
		formation [0, column].Clone(temp);
		FormWeaponUnit (formation);
	}

	public void NewTurn(Soldier[,] formation, ref int remainSoldier){
		List<int> needMoveUp = new List<int> ();
		for (int i = 0; i < Row; i++) {
			for (int j = 0; j < Column; j++) {
				if (formation [i, j].type == Soldier.soldierType.weapon && formation[i,j].atk > 0) {
					formation [i, j].turn--;
					if (formation [i, j].turn == 0) {
						Vector3 rotation = new Vector3(0, 0, 90);
						Vector2 location = new Vector2 ((float)(i + 1) * -1.1f, (float)3.25 - j * 1.3f);
						GameObject newObject = Instantiate (fire, location, Quaternion.Euler(rotation));
						newObject.name = "Fire " + formation [i, j].atk;
						formation [i, j] = new Soldier ();
						formation [i + 1, j] = new Soldier ();
						formation [i + 2, j] = new Soldier ();
						remainSoldier = remainSoldier + 3;
						needMoveUp.Add (j);
					}
				}
			}
		}
		for (int j = 0; j < needMoveUp.Count; j++) {
			for (int i = 0; i < Row - 3; i++) {
				formation [i, needMoveUp [j]] = formation [i + 3, needMoveUp [j]];
			}
			formation [Row - 1, needMoveUp [j]] = new Soldier ();
			formation [Row - 2, needMoveUp [j]] = new Soldier ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Soldier {

	public int soldierID;
	public GameObject soldierObject;
	public int atk, hp, turn;
	public soldierType type;

	public enum soldierType{
		undefined,
		weapon,
		soldier,
		enemy
	}

	public Soldier() {
		soldierID = 0;
		type = soldierType.undefined;
	}

	public void Clone(Soldier s){
		soldierID = s.soldierID;
		soldierObject = s.soldierObject;
		atk = s.atk;
		hp = s.hp;
		turn = s.turn;
		type = s.type;
	}
}
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

	public string itemName;
	public Texture2D itemIcon;
	public Sprite itemSprite;
	public string itemDescription;
	public Vector3 itemScale;

	public Item(string name) {
		itemName = name;
	}

	public Item() {
	}
}

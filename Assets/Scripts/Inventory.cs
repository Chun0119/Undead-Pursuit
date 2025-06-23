using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item> ();
	public List<Item> slots = new List<Item> ();
	public bool showInventory;
	public Item selectedToUse = new Item ();
	private itemDatabase database;
	public List<Item> selected = new List<Item> ();
	private bool showToolTip;
	private string toolTip;
	private Vector3 scale;
	public GameObject block;
	public bool createBlock;
	public Texture2D close;

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("1F Living Room Can Access", 1);
		PlayerPrefs.SetInt ("1F Game Room Can Access", 1);
		PlayerPrefs.SetInt ("1F Master Study Room Can Access", 1);
		PlayerPrefs.SetInt ("GF Girl Study Room Can Access", 1);
		DontDestroyOnLoad(this.gameObject);
		for (int i = 0; i < slotsX * slotsY; i++) {
			slots.Add (new Item ());
			inventory.Add (new Item ());
			selected.Add (new Item ());
			selectedToUse = new Item ();
		}
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<itemDatabase> ();
	}

	void Update(){
		if (createBlock) {
			GameObject newObject = Instantiate (block, new Vector2 (0f, 0f), Quaternion.identity);
			newObject.transform.position = new Vector3 (0f, 0f, -9.5f);
			newObject.name = "Block";
			createBlock = false;
		} else {
			if (showInventory == false) {
				if (GameObject.Find ("Block")) {
					Destroy (GameObject.Find ("Block"));
				}
			}
		}
	}

	public void OnGUI(){
		GUI.skin = skin;
		scale.x = Screen.width / 952f;
		scale.y = Screen.height / 536f;
		scale.z = 1f;
		Matrix4x4 svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
		toolTip = "";
		if (showInventory) {
			DrawInventory ();
			DrawSelected ();
			if (GUI.Button(new Rect(730, 420, 100, 40), "Combine")){
				CombineInventory ();
			}
			if (GUI.Button (new Rect (730, 460, 100, 40), "Use")) {
				UseInventory ();
			}
			if (GUI.Button (new Rect (850, 25, 50, 50), close)) {
				showInventory = false;
			}
		}
		if (toolTip == "") {
			showToolTip = false;
		}
		if (showToolTip) {
			GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 100, 50), toolTip, skin.GetStyle("ToolTip"));
		}
		GUI.matrix = svMat;
	}

	public void DrawInventory(){
		Event currentEvent = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++) {
			for (int x = 0; x < slotsX; x++) {
				Vector2 slotPosition = new Vector2 (x * 80 + 200, y * 80 + 30);
				Vector2 slotSize = new Vector2 (70, 70);
				Rect slotRect = new Rect (slotPosition, slotSize);
				GUI.Box (slotRect, "", skin.GetStyle("Slot"));
				slots [i] = inventory [i];
				if (slots [i].itemName != null) {
					Vector2 itemPosition = new Vector2 (x * 80 + 207, y * 80 + 37);
					Vector2 itemSize = new Vector2 (55, 55);
					Rect itemRect = new Rect (itemPosition, itemSize);
					Item item = slots [i];
					GUI.DrawTexture (itemRect, slots [i].itemIcon, ScaleMode.ScaleToFit);
					if (slotRect.Contains(Event.current.mousePosition)){
						showToolTip = true;
						toolTip = CreateToolTip (slots [i]);
						if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown) {
							Selected(slots[i]);
						}
					}
				}
				i++;
			}
		}
	}

	string CreateToolTip(Item item){
		toolTip = item.itemDescription;
		return toolTip;
	}

	public void DrawSelected(){
		Event currentEvent = Event.current;
		int i = 0;
		for (int y = 0; y < 4; y++) {
			for (int x = 0; x < 2; x++) {
				Vector2 selectedPosition = new Vector2 (x * 80 + 700, y * 80 + 100);
				Vector2 selectedSize = new Vector2 (70, 70);
				Rect selectedRect = new Rect (selectedPosition, selectedSize);
				GUI.Box (selectedRect, "", skin.GetStyle ("Slot"));
				if (selected [i].itemName != null) {
					Vector2 itemPosition = new Vector2 (x * 80 + 707, y * 80 + 107);
					Vector2 itemSize = new Vector2 (55, 55);
					Rect itemRect = new Rect (itemPosition, itemSize);
					GUI.DrawTexture (itemRect, selected [i].itemIcon, ScaleMode.ScaleToFit);
					if (selectedRect.Contains(Event.current.mousePosition)){
						if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown) {
							selected[i] = new Item();
						}
						showToolTip = true;
						toolTip = CreateToolTip (selected [i]);
					}
				}
				i++;
			}
		}
	}

	public void AddItem(string name){
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemName == null) {
				for (int j = 0; j < database.items.Count; j++) {
					if (database.items[j].itemName == name) {
						inventory [i] = database.items[j];
						PlayerPrefs.SetInt (inventory[i].itemName, 1);
					}
				}
				break;
			}
		}
	}

	public void RemoveItem(string name) {
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemName == name) {
				if (inventory [i] == selectedToUse) {
					selectedToUse = new Item ();
					GameObject newObject = GameObject.Find ("Selected Object");
					newObject.GetComponent<SpriteRenderer>().sprite = null;
					newObject.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				inventory [i] = new Item ();
			}
		}
	}

	void Selected(Item item){
		if (!SelectedContains (item.itemName)) {
			for (int i = 0; i < selected.Count; i++) {
				if (selected [i].itemName == null) {
					selected [i] = item;
					break;
				}
			}
		}
	}

	bool SelectedContains(string name){
		for (int i = 0; i < selected.Count; i++) {
			if (selected [i].itemName == name) {
				return true;
			}
		}
		return false;
	}

	void CombineInventory (){
		if (SelectedContains("Battery") && SelectedContains("Battery 2") && SelectedContains("Torch without Battery")){
			RemoveItem("Battery");
			RemoveItem("Battery 2");
			RemoveItem("Torch without Battery");
			AddItem("Torch with Battery");
		}
		if (SelectedContains ("Paper without Password") && SelectedContains ("Pen with UV Light")) {
			RemoveItem ("Paper without Password");
			RemoveItem ("Pen with UV Light");
			AddItem ("Paper with Password");
		}
		if (SelectedContains ("Photo Piece") &&
		    SelectedContains ("Photo Piece 2") &&
		    SelectedContains ("Photo Piece 3") &&
		    SelectedContains ("Photo Piece 4") &&
		    SelectedContains ("Photo Piece 5") &&
		    SelectedContains ("Photo Piece 6") &&
		    SelectedContains ("Glue")) {
			RemoveItem ("Photo Piece");
			RemoveItem ("Photo Piece 2");
			RemoveItem ("Photo Piece 3");
			RemoveItem ("Photo Piece 4");
			RemoveItem ("Photo Piece 5");
			RemoveItem ("Photo Piece 6");
			AddItem ("Photo Piece 1-6");
		}
		if (SelectedContains ("Photo Piece 1-6") && SelectedContains ("Photo Piece 7") && SelectedContains ("Glue")) {
			RemoveItem ("Photo Piece 1-6");
			RemoveItem ("Photo Piece 7");
			AddItem ("Full Photo");
		}
		for (int i = 0; i < selected.Count; i++) {
			selected [i] = new Item ();
		}
	}

	void UseInventory(){
		for (int i = 0; i < selected.Count; i++) {
			if (selected[i].itemName != null) {
				selectedToUse = selected [i];
				break;
			}
		}
		GameObject newObject = GameObject.Find ("Selected Object");
		newObject.GetComponent<SpriteRenderer>().sprite = selectedToUse.itemSprite;
		newObject.transform.localScale = selectedToUse.itemScale;
		showInventory = false;
	}
}

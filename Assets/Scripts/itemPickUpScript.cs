using UnityEngine;
using UnityEngine.UI;

public class itemPickUpScript : MonoBehaviour {

	public AudioClip sound;
	public string name;

	private Inventory inventory;

	public void Start(){
		CheckingAvailability ();
	}

	public void CheckingAvailability(){
		if (PlayerPrefs.HasKey (name)) {
			Destroy (gameObject);
		}
	}

	public void OnMouseDown() {
		AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, 0));
		if (name != "Master Diary" && name != "Girl Diary" && name != "Tutorial Book") {
			inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
			inventory.AddItem (name);
		}
		Destroy (gameObject);
	}
}

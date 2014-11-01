using UnityEngine;
using System.Collections;

public class Button_GoToHouse : MonoBehaviour {

	Map map;
	GameManager manager;
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		map = gameObject.GetComponentInParent<Map> ();
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {
		map.NextMap ();
		manager.canMakeAction = false;
		manager.currentMaxPlayerText = 5;
		manager.currentMaxOtherText = 4;
	}
	
}

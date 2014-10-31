using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Objectives : MonoBehaviour {

	public List<string> stringsToShow;
	public GUISkin MyAwesomeStyle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		GUI.Label (new Rect(6*Screen.width/8 - 10, 130, Screen.width/8f,50), "Objectives : ",MyAwesomeStyle.customStyles[0]);
		for (int i =0; i<stringsToShow.Count; i++) {
			GUI.Label (new Rect(6*Screen.width/8, 160 + 22*i, Screen.width/8f,50), stringsToShow[i],MyAwesomeStyle.customStyles[1]);

		}
	}

	public void RemoveObjective(string objText) {
		stringsToShow.Remove (objText);
	}
	public void AddObjective(string objText) {
		stringsToShow.Add (objText);
	}
}

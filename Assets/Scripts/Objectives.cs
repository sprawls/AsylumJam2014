using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Objectives : MonoBehaviour {

	public List<string> stringsToShow;
	public List<string> currentlyShowing;
	public GUISkin MyAwesomeStyle;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		if(currentlyShowing.Count > 0) {
			GUI.Label (new Rect(6*Screen.width/8 - 10, 130, Screen.width/8f,50), "Objectives : ",MyAwesomeStyle.customStyles[0]);
			for (int i =0; i<currentlyShowing.Count; i++) {
				GUI.Label (new Rect(6*Screen.width/8, 160 + 22*i, Screen.width/8f,50), currentlyShowing[i],MyAwesomeStyle.customStyles[1]);
			}
		}
	}

	public void RemoveObjective(string objText) {
		stringsToShow.Remove (objText);
		StartAnimation ();
	}
	public void AddObjective(string objText) {
		stringsToShow.Add (objText);
		StartAnimation ();
	}

	public void StartAnimation(){
		StopAllCoroutines ();
		StartCoroutine (AnimateText ());
	}

	IEnumerator AnimateText(){
		float timeBetweenShow = 0.2f;
		currentlyShowing.Clear ();
		yield return new WaitForSeconds(timeBetweenShow);
		currentlyShowing.Add ("");
		yield return new WaitForSeconds(timeBetweenShow);
		currentlyShowing.Clear ();
		for(int i = 0; i<stringsToShow.Count; i++) {
			currentlyShowing.Add (stringsToShow[i]);
			yield return new WaitForSeconds(timeBetweenShow);
		}
	}

}

using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public GUISkin MyAwesomeStyle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(Application.loadedLevel == 0){
			if(GUI.Button (new Rect(0.47f*Screen.width, 0.685f*Screen.height, 90,50), "Play",MyAwesomeStyle.customStyles[4])) {
				Application.LoadLevel(1);
			}
		} else {
			if(GUI.Button (new Rect(0.47f*Screen.width, 0.785f*Screen.height, 90,50), "Menu",MyAwesomeStyle.customStyles[4])) {
				Application.LoadLevel(0);
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionsMenu : MonoBehaviour {

	public GUISkin MyAwesomeStyle;
	public List<string> stringsToShow;
	public Interactable currentInteractable;
	GameManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(stringsToShow.Count > 0) {
			GUI.Label (new Rect(5*Screen.width/8 - 60, 130, Screen.width/8f,50), "Actions : ",MyAwesomeStyle.customStyles[0]);
			for (int i =0; i<stringsToShow.Count; i++) {
				if(GUI.Button (new Rect(5*Screen.width/8 -30, 160 + 22*i, Screen.width/8f,22), stringsToShow[i],MyAwesomeStyle.customStyles[3])) {
					ReturnAction(i);
				}
			}
		}
	}

	void ReturnAction(int index) {
		stringsToShow.Clear ();
		currentInteractable.DoAction(index);
		currentInteractable = null;
	}

	public void SetNewMenu(Interactable interactable) {
		stringsToShow.Clear ();
		currentInteractable = interactable;
		if(interactable.isSearchable) stringsToShow.Add ("- Search");
		if(interactable.isScanable) stringsToShow.Add ("- Scan");
		if(interactable.isOpenable) stringsToShow.Add ("- Open");
		if(interactable.isBreakable) stringsToShow.Add ("- Break");
		if(interactable.isUploadable) stringsToShow.Add ("- Upload");
		if(interactable.canPhone911) stringsToShow.Add ("- Call Police");
		if(interactable.canPhone911 && interactable.canPhoneNum && manager.found_phoneNumber) stringsToShow.Add ("- Call Found Number");
		if(interactable.isSecretPassage && manager.called_phoneNumber) stringsToShow.Add ("- Investigate");
	}

	public void ClearMenu(){
		stringsToShow.Clear ();
		currentInteractable = null;
	}
}

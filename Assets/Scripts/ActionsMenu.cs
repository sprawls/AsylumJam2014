using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionsMenu : MonoBehaviour {

	public GUISkin MyAwesomeStyle;
	public List<string> stringsToShow;
	public List<string> currentlyShowing;
	public Interactable currentInteractable;
	GameManager manager;

	//Audio
	AudioSource audioSource;
	AudioClip action;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> ();
		action = (AudioClip)Resources.Load ("confirm_1");
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(currentlyShowing.Count > 0) {
			GUI.Label (new Rect(5*Screen.width/8 - 60, 130, Screen.width/8f,50), "Actions : ",MyAwesomeStyle.customStyles[0]);
			for (int i =0; i<currentlyShowing.Count; i++) {
				if(GUI.Button (new Rect(5*Screen.width/8 -30, 160 + 22*i, Screen.width/8f,22), currentlyShowing[i],MyAwesomeStyle.customStyles[3])) {
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

		StartAnimation ();
		//sounds
		if(stringsToShow.Count > 0) audioSource.PlayOneShot(action);
	}

	public void ClearMenu(){
		currentInteractable = null;
		stringsToShow.Clear ();
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
		if(stringsToShow.Count > 0) currentlyShowing.Add ("");
		yield return new WaitForSeconds(timeBetweenShow);
		currentlyShowing.Clear ();
		for(int i = 0; i<stringsToShow.Count; i++) {
			currentlyShowing.Add (stringsToShow[i]);
			yield return new WaitForSeconds(timeBetweenShow);
		}
	}
	
}

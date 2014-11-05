using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayDialog : MonoBehaviour {

	public GameManager manager;
	public List<string> StringsToShow;
	public List<Vector2> Display;
	public bool skipAutomaticly = false;
	public float charDelay = 0.08f; //Delay between characters appeareance
	
	string currentText;
	GUIText GuiText;
	
	
	//int currentString = 0; //Current String of the list
	//int currentChar = 0; //Current Char of the String
	bool StringShown = false; //is the current string finished showing
	bool wantToSkip = false; //If true, skip text if possible
	bool canClickSkip = true; //if false cant click (delay to avoid double clicks!)
	public bool isInactive = true;
	public bool NeedToSwitch = false;

	//audio
	AudioSource audioSource;
	AudioClip keyPress;
	bool hasPlayedSound = false; //bool used not to repeat sound too fast when skipping
	
	void Awake() {
		GuiText = gameObject.GetComponent<GUIText> ();
		manager = gameObject.GetComponentInParent<GameManager> ();
	}
	
	void Start () {
		audioSource = (AudioSource)gameObject.AddComponent<AudioSource> ();
		keyPress = (AudioClip)Resources.Load ("keyPress");
		if(manager == null) StartText ();
	}

	void Update() {
		if(NeedToSwitch == true && isInactive == false) {
			NeedToSwitch = false;
			manager.SwitchDialog();
		}
	}

	void LateUpdate () {
		if(Input.GetMouseButtonDown(0) && canClickSkip == true){
			StartCoroutine(ClickCooldown());
			wantToSkip = true;
		}
	}

	public void StartText(){
		isInactive = false;
		StringShown = false;

		StartCoroutine (ShowText ());
	}
	
	IEnumerator ShowText(){
		for(int i=0; i<StringsToShow.Count; i++){
			StringShown = false;
			currentText = ""; //Empty String
			object[] parms = new object[1]{StringsToShow[i]};
			StartCoroutine ("ShowString",parms);
			while(StringShown==false) {
				yield return null;
			}
		}
		NeedToSwitch = true;
	}
	
	IEnumerator ShowString(object[] parms){
		string myString = (string) parms [0];
		wantToSkip = false;
		for(int i=0; i< myString.Length; i++) {
			currentText += myString[i];
			GuiText.text = currentText;
			//PlaySound
			if(myString[i] != ' ' && hasPlayedSound == false) {
				audioSource.volume = Random.Range (0.6f,0.7f);
				audioSource.pitch = Random.Range (0.85f,1.15f);
				audioSource.PlayOneShot(keyPress);
				StartCoroutine (CheckSound());
			}
			
			if(wantToSkip == false) yield return new WaitForSeconds(charDelay); //if want to skip, dont do delay
			else yield return null;
		}
		wantToSkip = false;
		
		//After is shown 
		if(skipAutomaticly == true) {
			yield return new WaitForSeconds (1.0f);
		} else {
			while(true) {
				if(Input.GetMouseButtonDown(0) && canClickSkip == true) {
					StartCoroutine(ClickCooldown());
					break;
				}
				yield return null;
			}
		}
		StringShown = true;

	}
	
	IEnumerator ClickCooldown(){
		canClickSkip = false;
		yield return new WaitForSeconds(0.1f);
		canClickSkip = true;
	}

	IEnumerator CheckSound(){
		hasPlayedSound = true;
		yield return new WaitForSeconds(0.035f);
		hasPlayedSound = false;
	}

	
}

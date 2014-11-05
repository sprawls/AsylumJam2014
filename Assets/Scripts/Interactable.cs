 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {


	//Colors
	private Color defColor = new Color(1,1,1,1);
	private Color UsedColor = new Color(0.85f,0.85f,0.85f,0.6f);
	private bool isUsed = false;

	//Actions
	public bool isSearchable = false;
	public bool isScanable = false;
	public bool isOpenable = false;
	public bool isBreakable = false;
	public bool isUploadable = false;
	public bool canPhone911 = false;
	public bool canPhoneNum = false;
	public bool isSecretPassage = false;
	//Results
	public bool hasResult = false;
	public bool hasScanResult = false;
	public bool hasOpenResult = false;
	public bool hasSpecialOpenResult = false;
	//Changes
	public bool afterSearch_break = false;
	public bool afterSearch_open = false;
	public bool afterSearch_scan = false;
	public int afterSearch_bonus = -1; // -1:none, 0:password, 1:key, 2:safe, 3: find phone

	//Audio
	AudioSource myAudio;
	public AudioClip BreakAudioClip;
	AudioClip confirm_1;
	AudioClip confirm_2;
	AudioClip search_fail;
	AudioClip sliding;
	AudioClip dialtone;
	AudioClip dialtone_police;

	bool isWaiting = false;
	bool isPoping = false;

	ActionsMenu menu;
	GameManager manager;
	SpriteRenderer[] mySprites;
	//Prefabs 
	public GameObject ProgressBarPrefab;
	public GameObject ProgressResultPrefab;
	public GameObject PhoneIconPrefab;
	public GameObject SafeIconPrefab;
	public GameObject PcIconPrefab;
	public GameObject PhoneRingingPrefab;

	// Use this for initialization
	void Start () {
		mySprites = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		myAudio = gameObject.AddComponent<AudioSource>();
		menu = GameObject.FindGameObjectWithTag ("ActionMenu").GetComponent<ActionsMenu> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		confirm_1 = (AudioClip)Resources.Load ("confirm_1");
		confirm_2 = (AudioClip)Resources.Load ("confirm_2");
		search_fail = (AudioClip)Resources.Load ("search_fail");
		sliding = (AudioClip)Resources.Load ("sliding");
		dialtone = (AudioClip)Resources.Load ("dialtone");
		dialtone_police = (AudioClip)Resources.Load ("dialtone_police");
		StartCoroutine (CheckColorLater ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CreateResult(string message){
		TextMesh t= ((GameObject)Instantiate (ProgressResultPrefab, Vector3.zero, Quaternion.identity)).GetComponentInChildren<TextMesh> ();
		t.text = message;
	}
	void CreateResult2(string message){ //For additional message
		TextMesh t= ((GameObject)Instantiate (ProgressResultPrefab, new Vector3(0,-0.5f,0), Quaternion.identity)).GetComponentInChildren<TextMesh> ();
		t.text = message;
	}

	void OnMouseUp() {
		if(manager.canMakeAction == true) {
			menu.SetNewMenu (this);
			if(isPoping==false && isUsed == false) {
				for(int i=0; i< mySprites.Length; i++){
					StartCoroutine(Pop(mySprites[i]));
				}
			}
			//tuto
			if(isOpenable == true) manager.has_clicked_open_door = true;
		}

	}

	IEnumerator Pop(SpriteRenderer mySprite) {
		isPoping = true;
		Vector3 startScale = mySprite.transform.localScale;
		Vector3 endScale = new Vector3 (startScale.x + 0.2f, startScale.y + 0.2f, startScale.z + 0.2f);
		for(float i=0; i < 1f; i+= Time.deltaTime/0.10f) {
			mySprite.transform.localScale = Vector3.Lerp(startScale,endScale,i);
			yield return null;
		}
		for(float i=0; i < 1f; i+= Time.deltaTime/0.05f) {
			mySprite.transform.localScale = Vector3.Lerp(endScale,startScale,i);
			yield return null;
		}
		isPoping = false;
	}

	void CheckColor(){
		if(isSearchable == false &&
		   isScanable == false &&
		   isOpenable == false &&
		   isBreakable == false &&
		   isUploadable == false &&
		   canPhone911 == false &&
		   canPhoneNum == false &&
		   isSecretPassage == false )
		{
			for(int i=0; i< mySprites.Length; i++){
				mySprites[i].color = UsedColor;
			}
			isUsed = true;
		} else {
			for(int i=0; i< mySprites.Length; i++){
				mySprites[i].color = defColor;
			}
			isUsed = false;
		}
	}

	int AdjustIndex(int actionToDo){
		int amtToAdd = actionToDo;
		if(isSearchable == false) amtToAdd++;
		if(amtToAdd > 0 && isScanable == false) amtToAdd++;
		if(amtToAdd > 1 && isOpenable == false) amtToAdd++;
		if(amtToAdd > 2 && isBreakable == false) amtToAdd++;
		if(amtToAdd > 3 && isUploadable == false) amtToAdd++;
		if(amtToAdd > 4 && canPhone911 == false) amtToAdd++;
		if(amtToAdd > 5 && (canPhoneNum == false || canPhone911==false || manager.found_phoneNumber== false)) amtToAdd++;
		if(amtToAdd > 6 && isSecretPassage == false) amtToAdd++;

		return (amtToAdd);
	}

	public void DoAction(int actionToDo){
		manager.canMakeAction = false;
		actionToDo = AdjustIndex (actionToDo);
		if(actionToDo == 0) Search ();
		else if(actionToDo == 1) Scan ();
		else if(actionToDo == 2) Open ();
		else if(actionToDo == 3) Break ();
		else if(actionToDo == 4) Upload ();
		else if(actionToDo == 5) Phone911();
		else if(actionToDo == 6) Phone();
		else if(actionToDo == 7) OpenPassage ();
	}

	void Search(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine(Search (4f));
	}
	void Scan(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine(Scan (8f));
	}
	void Open(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine(Open (2f));
	}
	void Break(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine(Break (2f));
	}
	void Upload(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine(Upload (8f));
	}
	void Phone911(){
		myAudio.PlayOneShot (confirm_1);
		if(manager.called_police == false) {
			StartCoroutine (Phone911 (4f));
		} else {
			CreateResult("I'm not calling the police again");
			manager.canMakeAction = true;
		}
	}
	void Phone(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine (Phone (4f));
	}
	void OpenPassage(){
		myAudio.PlayOneShot (confirm_1);
		StartCoroutine (OpenPassage (3f));
	}

	IEnumerator Search(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 

		//then , once wait is over
		manager.canMakeAction = true;
		isSearchable = false;
		if(hasResult) {
			myAudio.PlayOneShot (confirm_2);
			if(afterSearch_break) {
				isBreakable = true;
				CreateResult("This window lead to the living room");
				CreateResult2("You could break it");
			}
			if(afterSearch_open) {
				isOpenable = true;
			}
			if(afterSearch_scan) {
				isScanable = true;
				CreateResult("You find a laptop in a drawer");
				Instantiate(PcIconPrefab, Vector3.zero, Quaternion.identity);

			}
			if(afterSearch_bonus == 0) {
				CreateResult("You find a piece of paper in a jacket.");
				CreateResult2("There is a password written on it.");
				manager.found_password = true;
			}
			if(afterSearch_bonus == 1) {
				CreateResult("You find key under the sink.");
				CreateResult2("This will surely be usefull.");
				manager.found_key = true;
				manager.AddObjective("- Find a use for the key");
			}
			if(afterSearch_bonus == 2) {
				CreateResult("There is a safe hidden here.");
				CreateResult2("It needs a key to be opened.");
				isOpenable = true;
				hasSpecialOpenResult = true;
				manager.AddObjective("- Find key to open the safe");
				Instantiate(SafeIconPrefab, Vector3.zero, Quaternion.identity);
			}
			if(afterSearch_bonus == 3) {
				CreateResult("There is a phone here.");
				CreateResult2("I can use it if needed.");
				canPhone911 = true;
				Instantiate(PhoneIconPrefab, Vector3.zero, Quaternion.identity);

			}
		} else {
			myAudio.PlayOneShot(search_fail);
			CreateResult("Nothing Here");
		}
		CheckColor ();
	}

	IEnumerator Scan(float time) {
		if(manager.found_password) {
			manager.found_password = false;
			CreateResult2("Used Password");
		} else {
			isWaiting = false;
			StartCoroutine(Wait (time,isWaiting));
			while(isWaiting==false) {
				yield return null;
			} 
		}
		//then , once wait is over
		manager.canMakeAction = true;
		isScanable = false;
		if(hasScanResult){
			CreateResult("Data has been found");
			manager.ShowGoodPcDialog();
			isUploadable = true;
		} else {
			CreateResult("No data in there");
			manager.ShowBadPCDialog();
		}
		CheckColor ();
	}

	IEnumerator Open(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.canMakeAction = true;
		if(hasOpenResult){
			CreateResult("You open the door");
			manager.map.NextMap();
		} else if(hasSpecialOpenResult){
			if(manager.found_key) {
				isOpenable = false;
				CreateResult("You use the key to open the safe");
				CreateResult2("There is a phone number and money inside.");
				manager.RemoveObjective("- Find a use for the key");
				manager.RemoveObjective("- Find key to open the safe");
				manager.AddObjective ("- Find a use for the phone number");
				canPhoneNum = true;
				manager.found_phoneNumber = true;
			} else {
				CreateResult("It needs a key");

			}

		} else {
			isOpenable = false;
			manager.has_tried_opening_front_door = true;
			CreateResult("The door is Locked");
		}
		CheckColor ();
	}

	IEnumerator OpenPassage(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		myAudio.PlayOneShot(sliding);
		manager.RemoveObjective ("- Investigate Sound");
		CreateResult("The ringing is coming from behind");
		CreateResult2("You discover an hidden room");
		manager.map.NextMap ();
		manager.canMakeAction = true;
		CheckColor ();
	}

	IEnumerator Break(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.map.NextMap ();
		myAudio.PlayOneShot (BreakAudioClip);
		manager.CreateTimer ();
		manager.currentMaxPlayerText = 7;
		manager.currentMaxOtherText = 6;
		CheckColor ();
	}

	IEnumerator Upload(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.canMakeAction = false;
		CreateResult("Data has been sent");
		manager.StopClock ();
		manager.ShowUploadDialog ();
		CheckColor ();
	}

	IEnumerator Phone911(float time) {
		myAudio.PlayOneShot (dialtone_police);
		isWaiting = false;
		StartCoroutine (Wait (time, isWaiting));
		while(isWaiting == false) {
			yield return null;
		}
		//then , once wait is over
		manager.ShowPoliceDialog ();
		manager.called_police = true;
		CheckColor ();
	}

	IEnumerator Phone(float time) {
		myAudio.PlayOneShot (dialtone);
		isWaiting = false;
		StartCoroutine (Wait (time, isWaiting));
		while(isWaiting == false) {
			yield return null;
		}
		//then , once wait is over
		manager.canMakeAction = true;
		manager.called_phoneNumber = true;
		CreateResult("It's ringing...");
		GameObject phoneRing = (GameObject)Instantiate (PhoneRingingPrefab, Vector3.zero, Quaternion.identity);
		phoneRing.transform.parent = manager.map.transform;
		canPhone911 = false;
		canPhoneNum = false;
		manager.RemoveObjective ("- Find a use for the phone number");
		manager.AddObjective ("- Investigate Sound");
		CheckColor ();
	}

	IEnumerator Wait(float time, bool  ToMakeTrue){
		ProgressBar timerBar = ((GameObject)Instantiate (ProgressBarPrefab, transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity)).GetComponentInChildren<ProgressBar>();
		timerBar.StartTimer (time);
		yield return new WaitForSeconds (time);
		isWaiting = true;
		menu.ClearMenu ();
	}

	IEnumerator CheckColorLater(){
		yield return new WaitForSeconds(2f);
		CheckColor();
	}
}

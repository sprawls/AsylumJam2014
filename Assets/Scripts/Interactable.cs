using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	//Actions
	public bool isSearchable = false;
	public bool isScanable = false;
	public bool isOpenable = false;
	public bool isBreakable = false;
	public bool isUploadable = false;
	//Results
	public bool hasResult = false;
	public bool hasScanResult = false;
	public bool hasOpenResult = false;
	//Changes
	public bool afterSearch_break = false;
	public bool afterSearch_open = false;
	public bool afterSearch_scan = false;
	//Audio
	public AudioClip BreakAudioClip;

	bool isWaiting = false;

	ActionsMenu menu;
	GameManager manager;
	public GameObject ProgressBarPrefab;
	public GameObject ProgressResultPrefab;

	// Use this for initialization
	void Start () {
		menu = GameObject.FindGameObjectWithTag ("ActionMenu").GetComponent<ActionsMenu> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CreateResult(string message){
		TextMesh t= ((GameObject)Instantiate (ProgressResultPrefab, Vector3.zero, Quaternion.identity)).GetComponentInChildren<TextMesh> ();
		t.text = message;
	}

	void OnMouseUp() {
		if(manager.canMakeAction == true) {
			menu.SetNewMenu (this);
		}

	}

	int AdjustIndex(int actionToDo){
		int amtToAdd = actionToDo;
		if(isSearchable == false) amtToAdd++;
		if(amtToAdd > 0 && isScanable == false) amtToAdd++;
		if(amtToAdd > 1 && isOpenable == false) amtToAdd++;
		if(amtToAdd > 2 && isBreakable == false) amtToAdd++;
		if(amtToAdd > 3 && isUploadable == false) amtToAdd++;

		return (amtToAdd);
	}

	public void DoAction(int actionToDo){
		manager.canMakeAction = false;
		Debug.Log ("Before: " + actionToDo);
		actionToDo = AdjustIndex (actionToDo);
		Debug.Log ("After: " + actionToDo);
		if(actionToDo == 0) Search ();
		else if(actionToDo == 1) Scan ();
		else if(actionToDo == 2) Open ();
		else if(actionToDo == 3) Break ();
		else if(actionToDo == 4) Upload ();
	}

	void Search(){
		StartCoroutine(Search (4f));
	}
	void Scan(){
		StartCoroutine(Scan (8f));
	}
	void Open(){
		StartCoroutine(Open (2f));
	}
	void Break(){
		StartCoroutine(Break (2f));
	}
	void Upload(){
		StartCoroutine(Upload (8f));
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
			if(afterSearch_break) {
				isBreakable = true;
				CreateResult("This window lead to the living room");
			}
			if(afterSearch_open) {
				isOpenable = true;
			}
			if(afterSearch_scan) {
				isScanable = true;
				CreateResult("You find a laptop in a drawer");
			}
		} else {
			CreateResult("Nothing Here");
		}
	}

	IEnumerator Scan(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.canMakeAction = true;
		isScanable = false;
		if(hasScanResult){
			CreateResult("Data has been found");
			isUploadable = true;
		} else {
			CreateResult("No data in there");

		}
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
		} else {
			CreateResult("The door is Locked");
		}
	}

	IEnumerator Break(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.map.NextMap ();
		AudioSource myAudio = gameObject.AddComponent<AudioSource>();
		myAudio.PlayOneShot (BreakAudioClip);
		manager.CreateTimer ();
		manager.currentMaxPlayerText = 7;
		manager.currentMaxOtherText = 6;

	}

	IEnumerator Upload(float time) {
		isWaiting = false;
		StartCoroutine(Wait (time,isWaiting));
		while(isWaiting==false) {
			yield return null;
		} 
		//then , once wait is over
		manager.canMakeAction = false;
	}

	IEnumerator Wait(float time, bool  ToMakeTrue){
		ProgressBar timerBar = ((GameObject)Instantiate (ProgressBarPrefab, transform.position + new Vector3 (0, 1, 0), Quaternion.identity)).GetComponentInChildren<ProgressBar>();
		timerBar.StartTimer (time);
		yield return new WaitForSeconds (time);
		isWaiting = true;
	}

}

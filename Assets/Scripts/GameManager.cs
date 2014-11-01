using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	Timer timer;
	public GameObject Timer_Obj;

	Objectives objectives;
	[HideInInspector] public Map map;
	public string playerName = "Patrick";

	public bool canMakeAction = true;

	//unlocks
	bool unlockedMap = false;
	bool unlockedObjective = false;
	bool unlockedTimer = false;
	[HideInInspector] public bool found_password = false;
	[HideInInspector] public bool found_key = false;
	[HideInInspector] public bool found_phoneNumber = false;
	[HideInInspector] public bool called_phoneNumber = false;


	//text Lock
	public DisplayDialog playerDialog;
	public DisplayDialog otherDialog;

	public int currentPlayerText = 0;
	public int currentOtherText = 0;
	public int currentMaxPlayerText = 3;
	public int currentMaxOtherText = 2;
	public int currentUnlock = 0;

	//Texts
	public List<List<string>> listsOfPlayerText;
	public List<List<string>> listsOfOtherText;



	// Use this for initialization
	void Start () {
		map = gameObject.GetComponentInChildren<Map> ();
		CreateListOfPlayerText ();
		CreateListOfOtherText ();
		//map.NextMap(); //DEBUG

	}
	
	// Update is called once per frame
	void Update () {
		if(unlockedObjective == true) {
			if(playerDialog.isInactive && otherDialog.isInactive) {
				if(LookForPlayerDialog()) {
					playerDialog.StringsToShow = listsOfPlayerText[currentPlayerText];
					playerDialog.StartText();
				} else if(LookForOtherDialog()){
					otherDialog.StringsToShow = listsOfOtherText[currentOtherText];
					otherDialog.StartText();
				}
			}
		}
	}

	public void StopClock(){
		timer.StopTimer ();
	}

	public void UnlockObjective(Objectives newObj) {
		objectives = newObj;
		unlockedObjective = true;
	}
	public void CreateTimer(){
		timer = ((GameObject)Instantiate (Timer_Obj, Vector3.zero, Quaternion.identity)).GetComponent<Timer> ();
	}

	bool LookForPlayerDialog(){
		if(currentPlayerText <= currentMaxPlayerText) return true;
		else return false;
	}
	bool LookForOtherDialog(){
		if(currentOtherText <= currentMaxOtherText) return true;
		else return false;
	}

	public void SwitchDialog(){
		if(playerDialog.isInactive == false) {
			currentPlayerText++;
			CheckForEvents();
			playerDialog.isInactive = true;
			if(currentOtherText <= currentMaxOtherText) {
				otherDialog.StringsToShow = listsOfOtherText[currentOtherText];
				otherDialog.StartText();
			}
		} 
		else if(otherDialog.isInactive == false) {
			currentOtherText++;
			CheckForEvents();
			otherDialog.isInactive = true;
			if(currentPlayerText <= currentMaxPlayerText) {
				playerDialog.StringsToShow = listsOfPlayerText[currentPlayerText];
				playerDialog.StartText();
			}
		}

	}

	void CheckForEvents(){
		if(currentUnlock == 0 && currentPlayerText > 3) {
			currentUnlock++;
			objectives.AddObjective("- Go to the given address");
			map.NextMap();
		}
		if(currentUnlock == 1 && currentPlayerText > 5) {
			currentUnlock++;
			canMakeAction = true;
			objectives.AddObjective("- Break in the house.");
			objectives.RemoveObjective("- Go to the given address");
		}
		if(currentUnlock == 2 && currentPlayerText > 7) {
			currentUnlock++;
			canMakeAction = true;
			objectives.AddObjective("- Do not let time run out");
			objectives.AddObjective("- Find the data");
			objectives.RemoveObjective("- Break in the house.");
		}
	}
	public void AddObjective(string nObjective){
		objectives.AddObjective(nObjective);
	}
	public void RemoveObjective(string nObjective){
		objectives.RemoveObjective(nObjective);
	}

	public void ShowBadPCDialog(){
		currentPlayerText = 7;
		currentOtherText = 6;
		currentMaxPlayerText = 9;
		currentOtherText = 7;
	}



	//Fuck database, hardcode all text lines
	void CreateListOfPlayerText(){
		listsOfPlayerText = new List<List<string>> ();
		List<string> player0 = new List<string> ();
		player0.Add ("                         Hello.");
		player0.Add ("                       . . . ");
		player0.Add ("Alex ? is that you ?");
		listsOfPlayerText.Add (player0);
		List<string> player1 = new List<string> ();
		player1.Add ("Who are you ?");
		listsOfPlayerText.Add (player1);
		List<string> player2 = new List<string> ();
		player2.Add (" ... ");
		player2.Add (" yes. ");
		listsOfPlayerText.Add (player2);
		List<string> player3 = new List<string> ();
		player3.Add ("Alright");
		listsOfPlayerText.Add (player3);
		//Dialog 2
		List<string> player4 = new List<string> ();
		player4.Add ("I'm there. What Am I supposed to do now ?");
		listsOfPlayerText.Add (player4);
		List<string> player5 = new List<string> ();
		player5.Add ("What kind of data ?");
		listsOfPlayerText.Add (player5);
		//Dialog 3
		List<string> player6 = new List<string> ();
		listsOfPlayerText.Add (player6);
		List<string> player7 = new List<string> ();
		player7.Add ("I'm in.");
		listsOfPlayerText.Add (player7);
		//Dialog Found bad pc
		List<string> player8 = new List<string> ();
		listsOfPlayerText.Add (player8);
		List<string> player9 = new List<string> ();
		player9.Add ("    . . .");
		player9.Add ("Alright.");
		listsOfPlayerText.Add (player9);

	}

	void CreateListOfOtherText(){
		listsOfOtherText = new List<List<string>> ();
		List<string> other0 = new List<string> ();
		other0.Add ("Hello " + playerName + ".");
		listsOfOtherText.Add (other0);
		List<string> other1 = new List<string> ();
		other1.Add ("That's not important. Now Listen to me.");
		other1.Add ("The person you hear behind me is indeed Alex.");
		other1.Add ("Just listen to me, and everyone will be alright.");
		other1.Add ("Do everything as I say.");
		other1.Add ("First, Do not hang up.");
		other1.Add ("Second, Do not try to contact anyone.");
		other1.Add ("Third, Do not question me.");
		other1.Add ("You understand ?");
		listsOfOtherText.Add (other1);
		List<string> other2 = new List<string> ();
		other2.Add ("I've sent you an address, go there. Fast.");
		listsOfOtherText.Add (other2);
		//Dialog 2
		List<string> other3 = new List<string> ();
		other3.Add ("It's simple. Break in and search for a computer.");
		other3.Add ("Once you find a computer, plug your phone in so I can scan it for the data.");
		listsOfOtherText.Add (other3);
		List<string> other4 = new List<string> ();
		other4.Add ("I thought I said no question.");
		other4.Add ("No one's home. You can Proceed. Stay Subtle.");
		listsOfOtherText.Add (other4);
		//Dialog 3
		List<string> other5 = new List<string> ();
		other5.Add ("What just happened ? The alarm went off !");
		listsOfOtherText.Add (other5);
		List<string> other6 = new List<string> ();
		other6.Add ("No shit. The police is coming now.");
		other6.Add ("You have 10 minutes until they're there. You better hurry.");
		other6.Add ("If you don't get the data until then. You're not seeing your friend in one piece again.");
		listsOfOtherText.Add (other6);
		//Dialog Found bad pc
		List<string> other7 = new List<string> ();
		other7.Add ("Wronc PC.  Keep looking. Now.");
		listsOfOtherText.Add (other7);
	}

}

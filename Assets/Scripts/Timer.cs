using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GUISkin MyAwesomeStyle;
	public int startingTime = 600; //time at start
	int timeLeft; //in seconds

	void Start () {
		timeLeft = startingTime;
		StartTimer ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		GUI.Label (new Rect(6*Screen.width/8 - 30, 30, Screen.width/8f,50), "Time Left   " + timeLeft/60 + " : " + timeLeft%60,MyAwesomeStyle.customStyles[2]);

	}

	public void StartTimer() {
		StartCoroutine (CalculateTime());
	}

	public void StopTimer() {
		StopAllCoroutines ();
	}

	IEnumerator CalculateTime(){
		while(timeLeft >= 0) {
			timeLeft--;
			yield return new WaitForSeconds(1f);
		}
	}
}

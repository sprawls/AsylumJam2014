using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GUISkin MyAwesomeStyle;
	public int startingTime = 340; //time at start
	public int timeLeft; //in seconds

	//Sounds
	AudioSource audioSource;
	AudioClip tick;


	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> ();
		audioSource.volume = 0.7f;
		tick = (AudioClip)Resources.Load ("tick_1");

		timeLeft = startingTime;
		StartTimer ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		int minutes = timeLeft / 60;
		int seconds = timeLeft % 60;
		if(seconds<10) GUI.Label (new Rect(6*Screen.width/8 - 30, 30, Screen.width/8f,50), "Time Left   " + timeLeft/60 + " : " + "0" + timeLeft%60,MyAwesomeStyle.customStyles[2]);
		else GUI.Label (new Rect(6*Screen.width/8 - 30, 30, Screen.width/8f,50), "Time Left   " + timeLeft/60 + " : " + timeLeft%60,MyAwesomeStyle.customStyles[2]);
		
	}

	public void StartTimer() {
		StartCoroutine (CalculateTime());
	}

	public void StopTimer() {
		StopAllCoroutines ();
	}

	IEnumerator CalculateTime(){
		while(timeLeft > 0) {
			audioSource.pitch = Random.Range (0.95f,1.05f);
			audioSource.PlayOneShot(tick);
			timeLeft--;
			yield return new WaitForSeconds(1f);
		}
	}
}

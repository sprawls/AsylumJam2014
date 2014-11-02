using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GUISkin MyAwesomeStyle;
	public int startingTime = 600; //time at start
	int timeLeft; //in seconds

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
			audioSource.pitch = Random.Range (0.95f,1.05f);
			audioSource.PlayOneShot(tick);
			timeLeft--;
			yield return new WaitForSeconds(1f);
		}
	}
}

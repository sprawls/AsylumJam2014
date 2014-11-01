using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CycleThroughSounds : MonoBehaviour {

	public bool isOn = false;
	public List<AudioClip> breathSounds;

	private bool isCurrentlyPlaying = false;
	private AudioSource audioSource;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.volume = 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
		if(isOn) {
			if(isCurrentlyPlaying == false) {
				int index = Random.Range(0,breathSounds.Count);
				audioSource.clip = (breathSounds[index]);
				audioSource.Play ();
				StartCoroutine(WaitForSounds(breathSounds[index].length));
				
			}
		} else {
			audioSource.Stop();
		}
	}

	IEnumerator WaitForSounds(float lenght){
		isCurrentlyPlaying = true;
		yield return new WaitForSeconds (lenght);
		isCurrentlyPlaying = false;
	}
}

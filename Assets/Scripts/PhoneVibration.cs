using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhoneVibration : MonoBehaviour {

	public List<Sprite> spriteList; //0 : nothing, 1 : incoming call, 2 : in call
	public bool hasPressed = false;
	public bool isVibrating = false;
	private bool VibrationCoroutineOn = false;
	SpriteRenderer spriteRenderer;

	//coroutine animation var
	Vector3 startingRot, maxRot, minRot;
	public float angleDiff = 2.5f; //amount of angle between min and rot
	public float speed = 1f;




	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		startingRot = transform.localRotation.eulerAngles;
		maxRot = startingRot + new Vector3 (0, 0, angleDiff / 2f);
		minRot = startingRot + new Vector3 (0, 0, -angleDiff / 2f);
	}
	
	// Update is called once per frame
	void Update () {
		//Start Coroutine if not started
		if(isVibrating && !VibrationCoroutineOn) {
			StartVibration();
		}

	}

	void OnMouseUp() {
		if(isVibrating) {
			PressPhone();
		}
	}

	void PressPhone(){
		StopAllCoroutines ();
		VibrationCoroutineOn = false;
		isVibrating = false;
		hasPressed = true;
		audio.Stop ();
		ChangeSprite (2);
	}

	public void StartVibration(){
		StartVibrationCoroutine ();
		VibrationCoroutineOn = true;
		isVibrating = true;
		hasPressed = false;
		ChangeSprite (1);
	}

	public void StartVibrationCoroutine() {
		StartCoroutine (Vibrate ());
		audio.Play ();
	}

	public void ChangeSprite(int index){
		if(spriteList.Count-1 >= index) spriteRenderer.sprite = spriteList[index];
	}
	

	IEnumerator Vibrate(){
		bool isClockwise = true;
		while(true){
			isClockwise = !isClockwise;
			float step = 0f; //raw step
			float rate = (1f/0.02f)*speed; //amount to add to raw step

			Vector3 startRot = transform.localRotation.eulerAngles;
			Vector3 targetRotation;
			if(isClockwise) targetRotation = maxRot;
			else targetRotation = minRot;

			while(step <1f){
				step += Time.deltaTime * rate; 
				transform.localRotation = Quaternion.Euler (Vector3.Lerp(startRot,targetRotation,rate));
				yield return null;
			}
		}
	}



}

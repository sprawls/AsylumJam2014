using UnityEngine;
using System.Collections;

public class tutorial_arrow : MonoBehaviour {

	GameManager manager;

	float animTime = 1f;
	float xChange = 0.3f;
	float startingPosition;
	float endingPosition;

	bool hasChangedPos = false;

	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		startingPosition = transform.localPosition.x;
		endingPosition = startingPosition + xChange;
		StartCoroutine (LoopAnimate ());
		ChangePos1 ();
	}
	

	void Update () {
		if(manager.has_clicked_open_door == true && hasChangedPos == false) {
			hasChangedPos = true;
			ChangePos2();
		}
		if(manager.has_tried_opening_front_door == true && hasChangedPos == true) {
			Destroy (transform.parent.gameObject);
		}
		if(manager.unlockedTimer == true) {
			Destroy (transform.parent.gameObject);
		}

	}

	void ChangePos1() {
		transform.parent.position = new Vector3 (-1.380683f, -4.361235f,-9f);
	}
	void ChangePos2() {
		transform.parent.position = new Vector3 (4.010766f, -0.3639095f,-9f);
	}


	IEnumerator LoopAnimate() {
		while(true){
			StartCoroutine (Animate(startingPosition,endingPosition));
			yield return new WaitForSeconds(animTime);
			StartCoroutine (Animate(endingPosition,startingPosition));
			yield return new WaitForSeconds(animTime);
		}
	}

	IEnumerator Animate(float startPos, float endPos){
		//FIRST ANIMATIOM
		float step = 0f; //raw step
		float rate = 1f/animTime; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step
		
			transform.localPosition = new Vector3 (Mathf.Lerp(startPos, endPos, (smoothStep)),transform.localPosition.y,transform.localPosition.z); //lerp position
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		if(step > 1.0) transform.localPosition = new Vector3 (Mathf.Lerp(startPos, endPos, (1f - lastStep)),transform.localPosition.y,transform.localPosition.z);
	}





}

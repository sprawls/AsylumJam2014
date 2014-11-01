using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartTimer(float time){
		StartCoroutine (Progress (time));
	}

	IEnumerator Progress(float time){
		Vector3 start = new Vector3 (0, 1, 1);
		Vector3 end = new Vector3 (1, 1, 1);
		for(float i=0; i<1; i+= Time.deltaTime/time){
			transform.localScale = Vector3.Lerp (start,end,i);
			yield return null;
		}
		Destroy (transform.parent.gameObject);
	}


}

using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	static KongregateAPI myAPI = new KongAPI();

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}

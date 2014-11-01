using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public List<GameObject> mapList;
	public List<GameObject> objList;
	public int currentMap = -1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextMap(){
		SpriteRenderer[] oldSprites = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		for(int i=0; i<oldSprites.Length; i++) {
			StartCoroutine(FadeOut (oldSprites[i]));
		}
		currentMap++;
		GameObject newMap = (GameObject)Instantiate (mapList [currentMap], transform.position, Quaternion.identity);
		newMap.transform.parent = transform;
		SpriteRenderer[] newSprites = newMap.GetComponentsInChildren<SpriteRenderer> ();
		for(int i=0; i<newSprites.Length; i++) {
			StartCoroutine(FadeIn (newSprites[i]));
		}
		if(currentMap == 2) {
			GameObject newObj1 = (GameObject) Instantiate (objList[0], Vector3.zero,Quaternion.identity);
			SpriteRenderer[] newSprites1 = newObj1.GetComponentsInChildren<SpriteRenderer> ();
			for(int i=0; i<newSprites1.Length; i++) {
				StartCoroutine(FadeIn (newSprites1[i]));
			}
		}
		if(currentMap == 3) {
			GameObject newObj2 = (GameObject)Instantiate (objList[1], Vector3.zero,Quaternion.identity);
			SpriteRenderer[] newSprites2 = newObj2.GetComponentsInChildren<SpriteRenderer> ();
			for(int i=0; i<newSprites2.Length; i++) {
				StartCoroutine(FadeIn (newSprites2[i]));
			}
		}

	}

	IEnumerator FadeOut(SpriteRenderer sprite){
		Color startColor = sprite.color;
		Color endColor = new Color (startColor.r, startColor.g, startColor.b, 0);
		for(float i = 0; i < 1; i += Time.deltaTime/1f) {
			sprite.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		sprite.color = endColor;
		Destroy (sprite.gameObject);
	}

	IEnumerator FadeIn(SpriteRenderer sprite){
		Color endColor = new Color(sprite.color.r,sprite.color.g,sprite.color.b,1);
		Color startColor = new Color (endColor.r, endColor.g, endColor.b, 0);
		for(float i = 0; i < 1; i += Time.deltaTime/1f) {
			sprite.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		sprite.color = endColor;
	}

}

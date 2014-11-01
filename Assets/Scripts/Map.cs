using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public List<GameObject> mapList;
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

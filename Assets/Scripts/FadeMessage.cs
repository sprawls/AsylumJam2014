using UnityEngine;
using System.Collections;

public class FadeMessage : MonoBehaviour {

	float time = 1.5f;
	SpriteRenderer sprite;
	TextMesh mesh;

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
		mesh = gameObject.GetComponentInChildren<TextMesh> ();

		StartCoroutine (FadeIn(sprite));
		StartCoroutine (FadeIn(mesh));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator FadeOut(SpriteRenderer sprite){
		Color startColor = sprite.color;
		Color endColor = new Color (startColor.r, startColor.g, startColor.b, 0);
		for(float i = 0; i < 1f; i += Time.deltaTime/time) {
			sprite.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		sprite.color = endColor;
		Destroy (gameObject);
	}

	IEnumerator FadeOut(TextMesh mesh){
		Color startColor = mesh.color;
		Color endColor = new Color (startColor.r, startColor.g, startColor.b, 0);
		for(float i = 0; i < 1; i += Time.deltaTime/time) {
			mesh.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		sprite.color = endColor;
	}
	
	IEnumerator FadeIn(SpriteRenderer sprite){
		Color endColor = new Color(sprite.color.r,sprite.color.g,sprite.color.b,0.5f);
		Color startColor = new Color (endColor.r, endColor.g, endColor.b, 0);
		for(float i = 0; i < 1f; i += Time.deltaTime/time) {
			sprite.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		yield return new WaitForSeconds (time);
		StartCoroutine (FadeOut (sprite));
	}

	IEnumerator FadeIn(TextMesh mesh){
		Color endColor = new Color(mesh.color.r,mesh.color.g,mesh.color.b,1);
		Color startColor = new Color (endColor.r, endColor.g, endColor.b, 0);
		for(float i = 0; i < 1; i += Time.deltaTime/time) {
			mesh.color = Color.Lerp(startColor,endColor,i);
			yield return null;
		}
		yield return new WaitForSeconds (time);
		StartCoroutine (FadeOut (mesh));
	}

}

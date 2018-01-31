using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	SpriteRenderer sr;
	public KeyCode key;
	bool active = false;
	GameObject note;
	Color old;

	void Awake(){
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		old = sr.color;
	}
	
	// Update is called once per framerome
	void Update ()
	{
		if(Input.GetKeyDown(key)){
			StartCoroutine(Pressed());
		}
		if (active && Input.GetKeyDown(key))
		{
			DestroyObject(note);
		} 
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		active = true;
			
		if (col.gameObject.tag == "Note")
		{
			note = col.gameObject;
		}
	}

	void OnTriggerExit2D( Collider2D col)
	{
		active=false;
	}

	IEnumerator Pressed(){
		sr.color = new Color(0,0,0);
		yield return new WaitForSeconds(0.1f);
		sr.color = old;
	}
	
}

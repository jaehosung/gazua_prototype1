﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	SpriteRenderer sr;
	public KeyCode key;
	bool active = false;
	GameObject note;

	void Awake(){
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (active && Input.GetKeyDown (key))
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
	 
}

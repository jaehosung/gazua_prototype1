using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	SpriteRenderer sr;
	public KeyCode key;
	bool active = false;
	GameObject note;
	Color old;
	public bool createNode;
	public GameObject n;

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
		if(createNode && Input.GetKeyDown(key)){
			if(Input.GetKeyDown(key)){
				Instantiate(n,transform.position,Quaternion.identity);  
			}
		}else{
			if(Input.GetKeyDown(key)){
				StartCoroutine(Pressed());
			}
			if (active && Input.GetKeyDown(key))
			{
				DestroyObject(note);
				AddScore();
			} 
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
	
	void AddScore(){
		PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+100);
	}

	IEnumerator Pressed(){
		sr.color = new Color(0,0,0);
		yield return new WaitForSeconds(0.1f);
		sr.color = old;
	}
	
}

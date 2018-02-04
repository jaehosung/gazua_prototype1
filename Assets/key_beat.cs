using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_beat : MonoBehaviour {

	public AudioSource somesound;
	public KeyCode key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	

	void crateBeatInput(int beats){
		Vector2 velocity = new Vector2(0.0f,-0.4f);
		Vector3 size = new Vector3(0.3f,0.3f,0.3f);
		for(int i = 0; i < beats; i++){
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.AddComponent<Rigidbody>();
			cube.transform.position = new Vector2(i, -3);
			cube.transform.localScale = size;
			cube.GetComponent<Rigidbody>().velocity = velocity;
			cube.GetComponent<Renderer>().material.color = Color.yellow;
			cube.GetComponent<BoxCollider>().enabled = false;
		}
	}
}

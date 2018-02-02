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
		if(Input.GetKeyDown(key)){
			somesound.Play();
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.AddComponent<Rigidbody>();
			cube.transform.position = new Vector3(3, 0, 0);
			cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public AudioSource bgSound;
	public KeyCode key;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key))
		{
			Debug.Log("what");
			bgSound.Stop();
			bgSound.Play();
		}
	}
}

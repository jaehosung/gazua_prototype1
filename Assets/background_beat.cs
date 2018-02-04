using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_beat : MonoBehaviour {


	public AudioSource bgsound;
	public AudioSource beatsound;
	public KeyCode key;

	float bpm = 168;
	int clapperbeat = 4;

	float bgtime;
	float barbeat;
	float bgbeat;
	int prevbeat = -1;
	float session =0;

	Vector2 velocity = new Vector2(0.0f,-0.8f);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(key)){
			beatsound.Stop();
			beatsound.Play();
			userBeatInput(3);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			bgsound.Stop();
			bgsound.Play();
			session = 0;
		}
		// Debug.Log(bgsound.time);
		bgtime = bgsound.time;
        barbeat = (bgtime * bpm /(60*clapperbeat));
        bgbeat = (bgtime * bpm /(60))*2;

		if((int)barbeat > prevbeat){
			prevbeat = (int)barbeat;
			crateBeatGroup(4);
			session++;
		}
    }

	void crateBeatGroup(int beats){
		Vector2 size = new Vector2(0.3f,0.3f);
		for(int i = 0; i < beats; i++){
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.AddComponent<Rigidbody>();
			cube.transform.position = new Vector2(i, 0);
			cube.transform.localScale = size;
			cube.GetComponent<Rigidbody>().velocity = velocity;
			cube.GetComponent<BoxCollider>().enabled = false;
		}
	}

	void userBeatInput(int beats){
		Vector3 size = new Vector3(0.3f,0.3f,0.3f);
		// TODO : Note at a time
		if(Mathf.Abs(bgbeat-Mathf.Round(bgbeat))<0.45){
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.transform.position = new Vector2((float)Mathf.Round(bgbeat)%8/2, 0);
				cube.transform.localScale = size;
				cube.GetComponent<Rigidbody>().velocity = velocity;
				cube.GetComponent<Renderer>().material.color = Color.yellow;
				cube.GetComponent<BoxCollider>().enabled = false;
				Debug.Log(bgbeat%4);
		}
	}
}


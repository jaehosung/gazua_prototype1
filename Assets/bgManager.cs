using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgManager : MonoBehaviour {
	public AudioSource bgSound;
	public AudioSource beatSound;
	public KeyCode key;
	public KeyCode key2;

	private float bpm = 168;
	//beat per second
	private float bps;
	private int beatPerBar = 4;

	private float bgTime;
	private float bgBar;
	private float bgBeat;
	private int prevTemp = 0;

	private int sessionPerBeat = 2;

	//judgement Max : 0.5 / Min : 0.1
	private float judgement = 0.49f;

	private bool firstNodeFlag = false;
	
	public GameObject blueTile;
	public GameObject blackTile;
	public GameObject indicator;
	private GameObject[] beatIndicators = new GameObject[8];
	// Use this for initialization
	void Awake(){
		for(int i = 0; i<8; i++){
			beatIndicators[i] = Instantiate(blueTile, new Vector3(0,0), Quaternion.identity);
			if(i%2 == 0){
				beatIndicators[i].transform.position = new Vector2(0.2f + 0.9f *  i/2f, 0f);
			}else{
				beatIndicators[i].transform.position = new Vector2(0.6f + 0.9f * (i-1)/2f,0f);
			}
			beatIndicators[i].SetActive(false);
		}
		Instantiate(indicator);
	}
	void Start ()
	{
		bps = bpm / 60;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(barEnd()){
			for(int i = 0; i < 8; i ++){
				if(i==0&&firstNodeFlag!=false){
					firstNodeFlag = false;
				}else{
					beatIndicators[i].SetActive(false);
				}
			}
		}
		MusicStart();

		if(Input.GetKeyDown(KeyCode.Space)){
			beatSound.Stop();
			beatSound.Play();
			correctBeatMaker(sessionPerBeat);
		}
	}
	void correctBeatMaker(int inputPerBeat){
		float inputBeat = bgBeat*2;
		if(Mathf.Abs(inputBeat - Mathf.Round(inputBeat))<judgement){
			int beat = (int)Mathf.Round(inputBeat)%8;
			//Fix Bug Removing the first node
			if(beat == 0){
				firstNodeFlag = true;
			}
			beatIndicators[beat].SetActive(true);
		}
	}

	void indicatorMove(){
		indicator.GetComponent<Rigidbody2D>().velocity = new Vector2(1,0);
	}
	void MusicStart()
	{
		if (Input.GetKeyDown(key))
		{
			bgSound.Stop();
			bgSound.Play();
		}
		
		//Calculating bgTime and bgBar and bgBeat
		bgTime = bgSound.time;
		bgBar = bgTime * bps / beatPerBar;
		bgBeat = bgTime * bps;

	}
	private float barBeat = 0;
	private int prevBeat = -1;


	bool barEnd(){
		if((int)bgBar != prevBeat){
			prevBeat = (int)bgBar;
			return true;
		}
		return false;
	}
}

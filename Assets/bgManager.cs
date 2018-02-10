using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgManager : MonoBehaviour {
	public AudioSource bgSound;
	public AudioSource beatSound;
	public KeyCode key;
	public KeyCode key2;
	public KeyCode keyA;
	public KeyCode keyB;

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
	private float judgement = 0.499f;

	private bool firstNodeFlag = false;

	public GameObject blueTile;
	public GameObject blackTile;
	public GameObject indicator;
	public GameObject blackNode;
	private GameObject[] beatIndicators = new GameObject[8];
	private GameObject[] beatIndicatorsNew = new GameObject[8];
	private GameObject[] beatShoots = new GameObject[8];
	private GameObject[] movingIndicator = new GameObject[3];
	private GameObject[, ] downBeatArray = new GameObject[8, 14];
	private GameObject[, ] downBeatArrayBlack = new GameObject[8, 14];
	private List<int>[] downBoolArray = new List<int>[8];

	// Use this for initialization
	private bool[] beatChecker = new bool[8];

	void Awake () {
		bps = bpm / 60;
		beatIndicatorsInit();
		movingIndicatorInit();
		downBoolArrayInit();
		DownBeatMaking ();

	}
	void Start () { }

	bool lastBeatCheckFlag = false;
	void Update () {
		MusicStart ();

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (keyA) || Input.GetKeyDown (keyB)) {
			beatSound.Stop ();
			beatSound.Play ();
			CorrectBeatMaker (sessionPerBeat);
			//CorrectBeatMakerNew (sessionPerBeat);
		}
		if (BarEnd ()) {
			DownArrayGenerating (1);

			//Beat Generating or Beat Moving
			IndicatorMove ();

			beatIndicatorRenderer();
		}
		DownBeatUpdate ();
	}


	/*========== Functions ========== */
	void beatIndicatorRenderer(){
		for (int i = 0; i < 8; i++) {
			beatIndicators[i].SetActive (false);
			for (int j = 0; j < beatChecker.Length; j++) {
				beatChecker[j] = false;
			}
		}
		if (lastBeatCheckFlag) {
			beatIndicators[0].SetActive (true);
			beatChecker[0] = true;
			lastBeatCheckFlag = false;
		}
		
	}


	private int[,] sampleBeatArray = new int[, ] { { 1, 0, 0, 0, 0, 0, 0, 0 }, { 1, 0, 1, 0, 0, 0, 0, 0 }, { 1, 0, 0, 0, 1, 0, 0, 0 }, { 1, 0, 1, 0, 1, 0, 0, 0 }, { 1, 0, 1, 0, 1, 0, 1, 0 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 1, 0, 1, 0 }, { 1, 1, 1, 0, 1, 0, 0, 0 }, { 1, 1, 1, 0, 1, 1, 1, 0 }, { 1, 1, 1, 0, 1, 1, 0, 0 }, { 0, 1, 0, 1, 0, 1, 0, 1 } };
	void DownBeatUpdate () {
		int[] temp = new int[15];
		for (int i = 0; i < 8; i++) {
			//downBoolArray[i].CopyTo(temp,0);
			for (int j = 0; j < downBoolArray[i].Count; j++) {
				//Debug.Log(downBoolArray[i].Count + " " + temp[j]);
				if (downBoolArray[i][j] == 1) {
					downBeatArray[i, j].SetActive (true);
					downBeatArrayBlack[i, j].SetActive (false);
				} else {
					downBeatArray[i, j].SetActive (false);
					downBeatArrayBlack[i, j].SetActive (true);
				}
			}
		}
	}
	void DownArrayGenerating (int level) {
		//TODO_Add code for level
		int index = (int) Random.Range (0f, 6f);
		Debug.Log (index);
		for (int i = 0; i < 8; i++) {
			int _temp = (sampleBeatArray[index,i])%2;
			if (downBoolArray[i].Count != 0 || _temp != 0)
				downBoolArray[i].Insert(0,_temp);
		}
	}
	void DownBeatMaking () {
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 14; j++) {
				downBeatArray[i, j] = Instantiate (blackTile, new Vector3 (0, 0), Quaternion.identity);
				downBeatArrayBlack[i, j] = Instantiate (blackNode, new Vector3 (0, 0), Quaternion.identity);


				/*  ===== For queue ===== */
				if(i%2 == 0){
					downBeatArray[i,j].transform.position = new Vector2(0.2f + 0.9f *  i/2f,5.6f-j*0.4f);
					downBeatArrayBlack[i,j].transform.position = new Vector2(0.2f + 0.9f *  i/2f,5.6f-j*0.4f);
				}else{
					downBeatArray[i,j].transform.position = new Vector2(0.6f + 0.9f * (i-1)/2f,5.6f-j*0.4f);
					downBeatArrayBlack[i,j].transform.position = new Vector2(0.6f + 0.9f * (i-1)/2f,5.6f-j*0.4f);
				}
				downBeatArray[i, j].SetActive (false);
				downBeatArrayBlack[i, j].SetActive (false);
			}
		}
	}
	void CorrectBeatMaker (int inputPerBeat) {
		float inputBeat = bgBeat * 2;
		if (Mathf.Abs (inputBeat - Mathf.Round (inputBeat)) < judgement) {
			int beat = (int) Mathf.Round (inputBeat) % 8;
			if (beat == 0 && inputBeat < Mathf.Round (inputBeat)) {
				lastBeatCheckFlag = true;
			} else {
				beatIndicators[beat].SetActive (true);
				beatChecker[beat] = true;
			}
		}
	}

	void IndicatorMove () {
		movingIndicator[0].GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.9f * (float) bps, 0);
		movingIndicator[1].GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.9f * (float) bps, 0);
		movingIndicator[2].GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.9f * (float) bps, 0);

		if (bgBar > 0)
			movingIndicator[(int) bgBar % 3].transform.position = new Vector2 (0.2f - 0.9f * 4f, 2.8f);
	}
	void IndicatorShot () {
		//instantiate
		for (int i = 0; i < 8; i++) {
			if (beatChecker[i]) {
				beatShoots[i] = Instantiate (blueTile, new Vector3 (0, 0), Quaternion.identity);
				if (i % 2 == 0) {
					beatShoots[i].transform.position = new Vector2 (0.2f + 0.9f * i / 2f, 0.4f);
				} else {
					beatShoots[i].transform.position = new Vector2 (0.6f + 0.9f * (i - 1) / 2f, 0.4f);
				}
				//beatShoots[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0,0.9f*(float)bps);
				beatShoots[i].GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0.1f * (float) bps);
			}
		}
	}
	/* ===== Awake Functions ===== */
	
	void beatIndicatorsInit(){
		for (int i = 0; i < 8; i++) {
			beatIndicators[i] = Instantiate (blueTile, new Vector3 (0, 0), Quaternion.identity);
			if (i % 2 == 0) {
				beatIndicators[i].transform.position = new Vector2 (0.2f + 0.9f * i / 2f, 0f);
			} else {
				beatIndicators[i].transform.position = new Vector2 (0.6f + 0.9f * (i - 1) / 2f, 0f);
			}
			beatIndicators[i].SetActive (false);
		}
	}


	void movingIndicatorInit(){
		movingIndicator[0] = Instantiate (indicator, new Vector3 (0.2f - 0.9f * 4f, 2.8f), Quaternion.identity);
		movingIndicator[1] = Instantiate (indicator, new Vector3 (0.2f + 0.9f * 0f, 2.8f), Quaternion.identity);
		movingIndicator[2] = Instantiate (indicator, new Vector3 (0.2f + 0.9f * 4f, 2.8f), Quaternion.identity);
	}
	void downBoolArrayInit(){
		for (int i = 0; i < 8; i++) {
			downBoolArray[i] = new List<int> ();
		}
	}
	void MusicStart () {
		if (Input.GetKeyDown (key)) {
			bgSound.Stop ();
			bgSound.Play ();
		}

		//Calculating bgTime and bgBar and bgBeat
		bgTime = bgSound.time;
		bgBar = bgTime * bps / beatPerBar;
		bgBeat = bgTime * bps;

	}
	private float barBeat = 0;
	private int prevBeat = -1;
	bool BarEnd () {
		if ((int) bgBar != prevBeat) {
			prevBeat = (int) bgBar;
			return true;
		}
		return false;
	}
	bool BarEnd_lastBeat () {
		int lastBeat = (int) (bgBar + (2 / bps) / 8);
		if (lastBeat != prevBeat) {
			prevBeat = lastBeat;
			return true;
		}
		return false;
	}
}
using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public GetTouch getTouchScript;
	//private int comboCounter;
	private AudioSource comboSound;
	public AudioClip Fail;
	public AudioClip Do;
	public AudioClip Re;
	public AudioClip Mi;
	public AudioClip Fa;
	public AudioClip Sol;
	public AudioClip La;
	public AudioClip Si;
	private bool failPlayed;
	private bool doPlayed;
	private bool rePlayed;
	private bool miPlayed;
	private bool faPlayed;
	private bool solPlayed;
	private bool laPlayed;
	private bool siPlayed;

	// Use this for initialization
	void Awake () {
		//comboSound = GetComponent<AudioSource> ();
		comboSound = GetComponent<AudioSource>();
		comboSound.Stop ();
		doPlayed = false;
		rePlayed = false;
		miPlayed = false;
		faPlayed = false;
		solPlayed = false;
		laPlayed = false;
		siPlayed = false;
		failPlayed = false;
	}
	
	// Update is called once per frame
	public void Update() {
		comboSound = GetComponent<AudioSource>();

		//comboCounter = getTouchScript.comboCounter;
		//Debug.Log (getTouchScript.comboCounter);
		if (getTouchScript.comboCounter == 0 ) {
			doPlayed = false;
			rePlayed = false;
			miPlayed = false;
			faPlayed = false;
			solPlayed = false;
			laPlayed = false;
			siPlayed = false;
			failPlayed = false;
		}
		if (getTouchScript.comboCounter == 0 && !failPlayed && (Input.touchCount > 0 || Input.GetMouseButtonDown(0))) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot (Fail);
			failPlayed = true;
		}
		if (getTouchScript.comboCounter == 1 && !doPlayed) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot(Do);
			doPlayed = true;
		}
		if (getTouchScript.comboCounter == 2 && !rePlayed) {
			//comboSound.clip = Re;
			comboSound.PlayOneShot(Re);
			rePlayed = true;
		}
		if (getTouchScript.comboCounter == 3 && !miPlayed) {
			//comboSound.clip = Mi;
			comboSound.PlayOneShot(Mi);
			miPlayed = true;
		}
		if (getTouchScript.comboCounter == 4 && !faPlayed) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot (Fa);
			faPlayed = true;
		}
		if (getTouchScript.comboCounter == 5 && !solPlayed) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot (Sol);
			solPlayed = true;
		}
		if (getTouchScript.comboCounter == 6 && !laPlayed) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot (La);
			laPlayed = true;
		}
		if (getTouchScript.comboCounter > 7 && !siPlayed) {
			//comboSound.clip = Do;
			comboSound.PlayOneShot (Si);
			siPlayed = true;
		}
	}
}




/*
comboCounter = getTouchScript.comboCounter;
Debug.Log (comboCounter);
if (comboCounter == 1) {
	comboSound.clip = Do;
	comboSound.Play ();
}
if (comboCounter == 2) {
	comboSound.clip = Re;
	comboSound.Play ();
}
if (comboCounter == 3) {
	comboSound.clip = Mi;
	comboSound.Play ();
}
comboSound.Play ();
*/
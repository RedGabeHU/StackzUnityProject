using UnityEngine;
using System.Collections;

public class EndPanel : MonoBehaviour {

	public GameObject endPanel;
	public GetTouch getTouchScript;

	void Start(){
		endPanel.SetActive (false);
	}

	void Update(){
		if (getTouchScript.GameOver) {
			endPanel.SetActive (true);
		}
	}
}

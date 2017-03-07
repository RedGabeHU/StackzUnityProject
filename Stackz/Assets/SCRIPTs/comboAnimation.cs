using UnityEngine;
using System.Collections;

public class comboAnimation : MonoBehaviour {
	

	public void PlayAnimation () {
		GameObject.FindGameObjectWithTag ("Plane").GetComponent<Renderer> ().enabled = true;
		//print("combo animation");
		GetComponent<Animation> ().Play();

	}
}
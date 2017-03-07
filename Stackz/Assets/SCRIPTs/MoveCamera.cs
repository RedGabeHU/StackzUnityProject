using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	Vector3 camPos;
	float cameraHeight = 15;

	public void LateUpdate () {
		
		camPos = transform.position;
		camPos.y = GameObject.FindGameObjectWithTag ("TopBlock").transform.position.y+cameraHeight;
		transform.position = camPos;
		//Debug.Log (camPos);

	}
}

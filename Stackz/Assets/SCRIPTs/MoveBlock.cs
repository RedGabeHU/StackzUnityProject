using UnityEngine;
using System.Collections;

public class MoveBlock: MonoBehaviour {

	static public float speed = 25f; //sebesseg valtozoja
	public CubePrefab CubeSpawnerScript;

	void FixedUpdate () {
		if(gameObject.tag=="TopBlock"){
			transform.Translate (speed * Time.deltaTime,0,0);
			if (GameObject.FindGameObjectWithTag ("TopBlock").transform.position.x > 40) {
				speed -= (2 * speed);
			}
			if (GameObject.FindGameObjectWithTag ("TopBlock").transform.position.x < -40) {
				speed -= (2 * speed);
			}
			if (GameObject.FindGameObjectWithTag ("TopBlock").transform.position.z > 40) {
				speed -= (2 * speed);
			}
			if (GameObject.FindGameObjectWithTag ("TopBlock").transform.position.z < -40) {
				speed -= (2 * speed);
			}
		}
	}
}

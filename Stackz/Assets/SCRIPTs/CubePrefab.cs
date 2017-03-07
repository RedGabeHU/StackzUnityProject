using UnityEngine;
using System.Collections;

public class CubePrefab : MonoBehaviour {

	public GameObject cubePrefab;
	CubePrefab cubePrefabClone;
	public GetTouch lastBlockPosition;


	float rotateToRight=90;
	float rotateToLeft = 0;
	public float rotate = 0;

	public float spawningHeight = 1;

	float spawnLeftX = -40;
	float spawnLeftZ = 0;
	float spawnX = -40; 

	float spawnRightX = 0;
	float spawnRightZ = 40;
	float spawnZ = 0;

	Vector3 localScale;

	public void Start(){

	}


	public void CubeSpawn(){
		//Spawnol egy uj cube
		//spawnX = lowerBlockPosition.lowerBlock.x - spawnX;
		//spawnZ = lowerBlockPosition.lowerBlock.z - spawnZ;
		if (rotate == rotateToRight) {
			cubePrefabClone = Instantiate (cubePrefab, new Vector3 (GameObject.FindGameObjectWithTag("LowerBlock").transform.position.x, spawningHeight, spawnZ), Quaternion.Euler (0, rotate, 0)) as CubePrefab;
			localScale.x = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.z;
			localScale.y = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.y;
			localScale.z = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.x;
			GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale=localScale;
			//GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Renderer> ().material.color = Color.black;

		} else {
			cubePrefabClone = Instantiate (cubePrefab, new Vector3 (spawnX, spawningHeight, GameObject.FindGameObjectWithTag("LowerBlock").transform.position.z), Quaternion.Euler (0, rotate, 0)) as CubePrefab;
			localScale.x = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.z;
			localScale.y = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.y;
			localScale.z = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.x;
			GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale=localScale;
		}

		//elforgatja a soron kovetkezo elemet es beallitja jobb vagy bal oldali spawnt
		if (rotate == rotateToRight) {
			rotate = rotateToLeft;
			spawnX = spawnLeftX;
			spawnZ = spawnLeftZ;
		}
		else {
			rotate = rotateToRight;
			spawnX = spawnRightX;
			spawnZ = spawnRightZ;
		}

		//novelii a spawn maassagot
		spawningHeight++; //magassag novelese
	}

}

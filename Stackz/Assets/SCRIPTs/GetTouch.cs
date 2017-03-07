using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetTouch : MonoBehaviour {

	public Text scoreText;
	public int scoreCounter;
	public CubePrefab CubeSpawnerScript;
	public Vector3 lowerBlock;
	public Vector3 topBlock;
	public float distance;
	public int comboCounter = 0;
	public const int COMBO_START = 5;
	public float comboBlockGrow=0.5f;
	float cuttedX;
	float cuttedZ;
	Vector3 topBlockLocalScale;
	public Color cubeColor;
	public bool GameOver;
	public Sound soundScript;
	public comboAnimation comboAnimationScript; 
	public float effectScaler;
	public Vector3 effectTempScale;
	public Vector3 effectTempPosition;
	public Vector3 lowerBlockEffectScale;
	public float middle;
	public Vector3 gameOverCamPos = new Vector3(45,100,-45);


	// Use this for initialization
	void Start () {
		GameOver = false;
		CubeSpawnerScript.CubeSpawn ();
		scoreCounter = 0;
		cubeColor.r = 0.30f;
		cubeColor.g = 0.70f;
		cubeColor.b = 0.70f;
		GameObject.FindGameObjectWithTag ("LowerBlock").GetComponent<Renderer> ().material.color = cubeColor;
		GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Renderer> ().material.color = cubeColor;
		GameObject.FindGameObjectWithTag ("Plane").GetComponent<Renderer> ().enabled = false;
		lowerBlockEffectScale = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (GameOver)
			return;
		*/
		if (Input.touchCount > 0 /*|| Input.GetMouseButtonDown(0)*/) { //A MEGJEGYZES KISZEDESEVEL EGERGOMRA IS MEGY!!! teszteli hogy erintjuk e a kepernyot

			//Csinalj dolgokat...
			//GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Renderer> ().material.color = cubeColor;

			if (Input.GetTouch (0).phase == TouchPhase.Began) {//EZT AZ IF METODUST MEGJEGYZESNEK KELL JELOLNI, HOGY EGERREL MUKODJON
				checkDistance ();
				GameObject.FindGameObjectWithTag ("LowerBlock").tag = "Untagged";
				GameObject.FindGameObjectWithTag ("TopBlock").tag = "LowerBlock";
				if (!GameOver) {
					CubeSpawnerScript.CubeSpawn ();
					GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Renderer> ().material.color = cubeColor;
					GameObject.Find ("Main Camera").GetComponent<Camera> ().backgroundColor = cubeColor;
					ColorChanger ();
				}

			}	//IF METODUS VEGE!!!
			//Input.touchCountvarakozz egy kicsit
			//new WaitForSeconds(1);

		}
	
	
		if (GameOver) {
			HighScore ();
			GameObject.FindGameObjectWithTag ("MainCamera").transform.position  = gameOverCamPos;
		}
	
	}

	public void checkDistance(){
		//Debug.Log("distanceCheck");
		lowerBlock = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localPosition;
		topBlock = GameObject.FindGameObjectWithTag ("TopBlock").transform.localPosition;
		//float Distance(Vector3 as, Vector3 b);
		distance = Vector3.Distance (lowerBlock, topBlock);
		//Debug.Log (topBlock.x);
		//Debug.Log (topBlock.z);
		if(distance<=1.1 && distance >=1){
			OsszeIlleszt();
			comboCounter++;

			effectTempPosition = GameObject.FindGameObjectWithTag ("Plane").transform.position;
			++effectTempPosition.y;
			GameObject.FindGameObjectWithTag ("Plane").transform.position = effectTempPosition;
			lowerBlockEffectScale = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale;

			comboAnimationScript.PlayAnimation ();


			MoveBlock.speed += 0.5f;
			//Debug.Log (comboCounter);
			if (comboCounter >= COMBO_START) {
				//if (CubeSpawnerScript.rotate != 0) {
					Vector3 blockGrowerScale = GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale;
					GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale = new Vector3 (blockGrowerScale.x + comboBlockGrow, blockGrowerScale.y, blockGrowerScale.z);
				/*} else {
					Vector3 blockGrowerScale = GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale;
					GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale = new Vector3 (blockGrowerScale.x + comboBlockGrow, blockGrowerScale.y, blockGrowerScale.z);
					}*/
			}
			//GameObject.FindGameObjectWithTag("SoundDo").GetComponent<AudioSource>().Play();
			//soundScript.PlaySound();
			scoreCounter++;
			scoreText.text = scoreCounter.ToString ();
		}else
		{
			if (CubeSpawnerScript.rotate!=0)
			{
				//cut block!
				CutX();
				//make falling block
				if (cuttedX > 0) {
					CreateFallingBlock (
						new Vector3 (
							(topBlock.x > lowerBlock.x)
						? topBlock.x + (cuttedX / 2)
						: topBlock.x - (cuttedX / 2)
						, topBlock.y
						, topBlock.z
						),
						new Vector3 (Mathf.Abs (topBlockLocalScale.x - cuttedX), topBlockLocalScale.y, topBlockLocalScale.z)
					);
				
					//reposition it

					middle = (lowerBlock.x + topBlock.x) / 2;
					GameObject.FindGameObjectWithTag ("TopBlock").transform.position = new Vector3 (middle, topBlock.y, topBlock.z);

				} else {
					

					GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Rigidbody> ().useGravity=true;
					GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Rigidbody> ().isKinematic=false;
				}
				MoveEffectOnX ();
			} else
			{
				//az a baj hogy a masodik elem el van forgatva igy varialni kell az x es z scalelesnel es positionalasnal
				CutZ();
				//make falling block	
				if (cuttedZ > 0) {
					CreateFallingBlock (
						new Vector3 (
							topBlock.x
						, topBlock.y
							, (topBlock.z > lowerBlock.z)
							? topBlock.z + (cuttedZ / 2)
							: topBlock.z - (cuttedZ / 2)
						),
						new Vector3 (topBlockLocalScale.z, topBlockLocalScale.y, Mathf.Abs (topBlockLocalScale.x - cuttedZ))
					);
				
					//Debug.Log ((topBlock.z > 0)
					//reposition it

					middle = (lowerBlock.z + topBlock.z) / 2;
					GameObject.FindGameObjectWithTag ("TopBlock").transform.position = new Vector3 (lowerBlock.x, topBlock.y, middle);

				}else {
					GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Rigidbody> ().useGravity=true;
					GameObject.FindGameObjectWithTag ("TopBlock").GetComponent<Rigidbody> ().isKinematic=false;
				}
				MoveEffectOnZ ();
			}

		}

	}
	public void OsszeIlleszt(){
		//Debug.Log ("jo tavolsag!");
		//Debug.Log (distance);
		GameObject.FindGameObjectWithTag ("TopBlock").transform.position = GameObject.FindGameObjectWithTag ("LowerBlock").transform.position + Vector3.up;
	}

	public void CutX(){
		comboCounter = 0;
		//Debug.Log("rossz tavolsag rotate = 0");
		//Debug.Log (comboCounter);
		//Debug.Log (GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.x - Mathf.Abs(topBlock.x));
		cuttedX = (GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.z - Mathf.Abs (topBlock.x-lowerBlock.x));
		//Debug.Log (cuttedX);
		if (cuttedX > 0) {
			topBlockLocalScale = GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale;
			GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale = new Vector3 (cuttedX, topBlockLocalScale.y, topBlockLocalScale.z);
			scoreCounter++;
			scoreText.text = scoreCounter.ToString ();
		} else {
			GameOver = true;
		}
	}

	public void CutZ(){
		comboCounter = 0;
		//Debug.Log("rossz tavolsag rotate = 90");
		//Debug.Log (comboCounter);	
		cuttedZ = (GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale.z - Mathf.Abs (topBlock.z-lowerBlock.z));
		//Debug.Log (cuttedZ);
		if(cuttedZ>0){
			topBlockLocalScale = GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale;
			GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale = new Vector3 (cuttedZ, topBlockLocalScale.y, topBlockLocalScale.z);
			scoreCounter++;
			scoreText.text = scoreCounter.ToString ();
		} else {
			GameOver = true;
		}
	}

	private void CreateFallingBlock(Vector3 pos, Vector3 scale){
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);
		go.transform.localPosition = pos;
		go.transform.localScale = scale;
		go.AddComponent<Rigidbody> ();
		go.GetComponent<Renderer> ().material.color = cubeColor;
	}

	void ColorChanger(){
		if (cubeColor.r <= 0.30f && cubeColor.b >0.30f) {
			cubeColor.b -= 0.02f;
		} 
		if (cubeColor.b <= 0.30f && cubeColor.r <0.70f) {
			cubeColor.r += 0.02f;
		}
		if (cubeColor.r >= 0.70f && cubeColor.g >0.30f) {
			cubeColor.g -= 0.02f;
		}
		if (cubeColor.g <= 0.30f && cubeColor.b <0.70f){
			cubeColor.b += 0.02f;
		}
		if (cubeColor.b >= 0.70f && cubeColor.r >0.30f){
			cubeColor.r -= 0.02f;
		}
		if (cubeColor.b >= 0.70f && cubeColor.g <0.30f){
			cubeColor.g += 0.02f;
		}

	}

	public void OnButtonClick (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void HighScore()
	{
		if (PlayerPrefs.GetInt ("score") < scoreCounter) {
			PlayerPrefs.SetInt ("score", scoreCounter);
		}	
	}



	void MoveEffectOnX(){
		effectTempPosition = GameObject.FindGameObjectWithTag ("Plane").transform.position;
		effectTempScale = GameObject.FindGameObjectWithTag ("Plane").transform.localScale;

		effectScaler =GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale.x/lowerBlockEffectScale.x ;

		effectTempScale = new Vector3 (effectTempScale.x * effectScaler,
			effectTempScale.y,
			effectTempScale.z
		);
		GameObject.FindGameObjectWithTag ("Plane").transform.localScale = effectTempScale;

		effectTempPosition.x = middle;
		++effectTempPosition.y;
		GameObject.FindGameObjectWithTag ("Plane").transform.position = effectTempPosition;
		print("MoveEffectOnX");
		print(effectScaler);
		print(effectTempScale.x);
		lowerBlockEffectScale = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale;
	}

	void MoveEffectOnZ(){
		effectTempPosition = GameObject.FindGameObjectWithTag ("Plane").transform.position;
		effectTempScale = GameObject.FindGameObjectWithTag ("Plane").transform.localScale;

		effectScaler = GameObject.FindGameObjectWithTag ("TopBlock").transform.localScale.x/lowerBlockEffectScale.x;

		effectTempScale = new Vector3 (effectTempScale.x,
			effectTempScale.y,
			effectTempScale.z * effectScaler
		);
		GameObject.FindGameObjectWithTag ("Plane").transform.localScale = effectTempScale;

		effectTempPosition.z = middle;
		++effectTempPosition.y;
		GameObject.FindGameObjectWithTag ("Plane").transform.position = effectTempPosition;
		print("MoveEffectOnZ");
		print(effectScaler);
		print(effectTempScale.z);
		lowerBlockEffectScale = GameObject.FindGameObjectWithTag ("LowerBlock").transform.localScale;
	}

}
/* Ki kell tolteni a pos es scale ertekeket
 * atnevezem az elforgatott elemeknel
 * az alap fuggveny szerint alkalmazom az ertekeket
 * pl 	lowerPosX = lower.transform.position.x
 * 		lowerPosZ = lower.transform.position.z
 * elforgatottnal:
 * 		lowerPosX = lower.transform.position.z
 * 		lowerPosZ = lower.transform.position.x
 * fuggveny:
 * 		cutX= lowerScale.x - ABSTopX
 */
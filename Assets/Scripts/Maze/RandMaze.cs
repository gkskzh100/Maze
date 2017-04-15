using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandMaze : MonoBehaviour {

	private int randomNum;
	private int[] numConvert = new int[4] {0,0,0,0};
	private int result;
	// private PlayController playController;
	// private SphereCollider sphereCollider;

	// Use this for initialization
	public void Start () {
	// 	sphereCollider = gameObject.AddComponent<SphereCollider>();
	// 	sphereCollider.radius = 10f;
	// 	sphereCollider.isTrigger = true;
	StartCoroutine (countTime());
	}
	// public void coroutineActive() {
	// 	StartCoroutine (countTime());
	// }

	void Update()
	{
		if(Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	IEnumerator countTime() {
		numberSelect();
		// playController = GameObject.Find("Player").GetComponent<PlayController>();
		// if(!playController.triggerBool)
		// 	yield break;
		// if(playController.triggerBool){
			yield return new WaitForSeconds(5); //5초마다 반복
			StartCoroutine (countTime());
		// }
	}

	private void numberSelect() {
		randomNum = UnityEngine.Random.Range(0,16);
		result = Convert.ToInt32(Convert.ToString(randomNum,2));
		int num = result;

		for (int i = 3; i >= 0; i--) {
			numConvert[i] = num % 2;
			num = num / 2;
		}
		mazeCheck();
	}

	private void mazeCheck() {
		switch(numConvert[0]) {
			case 0: 
				var cubeConF = transform.FindChild("Cube1").GetComponent<Cube1Controller>();
				cubeConF.Cube1Check(false);
				break;
			case 1:
				var cubeConT = transform.FindChild("Cube1").GetComponent<Cube1Controller>();
				cubeConT.Cube1Check(true);
				break;
		}
		switch(numConvert[1]) {
			case 0: 
				var cubeConF = transform.FindChild("Cube2").GetComponent<Cube2Controller>();
				cubeConF.Cube2Check(false);
				break;
			case 1:
				var cubeConT = transform.FindChild("Cube2").GetComponent<Cube2Controller>();
				cubeConT.Cube2Check(true);
				break;
		}
		switch(numConvert[2]) {
			case 0: 
				var cubeConF = transform.FindChild("Cube3").GetComponent<Cube3Controller>();
				cubeConF.Cube3Check(false);
				break;
			case 1:
				var cubeConT = transform.FindChild("Cube3").GetComponent<Cube3Controller>();
				cubeConT.Cube3Check(true);
				break;
		}
		switch(numConvert[3]) {
			case 0: 
				var cubeConF = transform.FindChild("Cube4").GetComponent<Cube4Controller>();
				cubeConF.Cube4Check(false);
				break;
			case 1:
				var cubeConT = transform.FindChild("Cube4").GetComponent<Cube4Controller>();
				cubeConT.Cube4Check(true);
				break;
		}
	}
	
}

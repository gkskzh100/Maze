using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1Controller : MonoBehaviour {

	private float moveSpeed = 5.0f;
	public bool cubeCheck;
	
	// Update is called once per frame
	void Update () {
		if (cubeCheck) {
			if(transform.position.y >= -5){
				transform.Translate(0,(moveSpeed * Time.deltaTime * -1), 0);
			}
		} else {
			if (transform.position.y < 5) {
				transform.Translate(0,(moveSpeed * Time.deltaTime), 0);
			}
		}
	}

	public void Cube1Check(bool check) {
		cubeCheck = check;
	}


}

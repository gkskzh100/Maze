using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player") {
			Debug.Log ("player entered");
			GameOver();
		}
	}
	public void GameOver() {
		SceneManager.LoadScene("GameOver");
	}
}

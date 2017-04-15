using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player") {
			GameObject.Find("Player").SendMessage("Finish");
			StartCoroutine(delayTime());
		}
	}
	public void GameOver() {
		SceneManager.LoadScene("GameOver");
	}

	IEnumerator delayTime() {
		yield return new WaitForSeconds(3);
		GameOver();
	}
}

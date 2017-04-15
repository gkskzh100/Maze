using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text timerText;
	private float startTime;
	private bool finished = false;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(finished) return ;

		float t = Time.time - startTime;
		string minutes = ((int) t / 60).ToString("d2");
		string seconds = (t % 60).ToString("f1");
		
		timerText.text = string.Format("{0:00}",minutes) + ":" + string.Format("{0:00}",seconds);
	}

	void Finish() {
		finished = true;
		timerText.color = Color.yellow;
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player") {
			Debug.Log ("player entered");
		}
	}
}

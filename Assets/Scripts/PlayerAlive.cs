using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlive : MonoBehaviour {

	public bool alive = true;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "eyes") {
			other.transform.parent.GetComponent<MonsterController>().checkSight();
		}
	}
}

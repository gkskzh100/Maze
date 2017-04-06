using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGizmo : MonoBehaviour {

	public Color color = Color.red;
	public float radius = 1.0f;
	void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, radius);	
	}
}

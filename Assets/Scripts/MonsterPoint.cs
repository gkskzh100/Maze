using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour {

	public Transform[] points;
	public GameObject monster;
	public float createTime = 5.0f;

	// Use this for initialization
	void Start () {
		points = GameObject.Find("MonsterPoint").GetComponentsInChildren<Transform>();
		StartCoroutine(CreateMonster());
	}
	
	IEnumerator CreateMonster() {
		for(int i = 0; i <5; i++) {
			int index = Random.Range(1,points.Length);
			
			Instantiate(monster,points[index].position,Quaternion.Euler(0,-180,0));

			yield return new WaitForSeconds(createTime);
		}
	}
}
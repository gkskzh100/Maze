using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public Transform[] points;
	public GameObject door;
	public float createTime = 5.0f;
	private int rotation;

	// Use this for initialization
	void Start () {
		points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
		StartCoroutine(CreateDoor());
	}

	IEnumerator CreateDoor() {
		int index = Random.Range(1,points.Length);
		if(points[index].tag == "Z_Gizmo") {
			rotation = -180;
		} else if (points[index].tag == "X_Gizmo") {
			rotation = -270;
		}
		Instantiate (door, points[index].position, Quaternion.Euler(0,rotation,0));
		yield return new WaitForSeconds(createTime); // 5초있다가 밑에줄 실행
		
		Destroy(GameObject.Find("door(Clone)"));
		StartCoroutine (CreateDoor());
	}
}

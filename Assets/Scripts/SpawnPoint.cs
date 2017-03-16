using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public Transform[] points;
	public GameObject door;
	public float createTime = 60.0f;
	private int rotation;


	// Use this for initialization
	void Start () {
		points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
		StartCoroutine(CreateDoor());
	}

	IEnumerator CreateDoor() {
		while(true) {
			int index = Random.Range(1,points.Length);
			if(points[index].tag == "Z_Gizmo") {
				rotation = -180;
			} else if (points[index].tag == "X_Gizmo") {
				rotation = -270;
			}
			Instantiate (door, points[index].position, Quaternion.Euler(0,rotation,0));
			
			yield return new WaitForSeconds(createTime);
				
			Destroy(GameObject.Find("door(Clone)"));
		}
	}
}

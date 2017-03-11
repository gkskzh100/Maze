using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

	public float moveSpeed = 20.0f;
	public float drag = 0.5f;
	public float jump = 5f;
	private float radius = 40f;
	private bool onGround;
	public bool triggerBool = true;
	public Vector3 MoveVector{set; get;}
	public VirtualJoystick joystick;
	private Rigidbody thisRigidbody;

	public JumpController jumpJoystick;
	private GameObject mazeObject;
	private RandMaze randMaze = null;

	// Use this for initialization
	void Start () {
		thisRigidbody = gameObject.AddComponent<Rigidbody>();
		thisRigidbody.drag = drag;
		thisRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		onGround = true;

	}
	
	// Update is called once per frame
	void Update () {
		MoveVector = PoolInput();
		Move();
		Jump();
	}

	private void Move() {
		thisRigidbody.transform.Translate((MoveVector * moveSpeed * Time.deltaTime),Space.Self);
		transform.Rotate(0,joystick.Horizontal() * Time.deltaTime * 20.0f,0);
	}
	private void Jump() {
		if(jumpJoystick.isPressed && onGround) {
			thisRigidbody.velocity = new Vector3(0,jump,0);
			onGround = false;

		} else if (!jumpJoystick.isPressed && !onGround && transform.position.y <=2) {
			onGround = true;
		}
	}

	private Vector3 PoolInput() {
		Vector3 dir = Vector3.zero;

		dir.x = joystick.Horizontal();
		dir.z = joystick.Vertical();

		if(dir.magnitude > 1)
			dir.Normalize();

		return dir;
	}

	// private GameObject FindMaze() {
	// 	Collider[] stopColliders = Physics.OverlapSphere(transform.position, radius);

	// 	float temp = 0;
	// 	float shortTemp = 10000;

	// 	for (int i = 0; i < stopColliders.Length; i++ ) {
	// 		if (stopColliders[i].tag == "Maze") {
	// 			temp = Vector3.Distance(stopColliders[i].transform.position,transform.position);
	// 			if(temp < shortTemp) {
	// 				mazeObject = stopColliders[i].gameObject;
	// 				shortTemp = temp;
	// 			}
	// 		}
	// 	}
	// 	return mazeObject;
	// }
	// void OnTriggerEnter(Collider col)
	// {
	// 	if(col.transform.tag == "Maze") {
	// 		triggerBool = true;
	// 		randMaze = FindMaze().GetComponent<RandMaze>();
	// 		if(randMaze != null) {
	// 			randMaze.coroutineActive();
	// 		}
	// 	}
	// }
	// void OnTriggerExit(Collider col)
	// {
	// 	if(col.transform.tag == "Maze")
	// 		triggerBool = false;
	// }
}

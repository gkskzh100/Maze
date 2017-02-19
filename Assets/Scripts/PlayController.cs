using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

	public float moveSpeed = 20.0f;
	public float drag = 0.5f;
	public Vector3 MoveVector{set; get;}
	public VirtualJoystick joystick;
	private Rigidbody thisRigidbody;

	// Use this for initialization
	void Start () {
		thisRigidbody = gameObject.AddComponent<Rigidbody>();
		thisRigidbody.drag = drag;
		thisRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		MoveVector = PoolInput();
		Move();
	}

	private void Move() {
		thisRigidbody.transform.Translate((MoveVector * moveSpeed * Time.deltaTime),Space.Self);
		transform.Rotate(0,joystick.Horizontal() * Time.deltaTime * 20.0f,0);

	}

	private Vector3 PoolInput() {
		Vector3 dir = Vector3.zero;

		dir.x = joystick.Horizontal();
		dir.z = joystick.Vertical();

		if(dir.magnitude > 1)
			dir.Normalize();

		return dir;
	}
}

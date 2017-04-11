using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouch : MonoBehaviour {

	// private Vector2 movePos;
	private float prePos,nowPos;
	public float movePos;
	private VirtualJoystick joystick;
	private bool touchBool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		joystick = GameObject.Find("BackgroundImage").GetComponent<VirtualJoystick>();
		touchBool = joystick.touch;
		if(Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began && !touchBool) { //처음 터치할때
				prePos = touch.position.x;
				// Debug.Log(prePos);
			} else if (touch.phase == TouchPhase.Moved && !touchBool) { // 터치하고 움직일때
				nowPos = prePos - touch.position.x;
				nowPos = nowPos/180;
				// Debug.Log(nowPos);
				movePos = nowPos * Time.deltaTime * 20.0f;
			} else if (touch.phase == TouchPhase.Ended && !touchBool) { //터치 끝날때
				movePos = 0;
			}
		} else if (Input.touchCount == 2) {
			Touch touch = Input.GetTouch(1);
			if(touch.phase == TouchPhase.Began && touchBool) {
				prePos = touch.position.x;
				// Debug.Log(prePos);
			} else if (touch.phase == TouchPhase.Moved && touchBool) {
				nowPos = prePos - touch.position.x;
				nowPos = nowPos/180;
				// Debug.Log(nowPos);
				movePos = nowPos * Time.deltaTime * 20.0f;
			} else if (touch.phase == TouchPhase.Ended && touchBool) {
				movePos = 0;
			}
		}

		// if(Input.GetMouseButton(0)) {
		// 	Debug.Log(Input.mousePosition.x);
		// }
	}
}

//./adb logcat Unity:I Native:I *:S

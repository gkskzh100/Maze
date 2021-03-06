﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

	private Image bgImage;
	private Image joystickImage;
	private Vector3 inputVector;
	public bool touch;

	private Portal portal;
	private bool moveBool = true;

	private void Start()
	{
		Screen.SetResolution(2560,1440,true);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		bgImage = GetComponent<Image>();
		joystickImage = transform.GetChild(0).GetComponent<Image>();
		portal = GameObject.Find("door(Clone)").GetComponent<Portal>();
	}

	void Update()
	{
		moveBool = portal.userBool;
	}

	public virtual void OnDrag(PointerEventData ped) {
		if(moveBool) {
			Vector2 pos;
			if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform,
																	ped.position,
																	ped.pressEventCamera,
																	out pos)) {
			pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f)?inputVector.normalized:inputVector;

			joystickImage.rectTransform.anchoredPosition = 
					new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x/3)
								,inputVector.z * bgImage.rectTransform.sizeDelta.y/3);
			}
		} else {
			joystickImage.rectTransform.anchoredPosition = Vector3.zero;
		}
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		OnDrag(ped);
		touch = true;
	}
	public virtual void OnPointerUp(PointerEventData ped) {
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
		touch = false;
	}

	public float Horizontal() {
		if(inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis("Horizontal");
	}
	public float Vertical() {
		if(inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis("Vertical");
	}
}

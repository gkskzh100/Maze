using UnityEngine;
using UnityEngine.EventSystems;
public class JumpController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler{
	
	public bool isPressed = false;

	public void OnPointerDown(PointerEventData ped) {
		isPressed = true;
	}

	public void OnPointerUp(PointerEventData ped) {
		isPressed = false;
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReturnBtn : MonoBehaviour {
	public void ReturnButton() {
		SceneManager.LoadScene("MazeScene");
	}
	
}

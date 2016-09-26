using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

	public Camera mainCamera;
	public Camera pauseCamera;

	private Canvas pauseCanvas;

	void Start(){
		pauseCanvas = GetComponent<Canvas> ();
		pauseCanvas.enabled = false;
		mainCamera.enabled = true;
		pauseCamera.enabled = false;
	}

	public void Toggle(){
		mainCamera.enabled = !mainCamera.enabled;
		pauseCamera.enabled = !pauseCamera.enabled;

		pauseCanvas.enabled = !pauseCanvas.enabled;
	}
}

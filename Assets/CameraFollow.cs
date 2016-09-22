using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + 20 * Vector3.up;
	}
}

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		transform.position = (player.transform.position + (40 * Vector3.up) + 
			new Vector3(0f, 0f, 5f));
	}
}

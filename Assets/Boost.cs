using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {


	void OnTriggerStay(Collider other) {
		Debug.Log ("OMG COLLISION");
		if(other.gameObject.tag=="Player"){
			other.attachedRigidbody.velocity *= 1.2f;
		}
	}
}

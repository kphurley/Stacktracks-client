using UnityEngine;
using System.Collections;

public class Trigger_circuit_end : MonoBehaviour {

	public RaceManager rm;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.parent.parent.gameObject.name == "PlayerCar") {
			rm.EndRace ();
		}
	}
}

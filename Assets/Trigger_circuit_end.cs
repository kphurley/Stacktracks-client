using UnityEngine;
using System.Collections;

public class Trigger_circuit_end : MonoBehaviour {

	public RaceManager rm;

	void OnTriggerEnter(){
		Debug.Log ("END CIRCUIT");
		rm.EndRace ();
	}
}

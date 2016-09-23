using UnityEngine;
using System.Collections;
using SocketIO;

public class MovementEmitter : MonoBehaviour {

	private Rigidbody rb;
	public SocketIOComponent socket;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		//Debug.Log (getMovementJSON());
		socket.Emit("move", getMovementJSON());
	}


	string vectorToObj(Vector3 vector, string desc){
		
//		return "{\"x\":\"" + vector.x + "\"," +
//			"\"y\":\"" + vector.y + "\"," +
//			"\"z\":\"" + vector.z + "\"}";

		return "\"x" + desc + "\":\"" + vector.x + "\"," +
			   "\"y" + desc + "\":\"" + vector.y + "\"," +
			   "\"z" + desc + "\":\"" + vector.z + "\"";

	}

	JSONObject getMovementJSON() {
		//return new JSONObject("{ \"position\":" + vectorToObj (transform.position) + "," +
		//	"\"velocity\":" + vectorToObj (rb.velocity) + "}");
		return new JSONObject("{" + vectorToObj (transform.position, "Pos") + "," +
			vectorToObj (rb.velocity, "Vel") + "}");
	}
}

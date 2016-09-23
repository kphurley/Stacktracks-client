using UnityEngine;
using System.Collections;
using SocketIO;
using UnityStandardAssets.CrossPlatformInput;

public class MovementEmitter : MonoBehaviour {

	private Rigidbody rb;
	public SocketIOComponent socket;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		//Debug.Log (getMovementJSON());
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		float handbrake = CrossPlatformInputManager.GetAxis("Jump");
		socket.Emit("move", new JSONObject("{" + moveVectorToObj(h, v, handbrake) + 
			"," + vectorToObj (transform.position, "pos") +
			"," + vectorToObj (rb.velocity, "vel") + "}"));
	}


	string vectorToObj(Vector3 vector, string desc){
		
//		return "{\"x\":\"" + vector.x + "\"," +
//			"\"y\":\"" + vector.y + "\"," +
//			"\"z\":\"" + vector.z + "\"}";

		return "\"x" + desc + "\":\"" + vector.x + "\"," +
			   "\"y" + desc + "\":\"" + vector.y + "\"," +
			   "\"z" + desc + "\":\"" + vector.z + "\"";

	}

	string moveVectorToObj(float h, float v, float hb){

		return "\"h" + "\":\"" + h + "\"," +
			"\"v1" + "\":\"" + v + "\"," +
			"\"v2" + "\":\"" + v + "\"," +
			"\"hb" + "\":\"" + hb + "\"";

	}

//	JSONObject getMovementJSON() {
//		return new JSONObject(vectorToObj (transform.position) + "," +
//			vectorToObj (rb.velocity));
//	}
}

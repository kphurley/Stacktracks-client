using UnityEngine;
using System.Collections;
using SocketIO;


public class NetworkMove : MonoBehaviour {

	public SocketIOComponent socket;
	public string id;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		id = gameObject.name;
	}

	void AssignNetwork(Network n){
		socket = n.GetComponent<SocketIOComponent> ();
	}

	void ApplyNetworkMove(string posValues){
		string[] values = posValues.Split (',');

		//applying the components in this way works but doesnt turn the car!
		transform.position = new Vector3 (float.Parse (values [1]),
			float.Parse (values [3]), float.Parse (values [5]));
		rb.velocity = new Vector3 (float.Parse (values [7]),
			float.Parse (values [9]), float.Parse (values [11]));
	}
		

}
	


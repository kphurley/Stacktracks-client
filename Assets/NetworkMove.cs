using UnityEngine;
using System.Collections;
using SocketIO;
using UnityStandardAssets.Vehicles.Car;


public class NetworkMove : MonoBehaviour {

	public SocketIOComponent socket;
	public string id;
	public CarController cc;
	public ColorPicker cp;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		id = gameObject.name;
		cc = GetComponent<CarController> ();
	}

	//Hack to get a reference to the SocketIO Component at runtime
	void AssignNetwork(Network n){
		socket = n.GetComponent<SocketIOComponent> ();
	}

	void ApplyNetworkMove(string posValues){
		
		string[] values = posValues.Split (',');
		cc.Move (float.Parse(values [1]), float.Parse(values [3]), 
			float.Parse(values [5]), float.Parse(values [7]));

		//attempt to enforce some level of syncronicity
		transform.position = new Vector3 (float.Parse (values [9]),
			float.Parse (values [11]), float.Parse (values [13]));
		rb.velocity = new Vector3 (float.Parse (values [15]),
			float.Parse (values [17]), float.Parse (values [19]));
		transform.rotation = new Quaternion (float.Parse (values [21]),
			float.Parse (values [23]), float.Parse (values [25]), float.Parse (values [27]));
		if (int.Parse (values [29]) != cp.GetColorIdx ()) {
			cp.ApplyChosenMaterial(int.Parse(values[29]));
		}

	}
		

}
	


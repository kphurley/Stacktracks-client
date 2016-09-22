using UnityEngine;
using System.Collections;
using SocketIO;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;

	public GameObject player;

	void Start () {
		socket = GetComponent<SocketIOComponent> ();

		//Event handler registration
		socket.On("open", OnConnected);
		socket.On ("spawn", OnSpawn);
	}

	void OnConnected(SocketIOEvent e){
		Debug.Log ("Connected to server");
	}

	void OnSpawn(SocketIOEvent e) {
		
		Debug.Log ("Spawning player: " + e.data);
		Instantiate (player);
	}

}

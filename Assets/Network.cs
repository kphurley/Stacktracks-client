using UnityEngine;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;

	public GameObject player;

	Dictionary<string, GameObject> players;

	void Start () {
		socket = GetComponent<SocketIOComponent> ();

		players = new Dictionary<string, GameObject> ();

		//Event handler registration
		socket.On("open", OnConnected);
		socket.On("spawn", OnSpawn);
		socket.On ("disconnected", OnDisconnected); 
	}

	void OnConnected(SocketIOEvent e){
		Debug.Log ("Connected to server");
	}

	void OnSpawn(SocketIOEvent e) {
		
		Debug.Log ("Spawning player: " + e.data.ToString());
		var newPlayer = Instantiate (player);
		players.Add (e.data ["id"].ToString (), newPlayer);
		Debug.Log ("Player count: " + players.Count);
	}

	void OnDisconnected(SocketIOEvent e) {
		var id = e.data ["id"].ToString ();
		Debug.Log ("Player disconnected: " + id);
		Destroy (players [id]);
		players.Remove (id);
	}

}

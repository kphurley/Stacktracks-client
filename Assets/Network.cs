using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;

	public GameObject networkCar;

	private int NUM_OF_KEYS_AND_VALUES = 21;
	private GlobalControl GC;
	//The set of player game objects obtained via network
	Dictionary<string, GameObject> players;

	//A hash table containing server provided info about all clients,
	//indexed by a string id
	Dictionary<string, string> gameState;


	void Start () {
		socket = GetComponent<SocketIOComponent> ();
		GC = GameObject.Find ("GlobalControl").GetComponent<GlobalControl>();
		socket.url = GC.GetIpAddress ();

		players = new Dictionary<string, GameObject> ();
		gameState = new Dictionary<string, string> ();

		//Event handler registration
		socket.On("open", OnConnected);
		socket.On("spawn", OnSpawn);
		socket.On ("disconnected", OnDisconnected); 
		socket.On ("tick", OnTick);
	}

	void OnConnected(SocketIOEvent e){
		Debug.Log ("Connected to server");
	}

	void OnSpawn(SocketIOEvent e) {
		
		Debug.Log ("Spawning player: " + e.data.ToString());
		var newPlayer = Instantiate (networkCar);
		newPlayer.name = e.data ["id"].ToString ();
		newPlayer.SendMessage ("AssignNetwork", this);
		players.Add (e.data ["id"].ToString (), newPlayer);
		Debug.Log ("Player count: " + players.Count);
	}

	void OnDisconnected(SocketIOEvent e) {
		var id = e.data ["id"].ToString ();
		Debug.Log ("Player disconnected: " + id);
		Destroy (players [id]);
		players.Remove (id);
	}
		
	void OnTick(SocketIOEvent e) {
		
		List<string> values = GetKeys(e.data.ToString());
//		string parsedData = "";
//		for (int i = 0; i < values.Count; i++) {
//			parsedData += values [i] + "|";
//		}
//		Debug.Log (parsedData);
		gameState = BuildState (values, NUM_OF_KEYS_AND_VALUES);
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < objs.Length; i++) {
			if(!objs[i].name.Equals("PlayerCar")){
				//substring hack to take out " " from generated ID
				objs [i].SendMessage ("ApplyNetworkMove", 
					gameState [objs [i].name.Substring(1,8)]);
			}
		}
	}

	//should eventually end up in a utils class
	//This gives me a list of keys so I can parse the JSON to record ticks properly
	List<string> GetKeys(string text){	
		//string text = "One car red car blue car";
		string pat = "\"(.*?)\"";

		// Instantiate the regular expression object.
		Regex r = new Regex(pat, RegexOptions.IgnoreCase);

		// Match the regular expression pattern against a text string.
		Match m = r.Match(text);
		//int matchCount = 0;
		List<string> keys = new List<string>();


		while (m.Success) 
		{

			Group g = m.Groups[1];

			keys.Add (g.ToString ());

			m = m.NextMatch();
		}
			
		return keys;
	}

	//Store the game state from the parsed JSON
	//The code below is horrible.  I'll try to explain.
	//The keys come back in groups of numOfValues and look like this:
	//  id_hash | h | 13 | v1 | 0.0005 ... etc
	//  We want to to store this in a dictionary so we can quickly access 
	// the values by id on each tick.
	Dictionary<string, string> BuildState(List<string> keys, int numOfValues){
		
		Dictionary<string, string> newState = new Dictionary<string, string> ();

		string id = ""; 

		for (int i = 0; i < keys.Count; i++) {
				
			if (i % numOfValues == 0) {
				id = keys[i];
				newState.Add (id, "");
			} else {
				newState[id] += keys[i]+",";
			}
					
		}

		return newState;
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance;
	public static string ipAddress;
	public static string username;

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	//The ip methods don't work.  It seems the only reliable way 
	//to set the SocketIOComponent's ip address is in the inspector.
	public void SetIpAddress(string ip) {
		ipAddress = ip;
	}

	public string GetIpAddress(){
		return "ws://" + ipAddress + ":3000/socket.io/?EIO=4&transport=websocket";
	}

	public void SetUsername(string name) {
		username = name;
	}

	public string GetUsername(){
		return username;
	}

	public void GoToPlayScene(){
		SceneManager.LoadScene (1);
	}
}

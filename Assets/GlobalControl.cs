using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance;
	public static string ipAddress;

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

	public void SetIpAddress(string ip) {
		ipAddress = ip;
	}

	public string GetIpAddress(){
		return "ws://" + ipAddress + ":3000/socket.io/?EIO=4&transport=websocket";
	}

	public void GoToPlayScene(){
		SceneManager.LoadScene (1);
	}
}

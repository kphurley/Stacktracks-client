using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkPlayerName : MonoBehaviour {

	public Text playerNameText;

	void Start(){
		playerNameText = GetComponent<Text> ();
	}

	public void SetNameText(string name){
		//strip off extraneous quotes at ends
		playerNameText.text = name.Substring(1,name.Length-2);
	}

}

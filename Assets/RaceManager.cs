using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Vehicles.Car;

public class RaceManager : MonoBehaviour {

	DateTime startTime, spawnTime;
	TimeSpan elapsedTime;
	public bool timerOn, raceIsOver;
	public GameObject playerCar;
	public GameObject startPosition;
	public Quaternion playerInitialRotation;
	public Text timer;

	void StartRace(){
		TurnCarOff ();  //Will turn back on after coundown is up!
		timerOn = false;
		raceIsOver = false;
		spawnTime = DateTime.Now;
		startTime = spawnTime + new TimeSpan(0, 0, 3);
	}

	void Start () {
		playerInitialRotation = playerCar.transform.rotation;
		StartRace ();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			StartRace ();
		}
	}

	void LateUpdate () {

		if (raceIsOver) {
			timerOn = false;
		} else if (DateTime.Now >= startTime) {
			//The race started!  Game on!
			timerOn = true;
			TurnCarOn ();

		} else {
			//Sometimes the car keeps rolling after a restart.  
			//This keeps it in place until the countdown ends.
			playerCar.transform.position = startPosition.transform.position;
			playerCar.transform.rotation = playerInitialRotation;
			playerCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		if (timerOn) {
			elapsedTime = DateTime.Now - startTime;

			string displayTime = String.Format ("{0:00}:{1:00}:{2:00}", 
				                     elapsedTime.Minutes, 
				                     elapsedTime.Seconds, 
				                     elapsedTime.Milliseconds);

			timer.text = displayTime;	
		} 

	}

	public void EndRace(){
		raceIsOver = true;
		//send time to server
	}

	//Prevents user action and car "stuff" while countdown is happening.
	//Disabling components is definitely not ideal..but..its a hackathon..so..yanno.
	void TurnCarOff(){
		playerCar.transform.position = startPosition.transform.position;
		playerCar.transform.rotation = playerInitialRotation;
		playerCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
		playerCar.GetComponent<CarUserControl>().enabled = false;
		playerCar.GetComponent<CarAudio>().enabled = false;
		if (playerCar.GetComponent<AudioSource> ()) {
			playerCar.GetComponent<AudioSource> ().enabled = false;
		}
	}

	void TurnCarOn(){
		playerCar.GetComponent<CarUserControl>().enabled = true;
		playerCar.GetComponent<CarAudio>().enabled = true;
		if (playerCar.GetComponent<AudioSource> ()) {
			playerCar.GetComponent<AudioSource> ().enabled = true;
		}
	}
		
}

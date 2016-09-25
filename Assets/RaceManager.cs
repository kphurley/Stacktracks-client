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
	public Text leaderboard;
	public Text speedDisplay;
	public Network network;

	void StartRace(){
		TurnCarOff ();  //Will turn back on after coundown is up!
		timerOn = false;
		raceIsOver = false;
		spawnTime = DateTime.Now;
		startTime = spawnTime + new TimeSpan(0, 0, 3);
		timer.text = "00:00:000";
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

		HandleTimerControls ();
		UpdateSpeedDisplay ();

	}

	void HandleTimerControls(){

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

	void UpdateSpeedDisplay(){
		speedDisplay.text = 
			((int)playerCar.GetComponent<CarController> ().CurrentSpeed).ToString() + " MPH";
	}

	public void EndRace(){
		raceIsOver = true;
		network.RecordCompletionTime (timer.text);
		//send time to server
	}

	//The comparer used for sorting TimeEntries
	//See https://msdn.microsoft.com/en-us/library/system.collections.icomparer(v=vs.110).aspx
	public class ArrangeByTime : IComparer  {

		public int Compare( System.Object x, System.Object y )  {
			return(String.Compare(((JsonHelper.TimeEntryArray.TimeEntry)x).time, 
				((JsonHelper.TimeEntryArray.TimeEntry)y).time ));
		}

	}

	public void SetLeaderboard(JsonHelper.TimeEntryArray.TimeEntry[] times){
		Array.Sort (times, new ArrangeByTime ());
		string newLeaderboard = "";
		for (int i = 0; i < times.Length; i++) {
			Debug.Log ("TimeEntryObject: (" + times [i].id + ") " + times [i].username +
			": " + times [i].time);
			newLeaderboard += ((i+1) + ". " + times[i].username + 
							": " + times[i].time + "\n");
		}
		leaderboard.text = newLeaderboard;
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

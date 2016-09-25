using UnityEngine;

//An awesome Json array helper class I found while browsing the Unity forums
//Amazingly, there is no official solution to deal with a Json array in Unity!
public class JsonHelper
{
	[System.Serializable]
	public struct TimeEntryArray
	{
		[System.Serializable]
		public struct TimeEntry
		{
			public string id;
			public string time;
		}

		public TimeEntry[] times;
	}
}

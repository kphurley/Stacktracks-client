using UnityEngine;
using System.Collections;

public class ColorPicker : MonoBehaviour {

	public GameObject[] bodyColorPrefab;
	public GameObject activePrefab;
	public int colorIdx;

	void Start () {
		ApplyActivePrefab ();
		colorIdx = 0;
	}

	public void ApplyChosenMaterial(int idx){

		if (colorIdx != idx) {
			colorIdx = idx;
			Destroy (GameObject.Find (activePrefab.name + "(Clone)"));
			activePrefab = bodyColorPrefab [idx];
			ApplyActivePrefab ();
		}

	}

	void ApplyActivePrefab(){
		GameObject newBody = Instantiate (activePrefab);
		newBody.transform.SetParent (this.transform);
		newBody.transform.localPosition = Vector3.zero;
		newBody.transform.localRotation = Quaternion.identity;
	}

	public int GetColorIdx(){
		return colorIdx;
	}

}

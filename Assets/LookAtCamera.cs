using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	private int updatesWithoutCam=0;
	[SerializeField]
    Transform myCam;
	public bool isCharge = false;
	// Use this for initialization
	void Start () {
		Debug.Log ("I started_LookAtCam");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (myCam != null) {
			transform.LookAt (myCam);
			if (isCharge == false) {
				transform.Rotate (0, 180, 0);
			} else {
				transform.Rotate (0, 90, 0);
			}
		} 
		else {
			updatesWithoutCam++;
			if(updatesWithoutCam>=6){
				if(GameObject.Find ("Main Camera")){
					newCam(GameObject.Find ("Main Camera").transform);
				}
				else if(GameObject.Find ("Camera")){
					newCam(GameObject.Find ("Camera").transform);
				}
			}
		}
    }

	public void newCam(Transform newCam){
		myCam = newCam;
	}
}

using UnityEngine;
using System.Collections;

public class SpecialText : MonoBehaviour {
	private int updatesWithoutCam=0;
	Transform myCam;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (myCam != null) {
			transform.LookAt (myCam);
			transform.Rotate (0, 180, 0);
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

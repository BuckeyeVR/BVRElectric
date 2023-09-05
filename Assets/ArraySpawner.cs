using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArraySpawner : MonoBehaviour {
    [SerializeField]
    Transform thing;
	[SerializeField]
	float[] radii;
	[SerializeField]
	int thetaIncrementor;
	[SerializeField]
	int phiIncrementor;

	int timerWait = 0;
	

	private bool shouldSpawn = true;
    // Use this for initialization
    void Start () {
	}

	void Update(){
		if (shouldSpawn == true) {
			if(timerWait==5){
				Spawn ();
				timerWait++;
			}
			else if(timerWait<7){
				timerWait++;
			}
		}
	}

	public void Spawn(){
		for (int radInc = 0; radInc < radii.Length; radInc++) {
			for (int theta = 0; theta < 360; theta+=thetaIncrementor) {
				for (int phi=0; phi < 360; phi+=phiIncrementor) {
					Instantiate (thing, new Vector3 (radii[radInc] * Mathf.Sin (theta * Mathf.Deg2Rad) * Mathf.Cos (phi * Mathf.Deg2Rad), radii[radInc] * Mathf.Sin (theta * Mathf.Deg2Rad) * Mathf.Sin (phi * Mathf.Deg2Rad), radii[radInc] * Mathf.Cos (theta * Mathf.Deg2Rad)), Quaternion.identity);
				}
			}
		}
	}

	//public void setShouldSpawn(bool b){
	//	shouldSpawn = b;
	//}

	public void setAmtRadii(int i){
		radii = new float[i];
	}

	public void setRadius(int location, float radius){
		if (location <= radii.Length && location>=1) {
			radii[location-1] = radius;
		}
	}

	public void setThetaIncrementor(int i){
		thetaIncrementor = i;
	}

	public void setPhiIncrementor(int i){
		phiIncrementor = i;
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineThing : MonoBehaviour {
    private GameObject [] charges;
	private Vector3 [] oldPos;
	private bool didChange = true;
	private GameObject [] otherLines; 
    // Use this for initialization
    void Start () {
		//Debug.Log ("I started_VectorThing");
        //transform.GetChild(0).localScale = new Vector3(0.15f, 1f, 0.15f);
		charges = GameObject.FindGameObjectsWithTag ("Charge");
		didChange = true;
        otherLines = GameObject.FindGameObjectsWithTag ("Line");
		oldPos = new Vector3[charges.Length];
		for(int i=0; i< otherLines.Length;i++){
			if((transform.root.position == otherLines[i].transform.root.position) && (transform.root != otherLines[i].transform.root)){
				Destroy(otherLines[i]);
			}
		}
    }

	// Update is called once per frame
	void Update () {
		if(didChange==true){
			didChange=false;
			Vector3 realVector = new Vector3 ();
            Transform currentBone = transform.GetChild(0);
            for (int j = 0; j < 27; j++)
               {
				realVector = new Vector3 ();
                for (int i=0; i<charges.Length; i++){
                    oldPos[i] = charges[i].transform.position;
                    float kq = charges[i].transform.gameObject.GetComponent<ChargeStrength>().getChargeStrength();
                    float r = Mathf.Sqrt(((currentBone.transform.position.x - charges[i].transform.position.x) * (currentBone.transform.position.x - charges[i].transform.position.x)) + ((currentBone.transform.position.y - charges[i].transform.position.y) * (currentBone.transform.position.y - charges[i].transform.position.y)) + ((transform.position.z - charges[i].transform.position.z) * (transform.position.z - charges[i].transform.position.z)));
                    Vector3 tmp = new Vector3(((kq / (r)) * ((currentBone.transform.position.x - charges[i].transform.position.x) / r)), ((kq / (r)) * ((currentBone.transform.position.y - charges[i].transform.position.y) / r)), ((kq / (r)) * ((currentBone.transform.position.z - charges[i].transform.position.z) / r)));
                    realVector.x += tmp.x;
                    realVector.y += tmp.y;
                    realVector.z += tmp.z;
                }
				currentBone.transform.LookAt(realVector);
				//currentBone.transform.Rotate(90, 0, 0);
                if (float.IsNaN(realVector.magnitude) == false)
                {
					//currentBone.localScale = new Vector3(1f, 1f, realVector.magnitude /1500f);
					currentBone.localScale = new Vector3(realVector.magnitude/500f, 1f, 1f);
                }
                else
                {
                    currentBone.localScale = new Vector3(0f, 0f, 0f);
                }
				if(currentBone.childCount>0){
					currentBone = currentBone.GetChild(0);
				}
            }
		}
		else{
			for(int i=0; i<charges.Length;i++){
				if(charges[i].transform.position == oldPos[i]){
					//nothing changed
				}
				else{
					didChange=true;
					break;
				}
			}
		}
	}
}

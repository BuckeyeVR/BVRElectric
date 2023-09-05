using UnityEngine;
using System.Collections;

public class ChargeStrength : MonoBehaviour {
	[SerializeField]
	public float chargeStrength = 0;
	[SerializeField]
	public Material positiveMaterial;
	[SerializeField]
	public Material negativeMaterial;
	[SerializeField]
	public Material neutralMaterial;
	// Use this for initialization
	void Start () {
		if (chargeStrength > 0) {
			transform.GetComponent<Renderer>().material = positiveMaterial;
		} else if (chargeStrength < 0) {
			transform.GetComponent<Renderer>().material = negativeMaterial;
		} else {
			transform.GetComponent<Renderer>().material = neutralMaterial;
		}
		if (GameObject.Find ("DisplayController")) {
			GameObject controller = GameObject.Find ("DisplayController");
			controller.transform.GetComponent<DisplayControllerScript> ().addChargeToArray (transform);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setChargeStrength(float f){
		chargeStrength = f;
	}

	public float getChargeStrength(){
		return chargeStrength;
	}
}

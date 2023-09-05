using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class DisplayControllerScript : MonoBehaviour {

	[SerializeField]
	private bool IsUsingWEB;
	private bool IsUsingDualDisplay;
	Transform [] currentActiveCamera;
	Transform [] charges;
	Transform currentCam; //current activeCam
	Transform dualCam; //vr view cam
	Transform dualCamSecondary; // controlsVR Settings
	Transform dualCamSelector; //level selector for dualcam
	Transform singleCamselector; //level selector for singlecam
	Transform singleCam; //non vr view cam
	private bool justLoaded = true;

	private Texture2D phoneIcon;
	private Texture2D vrIcon;
	private Texture2D buttomImg;
	//private string text; //added
	private int LevelLoadNumber = 1;
	private int InstructionNumber=6;
	//private CardboardProfile.ScreenSizes EmulatorSSize;
	// Use this for initialization
	void Start () {
		if (IsUsingWEB) {
			//PlayerPrefs.SetString ("instructions", "false");
		}
		phoneIcon = Resources.Load ("ph") as Texture2D;
		vrIcon = Resources.Load ("vr") as Texture2D;
		if (IsUsingDualDisplay) {
			buttomImg = phoneIcon;
		} else {
			buttomImg = vrIcon;
		}

		Debug.Log ("I started_DispContr");
		DontDestroyOnLoad (gameObject);
		if (GameObject.Find ("Main Camera") != null) {
			singleCam = GameObject.Find ("Main Camera").transform;
		}
		if (GameObject.Find ("Selector_2cam") != null) {
			dualCamSelector = GameObject.Find ("Selector_2cam").transform;
		}
		if (GameObject.Find ("Selector") != null) {
			singleCamselector = GameObject.Find ("Selector").transform;
		}
		if (GameObject.Find ("CardboardHead") != null) {
			dualCamSecondary = GameObject.Find ("Cardboard").transform;
			dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform; //we want to keep updating the position when the camera is disabled
		}
	}

	//Testing

	void OnGUI()
	{
		//Added
		/*
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.fontSize = 40; //change the font size
		guiStyle.normal.textColor = Color.red;
		GUILayout.Label(text, guiStyle);*/
		//Added
		GUIStyle buttonSameSize = new GUIStyle("button");
		buttonSameSize.fontSize = 60;
		//if (GUI.Button (new Rect (0, Screen.height - (Screen.height/10.0f), (Screen.width/10.0f), (Screen.height/10.0f)), "\u25A2|oo",buttonSameSize)) { //2610

		if (!IsUsingWEB) {
			if (GUI.Button (new Rect (0, Screen.height - (Screen.height / 10.0f), (Screen.width / 10.0f), (Screen.height / 10.0f)), buttomImg)) {	
				switchCam ();
			}
		}

	}



	// Update is called once per frame
	void LateUpdate () {
		//&& IsUsingWEB==false
		if ( Input.GetMouseButtonDown (0)  && (Input.mousePosition.x > (Screen.width/10.0f) || Input.mousePosition.y >(Screen.height/10.0f)) &&SceneManager.GetActiveScene().buildIndex >  LevelLoadNumber&&SceneManager.GetActiveScene().buildIndex!=InstructionNumber ) {
			//switchCam();
			SceneManager.LoadScene(LevelLoadNumber);//Go back to level load scene
			//text = "test" + VRDevice.isPresent;
			//text = Input.mousePosition.ToString(); 
		}
		if (IsUsingWEB == true) {
			if (GameObject.Find ("CardboardHead")) {
				Destroy(GameObject.Find ("CardboardHead"));
				if(singleCam.GetComponent<CardboardHead>()){
					singleCam.GetComponent<CardboardHead>().enabled=false;
				}
				singleCam.GetComponent<MouseOrbitImproved>().enabled=true;
			}
			if(GameObject.Find ("Cardboard")){
				if(singleCam.GetComponent<CardboardHead>()){
					singleCam.GetComponent<CardboardHead>().enabled=false;
				}
				singleCam.GetComponent<MouseOrbitImproved>().enabled=true;
				Destroy(GameObject.Find ("Cardboard"));
			}
			if(singleCam.GetComponent<MouseOrbitImproved>().isActiveAndEnabled==false){
				singleCam.GetComponent<MouseOrbitImproved>().enabled=true;
			}
		}
	}

	void OnLevelWasLoaded(int level) {
		singleCam = GameObject.Find ("Main Camera").transform;
		if (GameObject.Find ("CardboardHead") != null) {
			dualCamSecondary = GameObject.Find ("Cardboard").transform;
			dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform; //we want to keep updating the position when the camera is disabled
			if(IsUsingWEB == true){
				Destroy(GameObject.Find ("Cardboard"));
				Destroy(GameObject.Find ("CardboardHead"));
			}
			else{
				if(GameObject.Find ("Cardboard")){
					//GameObject.Find ("Cardboard").GetComponent<Cardboard>().setSize(EmulatorSSize);
				}
			}
		}
		if(IsUsingWEB == true){
			if(singleCam.GetComponent<CardboardHead>()){
				singleCam.GetComponent<CardboardHead>().enabled=false;
			}
			singleCam.GetComponent<MouseOrbitImproved>().enabled=true;
		}
		if (justLoaded == true) {
			justLoaded = false;
			if(GameObject.Find ("IntroLogo_Settings")){
				IsUsingDualDisplay = GameObject.Find ("IntroLogo_Settings").GetComponent<Intro> ().getIsUsingDualDisplay ();
				IsUsingWEB = GameObject.Find ("IntroLogo_Settings").GetComponent<Intro> ().getIsWeb();
				//EmulatorSSize = GameObject.Find ("IntroLogo_Settings").GetComponent<Intro> ().getSSize();
				Destroy (GameObject.Find ("IntroLogo_Settings"));
			}
		}
		if (IsUsingWEB == false) {
			switchCam ();
			switchCam ();
		}
		if (GameObject.Find ("Cardboard")) {

		}
	}

	public bool getDisplayMode(){
		return IsUsingDualDisplay;
	}

	public bool getWeb(){
		return IsUsingWEB;
	}
	
	void switchCam (){
		if (GameObject.Find ("Selector_2cam") != null) {
			dualCamSelector = GameObject.Find ("Selector_2cam").transform;
		}
		if (GameObject.Find ("Selector")!= null) {
			singleCamselector = GameObject.Find ("Selector").transform;
		}
		if (IsUsingDualDisplay == true) {
			//disabled the dualView make it one view
			if(singleCam == null){
				singleCam = GameObject.Find ("Main Camera").transform;
			}
			//if its still null it doesnt exist
			if(singleCam != null){
				if(currentCam!=null){
					if (dualCamSelector != null) {
						dualCamSelector.gameObject.SetActive(false);
					}
					dualCamSecondary.GetComponent<Cardboard>().setAlignmentMarker(false);
					dualCamSecondary.GetComponent<Cardboard>().setSettingsButton(false);
					dualCamSecondary.GetChild(0).gameObject.SetActive(false);
					dualCamSecondary.GetChild(1).gameObject.SetActive(false);
					currentCam.gameObject.SetActive(false);//currentCam.gameObject.SetActive(true);
				}
				if (singleCamselector != null) {
					singleCamselector.gameObject.SetActive(true);
				}
				currentCam = singleCam;
				currentCam.gameObject.SetActive(true);
			}
			else{
				IsUsingDualDisplay = !IsUsingDualDisplay;
			}
			
		} 
		else {
			//disabled the singleView make it dual view
			if(dualCam == null){
				if(GameObject.Find ("CardboardHead")!=null){
					dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform;
					dualCamSecondary = GameObject.Find ("Cardboard").transform;
				}
			}
			//if its still null it doesnt exist
			if(dualCam != null){
				if(currentCam != null){
					if (singleCamselector!= null) {
						singleCamselector.gameObject.SetActive(false);
					}
					currentCam.gameObject.SetActive(false);
				}
				if (dualCamSelector != null) {
					dualCamSelector.gameObject.SetActive(true);
				}
				dualCamSecondary.GetComponent<Cardboard>().setAlignmentMarker(true);
				dualCamSecondary.GetComponent<Cardboard>().setSettingsButton(true);
				currentCam = dualCam;
				currentCam.gameObject.SetActive(true);
				dualCamSecondary.GetChild(0).gameObject.SetActive(true);
				dualCamSecondary.GetChild(1).gameObject.SetActive(true);
			}
			else{
				IsUsingDualDisplay = !IsUsingDualDisplay;
			}
		}
		//if(charges!=null) {
		//	for (int i=0; i< charges.Length; i++) {
		//		if (charges [i] != null) {
		//			charges [i].GetComponent<LookAtCamera> ().newCam (currentCam);
		//		}
		//	}
		//}
		IsUsingDualDisplay = !IsUsingDualDisplay;
		if (IsUsingDualDisplay) {
			buttomImg = phoneIcon;
		} else {
			buttomImg = vrIcon;
		}
	}

	public void addChargeToArray (Transform charge){
		if (charges == null) {
			charges = new Transform[0];
		}
		Transform [] newArray = new Transform[charges.Length+1];
		for (int i=0; i< charges.Length; i++) {
			newArray[i] = charges[i];
		}
		newArray[charges.Length] = charge;
		charges = newArray;
		if(charges!=null) {
			for (int i=0; i< charges.Length; i++) {
				if (charges [i] != null) {
					if(charges [i].GetComponent<LookAtCamera> ()){
						charges [i].GetComponent<LookAtCamera> ().newCam (currentCam);
					}
				}
			}
		}
	}
}

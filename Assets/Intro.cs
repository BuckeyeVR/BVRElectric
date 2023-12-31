using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	[SerializeField]
	public Transform DispControllerPrefab;
	[SerializeField]
	public Texture Logo;
	[SerializeField]
	private bool IsUsingDualDisplay;
	[SerializeField]
	private bool IsWEB;
	public float LengthOfLogo;
	private Texture currentTexture;
	private bool isSwitching = false;
	private bool Instructions; //added

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		currentTexture = Logo;
		StartCoroutine (switchLevel (LengthOfLogo));
		PlayerPrefs.SetString ("url","URL");//added
		//PlayerPrefs.SetString ("instructions", "true");
		if (!PlayerPrefs.HasKey ("instructions") ) {
			PlayerPrefs.SetString ("instructions", "true");
			Instructions = true;
		} else {
			if (PlayerPrefs.GetString ("instructions") == "true") {
				Instructions = true;
			} else {
				Instructions = false;
			}
		}
	}

	void Awake() {
		Application.targetFrameRate = 60;
			
	    Screen.orientation = ScreenOrientation.Portrait; //Fix bug black bar on side of screen after exit/reenter
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && IsWEB==false) {
			IsUsingDualDisplay = !IsUsingDualDisplay;
		}
	}

	void OnGUI(){
		if (isSwitching == false) {
			if (IsUsingDualDisplay == false) {
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currentTexture);

				//For iPhones
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit);

			} else {
				//GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height), currentTexture);
				//GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height), currentTexture);
				//GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height), currentTexture, ScaleMode.ScaleToFit);
				//GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height), currentTexture, ScaleMode.ScaleToFit);

				//Could do two side by side for splashes for stere mode, but the start of this app should always be nonstereo?
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit);
			}
		}
	}

	public bool getIsUsingDualDisplay(){
		return IsUsingDualDisplay;
	}

	public bool getIsWeb(){
		return IsWEB;
	}

	//time to pass before switching to text in seconds.
	IEnumerator switchLevel(float timeToPass){
		yield return new WaitForSeconds(timeToPass);
		isSwitching = true;
		Instantiate (DispControllerPrefab, Vector3.zero, Quaternion.identity);
		GameObject dispC = GameObject.Find ("DisplayController(Clone)");
		dispC.transform.name = "DisplayController";
		DontDestroyOnLoad (dispC);
		if (Instructions&& !IsWEB) {//added
			//SceneManager.LoadScene(1);
			SceneManager.LoadScene(6);//changed for web
				}else{
			SceneManager.LoadScene (1);
				}
	}
}

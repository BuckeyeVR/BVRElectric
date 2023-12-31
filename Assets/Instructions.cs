using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {


	public Texture Logo;
	private Texture currentTexture;
	public Texture Next;
	public Texture DontShow;
	private bool IsUsingDualDisplay=false;



	// Use this for initialization
	void Start () {
		currentTexture = Logo;
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) &&Input.mousePosition.x < (Screen.width/10.0f) && Input.mousePosition.y <(Screen.height/10.0f)) {
			IsUsingDualDisplay = !IsUsingDualDisplay;
		}
	}

	void OnGUI(){
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currentTexture);
 
			if (IsUsingDualDisplay == false) {
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currentTexture);
			//GUI.DrawTexture(new Rect(0, Screen.height/10.0f, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height-Screen.height/10.0f), currentTexture, ScaleMode.StretchToFill);
			} else {
				//GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height), currentTexture); 
				//GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height), currentTexture);
			GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height-Screen.height/10.0f), currentTexture, ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height-Screen.height/10.0f), currentTexture, ScaleMode.StretchToFill);

				//Could do two side by side for splashes for stere mode, but the start of this app should always be nonstereo?
				//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit);
			}

		GUIStyle fontScaleSize = new GUIStyle("button");
		fontScaleSize.fontSize = Screen.height / 30;
		int nextScene = SceneManager.GetActiveScene ().buildIndex + 1;
		//if (GUI.Button (new Rect (Screen.width/2, Screen.height - (Screen.height/10.0f), (Screen.width/5.0f), (Screen.height/10.0f)), Next)) {
		if (GUI.Button (new Rect (Screen.width/2, Screen.height - (Screen.height/10.0f), (Screen.width/5.0f), (Screen.height/10.0f)), "Continue",fontScaleSize)) {
			SceneManager.LoadScene (1);
			}
		//if (GUI.Button (new Rect (Screen.width/2-Screen.width/5.0f, Screen.height - (Screen.height/10.0f), (Screen.width/5.0f), (Screen.height/10.0f)), DontShow)) {	
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/5.0f, Screen.height - (Screen.height/10.0f), (Screen.width/5.0f), (Screen.height/10.0f)), "Don't Show Again",fontScaleSize)) {
			SceneManager.LoadScene (1);
			PlayerPrefs.SetString("instructions","false");
		}
		}
}
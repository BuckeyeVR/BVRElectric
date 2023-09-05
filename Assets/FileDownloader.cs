using UnityEngine;
using System.Collections;

public class FileDownloader : MonoBehaviour {

	// Use this for initialization
	private string url = "URL";
	private bool choosingLevel = true;
	private bool showDropDown = false;
	private Vector2 scrollPosition = Vector2.zero;
	private float multiplyer=1.0f;
	private string vString = " V ";
	void Start () {
		if (PlayerPrefs.GetFloat ("savedMult") == null || PlayerPrefs.GetFloat ("savedMult") == 0.0f) {
			PlayerPrefs.SetFloat("savedMult",1.0f);
		} 
		else {
			multiplyer = PlayerPrefs.GetFloat ("savedMult");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// doesnt exist IGNORE - www.physics.ohio-state.edu/~madar.3/PhysElec/CustomLevel.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/CustomLevel2.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/lineTest.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/Sphere.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/Cube.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/SuperLongUnrealisticNamePartOneOfTrilogy.bvr
	//www.physics.ohio-state.edu/~madar.3/PhysElec/test.bvr
	private IEnumerator download(string myUrl){
		url = "Loading...";
		TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
		editor.selectIndex = 0;
		editor.cursorIndex = 0;
		editor.MoveTextStart();
		WWW www = new WWW(myUrl);
		yield return www;
		if (www.error != null) {
			url=(www.error.ToString ()+"");
			editor.selectIndex = 0;
			editor.cursorIndex = 0;
			editor.MoveTextStart();
		} 
		else {
			Debug.Log (www.text);
			bool overWrite = false;
			string fileName = myUrl;
			int levelIndex = 0;
			while (fileName.IndexOf ("/") != -1) {
				fileName = fileName.Substring (1);
			}
			Debug.Log(fileName);
			for (int i = 1; i <= PlayerPrefs.GetInt ("numLevels"); i++) {
				if (PlayerPrefs.GetString ("levelName" + i) == fileName) {
					overWrite = true;
					PlayerPrefs.SetString ("level" + i, www.text);
					levelIndex = i;
				}
			}
			choosingLevel = false;
			if (overWrite == false) {
				if (PlayerPrefs.GetInt ("numLevels") == 0 || PlayerPrefs.GetInt ("numLevels") == null) {
					PlayerPrefs.SetInt ("numLevels", 1);
				} else {
					PlayerPrefs.SetInt ("numLevels", PlayerPrefs.GetInt ("numLevels") + 1);
				}
				PlayerPrefs.SetString ("level" + PlayerPrefs.GetInt ("numLevels"), www.text);
				PlayerPrefs.SetString ("levelName" + PlayerPrefs.GetInt ("numLevels"), fileName);
				gameObject.GetComponent<fileSelectorLoader> ().parseText (PlayerPrefs.GetString ("level" + PlayerPrefs.GetInt ("numLevels")));
				//save it
			}
			else {
				gameObject.GetComponent<fileSelectorLoader> ().parseText (PlayerPrefs.GetString ("level" + levelIndex));
			}
		}
	}

	void OnGUI(){
		if (choosingLevel == true) {
			GUIStyle buttonSameSize = new GUIStyle("button");
			buttonSameSize.fontSize = 80;
			GUIStyle customButton = new GUIStyle("button");
			//customButton.fontSize = customButton.fontSize*((int)multiplyer);
			//Debug.Log(customButton.fontSize);
			customButton.fontSize = (int)((Screen.height/25.0f)*multiplyer);
			GUIStyle customTextField = new GUIStyle("textField");
			//customTextField.fontSize = customTextField.fontSize*((int)multiplyer);
			customTextField.fontSize = (int)((Screen.height/25.0f)*multiplyer);
			if (GUI.Button (new Rect (Screen.width - (Screen.width/8.0f), 0, (Screen.width/8.0f), (Screen.height/8.0f)), "+",buttonSameSize)) {
				multiplyer += 0.1f;
				PlayerPrefs.SetFloat("savedMult",multiplyer);
				Debug.Log (multiplyer);
			}
			if (GUI.Button (new Rect (Screen.width - (Screen.width/8.0f), (Screen.height/8.0f), (Screen.width/8.0f), (Screen.height/8.0f)), "-",buttonSameSize)) {
				if (multiplyer > 0.1f) {
					multiplyer -= 0.1f;
					PlayerPrefs.SetFloat ("savedMult", multiplyer);
					Debug.Log (multiplyer);
				}
			}
			url = GUI.TextField (new Rect (Screen.width / 2 - (Screen.width/3.0f)*multiplyer, Screen.height / 2 - (Screen.height/15.0f)*multiplyer, (Screen.width/2.2f)*multiplyer, (Screen.height/15.0f)*multiplyer), url,customTextField);
			if (GUI.Button (new Rect (Screen.width / 2+(Screen.width/5.0f)*multiplyer, Screen.height / 2 - (Screen.height/15.0f)*multiplyer, (Screen.width/10.0f)*multiplyer, (Screen.height/15.0f)*multiplyer), "Load",customButton)) {
				if (url.Length > 4) {
					if (url.Substring (url.Length - 4).ToLower () == ".bvr") {
						StartCoroutine (download (url));
					}
					else {
						url = "Doesn't end with .bvr";
					}
				}
				else {
					url = "Not a possible url";
				}
			}
			if (GUI.Button (new Rect (Screen.width / 2+(Screen.width/7.3f)*multiplyer, Screen.height / 2 - (Screen.height/15.0f)*multiplyer, (Screen.width/20.0f)*multiplyer, (Screen.height/15.0f)*multiplyer), vString,customButton)) {
				showDropDown = !showDropDown;
				if (showDropDown == true) {
					char upArrow = '\u039B';
					vString = " "+upArrow.ToString()+" ";
				} 
				else {
					vString = " V ";
				}
			}
			if (showDropDown == true) {
				scrollPosition = GUI.BeginScrollView(new Rect (Screen.width / 2 - (Screen.width/3.0f)*multiplyer, Screen.height / 2 + (Screen.height/100.0f)*multiplyer, (Screen.width/2.2f)*multiplyer, (Screen.height/5.0f)*multiplyer), scrollPosition, new Rect(0, 0, 0, PlayerPrefs.GetInt ("numLevels")*(Screen.height/15.0f)*multiplyer));
				for (int i = 1; i <= PlayerPrefs.GetInt ("numLevels"); i++) {
					if (GUI.Button (new Rect (0, (i-1)*(Screen.height/15.0f)*multiplyer, (Screen.width/2.58f)*multiplyer, (Screen.height/15.0f)*multiplyer), PlayerPrefs.GetString ("levelName"+i),customButton)) {
						gameObject.GetComponent<fileSelectorLoader> ().parseText (PlayerPrefs.GetString ("level" + i));
						choosingLevel = false;
					}
					if (GUI.Button (new Rect ((Screen.width/2.58f)*multiplyer, (i-1)*(Screen.height/15.0f)*multiplyer, (Screen.width/30.0f)*multiplyer, (Screen.height/15.0f)*multiplyer), "X",customButton)) {
						for (int levelIndex = i; levelIndex < PlayerPrefs.GetInt ("numLevels"); levelIndex++) {
							PlayerPrefs.SetString ("level" + levelIndex, PlayerPrefs.GetString ("level" + (levelIndex + 1)));
							PlayerPrefs.SetString ("levelName" + levelIndex, PlayerPrefs.GetString ("levelName" + (levelIndex + 1)));
						}
						PlayerPrefs.SetInt ("numLevels",PlayerPrefs.GetInt ("numLevels") - 1);
					}
				}
				GUI.EndScrollView();
			}
		}
	}
}

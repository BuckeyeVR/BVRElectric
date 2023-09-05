using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class fileSelectorLoader : MonoBehaviour {

	// Use this for initialization
	string fileLocation="Type File Location Here";
	bool selectedFile=false;
	public GameObject capsule;
	public GameObject cube;
	public GameObject cylinder;
	public GameObject plane;
	public GameObject quad;
	public GameObject sphere;
	public GameObject charge;
	public GameObject vector;
	public GameObject text;
	public GameObject vectorField;
	public GameObject LineSegement;
	public GameObject parametricSurface;
	private GameObject[] allObjects;
	private bool isLocalDirectory = false;
	private Dictionary<string,Color> colors = new Dictionary<string,Color>();

	void Start(){
		colors["black"] = Color.black;
		colors["blue"] = Color.blue;
		colors["cyan"] = Color.cyan;
		colors["aqua"] = Color.cyan;
		colors["gray"] = Color.gray;
		colors["grey"] = Color.gray;
		colors["green"] = Color.green;
		colors["magenta"] = Color.magenta;
		colors["fuchsia"] = Color.magenta;
		colors["red"] = Color.red;
		colors["white"] = Color.white;
		colors["yellow"] = Color.yellow;
		Application.ExternalCall ("genMyLevel");
	}

	public void clearLevel(string testParam){
		Application.ExternalCall("ConsoleOut", "Cleared Level");
		for (int i = allObjects.Length - 1; i >= 0; i--) {
			if (allObjects [i].transform.name != "Main Camera") {
				if (allObjects [i].transform.name != "Directional Light") {
					if (allObjects [i].transform.name != "GameObjectCenterPoint") {
						Destroy (allObjects [i].gameObject);
					}
				}
			}
		}
	}

	public void webTest(string test){
		test = test + "\n";
		string stringRemainder = test;
		int lineItter = 1;
		try{
		while (stringRemainder.IndexOf("\n")!=-1) {
			string currentString=stringRemainder.Substring(0,stringRemainder.IndexOf("\n"));
			if(currentString.IndexOf("//")!=-1){
				Application.ExternalCall("ConsoleOut", ". Comment on line: "+lineItter+" | "+currentString.Substring(currentString.IndexOf("//")+2));
				currentString = currentString.Substring(0,currentString.IndexOf("//"));
			}
			string prefix = "";
			string suffix = "";
			if(currentString.IndexOf("=")!=-1){
				prefix = currentString.Substring(0,currentString.IndexOf("="));
				suffix = currentString.Substring(currentString.IndexOf("=")+1);
			}
			else{
				Application.ExternalCall("ConsoleOut", "? No = sign: "+lineItter+" | "+currentString);
				prefix = currentString;
			}
			for(int i=0;i<suffix.Length;i++){
				if(((int)suffix[i])==13){
					suffix=suffix.Remove(i,1);
					i--;
				}
			}
			if(prefix=="" || prefix==" "){
				lineItter++;
				stringRemainder=stringRemainder.Substring(stringRemainder.IndexOf("\n")+1);
				continue;
			}
			GameObject refernceObj;
			bool spawningObj=false;
			if(suffix=="capsule"){
				refernceObj=Instantiate(capsule,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="cube"){
				refernceObj=Instantiate(cube,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="cylinder"){
				refernceObj=Instantiate(cylinder,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="plane"){
				refernceObj=Instantiate(plane,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="quad"){
				refernceObj=Instantiate(quad,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="sphere"){
				refernceObj=Instantiate(sphere,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="charge"){
				refernceObj=Instantiate(charge,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="vector"){
				refernceObj=Instantiate(vector,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="text"){
				refernceObj=Instantiate(text,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="vectorField"){
				refernceObj=Instantiate(vectorField,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix=="line"){
				refernceObj=Instantiate(LineSegement,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			else if(suffix == "parametricSurface"){
				refernceObj=Instantiate(parametricSurface,Vector3.zero,Quaternion.identity) as GameObject;
				refernceObj.transform.name=prefix;
				spawningObj=true;
			}
			if(spawningObj==false){
				refernceObj=GameObject.Find(prefix.Substring(0,prefix.IndexOf(".")));
				float arg0f, arg1f, arg2f, arg3f;
				int arg0i, arg1i, arg2i, arg3i;
				string arg0s, arg1s, arg2s, arg3s;
				char arg0c, arg1c, arg2c, arg3c;
				if (prefix.Contains (".position")) {
					arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
					suffix = suffix.Substring (suffix.IndexOf (",") + 1);
					arg1f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
					suffix = suffix.Substring (suffix.IndexOf (",") + 1);
					arg2f = float.Parse (suffix);
					refernceObj.transform.position = new Vector3 (arg0f, arg1f, arg2f);
				} else if (prefix.Contains (".setEquation")) {
					if (refernceObj.GetComponent<VectorField> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0s = (suffix);
						refernceObj.GetComponent<VectorField> ().setEquation (arg0c, arg0s);
					}
					if (refernceObj.GetComponent<lineSegment> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0s = (suffix);
						refernceObj.GetComponent<lineSegment> ().setEquation (arg0c, arg0s);
					}
					if (refernceObj.GetComponent<parametricSurface> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0s = (suffix);
						refernceObj.GetComponent<parametricSurface> ().setEquation (arg0c, arg0s);
					}
				} else if (prefix.Contains (".setInterval")) {
					if (refernceObj.GetComponent<VectorField> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0f = float.Parse (suffix);
						refernceObj.GetComponent<VectorField> ().setInterval (arg0c, arg0f);
					}
				} else if (prefix.Contains (".setMinMax")) {
					if (refernceObj.GetComponent<VectorField> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg1f = float.Parse (suffix);
						refernceObj.GetComponent<VectorField> ().setMinMax (arg0c, arg0f, arg1f);
					}
					if (refernceObj.GetComponent<lineSegment> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg1f = float.Parse (suffix);
						refernceObj.GetComponent<lineSegment> ().setMinMax (arg0c, arg0f, arg1f);
					}
					if (refernceObj.GetComponent<parametricSurface> ()) {
						arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg1f = float.Parse (suffix);
						refernceObj.GetComponent<parametricSurface> ().setMinMax (arg0c, arg0f, arg1f);
					}
				} 
				else if (prefix.Contains (".setSegmentCount")) {
					arg0i = int.Parse(suffix);
					if (refernceObj.GetComponent<lineSegment> ()) {
						refernceObj.GetComponent<lineSegment> ().setNumSegments (arg0i);
					}
					if (refernceObj.GetComponent<parametricSurface> ()) {
						refernceObj.GetComponent<parametricSurface> ().setNumSegments (arg0i);
					}
				}
				else if (prefix.Contains (".spawn")) {
					Debug.Log("test1");
					if (refernceObj.GetComponent<VectorField> ()) {
						refernceObj.GetComponent<VectorField> ().spawnVectors ();
					}
					if (refernceObj.GetComponent<lineSegment> ()) {
						refernceObj.GetComponent<lineSegment> ().drawLine ();
					}
					if (refernceObj.GetComponent<parametricSurface> ()) {
						refernceObj.GetComponent<parametricSurface> ().drawSurface ();
					}
				}
				else if(prefix.Contains(".rotation")){
					arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
					suffix=suffix.Substring(suffix.IndexOf(",")+1);
					arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
					suffix = suffix.Substring(suffix.IndexOf(",")+1);
					arg2f = float.Parse(suffix);
					refernceObj.transform.localEulerAngles = new Vector3(arg0f,arg1f,arg2f);
				}
				else if(prefix.Contains(".scale")){
					arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
					suffix=suffix.Substring(suffix.IndexOf(",")+1);
					arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
					suffix = suffix.Substring(suffix.IndexOf(",")+1);
					arg2f = float.Parse(suffix);
					refernceObj.transform.localScale = new Vector3(arg0f,arg1f,arg2f);
				}
				else if(prefix.Contains(".setRadiusAmt")){
					arg0i = int.Parse(suffix);
					if(refernceObj.GetComponent<ArraySpawner>()){
						refernceObj.GetComponent<ArraySpawner>().setAmtRadii(arg0i);
					}
				}
				else if(prefix.Contains(".setRadius")){
					arg0i = int.Parse(suffix.Substring(0,suffix.IndexOf(",")));
					suffix=suffix.Substring(suffix.IndexOf(",")+1);
					arg1f = float.Parse(suffix);
					if(refernceObj.GetComponent<ArraySpawner>()){
						refernceObj.GetComponent<ArraySpawner>().setRadius(arg0i,arg1f);
					}
				}

				else if(prefix.Contains(".setTheta")){
					arg0i = int.Parse(suffix);
					if(refernceObj.GetComponent<ArraySpawner>()){
						refernceObj.GetComponent<ArraySpawner>().setThetaIncrementor(arg0i);
					}
				}

				else if(prefix.Contains(".setPhi")){
					arg0i = int.Parse(suffix);
					if(refernceObj.GetComponent<ArraySpawner>()){
						refernceObj.GetComponent<ArraySpawner>().setPhiIncrementor(arg0i);
					}
				}

				else if(prefix.Contains(".setCharge")){
					arg0f = float.Parse(suffix);
					if(refernceObj.GetComponent<ChargeStrength>()){
						refernceObj.GetComponent<ChargeStrength>().setChargeStrength(arg0f);
					}
				}
				else if(prefix.Contains (".text")){
					arg0s = suffix;
					if(refernceObj.GetComponent<TextMesh>()){
						refernceObj.GetComponent<TextMesh>().text=arg0s;
					}
				}
				else if(prefix.Contains(".setWidth")){
						if(suffix.IndexOf(",")!=-1){
							arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix=suffix.Substring(suffix.IndexOf(",")+1);
							arg1f = float.Parse (suffix);
							if(refernceObj.GetComponent<lineSegment>()){
								refernceObj.GetComponent<lineSegment>().setWidth(arg0f,arg1f);
							}
						}
				}
				else if(prefix.Contains (".addSegment")){
						if(suffix.IndexOf(",")!=-1){
							arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix=suffix.Substring(suffix.IndexOf(",")+1);
							arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix = suffix.Substring(suffix.IndexOf(",")+1);
							arg2f = float.Parse (suffix);
							if(refernceObj.GetComponent<lineSegment>()){
								refernceObj.GetComponent<lineSegment>().addPosition(new Vector3 (arg0f,arg1f,arg2f));
							}
						}
				}
				else if(prefix.Contains (".color")){
					Color theColor = new Vector4(1f,1f,1f,1f);
					if(suffix.IndexOf(",")!=-1){
						arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix=suffix.Substring(suffix.IndexOf(",")+1);
						arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix = suffix.Substring(suffix.IndexOf(",")+1);
						if(suffix.IndexOf(",")!=-1){
							arg2f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix = suffix.Substring(suffix.IndexOf(",")+1);
							arg3f = float.Parse (suffix);
							theColor = new Vector4 (convertFloat (arg0f), convertFloat (arg1f), convertFloat (arg2f), convertFloat (arg3f));
						}
						else{
							arg2f = float.Parse(suffix);
							theColor = new Vector4(convertFloat(arg0f),convertFloat(arg1f),convertFloat(arg2f),1.0f);
						}
					}
					else{
						arg0s = suffix.ToLower();
						if(colors.ContainsKey(arg0s)){
							theColor=colors[arg0s];
						}
					}
					if(refernceObj.GetComponent<TextMesh>()){
						refernceObj.GetComponent<TextMesh>().color = theColor;
					}
					if(refernceObj.GetComponent<Renderer>()){
						if(refernceObj.GetComponent<Renderer>().material.HasProperty("_Color")){
							refernceObj.GetComponent<Renderer>().material.color = theColor;
						}
					}
					if(refernceObj.transform.childCount>0){
						Component[] textMeshes = refernceObj.GetComponentsInChildren<TextMesh>();
						for(int i=0;i<textMeshes.Length;i++){
							(textMeshes[i] as TextMesh).color = theColor;
						}
						Component[] renderers = refernceObj.GetComponentsInChildren<Renderer>();
						for(int i=0;i<renderers.Length;i++){
							if((renderers[i] as Renderer).material.HasProperty("_Color")){
								(renderers[i] as Renderer).material.color = theColor;
							}
						}
					}
				}
			}
			Application.ExternalCall("ConsoleOut", ". Completed line: "+lineItter);
			lineItter++;
			stringRemainder=stringRemainder.Substring(stringRemainder.IndexOf("\n")+1);
		}
		}
		catch(Exception e){
			Application.ExternalCall("ConsoleOut", "! There was an error on line: "+lineItter+" | Reason: "+e.ToString());
			//Application.ExternalCall("ConsoleOut", "Reason: "+e.ToString());
		}
		allObjects = FindObjectsOfType <GameObject>() as GameObject[];
	}

	public void parseText(string test){
		test = test + "\n";
		string stringRemainder = test;
		int lineItter = 1;
		try{
			while (stringRemainder.IndexOf("\n")!=-1) {
				string currentString=stringRemainder.Substring(0,stringRemainder.IndexOf("\n"));
				if(currentString.IndexOf("=")==-1){
					Debug.Log("? No = sign: "+lineItter+" | "+currentString);
				}
				if(currentString.IndexOf("//")!=-1){
					Debug.Log(". Comment on line: "+lineItter+" | "+currentString.Substring(currentString.IndexOf("//")+2));
					//Debug.Log("Pre: "+currentString);
					currentString = currentString.Substring(0,currentString.IndexOf("//"));
					//Debug.Log("Post: "+currentString);
				}
				string prefix = "";
				string suffix = "";
				if(currentString.IndexOf("=")!=-1){
					prefix = currentString.Substring(0,currentString.IndexOf("="));
					suffix = currentString.Substring(currentString.IndexOf("=")+1);
				}
				else{
					prefix = currentString;
				}
				if(prefix=="" || prefix==" "){
					lineItter++;
					stringRemainder=stringRemainder.Substring(stringRemainder.IndexOf("\n")+1);
					continue;
				}
				GameObject refernceObj;
				bool spawningObj=false;
				if(suffix=="capsule"){
					refernceObj=Instantiate(capsule,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="cube"){
					refernceObj=Instantiate(cube,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="cylinder"){
					refernceObj=Instantiate(cylinder,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="plane"){
					refernceObj=Instantiate(plane,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="quad"){
					refernceObj=Instantiate(quad,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="sphere"){
					refernceObj=Instantiate(sphere,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="charge"){
					refernceObj=Instantiate(charge,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="vector"){
					refernceObj=Instantiate(vector,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="text"){
					refernceObj=Instantiate(text,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="vectorField"){
					refernceObj=Instantiate(vectorField,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix=="line"){
					refernceObj=Instantiate(LineSegement,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				else if(suffix == "parametricSurface"){
					refernceObj=Instantiate(parametricSurface,Vector3.zero,Quaternion.identity) as GameObject;
					refernceObj.transform.name=prefix;
					spawningObj=true;
				}
				if(spawningObj==false){
					refernceObj=GameObject.Find(prefix.Substring(0,prefix.IndexOf(".")));
					float arg0f, arg1f, arg2f, arg3f;
					int arg0i, arg1i, arg2i, arg3i;
					string arg0s, arg1s, arg2s, arg3s;
					char arg0c, arg1c, arg2c, arg3c;
					if (prefix.Contains (".position")) {
						arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg1f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
						suffix = suffix.Substring (suffix.IndexOf (",") + 1);
						arg2f = float.Parse (suffix);
						refernceObj.transform.position = new Vector3 (arg0f, arg1f, arg2f);
					} else if (prefix.Contains (".setEquation")) {
						if (refernceObj.GetComponent<VectorField> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0s = (suffix);
							refernceObj.GetComponent<VectorField> ().setEquation (arg0c, arg0s);
						}
						if (refernceObj.GetComponent<lineSegment> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0s = (suffix);
							refernceObj.GetComponent<lineSegment> ().setEquation (arg0c, arg0s);
						}
						if (refernceObj.GetComponent<parametricSurface> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0s = (suffix);
							refernceObj.GetComponent<parametricSurface> ().setEquation (arg0c, arg0s);
						}
					} else if (prefix.Contains (".setInterval")) {
						if (refernceObj.GetComponent<VectorField> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0f = float.Parse (suffix);
							refernceObj.GetComponent<VectorField> ().setInterval (arg0c, arg0f);
						}
					} else if (prefix.Contains (".setMinMax")) {
						if (refernceObj.GetComponent<VectorField> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg1f = float.Parse (suffix);
							refernceObj.GetComponent<VectorField> ().setMinMax (arg0c, arg0f, arg1f);
						}
						if (refernceObj.GetComponent<lineSegment> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg1f = float.Parse (suffix);
							refernceObj.GetComponent<lineSegment> ().setMinMax (arg0c, arg0f, arg1f);
						}
						if (refernceObj.GetComponent<parametricSurface> ()) {
							arg0c = char.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg0f = float.Parse (suffix.Substring (0, suffix.IndexOf (",")));
							suffix = suffix.Substring (suffix.IndexOf (",") + 1);
							arg1f = float.Parse (suffix);
							refernceObj.GetComponent<parametricSurface> ().setMinMax (arg0c, arg0f, arg1f);
						}
					} 
					else if (prefix.Contains (".setSegmentCount")) {
						arg0i = int.Parse(suffix);
						if (refernceObj.GetComponent<lineSegment> ()) {
							refernceObj.GetComponent<lineSegment> ().setNumSegments (arg0i);
						}
						if (refernceObj.GetComponent<parametricSurface> ()) {
							refernceObj.GetComponent<parametricSurface> ().setNumSegments (arg0i);
						}
					}
					else if (prefix.Contains (".spawn")) {
						Debug.Log("test1");
						if (refernceObj.GetComponent<VectorField> ()) {
							refernceObj.GetComponent<VectorField> ().spawnVectors ();
						}
						if (refernceObj.GetComponent<lineSegment> ()) {
							refernceObj.GetComponent<lineSegment> ().drawLine ();
						}
						if (refernceObj.GetComponent<parametricSurface> ()) {
							refernceObj.GetComponent<parametricSurface> ().drawSurface ();
						}
					}
					else if(prefix.Contains(".rotation")){
						arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix=suffix.Substring(suffix.IndexOf(",")+1);
						arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix = suffix.Substring(suffix.IndexOf(",")+1);
						arg2f = float.Parse(suffix);
						refernceObj.transform.localEulerAngles = new Vector3(arg0f,arg1f,arg2f);
					}
					else if(prefix.Contains(".scale")){
						arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix=suffix.Substring(suffix.IndexOf(",")+1);
						arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix = suffix.Substring(suffix.IndexOf(",")+1);
						arg2f = float.Parse(suffix);
						refernceObj.transform.localScale = new Vector3(arg0f,arg1f,arg2f);
					}
					else if(prefix.Contains(".setRadiusAmt")){
						arg0i = int.Parse(suffix);
						if(refernceObj.GetComponent<ArraySpawner>()){
							refernceObj.GetComponent<ArraySpawner>().setAmtRadii(arg0i);
						}
					}
					else if(prefix.Contains(".setRadius")){
						arg0i = int.Parse(suffix.Substring(0,suffix.IndexOf(",")));
						suffix=suffix.Substring(suffix.IndexOf(",")+1);
						arg1f = float.Parse(suffix);
						if(refernceObj.GetComponent<ArraySpawner>()){
							refernceObj.GetComponent<ArraySpawner>().setRadius(arg0i,arg1f);
						}
					}

					else if(prefix.Contains(".setTheta")){
						arg0i = int.Parse(suffix);
						if(refernceObj.GetComponent<ArraySpawner>()){
							refernceObj.GetComponent<ArraySpawner>().setThetaIncrementor(arg0i);
						}
					}

					else if(prefix.Contains(".setPhi")){
						arg0i = int.Parse(suffix);
						if(refernceObj.GetComponent<ArraySpawner>()){
							refernceObj.GetComponent<ArraySpawner>().setPhiIncrementor(arg0i);
						}
					}

					else if(prefix.Contains(".setCharge")){
						arg0f = float.Parse(suffix);
						if(refernceObj.GetComponent<ChargeStrength>()){
							refernceObj.GetComponent<ChargeStrength>().setChargeStrength(arg0f);
						}
					}
					else if(prefix.Contains (".text")){
						arg0s = suffix;
						if(refernceObj.GetComponent<TextMesh>()){
							refernceObj.GetComponent<TextMesh>().text=arg0s;
						}
					}
					else if(prefix.Contains(".setWidth")){
						if(suffix.IndexOf(",")!=-1){
							arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix=suffix.Substring(suffix.IndexOf(",")+1);
							arg1f = float.Parse (suffix);
							if(refernceObj.GetComponent<lineSegment>()){
								refernceObj.GetComponent<lineSegment>().setWidth(arg0f,arg1f);
							}
						}
					}
					else if(prefix.Contains (".addSegment")){
						if(suffix.IndexOf(",")!=-1){
							arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix=suffix.Substring(suffix.IndexOf(",")+1);
							arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix = suffix.Substring(suffix.IndexOf(",")+1);
							arg2f = float.Parse (suffix);
							if(refernceObj.GetComponent<lineSegment>()){
								refernceObj.GetComponent<lineSegment>().addPosition(new Vector3 (arg0f,arg1f,arg2f));
							}
						}
					}
					else if(prefix.Contains (".color")){
						Color theColor = new Vector4(1f,1f,1f,1f);
						if(suffix.IndexOf(",")!=-1){
							arg0f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix=suffix.Substring(suffix.IndexOf(",")+1);
							arg1f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
							suffix = suffix.Substring(suffix.IndexOf(",")+1);
							if(suffix.IndexOf(",")!=-1){
								arg2f = float.Parse(suffix.Substring(0,suffix.IndexOf(",")));
								suffix = suffix.Substring(suffix.IndexOf(",")+1);
								arg3f = float.Parse (suffix);
								theColor = new Vector4 (convertFloat (arg0f), convertFloat (arg1f), convertFloat (arg2f), convertFloat (arg3f));
							}
							else{
								arg2f = float.Parse(suffix);
								theColor = new Vector4(convertFloat(arg0f),convertFloat(arg1f),convertFloat(arg2f),1.0f);
							}
						}
						else{
							arg0s = suffix.ToLower();
							if(colors.ContainsKey(arg0s)){
								theColor=colors[arg0s];
							}
						}
						if(refernceObj.GetComponent<TextMesh>()){
							refernceObj.GetComponent<TextMesh>().color = theColor;
						}
						if(refernceObj.GetComponent<Renderer>()){
							if(refernceObj.GetComponent<Renderer>().material.HasProperty("_Color")){
								refernceObj.GetComponent<Renderer>().material.color = theColor;
							}
						}
						if(refernceObj.transform.childCount>0){
							Component[] textMeshes = refernceObj.GetComponentsInChildren<TextMesh>();
							for(int i=0;i<textMeshes.Length;i++){
								(textMeshes[i] as TextMesh).color = theColor;
							}
							Component[] renderers = refernceObj.GetComponentsInChildren<Renderer>();
							for(int i=0;i<renderers.Length;i++){
								if((renderers[i] as Renderer).material.HasProperty("_Color")){
									(renderers[i] as Renderer).material.color = theColor;
								}
							}
						}
					}
				}
				//Application.ExternalCall("ConsoleOut", "Completed line: "+lineItter);
				lineItter++;
				stringRemainder=stringRemainder.Substring(stringRemainder.IndexOf("\n")+1);
			}
		}
		catch(Exception e){
			Debug.Log ("! There was an error on line: "+lineItter+" | Reason: "+e.ToString());
			//Application.ExternalCall("ConsoleOut", "There was an error on line: "+lineItter);
			//Application.ExternalCall("ConsoleOut", "Reason: "+e.ToString());
		}
		allObjects = FindObjectsOfType <GameObject>() as GameObject[];
	}

	float convertFloat(float f){
		if (f < 0) {
			f = 0f;
		} 
		else if (f > 255) {
			f=255f;
		}
		f = f / 255f;
		return f;
	}
}

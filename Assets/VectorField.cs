using UnityEngine;
using System.Collections;

public class VectorField : MonoBehaviour {

	private float xInterval = 0;
	private float yInterval = 0;
	private float zInterval = 0;

	private float yMin = 0;
	private float yMax = 0;
	private float xMin = 0;
	private float xMax = 0;
	private float zMin = 0;
	private float zMax = 0;

	private string xEquation = "";
	private string yEquation = "";
	private string zEquation = "";

	public GameObject specialVector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setEquation(char c, string eq){
		if (c == 'x' || c == 'X') {
			xEquation = eq;
		} else if (c == 'y' || c == 'Y') {
			yEquation = eq;
		} else if (c == 'z' || c == 'Z') {
			zEquation = eq;
		}
	}

	public void setMinMax(char c, float min, float max){
		if (c == 'x' || c == 'X') {
			xMin = min;
			xMax = max;

		} else if (c == 'y' || c == 'Y') {
			yMin = min;
			yMax = max;
		} else if (c == 'z' || c == 'Z') {
			zMin = min;
			zMax = max;
		}
	}

	public void setInterval(char c, float n){
		if (c == 'x' || c == 'X') {
			xInterval = n;
		} else if (c == 'y' || c == 'Y') {
			yInterval = n;
		} else if (c == 'z' || c == 'Z') {
			zInterval = n;
		}
	}

	private int getNumberOccurences(string needle, string hayStack){
		int toReturn = 0;
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Length>(i+needle.Length-1)){
				if(hayStack.Substring(i,needle.Length)==needle){
					toReturn++;
				}
			}
		}
		return toReturn;
	}

	public string customContains(string needle, string replacement ,string haystack){
		int loops = getNumberOccurences (needle, haystack);
		for (int i = 0; i < loops; i++) {
			if (haystack.Contains (needle)) {
				string charCheck = haystack.Substring (haystack.IndexOf (needle) - 1, 1);
				string charCheck2 = haystack.Substring (haystack.IndexOf (needle) + needle.Length, 1);
				if ((charCheck == "+") || (charCheck == "-") || (charCheck == "/") || (charCheck == "*") || (charCheck == "%") || (charCheck == "^") || (charCheck == "(")) {
					if ((charCheck2 == "+") || (charCheck2 == "-") || (charCheck2 == "/") || (charCheck2 == "*") || (charCheck2 == "%") || (charCheck2 == "^") || (charCheck2 == ")")) {
						haystack = haystack.Replace (charCheck + "" + needle + "" + charCheck2, charCheck + "" + replacement + "" + charCheck2);
					}
				}
			}
		}
		return haystack;
		//return false;
	}

	public void spawnVectors(){
		for (float i = xMin; i < xMax; i += xInterval) {
			for (float j = yMin; j < yMax; j += yInterval) {
				for (float k = zMin; k < zMax; k += zInterval) {
					GameObject temp = Instantiate(specialVector,Vector3.zero,Quaternion.identity) as GameObject;
					temp.transform.position = new Vector3 (i, j, k);
					string tmpXEQ = xEquation;
					string tmpYEQ = yEquation;
					string tmpZEQ = zEquation;
					//if (tmpXEQ.Contains ("x")) {
					tmpXEQ = customContains("x", i + "", tmpXEQ);
					tmpXEQ = customContains("y", j + "", tmpXEQ);
					tmpXEQ = customContains("z", k + "", tmpXEQ);
					tmpYEQ = customContains("x", i + "", tmpYEQ);
					tmpYEQ = customContains("y", j + "", tmpYEQ);
					tmpYEQ = customContains("z", k + "", tmpYEQ);
					tmpZEQ = customContains("x", i + "", tmpZEQ);
					tmpZEQ = customContains("y", j + "", tmpZEQ);
					tmpZEQ = customContains("z", k + "", tmpZEQ);
					Vector3 tmp = new Vector3 (GetComponent<EquationCalculator> ().solveEquation (tmpXEQ), GetComponent<EquationCalculator> ().solveEquation (tmpYEQ), GetComponent<EquationCalculator> ().solveEquation (tmpZEQ));
					temp.transform.GetChild(0).LookAt(tmp);
					temp.transform.GetChild(0).Rotate(90, 0, 0);
					if(float.IsNaN(tmp.magnitude)==false){
						temp.transform.GetChild(0).GetChild(0).localScale = new Vector3(0.5f, tmp.magnitude/1000.0f, 0.5f);
					}
					else{
						temp.transform.GetChild(0).GetChild(0).localScale = new Vector3(0f, 0f, 0f);
					}
				}
			}
		}
	}
}

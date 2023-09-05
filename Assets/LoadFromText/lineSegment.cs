using UnityEngine;
using System.Collections;

public class lineSegment : MonoBehaviour {
	private int segments=0;
	private int numSegments=0;
	private string xEquation="";
	private string yEquation="";
	private string zEquation="";
	private float xMin=0.0f;
	private float xMax=0.0f;
	private float yMin=0.0f;
	private float yMax=0.0f;
	private float zMin=0.0f;
	private float zMax=0.0f;

	// Use this for initialization
	void Start () {
		
	}

	//change scene to use seperate object for calculations
	//so all variables will be universal
	public void drawLine(){
		//numSegments
		float xInterval = (xMax - xMin)/(float)numSegments;
		float yInterval = (yMax - yMin)/(float)numSegments;
		float zInterval = (zMax - zMin)/(float)numSegments;
		float j = yMin;
		float k = zMin;
		//Debug.Log("yMax = "+yMax+" | yMin = "+yMin+" | yInterval = "+Mathf.Abs(yMax - yMin)/(float)numSegments);
		//Debug.Log("zMax = "+zMax+" | zMin = "+zMin+" | zInterval = "+Mathf.Abs(zMax - zMin)/(float)numSegments);
		for (float i = xMin; i < xMax; i += xInterval) {
			//for (float j = yMin; j < yMax; j += yInterval) {
				//for (float k = zMin; k < zMax; k += zInterval) {
					string tmpXEQ = xEquation;
					string tmpYEQ = yEquation;
					string tmpZEQ = zEquation;
					//Debug.Log ("x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
					tmpXEQ = customContains("x", i + "", tmpXEQ);
					tmpXEQ = customContains("y", j + "", tmpXEQ);
					tmpXEQ = customContains("z", k + "", tmpXEQ);
					tmpYEQ = customContains("x", i + "", tmpYEQ);
					tmpYEQ = customContains("y", j + "", tmpYEQ);
					tmpYEQ = customContains("z", k + "", tmpYEQ);
					tmpZEQ = customContains("x", i + "", tmpZEQ);
					tmpZEQ = customContains("y", j + "", tmpZEQ);
					tmpZEQ = customContains("z", k + "", tmpZEQ);
					//Debug.Log ("x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
					Vector3 tmp = new Vector3 (GetComponent<EquationCalculator> ().solveEquation (tmpXEQ), GetComponent<EquationCalculator> ().solveEquation (tmpYEQ), GetComponent<EquationCalculator> ().solveEquation (tmpZEQ));
					//Debug.Log ("new point: "+tmp);
					addPosition (tmp);
					j += yInterval;
					k += zInterval;

				//}
			//}
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

	string[] leftSide = { "+", "-", "/", "*", "%", "^", "(" };
	string[] rightSide = { "+", "-", "/", "*", "%", "^", ")" };
	public string customContains(string needle, string replacement ,string haystack){
		for (int i = 0; i < leftSide.Length; i++) {
			for (int j = 0; j < rightSide.Length; j++) {
				string bruteString = leftSide [i] + needle + rightSide [j] + "";
				haystack = haystack.Replace (bruteString, leftSide [i] + replacement + rightSide [j] + "");
			}
		}
		return haystack;
		//return false;
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

	public void setNumSegments(int n){
		numSegments = n;
	}

	public void setMinMax(char c, float min, float max){
		if (c == 'z' || c == 'Z') {
			zMin = min;
			zMax = max;
		}
		else if(c=='y' || c=='Y'){
			yMin = min;
			yMax = max;
		}

		else if(c=='x' || c=='X'){
			xMin = min;
			xMax = max;
		}
	}

	public void addPosition(Vector3 pos){
		if (segments == 0) {
			transform.GetComponent<LineRenderer> ().SetPosition (0, pos);
		}
		else if (segments == 1) {
			transform.GetComponent<LineRenderer> ().SetPosition (1, pos);
		}
		else {
			transform.GetComponent<LineRenderer> ().SetVertexCount(segments+1);
			transform.GetComponent<LineRenderer> ().SetPosition (segments, pos);
		}
		segments++;
	}

	public void setWidth(float f1,float f2){
		transform.GetComponent<LineRenderer> ().SetWidth (f1, f2);
	}

	public void setColors(Color c1, Color c2){
		transform.GetComponent<LineRenderer> ().SetColors (c1, c2);
	}

	// Update is called once per frame
	void Update () {
	
	}
}

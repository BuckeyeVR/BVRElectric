using UnityEngine;
using System.Collections;

public class parametricSurface : MonoBehaviour {

	// Use this for initialization
	public GameObject simpleQuad;
	public GameObject simpleCube;
	private string xEquation = "";
	private string yEquation = "";
	private string zEquation = "";
	private int numSegments = 0;
	private float sMin = 0.0f;
	private float sMax = 0.0f;
	private float tMin = 0.0f;
	private float tMax = 0.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createQuad(Vector3[] corners){
		GameObject temp = Instantiate (simpleQuad, Vector3.zero, Quaternion.identity) as GameObject;
		temp.transform.parent = transform;
		Mesh mesh = temp.GetComponent<MeshFilter> ().mesh;
		Vector3[] tempVertices = mesh.vertices;
		for(int i=0; i< tempVertices.Length; i++){
			tempVertices [i] = corners[i];
		}
		mesh.vertices = tempVertices;
	}

	public void drawSurface(){
		float sInterval = (sMax - sMin)/(float)numSegments;
		float tInterval = (tMax - tMin)/(float)numSegments;
		//Debug.Log("yMax = "+yMax+" | yMin = "+yMin+" | yInterval = "+Mathf.Abs(yMax - yMin)/(float)numSegments);
		//Debug.Log("zMax = "+zMax+" | zMin = "+zMin+" | zInterval = "+Mathf.Abs(zMax - zMin)/(float)numSegments);
		//float k = tMin;
		int tempCounter = 0;
		//Vector3[] c = new Vector3[4];
		for (float i = sMin; i < sMax; i += sInterval) {
			GameObject r = new GameObject ();
			r.transform.name = "Column " + (tempCounter + 1);
			r.transform.parent = transform;
            int itemCounter = 0;
			for (float k = tMin; k < tMax; k += tInterval) {
			//if (tempCounter == 4) {
			//	tempCounter = 0;
			//	createQuad (c);
			//	c = new Vector3[4];
			//}
			string tmpXEQ = xEquation;
			string tmpYEQ = yEquation;
			string tmpZEQ = zEquation;
			//Debug.Log ("x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
			//Debug.Log("Before: x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
			tmpXEQ = customContains("s", i + "", tmpXEQ);
			tmpXEQ = customContains("t", k + "", tmpXEQ);
			tmpYEQ = customContains("s", i + "", tmpYEQ);
			tmpYEQ = customContains("t", k + "", tmpYEQ);
			tmpZEQ = customContains("s", i + "", tmpZEQ);
			tmpZEQ = customContains("t", k + "", tmpZEQ);
			//Debug.Log("After: x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
			//Debug.Log ("x: "+tmpXEQ+" | y: "+tmpYEQ+" | z: "+tmpZEQ);
			Vector3 tmp = new Vector3 (GetComponent<EquationCalculator> ().solveEquation (tmpXEQ), GetComponent<EquationCalculator> ().solveEquation (tmpYEQ), GetComponent<EquationCalculator> ().solveEquation (tmpZEQ));
				if (tmp == new Vector3 (-3.874302f, -3.874302f, -3.874302f)) {
					Debug.Log ("s: "+i + " t: " + k);
					Debug.Log ("tmpXEQ: " +tmpXEQ+" = "+ GetComponent<EquationCalculator> ().solveEquation (tmpXEQ));
				}
			//gets 1 point of verticy
			//c[tempCounter] = tmp;
				GameObject tempCube = Instantiate(simpleCube, tmp, Quaternion.identity) as GameObject;
                //tempCube.transform.localScale = new Vector3 (0.03f, 0.03f, 0.03f);
                if (r == null)
                {
                    Debug.Log("?????");
                }
				tempCube.transform.parent = r.transform;
                tempCube.transform.name = "parent: " + r.name + " at " + itemCounter;
                itemCounter++;
            //Debug.Log ("new point: "+tmp);
            //addPosition (tmp);
            //k += tInterval;
            }
			tempCounter++;
		}
        Debug.Log("lets draw!");
        for(int i = -1; i < gameObject.transform.childCount-1; i++)
        {
            int firstColumn = i;
            if (firstColumn == -1)
            {
                firstColumn = gameObject.transform.childCount - 1;
            }
            int secondColumn = (i + 1);
            for (int j = 0; j < gameObject.transform.GetChild(firstColumn).transform.childCount-1; j++)
            {
                int columnChildOne = j;
                int columnChildTwo = (j + 1);
                if (gameObject.transform.GetChild(firstColumn) && gameObject.transform.GetChild(secondColumn) && gameObject.transform.GetChild(firstColumn).GetChild(columnChildOne) && gameObject.transform.GetChild(firstColumn).GetChild(columnChildTwo) && gameObject.transform.GetChild(secondColumn).GetChild(columnChildOne) && gameObject.transform.GetChild(secondColumn).GetChild(columnChildTwo))
                {
                    Vector3[] corners = { transform.GetChild(firstColumn).GetChild(columnChildOne).localPosition, transform.GetChild(secondColumn).GetChild(columnChildOne).localPosition, transform.GetChild(secondColumn).GetChild(columnChildTwo).localPosition, transform.GetChild(firstColumn).GetChild(columnChildTwo).localPosition };
                    createQuad(corners);
                }

               
                
                //transform.GetChild(i).
            }
        }
	}

	private int getNumberOccurences(string needle, string hayStack){
		int toReturn = 0;
		//Debug.Log ("Checking: " + hayStack + " for: " + needle);
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Length>(i+needle.Length-1)){
				if (hayStack.Substring (i, needle.Length) == needle) {
					//Debug.Log (hayStack.Substring (i, needle.Length) + " == " + needle);
					toReturn++;
				} else {
					//Debug.Log (hayStack.Substring (i, needle.Length) + " != " + needle);
				}
			}
		}
		//Debug.Log ("Count: "+toReturn);
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
		if (c == 's' || c == 'S') {
			sMin = min;
			sMax = max;
		}
		else if(c=='t' || c=='T'){
			tMin = min;
			tMax = max;
		}
	}
}

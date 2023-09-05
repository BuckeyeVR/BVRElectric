using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorFieldCalculator : MonoBehaviour {

	float maxX=10f;
	float minX=-10f;
	float xInterval = 1f;
	float maxY=10f;
	float minY=-10f;
	float yInterval = 1f;
	float maxZ=10f;
	float minZ=-10f;
	float zInterval = 1f;
	string equation = "2+2";
	// Use this for initialization
	void Start () {
		setEquation ("sin(5+5)");
	}

	public void setEquation(string s){
		equation = s;
		if (getNumberOccurences ("(", equation) == getNumberOccurences (")", equation)) {
			//probably a valid equation
			//time to work inside out
			int numOccurences=getNumberOccurences ("(", equation);
			for(int i=0;i<numOccurences;i++){
				Debug.Log(equation.Substring(getLastOccurence("(",equation),Mathf.Abs(getLastOccurence("(",equation)-(getFirstOccurence(")",equation)+1))));
			}
		}
	}

	private int getNumberOccurences(string needle, string hayStack){
		int toReturn = 0;
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Substring(i,needle.Length)==needle){
				toReturn++;
			}
		}
		return toReturn;
	}

	private int getLastOccurence(string needle, string hayStack){
		int toReturn = 0;
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Substring(i,needle.Length)==needle){
				toReturn=i;
			}
		}
		return toReturn;
	}

	private int getFirstOccurence(string needle, string hayStack){
		int toReturn = 0;
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Substring(i,needle.Length)==needle){
				toReturn=i;
				return toReturn;
			}
		}
		return toReturn;
	}

	public void setC(char c,float min, float max, float itt){
		if (c == 'x' || c == 'X') {
			maxX=max;
			minX=min;
			xInterval=itt;
		} 
		else if (c == 'y' || c == 'Y') {
			maxY=max;
			minY=min;
			yInterval=itt;
		} 
		else if (c == 'z' || c == 'Z') {
			maxZ=max;
			minZ=min;
			zInterval=itt;
		}
	}

	public void spawnField(){

	}
	// Update is called once per frame
	void Update () {
	
	}
}

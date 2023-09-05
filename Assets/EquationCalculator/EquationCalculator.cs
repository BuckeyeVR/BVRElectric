using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquationCalculator : MonoBehaviour {

	//HOW TO USE:
	//Use method addVariable(string,float) to add a variable to the list
	//EX: addVariable("x",transform.position.x);
	//Use method updateVariable(string,float) to update a previously added variable to a new value
	//EX: updateVariable("x",200f);
	//Use method removeVariable(string) to remove a previously inserted variable
	//EX: removeVariable("x");
	//Use method solveEquation(string) to solve an equation. ALL EQUATIONS MUST BE in parantheseis
	//for example you cannot do solveEquation("2+2");, you must do solveEquation("(2+2)");
	//EX_1: solveEquation("(2+2)"); //returns 4
	//EX_2: solveEquation("(0)"); //returns 0
	//EX_3: solveEquation("(1+2-3*4/5%9^(0.5))"); // returns 0.6 or 0.59999999

	//addVar(string var, float value);

	//INCLUDED FUNCTIONS:
	//sqrt //asin //atan
	//sin //cos //tan
	//abs //log (base 10) //ln

	//Easily Add your own! (CTRL+F "OWN FUNCTIONS")

	List<string> variables = new List<string>();
	List<float> variableValue = new List<float> ();
	
	// Use this for initialization
	void Start () {

	}
	
	public void addVariable(string s, float f){
		bool foundVal = false;
		for(int i=0;i<variables.Count;i++){
			if(variables[i]==s){
				foundVal=true;
				quickUpdate(i,f);
			}
		}
		if(foundVal==false){
			variables.Add (s);
			variableValue.Add (f);
		}
	}
	
	private void quickUpdate(int i, float f){
		variableValue [i] = f;
	}
	
	public void updateVariable(string s, float f){
		bool found = false;
		for (int i=0; i<variables.Count; i++) {
			if(variables[i]==s){
				found = true;
				variableValue[i]=f;
				break;
			}
		}
		if (found == false) {
			addVariable(s,f);
		}
	}
	
	public void removeVariable(string s){
		for (int i=0; i<variables.Count; i++) {
			if(variables[i]==s){
				variables.RemoveAt(i);
				variableValue.RemoveAt(i);
				break;
			}
		}
	}
	
	private int getNextOccurence(string needle1, string needle2, string hayStack){
		int toReturn = 0;
		hayStack = hayStack.Substring (getLastOccurence (needle1, hayStack),hayStack.Length-getLastOccurence (needle1, hayStack));
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Substring(i,needle2.Length)==needle2){
				toReturn=(i);
				return toReturn;
			}
		}
		return toReturn;
	}

	string[] leftSide = { "+", "-", "/", "*", "%", "^", "(" };
	string[] rightSide = { "+", "-", "/", "*", "%", "^", ")" };
	private string switchOutVars(string s){
		for (int z = 0; z < variables.Count; z++) {
			for (int i = 0; i < leftSide.Length; i++) {
				for (int j = 0; j < rightSide.Length; j++) {
					string bruteString = leftSide [i] + variables[z] + rightSide [j] + "";
					s = s.Replace (bruteString, leftSide [i] + variables[z] + rightSide [j] + "");
				}
			}
		}
		return s;
		//return false;
	}



	//HOW TO ADD YOUR OWN FUNCTIONS
	//In this example we will be making a function called div2
	//it will take a number and divide it by 2.
	//eg: solveEquation("div2(50)");
	//go down to the first else in this method (or ctrl+f "customFUNC")
	//based on the length of the function name, in this case div2 of length 4, either go into the if that says:
	//"equation.IndexOf(singleEQ)-(yourFunctionNameLength)>=0" or in this case just go into "equation.IndexOf(singleEQ)-4>=0"
	//Then after the charCheck line add this
	//if(charCheck=="div2"){
	//	float temp = solve (singleEQ);
	//	string f=""+(temp/2f);
	//	singleEQ="div2"+singleEQ;
	//	equation = equation.Replace(singleEQ,f);
	//}
	//thats it!
	
	public float solveEquation(string s){
		string equation = switchOutVars (s);
		if (getNumberOccurences ("(", equation) == getNumberOccurences (")", equation)) {
			int numOccurences=getNumberOccurences ("(", equation);
			for(int i=0;i<numOccurences;i++){
				string singleEQ = equation.Substring(getLastOccurence("(",equation),Mathf.Abs(getNextOccurence("(",")",equation)+1));
				if(equation.IndexOf(singleEQ)!=0){
					string charCheck = equation.Substring(equation.IndexOf(singleEQ)-1,1);
					if((charCheck=="+")||(charCheck=="-")||(charCheck=="/")||(charCheck=="*")||(charCheck=="%")||(charCheck=="^")||(charCheck=="(")){
						//Debug.Log ("INPUT: "+singleEQ);
						string f=""+solve(singleEQ);
						//Debug.Log("Replacing "+singleEQ+" with "+f);
						equation = equation.Replace(singleEQ,f);
						//Debug.Log ("NEW EQ: "+equation);
					}
					else{ //customFUNC
						if(equation.IndexOf(singleEQ)-4>=0){
							charCheck = equation.Substring(equation.IndexOf(singleEQ)-4,4);
							if(charCheck=="sqrt"){
								//Debug.Log ("INPUT: sqrt"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Sqrt(temp));
								//Debug.Log("Replacing sqrt"+singleEQ+" with "+f);
								singleEQ="sqrt"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="asin"){
								//Debug.Log ("INPUT: asin"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Asin(temp));
								//Debug.Log("Replacing asin"+singleEQ+" with "+f);
								singleEQ="asin"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="acos"){
								//Debug.Log ("INPUT: acos"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Acos(temp));
								//Debug.Log("Replacing acos"+singleEQ+" with "+f);
								singleEQ="acos"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="atan"){
								//Debug.Log ("INPUT: atan"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Atan(temp));
								//Debug.Log("Replacing atan"+singleEQ+" with "+f);
								singleEQ="atan"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
						}
						if(equation.IndexOf(singleEQ)-3>=0){
							charCheck = equation.Substring(equation.IndexOf(singleEQ)-3,3);
							if(charCheck=="sin"){
								//Debug.Log ("INPUT: sin"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Sin(temp));
								//Debug.Log("Replacing sin"+singleEQ+" with "+f);
								singleEQ="sin"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="cos"){
								//Debug.Log ("INPUT: ln"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Cos(temp));
								//Debug.Log("Replacing ln"+singleEQ+" with "+f);
								singleEQ="cos"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="tan"){
								//Debug.Log ("INPUT: ln"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+(Mathf.Tan(temp));
								//Debug.Log("Replacing ln"+singleEQ+" with "+f);
								singleEQ="tan"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="log"){
								Debug.Log ("INPUT: ln"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+Mathf.Log10(temp);
								Debug.Log("Replacing ln"+singleEQ+" with "+f);
								singleEQ="log"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								Debug.Log ("NEW EQ: "+equation);
							}
							else if(charCheck=="abs"){
								//Debug.Log ("INPUT: abs"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+Mathf.Abs(temp);
								//Debug.Log("Replacing abs"+singleEQ+" with "+f);
								singleEQ="abs"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								//Debug.Log ("NEW EQ: "+equation);
							}
						}
						if(equation.IndexOf(singleEQ)-2>=0){
							charCheck = equation.Substring(equation.IndexOf(singleEQ)-2,2);
							if(charCheck=="ln"){
								Debug.Log ("INPUT: ln"+singleEQ+"");
								float temp = solve (singleEQ);
								string f=""+Mathf.Log(temp);
								Debug.Log("Replacing ln"+singleEQ+" with "+f);
								singleEQ="ln"+singleEQ;
								equation = equation.Replace(singleEQ,f);
								Debug.Log ("NEW EQ: "+equation);
							}
						}
						//radians
						//sin,cos,tan,logbase10
					}
				}
				else{
					//Debug.Log ("INPUT: "+singleEQ);
					return solve(singleEQ);

				}
			}
		}
		return 0f;
	}
	
	//math operators + - / * % ^
	string[] eLeft = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
	string[] eRight = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "-" };
	public string replaceE(string s){
		//Debug.Log ("Checking " + s + " for E");
		for (int i = 0; i < eLeft.Length; i++) {
			for (int j = 0; j < eRight.Length; j++) {
				string bruteString = eLeft [i] + "E" + eRight [j] + "";
				//Debug.Log ("Replacing  " + bruteString + " with "+eLeft [i] + "*10^" + eRight [j] + "");
				s = s.Replace (bruteString, eLeft [i] + "*10^" + eRight [j] + "");
			}
		}
		return s;
		//return false;
	}

	private float solve(string s){
		List<string> f = new List<string> ();
		string temp = "";
		s = s.Substring (1, s.Length - 1);
		bool exp = false;
		bool modMultDiv = false;
		bool addSub = false;
		int operatorCount = 0;
		if (s.Contains ("E")) {
			s = replaceE (s);
		}
		for (int i = 0; i<s.Length; i++) {
			//Debug.Log(i+", "+(i+1)+", L: "+s.Length+", S: "+s.Substring(i,1));
			int t = 0;
			bool result = int.TryParse(s.Substring(i,1), out t);
			if(result==true){
				temp+=""+s.Substring(i,1);
			}
			else if(s.Substring(i,1)=="."){
				temp+=""+s.Substring(i,1);
			}
			else{
				f.Add(temp);
				temp = s.Substring(i,1);
				if(temp=="^" && exp==false){
					exp = true;
					operatorCount++;
				}
				else if((temp=="%"||temp=="*"||temp=="/")&&modMultDiv==false){
					modMultDiv = true;
					operatorCount++;
				}
				else if((temp=="+"||temp=="-")&&addSub==false){
					addSub = true;
					operatorCount++;
				}
				f.Add (temp);
				temp = "";
			}
		}
		f.RemoveAt (f.Count - 1);
		for(int z=0; z<f.Count;z++){
			if(f[z]==""||f[z]==" "||f[z].Length==0){
				f.RemoveAt(z);
				z--;
			}
		}
		for(int remNEG=0;remNEG<f.Count;remNEG++){
			if(addSub==true){
				float testNUM = 0;
				bool isNUM = true;
				if(remNEG>0){
					isNUM = float.TryParse(f[remNEG-1], out testNUM);
				}
				if(remNEG==0 && f[remNEG]=="-"){
					if(f.Count>=2){
						if(f[1].Contains("-")==true){
							f[1]=""+Mathf.Abs(float.Parse(f[1]));
						}
						else{
							f[1]="-"+f[1];
						}
						f.RemoveAt(0);
						remNEG=-1;
					}
				}
				else if(remNEG>0 && f[remNEG]=="-" && isNUM == false){
					if(f.Count>(remNEG+1)){
						if(f[remNEG+1].Contains("-")==true){
							f[remNEG+1]=""+Mathf.Abs(float.Parse(f[remNEG+1]));
						}
						else{
							f[remNEG+1]="-"+f[remNEG+1];
						}
						f.RemoveAt(remNEG);
						remNEG--;
					}
				}
			}
		}
		for (int j=0; j<operatorCount; j++) {
			for (int i=0; i<f.Count; i++) {
				//converts to neg
				if(exp==true){
					if(f[i]=="^"){
						float left = float.Parse(f[i-1]);
						float right= float.Parse(f[i+1]);
						float result = Mathf.Pow(left,right);
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
				}
				else if(modMultDiv==true){
					if(f[i]=="%"){
						float left = float.Parse(f[i-1]);
						float right= float.Parse(f[i+1]);
						float result = left%right;
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
					else if(f[i]=="*"){
						float left = float.Parse(f[i-1]);
						float right= float.Parse(f[i+1]);
						float result = left*right;
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
					else if(f[i]=="/"){
						float left = float.Parse(f[i-1]);
						float right= float.Parse(f[i+1]);
						float result = left/right;
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
				}
				else if(addSub==true){
					if(f[i]=="+"){
						float left = float.Parse(f[i-1]);
						float right = float.Parse(f[i+1]);
						float result = left+right;
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
					else if(f[i]=="-"){
						float left = float.Parse(f[i-1]);
						float right = float.Parse(f[i+1]);
						float result = left-right;
						f[i]=""+result;
						f.RemoveAt(i-1);
						f.RemoveAt(i);
						i-=1;
					}
				}
			}
			if(exp==true){
				exp=false;
			}
			else if(modMultDiv==true){
				modMultDiv=false;
			}
			else if(addSub==true){
				addSub=false;
			}
		}
		
		return float.Parse(f[0]);
	}
	
	private int getNumberOccurences(string needle, string hayStack){
		int toReturn = 0;
		for (int i=0; i<hayStack.Length; i++) {
			if(hayStack.Length>(i+needle.Length-1)){
				//Debug.Log ("Looking for : "+needle+", found: "+hayStack.Substring(i,needle.Length));
				if(hayStack.Substring(i,needle.Length)==needle){
					//Debug.Log ("SMRT");
					toReturn++;
				}
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
	
}

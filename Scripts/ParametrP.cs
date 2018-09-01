using UnityEngine;
using System.Collections;
using System.IO;

public class ParametrP : MonoBehaviour {

	int max;
	int i;
	int k=0;
	int a=0;
	char[] chislo;
	bool stop;
	public int LeftPix;
	public float P;
	float summ;
	float R;



	// Use this for initialization
	void Start () {
		string[] lines = System.IO.File.ReadAllLines (@"Assets\input.txt");
		max = System.Int32.Parse (lines [0]) +1;

		int[] X = new int[max];
		float[] Y = new float[max];

		
		for (i=1; i<max+1; i++) {
			char [] Line = lines[i].ToCharArray();
			chislo = new char[3];
			while(Line[k].ToString()!=" "){
				chislo[a]=Line[k];
				a++;
				k++;}
			if(Line[k].ToString()==" "){
				X[i] = (int)float.Parse(new string(chislo));
				if(X[i]==max-1){
					stop = true;
				}
				k++;
				a=0;}
			
			chislo = new char[11];
			while(k<Line.Length){
				chislo[a]=Line[k];
				a++;
				k++;}

			Y[i] = (float)float.Parse(new string(chislo));
			k=0;
			a=0;
			//Debug.Log(X[i]+"  "+Y[i]);
			if(stop){
				R=Y[i];
				i=max+1;
				stop = false;}
		}

		for (i=LeftPix; i<max; i++) {
			k=1;
			while(X[k]!=i){
				k++;
				if(k==max-1){
					stop = true;
					X[k]=i;
				}}
			if(stop){
				i++;
				stop = false;
			}else{
				summ+=Mathf.Pow((Y[k]-(((Mathf.Exp(P)-R)/(Mathf.Exp(P)-1))-(Mathf.Exp (P*i/(max-1)))*(1-R)/(Mathf.Exp(P)-1))),2);

			}

			}
		Debug.Log (summ);
	
	}
	

}

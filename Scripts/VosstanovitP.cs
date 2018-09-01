using UnityEngine;
using System.Collections;
using System.IO;

public class VosstanovitP : MonoBehaviour {

	public int left;
	public float P;
	public int max;
	public float R;
	int i;




	// Use this for initialization
	void Start () {
	
		StreamWriter str0 = new StreamWriter("output.txt");
		for (i=left; i<max; i++) {
			str0.WriteLine(i + " " + (((Mathf.Exp(P)-R)/(Mathf.Exp(P)-1))-(Mathf.Exp (P*i/(max-1)))*(1-R)/(Mathf.Exp(P)-1)));}
		str0.Close();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

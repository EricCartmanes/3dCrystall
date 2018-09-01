using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Collider[] hitCollider = Physics.OverlapSphere (transform.position, 1.5f);
		Debug.Log (hitCollider.Length);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.right);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate100 : MonoBehaviour {
	public GameObject sphere;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 200; i++) {
			for (int k = 0; k < 100; k++) {
				for (int j = 0; j < 100; j++) {
					GameObject site = Instantiate (sphere, new Vector3 (i, j, k), Quaternion.identity, gameObject.transform) as GameObject;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

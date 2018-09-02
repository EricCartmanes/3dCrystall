﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesat : MonoBehaviour {

	public GameObject visionSphere;
	private CrystalInfo crystalScript;

	// Use this for initialization
	void Start () {
		crystalScript = GetComponent<CrystalInfo> ();
		ShowGrowUpDimensions ();
		//Collider[] hitCollider = Physics.OverlapSphere (transform.position, 1.5f);
		//Debug.Log (hitCollider.Length);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.right);
	}

	void ShowGrowUpDimensions(){
		for(int y=-crystalScript.sizeDown;y<=crystalScript.sizeUp;y++){
			for(int z=-crystalScript.sizeInside;z<=crystalScript.sizeOutside;z++){
				Vector3 targetPoint = new Vector3(crystalScript.center.x+y*transform.up.x+z*transform.forward.x
					,crystalScript.center.y+y*transform.up.y+z*transform.forward.y
					,crystalScript.center.z+y*transform.up.z+z*transform.forward.z)
					+ new Vector3(transform.right.x*crystalScript.sizeRight,transform.right.y*crystalScript.sizeRight,transform.right.z*crystalScript.sizeRight)  + transform.right;
				//Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				GameObject LocalPoint = Instantiate (visionSphere, targetPoint, Quaternion.identity) as GameObject;
			}
		}
	}
}

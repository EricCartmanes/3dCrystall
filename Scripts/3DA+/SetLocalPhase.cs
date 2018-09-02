using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocalPhase : MonoBehaviour {

	//private Vector3 currentPos= new Vector3(50,100,100);
	private bool[,,] engageState = new bool[5,5,5];
	public GameObject localPoint;
	public GameObject parentForLocalState;
	public EnrgManager enrgManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void CheckArea(Vector3 pos){
		ClearMassive ();
		Collider[] hitCollider = Physics.OverlapBox (pos, new Vector3(2.1f,2.1f,2.1f));
		int i = 0;
		Debug.Log (hitCollider.Length);
		while (i < hitCollider.Length) {
			if (hitCollider [i].GetComponent<LocalStateInfo> ()) {
				ComparePos (hitCollider [i].transform.position, pos);
			}else if (hitCollider [i].GetComponent<CrystalInfo> ()) {
				CrystalInfo crystallScript = hitCollider [i].GetComponent<CrystalInfo> ();
				Debug.Log (crystallScript.center);
				for (int x = -crystallScript.sizeLeft; x <= crystallScript.sizeRight; x++) {
					for (int y = -crystallScript.sizeDown; y <= crystallScript.sizeUp; y++) {
						for (int z = -crystallScript.sizeInside; z <= crystallScript.sizeOutside; z++) {
							Vector3 targetPoint = new Vector3 (crystallScript.center.x + x * hitCollider [i].transform.right.x + y * hitCollider [i].transform.up.x + z * hitCollider [i].transform.forward.x
								, crystallScript.center.y + x * hitCollider [i].transform.right.y + y * hitCollider [i].transform.up.y + z * hitCollider [i].transform.forward.y
								, crystallScript.center.z + x * hitCollider [i].transform.right.z + y * hitCollider [i].transform.up.z + z * hitCollider [i].transform.forward.z);
							Debug.Log (targetPoint);
							ComparePos (targetPoint, pos);
						}
					}
				}
			}
			i++;
		}
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 5; y++) {
				for (int z = 0; z < 5; z++) {
					if (engageState [x, y, z] == false) {
						GameObject LocalPoint = Instantiate (localPoint, new Vector3 (x, y, z)+pos-new Vector3(2,2,2), Quaternion.identity) as GameObject;
						LocalPoint.transform.parent = parentForLocalState.transform;
						LocalPoint.GetComponent<LocalStateInfo> ().Enrg = enrgManager.LocalEnrg ();
					}
				}
			}
		}


	}

	void ComparePos(Vector3 crystalPoint, Vector3 currentPoint){
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 5; y++) {
				for (int z = 0; z < 5; z++) {
					if (crystalPoint == (currentPoint - new Vector3 (2, 2, 2))) {
						engageState [x, y, z] = true;
					}
				}
			}
		}
	}

	void ClearMassive(){
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 5; y++) {
				for (int z = 0; z < 5; z++) {
					engageState [x, y, z] = false;
				}
			}
		}
	}
}

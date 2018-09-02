using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

	public int startChance=50;
	public GameObject crystall;
	public GameObject parentForCrystall;
	private List<Transform> allCrystalObj = new List<Transform>();
	private List<CrystalInfo> allCrystalScript = new List<CrystalInfo>();
	public int growTime = 5;

	public EnrgManager enrgManager;
	public SetLocalPhase setLocalPhase;


	// Use this for initialization
	void Start () {
	
		for (int x = 0; x < 80; x++) {
			for (int y = 0; y < 80; y++) {
				for (int z = 0; z < 80; z++) {
					if ((int)Random.Range (0, startChance) == 1) {
						Collider[] hitCollider = Physics.OverlapSphere (new Vector3(x,y,z), 1.5f);
						if (hitCollider.Length == 0) {
							GameObject Cryst = Instantiate (crystall, new Vector3 (x, y, z), Quaternion.identity) as GameObject;
							Cryst.transform.parent = parentForCrystall.transform;

							allCrystalObj.Add (Cryst.transform);
							allCrystalScript.Add (Cryst.GetComponent<CrystalInfo> ());
							allCrystalScript[allCrystalScript.Count-1].center = new Vector3(x,y,z);
							allCrystalScript[allCrystalScript.Count-1].orientation = (int)Random.Range(0,3)+1;
						}
					}
				}
			}
		}
		FirstGrowStep ();
		for (int i = 0; i < growTime; i++) {
			GrowStep ();
		}
		//setLocalPhase.CheckArea (new Vector3(50, 100, 100));
	}

	void SetEnrg(){
		for (int i = 0; i < allCrystalScript.Count; i++) {
			if (!allCrystalScript [i].pointCrystal) {
				for(int x=-allCrystalScript [i].sizeLeft;x<=allCrystalScript [i].sizeRight;x++){
					for(int z=-allCrystalScript [i].sizeInside;z<=allCrystalScript [i].sizeOutside;z++){
						allCrystalScript [i].crystallEnrg.Add (enrgManager.CrystalEnrg());
					}
				}
			}else{
				//reset to local state
			}
		}

	}

	void FirstGrowStep(){
		int growSide = 1;
		for (int i = 0; i < allCrystalScript.Count; i++) {
			if (allCrystalScript [i].orientation == 1) {
				if ((int)Random.Range (0, 2) == 1) {
					growSide = 1;
				} else {
					growSide = -1;
				}
				Collider[] hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y + growSide, allCrystalScript [i].center.z), 1.5f);
				if (hitCollider.Length == 1) {
					allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
					allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y + 0.5f * growSide, allCrystalScript [i].center.z);
					allCrystalScript [i].sizeUp = 1;
				} else {
					growSide = growSide * (-1);
					hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y + growSide, allCrystalScript [i].center.z), 1.5f);
					if (hitCollider.Length == 1) {
						allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
						allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y + 0.5f * growSide, allCrystalScript [i].center.z);
						allCrystalScript [i].sizeDown = 1;
					} else {
						allCrystalScript [i].pointCrystal = true;
					}
				}

			} else if (allCrystalScript [i].orientation == 2) {
				allCrystalObj [i].localEulerAngles = new Vector3 (0, 0, -90f);
				if ((int)Random.Range (0, 2) == 1) {
					growSide = 1;
				} else {
					growSide = -1;
				}
				Collider[] hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x + growSide, allCrystalScript [i].center.y, allCrystalScript [i].center.z), 1.5f);
				if (hitCollider.Length == 1) {
					allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
					allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x + 0.5f * growSide, allCrystalScript [i].center.y, allCrystalScript [i].center.z);
					allCrystalScript [i].sizeUp = 1;
				} else {
					growSide = growSide * (-1);
					hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x + growSide, allCrystalScript [i].center.y, allCrystalScript [i].center.z), 1.5f);
					if (hitCollider.Length == 1) {
						allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
						allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x + 0.5f * growSide, allCrystalScript [i].center.y, allCrystalScript [i].center.z);
						allCrystalScript [i].sizeDown = 1;
					} else {
						allCrystalScript [i].pointCrystal = true;
					}
				}
			} else if (allCrystalScript [i].orientation == 3) {
				allCrystalObj [i].localEulerAngles = new Vector3 (90, 0, 0);
				if ((int)Random.Range (0, 2) == 1) {
					growSide = 1;
				} else {
					growSide = -1;
				}
				Collider[] hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y, allCrystalScript [i].center.z + growSide), 1.5f);
				if (hitCollider.Length == 1) {
					allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
					allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y, allCrystalScript [i].center.z + 0.5f * growSide);
					allCrystalScript [i].sizeUp = 1;
				} else {
					growSide = growSide * (-1);
					hitCollider = Physics.OverlapSphere (new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y, allCrystalScript [i].center.z + growSide), 1.5f);
					if (hitCollider.Length == 1) {
						allCrystalObj [i].localScale = new Vector3 (0.1f, 0.6f, 0.1f);
						allCrystalObj [i].position = new Vector3 (allCrystalScript [i].center.x, allCrystalScript [i].center.y, allCrystalScript [i].center.z + 0.5f * growSide);
						allCrystalScript [i].sizeDown = 1;
					} else {
						allCrystalScript [i].pointCrystal = true;
					}
				}
			}
		}
	}

	void GrowStep(){
		for (int i = 0; i < allCrystalScript.Count; i++) {
			if (!allCrystalScript [i].pointCrystal) {
				RandomDimension (i);
			}
		}
	}

	void RandomDimension(int i){
		bool checkUp = false;
		bool checkRight = false;
		bool checkInside = false;
		int growDemension = (int)Random.Range (0, 3) + 1;
		if (growDemension == 1) {
			if (TryGrowUp (i)) {return;}
			checkUp = true;
		}
		if (growDemension == 2) {
			if (TryGrowRight (i)) {return;}
			checkRight = true;
		}
		if (growDemension == 3) {
			if (TryGrowOutside (i)) {return;}
			checkInside = true;
		}

		int newGrowDemension = 1;
		while (newGrowDemension == growDemension) {
			newGrowDemension = (int)Random.Range (0, 3) + 1;
		}
		if (newGrowDemension == 1) {
			if (TryGrowUp (i)) {return;}
			checkUp = true;
		}
		if (newGrowDemension == 2) {
			if (TryGrowRight (i)) {return;}
			checkRight = true;
		}
		if (newGrowDemension == 3) {
			if (TryGrowOutside (i)) {return;}
			checkInside = true;
		}

		if ((growDemension + newGrowDemension)==5) {
			if (TryGrowUp (i)) {return;}
			checkUp = true;
		}
		if ((growDemension + newGrowDemension)==4) {
			if (TryGrowRight (i)) {return;}
			checkRight = true;
		}
		if ((growDemension + newGrowDemension)==3) {
			if (TryGrowOutside (i)) {return;}
			checkInside = true;
		}
	}

	bool TryGrowUp(int i){
		int growSide = 1;
		if ((int)Random.Range (0, 2) == 1) {
			growSide = 1;
		} else {
			growSide = -1;
		}

		if (growSide == 1) {
			if (CheckGrowUp (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y+0.5f, L.z);
				allCrystalScript [i].sizeUp++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowDown (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y+0.5f, L.z);
				allCrystalScript [i].sizeDown++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		} else {
			if (CheckGrowDown (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y+0.5f, L.z);
				allCrystalScript [i].sizeDown++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowUp (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y+0.5f, L.z);
				allCrystalScript [i].sizeUp++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	bool CheckGrowUp(int i){
		for(int x=-allCrystalScript [i].sizeLeft;x<=allCrystalScript [i].sizeRight;x++){
			for(int z=-allCrystalScript [i].sizeInside;z<=allCrystalScript [i].sizeOutside;z++){
				
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+x*allCrystalObj [i].right.x+z*allCrystalObj [i].forward.x
													,allCrystalScript [i].center.y+x*allCrystalObj [i].right.y+z*allCrystalObj [i].forward.y
													,allCrystalScript [i].center.z+x*allCrystalObj [i].right.z+z*allCrystalObj [i].forward.z)
					+ new Vector3(allCrystalObj [i].up.x*allCrystalScript [i].sizeUp,allCrystalObj [i].up.y*allCrystalScript [i].sizeUp,allCrystalObj [i].up.z*allCrystalScript [i].sizeUp)  + allCrystalObj [i].up;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}
	bool CheckGrowDown(int i){
		for(int x=-allCrystalScript [i].sizeLeft;x<=allCrystalScript [i].sizeRight;x++){
			for(int z=-allCrystalScript [i].sizeInside;z<=allCrystalScript [i].sizeOutside;z++){
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+x*allCrystalObj [i].right.x+z*allCrystalObj [i].forward.x
													,allCrystalScript [i].center.y+x*allCrystalObj [i].right.y+z*allCrystalObj [i].forward.y
													,allCrystalScript [i].center.z+x*allCrystalObj [i].right.z+z*allCrystalObj [i].forward.z)
					- new Vector3(allCrystalObj [i].up.x*allCrystalScript [i].sizeDown,allCrystalObj [i].up.y*allCrystalScript [i].sizeDown,allCrystalObj [i].up.z*allCrystalScript [i].sizeDown) - allCrystalObj [i].up;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}

	//=========================================================================================

	bool TryGrowRight(int i){
		int growSide = 1;
		if ((int)Random.Range (0, 2) == 1) {
			growSide = 1;
		} else {
			growSide = -1;
		}

		if (growSide == 1) {
			if (CheckGrowRight (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x+1, L.y, L.z);
				allCrystalScript [i].sizeRight++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowLeft (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x+1, L.y, L.z);
				allCrystalScript [i].sizeLeft++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		} else {
			if (CheckGrowLeft (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x+1, L.y, L.z);
				allCrystalScript [i].sizeLeft++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowRight (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x+1, L.y, L.z);
				allCrystalScript [i].sizeRight++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	bool CheckGrowRight(int i){
		for(int y=-allCrystalScript [i].sizeDown;y<=allCrystalScript [i].sizeUp;y++){
			for(int z=-allCrystalScript [i].sizeInside;z<=allCrystalScript [i].sizeOutside;z++){
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+y*allCrystalObj [i].up.x+z*allCrystalObj [i].forward.x
					,allCrystalScript [i].center.y+y*allCrystalObj [i].up.y+z*allCrystalObj [i].forward.y
					,allCrystalScript [i].center.z+y*allCrystalObj [i].up.z+z*allCrystalObj [i].forward.z)
					+ new Vector3(allCrystalObj [i].right.x*allCrystalScript [i].sizeRight,allCrystalObj [i].right.y*allCrystalScript [i].sizeRight,allCrystalObj [i].right.z*allCrystalScript [i].sizeRight)  + allCrystalObj [i].right;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}
	bool CheckGrowLeft(int i){
		for(int y=-allCrystalScript [i].sizeDown;y<=allCrystalScript [i].sizeUp;y++){
			for(int z=-allCrystalScript [i].sizeInside;z<=allCrystalScript [i].sizeOutside;z++){
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+y*allCrystalObj [i].up.x+z*allCrystalObj [i].forward.x
					,allCrystalScript [i].center.y+y*allCrystalObj [i].up.y+z*allCrystalObj [i].forward.y
					,allCrystalScript [i].center.z+y*allCrystalObj [i].up.z+z*allCrystalObj [i].forward.z)
					- new Vector3(allCrystalObj [i].right.x*allCrystalScript [i].sizeRight,allCrystalObj [i].right.y*allCrystalScript [i].sizeRight,allCrystalObj [i].right.z*allCrystalScript [i].sizeRight)  - allCrystalObj [i].right;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}


	//=========================================================================================

	bool TryGrowOutside(int i){
		int growSide = 1;
		if ((int)Random.Range (0, 2) == 1) {
			growSide = 1;
		} else {
			growSide = -1;
		}

		if (growSide == 1) {
			if (CheckGrowOutside (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y, L.z+1);
				allCrystalScript [i].sizeInside++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowInside (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y, L.z+1);
				allCrystalScript [i].sizeOutside++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		} else {
			if (CheckGrowInside (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y, L.z+1);
				allCrystalScript [i].sizeOutside++;
				RescaleCrystal (i);
				return true;
			} else if (CheckGrowOutside (i)) {
				Vector3 L = allCrystalObj [i].localScale;
				allCrystalObj [i].localScale = new Vector3 (L.x, L.y, L.z+1);
				allCrystalScript [i].sizeInside++;
				RescaleCrystal (i);
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	bool CheckGrowInside(int i){
		for(int x=-allCrystalScript [i].sizeLeft;x<=allCrystalScript [i].sizeRight;x++){
			for(int y=-allCrystalScript [i].sizeDown;y<=allCrystalScript [i].sizeUp;y++){
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+y*allCrystalObj [i].up.x+x*allCrystalObj [i].right.x
					,allCrystalScript [i].center.y+y*allCrystalObj [i].up.y+x*allCrystalObj [i].right.y
					,allCrystalScript [i].center.z+y*allCrystalObj [i].up.z+x*allCrystalObj [i].right.z)
					+ new Vector3(allCrystalObj [i].forward.x*allCrystalScript [i].sizeInside,allCrystalObj [i].forward.y*allCrystalScript [i].sizeInside,allCrystalObj [i].forward.z*allCrystalScript [i].sizeInside)  + allCrystalObj [i].forward;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}
	bool CheckGrowOutside(int i){
		for(int x=-allCrystalScript [i].sizeLeft;x<=allCrystalScript [i].sizeRight;x++){
			for(int y=-allCrystalScript [i].sizeDown;y<=allCrystalScript [i].sizeUp;y++){
				Vector3 targetPoint = new Vector3(allCrystalScript [i].center.x+y*allCrystalObj [i].up.x+x*allCrystalObj [i].right.x
					,allCrystalScript [i].center.y+y*allCrystalObj [i].up.y+x*allCrystalObj [i].right.y
					,allCrystalScript [i].center.z+y*allCrystalObj [i].up.z+x*allCrystalObj [i].right.z)
					- new Vector3(allCrystalObj [i].forward.x*allCrystalScript [i].sizeInside,allCrystalObj [i].forward.y*allCrystalScript [i].sizeInside,allCrystalObj [i].forward.z*allCrystalScript [i].sizeInside)  - allCrystalObj [i].forward;
				Collider[] hitCollider = Physics.OverlapSphere (targetPoint, 1.5f);
				if (hitCollider.Length > 1) {
					return false;
				}
			}
		}
		return true;
	}

	void RescaleCrystal(int i){
		Vector3 V3 = allCrystalObj [i].position;
		float offsetX = (allCrystalScript [i].sizeRight - allCrystalScript [i].sizeLeft) * 0.5f;
		float offsetY = (allCrystalScript [i].sizeUp - allCrystalScript [i].sizeDown) * 0.5f;
		float offsetZ = (allCrystalScript [i].sizeOutside - allCrystalScript [i].sizeInside) * 0.5f;
		V3 = new Vector3 (allCrystalScript [i].center.x + allCrystalObj [i].right.x * offsetX + allCrystalObj [i].up.x * offsetY + allCrystalObj [i].forward.x * offsetZ,
			allCrystalScript [i].center.y + allCrystalObj [i].right.y * offsetX + allCrystalObj [i].up.y * offsetY + allCrystalObj [i].forward.y * offsetZ,
			allCrystalScript [i].center.z + allCrystalObj [i].right.z * offsetX + allCrystalObj [i].up.z * offsetY + allCrystalObj [i].forward.z * offsetZ);
		allCrystalObj [i].transform.position = V3;
	}

	// Update is called once per frame
	void Update () {
		
	}


}

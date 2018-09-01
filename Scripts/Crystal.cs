using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Crystal : MonoBehaviour {

	public Texture2D textureNew;		//1800x1800
	public Camera MainCamera;
	private Color black = Color.black;
	private Color red = Color.red;
	private Color green = Color.green;
	private Color grey = Color.grey;
	private int X = 1000;
	private int Y = 1000;
	private float XCamera;
	private float YCamera;
	private float XCameraOld;
	private float YCameraOld;
	private int a=0;
	private int b=0;
	private float c=0;
	private float d=0;
	private int i;
	private Vector2 oldPixel;
	//private float[,] Enrg = new float[2000,2000];
	private int[,] crystalStart = new int[200,200];
	private float[,] crystalStartTime = new float[200,200];
	private int[,] crystalSize = new int[200,200];
	private int[,] crystalID = new int[200,200];
	private int[,] crystalSizeUp = new int[200,200];
	private int[,] crystalSizeDown = new int[200,200];
	private int[,] crystalSizeLeft = new int[200,200];
	private int[,] crystalSizeRight = new int[200,200];
	private float[,] Enrg = new float[200,200];
	private int chance = 50;
	private int growType = 0;
		//0 - same start time
	private int crystalType = 1;
		//0 - square
		//1 - random grow rectangle
	private int growSide=0;
		//0 - up
		//1 - down
		//2 - left
		//3 - right
	private bool growTimeLimit = true;
	private int timeLimit = 5;
	private int currentTimeStep = 1;
	private int clasterNumb = 0;
	private int growTimeDiff = 10;
	private bool canUp = true;
	private bool canDown = true;
	private bool canLeft = true;
	private bool canRight = true;
	private bool colorCentre = false;
	public GameObject densityTextUI;
	public GameObject densityUI;
	public GameObject growtimeTypeUI;
	public GameObject gausFactorUI;
	public GameObject gausFactorTextUI;
	public GameObject growtimeLimitUI;
	public GameObject growtimeLimitTextUI;
	public GameObject crystalTypeUI;
	public GameObject canvasBackUI;
	public GameObject stepByStepUI;
	public GameObject renderFullUI;
	public GameObject resetCameraUI;
	public GameObject EtTextUI;
	public GameObject EtUI;
	public GameObject sigmCrystTextUI;
	public GameObject sigmCrystUI;
	public GameObject colorCentreUI;


	private float maxEnrg;
	private float kT = 0.25f;
	private float sigma = 2.13f;
	private float limitGEnrg = 6;
	private float Et;
	private float sigmaCrystal=0.5f;

	private float[] centreNumb = new float[350];
	private float[] chainNumb = new float[32];
	private float[] lenghtChain = new float[32];
	private float[,] centreNumbGlobal = new float[350,2000];
	private float[,] chainNumbGlobal = new float[32,2000];
	private float[,] lenghtChainGlobal = new float[32,2000];
	private bool makeStat = false;
	private int statStep = 0;
	private int statStepLimit=1000;


	// Use this for initialization
	void Start () {
		maxEnrg = limitGEnrg * sigma * kT;
		Et = 1f*sigma;

		stepByStepUI.GetComponent<Button> ().onClick.AddListener (MakeStep);
		renderFullUI.GetComponent<Button> ().onClick.AddListener (RenderFull);
		resetCameraUI.GetComponent<Button> ().onClick.AddListener (ResetCamera);
		colorCentreUI.GetComponent<Toggle> ().onValueChanged.AddListener ((colorCentre)  => {
			reColorCentre();

		});
		//chance = (int)density.GetComponent<Slider> ().value;


		RenderFull ();



	}

	// Update is called once per frame
	void Update () {
		densityTextUI.GetComponent<Text>().text = "Crystal density 1/" + ((int)densityUI.GetComponent<Slider> ().value);
		if (growtimeTypeUI.GetComponent<Dropdown> ().value == 1) {
			gausFactorTextUI.transform.SetParent (growtimeTypeUI.transform);
		} else {
			gausFactorTextUI.transform.SetParent (canvasBackUI.transform);}
		gausFactorTextUI.GetComponent<Text>().text = "Distibute in " + ((int)gausFactorUI.GetComponent<Slider> ().value) + " times";
		growtimeLimitTextUI.GetComponent<Text>().text = "Growtime limit " + ((int)growtimeLimitUI.GetComponent<Slider> ().value);
		stepByStepUI.GetComponentInChildren<Text> ().text = "Step by step " + (currentTimeStep-1) + "/" + timeLimit;
		EtTextUI.GetComponentInChildren<Text> ().text = "Et = " + EtUI.GetComponent<Slider> ().value + " σ";
		sigmCrystTextUI.GetComponentInChildren<Text> ().text = "σ crystal = " + sigmCrystUI.GetComponent<Slider> ().value + " σ";

		if (makeStat) {
			Statistic ();
			for (int x = 0; x < 30; x++) {
				chainNumbGlobal [x, statStep] = chainNumb [x];
				lenghtChainGlobal [x, statStep] = lenghtChain [x];
			}
			for (int x = 0; x < 300; x++) {
				centreNumbGlobal [x,statStep] = centreNumb[x];
			}
			Debug.Log (statStep);
			statStep++;
			RenderFull ();
			if (statStep >= statStepLimit) {
				makeStat = false;
				for (int i = 0; i < statStepLimit-1; i++) {
					for (int x = 0; x < 30; x++) {
						chainNumb [x] += chainNumbGlobal [x, i];
						lenghtChain [x] += lenghtChainGlobal [x, i];
					}
					for (int x = 0; x < 300; x++) {
						centreNumb [x] += centreNumbGlobal [x, i];
					}
				}
				StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"___STAT_timeLimit="+timeLimit+".txt");
				str1.WriteLine("поле размером 100х100");
				str1.WriteLine("\t");
				str1.WriteLine("Распределение длин цепей\t");
				for (int x = 0; x < 30; x++) {
					str1.WriteLine("\tЦепей длиной "+x+" - "+(float)(lenghtChain[x]/statStepLimit)+">\t");
				}
				str1.WriteLine("\t\tРаспределение ширины кристаллов(количество цепей)\t");
				for (int x = 0; x < 30; x++) {
					str1.WriteLine("\tкрисстлаов ширины "+x+" - "+(float)(chainNumb[x]/statStepLimit)+">\t");
				}
				str1.WriteLine("Кристалоов с узлами\t");
				for (int x = 0; x < 300; x++) {
					str1.WriteLine("\tкрисстлаов с "+x+" узлами - "+(float)(centreNumb[x]/statStepLimit)+">\t");
				}
				str1.Close();
			}
		}

		if (Input.mouseScrollDelta.y != 0) {
			if (MainCamera.orthographicSize > 0.15f ){
				if(Input.mouseScrollDelta.y>0){
					MainCamera.orthographicSize -= 0.1f;}}
			if(MainCamera.orthographicSize < 3.45f){
				if(Input.mouseScrollDelta.y<0){
					MainCamera.orthographicSize += 0.1f;}}

		}
		if (Input.GetMouseButton (0)) {
			XCamera = -(Input.mousePosition.x - XCameraOld)*0.005f;
			YCamera = -(Input.mousePosition.y - YCameraOld)*0.005f;
			//Debug.Log(Input.mousePosition.x);
			if(Input.mousePosition.x>230){
				MainCamera.transform.position = new Vector3(MainCamera.transform.position.x+XCamera,MainCamera.transform.position.y+YCamera,-10);}
		}

		XCameraOld = Input.mousePosition.x;
		YCameraOld = Input.mousePosition.y;


	}

	void Statistic(){
		for (int x = 0; x < 30; x++) {
			chainNumb [x] = 0;
			lenghtChain [x] = 0;
		}
		for (int x = 0; x < 300; x++) {
			centreNumb [x] = 0;
		}
		for (int x = 35; x <= 135; x++) {
			for (int y = 35; y <= 135; y++) {
				if (crystalStart [x, y] != 0) {
					int a = 1;
					int b = 1;
					if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) != 0) {
						a = crystalSizeLeft [x, y] + crystalSizeRight [x, y] + 1;
					}
					if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) != 0) {
						b = crystalSizeUp [x, y] + crystalSizeDown [x, y] + 1;
					}
					centreNumb [a*b]++;
					

					if (crystalStart[x,y] == 1) {
						if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) == 0) {
							chainNumb [0]++;
							lenghtChain [0]++;
						} else {
							lenghtChain[(crystalSizeUp [x, y] + crystalSizeDown [x, y])]++;
							if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) != 0) {
								chainNumb [(crystalSizeLeft [x, y] + crystalSizeRight [x, y])]++;
							} else {
								chainNumb [1]++;
							}
						}
					} else {
						if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) == 0) {
							chainNumb [0]++;
							lenghtChain [0]++;
						} else {
							lenghtChain [(crystalSizeLeft [x, y] + crystalSizeRight [x, y])]++;
							if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) != 0) {
								chainNumb [(crystalSizeUp [x, y] + crystalSizeDown [x, y])]++;
							} else {
								chainNumb [1]++;
							}
						}
					}
				}
			}
		}
	}

	void NewSpace(){
		ClearTexture ();
		clasterNumb = 1;
		for (int x = 0; x <= 199; x++) {
			for (int y = 0; y <= 199; y++) {
				crystalStart [x, y] = 0;
				crystalStartTime[x,y] = 0;
				crystalSize[x,y] = 0;
				crystalID[x,y] = 0;
				crystalSizeUp[x,y] = 0;
				crystalSizeDown[x,y] = 0;
				crystalSizeLeft[x,y] = 0;
				crystalSizeRight[x,y] = 0;
				//Enrg [x, y] = 0;
			}
		}
		for(int x=20;x<=150; x++){
			for(int y=20;y<=150; y++){
				if (RandomNormal(chance)
					&& crystalStart [x-1, y+1]==0 && crystalStart [x, y+1]==0 && crystalStart [x+1, y+1]==0
					&& crystalStart [x-1, y]==0 && crystalStart [x+1, y]==0
					&& crystalStart [x-1, y-1]==0 && crystalStart [x, y-1]==0 && crystalStart [x+1, y-1]==0) {
					
					clasterNumb++;
					crystalStart [x, y] = (int)Random.Range(0,2)+1;		//orientation
					crystalSize[x,y] = 0;
					crystalID[x,y] = clasterNumb;
					Enrg [x, y] = RndEnrg1Crystal () - Et;
					DrowPixelCentre (x, y, ColorFormEnrg(x,y));
					if (growType == 1) {
						crystalStartTime [x, y] = (5f + Gauss ())/10f*((float)growTimeDiff);
					}
					if (crystalType == 1) {
						crystalSizeUp [x, y] = 0;
						crystalSizeDown[x, y] = 0;
						crystalSizeLeft[x, y] = 0;
						crystalSizeRight[x, y] = 0;
					}
				} else {
					Enrg [x, y] = RndEnrg1 ();
					if (!colorCentre) {
						DrowPixel (x, y, black);
					} else {
						DrowPixel (x, y, ColorFormEnrg (x, y));
					}
				}
			}
		}
		currentTimeStep = 0;
	}
	void Step(){
		if (growType == 0) {
			for (int x = 20; x <= 155; x++) {
				for (int y = 20; y <= 150; y++) {
					if (crystalStart [x, y] != 0) {
						CheckGrowSpace (x, y);
					}
				}
			}
		}
		if (growType == 1) {
			for (int x = 20; x <= 155; x++) {
				for (int y = 20; y <= 150; y++) {
					if (crystalStart [x, y] != 0) {
						if (currentTimeStep >= crystalStartTime [x, y]) {
							CheckGrowSpace (x, y);
						}
					}
				}
			}

		}
		currentTimeStep++;

	}

	bool CheckChange(){
		bool change = false;
		if (chance!=(int)densityUI.GetComponent<Slider> ().value) {
			change = true;
		}
		if (growType!=growtimeTypeUI.GetComponent<Dropdown>().value) {
			change = true;
		}
		if (growTimeDiff!=(int)gausFactorUI.GetComponent<Slider> ().value) {
			change = true;
		}
		if (timeLimit!=(int)growtimeLimitUI.GetComponent<Slider> ().value) {
			change = true;
		}
		if (crystalType!=crystalTypeUI.GetComponent<Dropdown>().value) {
			change = true;
		}
		if (Et!=EtUI.GetComponent<Slider>().value) {
			change = true;
		}
		if (sigmaCrystal!=sigmCrystUI.GetComponent<Slider>().value) {
			change = true;
		}
		chance = (int)densityUI.GetComponent<Slider> ().value;
		growType = growtimeTypeUI.GetComponent<Dropdown> ().value;
		growTimeDiff = (int)gausFactorUI.GetComponent<Slider> ().value;
		timeLimit = (int)growtimeLimitUI.GetComponent<Slider> ().value;
		crystalType = crystalTypeUI.GetComponent<Dropdown> ().value;
		Et = EtUI.GetComponent<Slider> ().value;
		sigmaCrystal = sigmCrystUI.GetComponent<Slider> ().value;
		return change;
	}

	void reColorCentre(){
		if (colorCentre) {
			colorCentre = false;
		} else {
			colorCentre = true;
		}

		for (int x = 20; x <= 150; x++) {
			for (int y = 20; y <= 150; y++) {
				if (crystalID [x, y] == 0) {
					if (colorCentre) {
						DrowPixel(x,y,ColorFormEnrg(x,y));
					}else{
						DrowPixel(x,y,black);
					}
				}
			}
		}
		textureNew.Apply();
	}

	void ResetCamera(){
		MainCamera.transform.position = new Vector3 (0,0,-10);
		MainCamera.orthographicSize = 2f;
	}
	void MakeStep(){
		if (currentTimeStep > timeLimit || CheckChange ()) {
			NewSpace ();
			currentTimeStep++;
		} else {
			//currentTimeStep++;
			Step ();

		}
		textureNew.Apply ();
	}
	void RenderFull(){
		if (CheckChange ()) {
		}
		NewSpace ();
		while (currentTimeStep <= timeLimit) {
			Step ();
		}
		//textureNew.Apply();
	}

	void CheckGrowSpace(int X, int Y){
		bool canGrow = true;
		int size = crystalSize [X, Y]+1;
		canUp = true;
		canDown = true;
		canLeft = true;
		canRight = true;

		if (crystalType == 0) {
			for (int i = X - size; i <= X + size; i++) {
				if (crystalID [i, Y + size+1] != 0) {
					canGrow = false;
				}
				if (crystalID [i, Y + size] != 0) {
					canGrow = false;
				}
				if (crystalID [i, Y - size-1] != 0) {
					canGrow = false;
				}
				if (crystalID [i, Y - size] != 0) {
					canGrow = false;
				}
			}
			for (int i = Y - size; i <= Y + size; i++) {
				if (crystalID [X + size+1, i] != 0) {
					canGrow = false;
				}
				if (crystalID [X + size, i] != 0) {
					canGrow = false;
				}
				if (crystalID [X - size-1, i] != 0) {
					canGrow = false;
				}
				if (crystalID [X - size, i] != 0) {
					canGrow = false;
				}
			}
			if (canGrow) {
				for (int i = X - size; i <= X + size; i++) {
					crystalID [i, Y + size] = crystalID [X, Y];
					crystalID [i, Y - size] = crystalID [X, Y];
					DrowPixel (i, Y + size, ColorFormEnrg(X,Y));
					DrowPixel (i, Y - size, ColorFormEnrg(X,Y));
					if (crystalStart [X, Y] == 1) {
						ConnectPixel (i, Y + size, i, Y + size - 1);
						ConnectPixel (i, Y - size, i, Y - size + 1);
					}
				}
				for (int i = Y - size; i <= Y + size; i++) {
					crystalID [X + size, i] = crystalID [X, Y];
					crystalID [X - size, i] = crystalID [X, Y];
					DrowPixel (X + size, i, ColorFormEnrg(X,Y));
					DrowPixel (X - size, i, ColorFormEnrg(X,Y));
					if (crystalStart [X, Y] == 2) {
						ConnectPixel (X + size, i, X + size - 1, i);
						ConnectPixel (X - size, i, X - size + 1, i);
					}
				}
				crystalSize [X, Y]++;
				OrientCrystal (X, Y);
			}
		}
		if (crystalType == 1) {
			if (size == 1) {
				if (crystalStart [X, Y] == 1) {
					if ((int)Random.Range (0, 2) == 1) {
						if (crystalID [X, Y + 1] == 0) {
							growSide = 0;
						} else if (crystalID [X, Y - 1] == 0) {
							growSide = 1;
						} else {
							canGrow = false;
						}
						if (crystalID [X, Y + 2] == 0) {
							growSide = 0;
						} else if (crystalID [X, Y - 2] == 0) {
							growSide = 1;
						} else {
							canGrow = false;
						}
					} else {
						if (crystalID [X, Y - 1] == 0) {
							growSide = 1;
						} else if (crystalID [X, Y + 1] == 0) {
							growSide = 0;
						} else {
							canGrow = false;
						}
						if (crystalID [X, Y - 2] == 0) {
							growSide = 1;
						} else if (crystalID [X, Y + 2] == 0) {
							growSide = 0;
						} else {
							canGrow = false;
						}
					}
				}
				if (crystalStart [X, Y] == 2) {
					if (RandomNormal (2)) {
						if (crystalID [X + 1, Y] == 0) {
							growSide = 3;
						} else if (crystalID [X - 1, Y] == 0) {
							growSide = 2;
						} else {
							canGrow = false;
						}
						if (crystalID [X + 2, Y] == 0) {
							growSide = 3;
						} else if (crystalID [X - 2, Y] == 0) {
							growSide = 2;
						} else {
							canGrow = false;
						}
					} else {
						if (crystalID [X - 1, Y] == 0) {
							growSide = 2;
						} else if (crystalID [X + 1, Y] == 0) {
							growSide = 3;
						} else {
							canGrow = false;
						}
						if (crystalID [X - 2, Y] == 0) {
							growSide = 2;
						} else if (crystalID [X + 2, Y] == 0) {
							growSide = 3;
						} else {
							canGrow = false;
						}
					}
				}
			} else if (size != 0) {

				int randomSide = CheckRandomSide ();
				if (CheckGrowSide (X, Y, randomSide)) {
					growSide = randomSide;
				} else {
					randomSide = CheckRandomSide ();
					if (CheckGrowSide (X, Y, randomSide)) {
						growSide = randomSide;
					} else {
						randomSide = CheckRandomSide ();
						if (CheckGrowSide (X, Y, randomSide)) {
							growSide = randomSide;
						} else {
							randomSide = CheckRandomSide ();
							if (CheckGrowSide (X, Y, randomSide)) {
								growSide = randomSide;
							} else {
								canGrow = false;
							}
						}
					}		
				}
			}

			if (canGrow) {
				if (growSide == 0) {
					for (int i = X - crystalSizeLeft [X, Y]; i <= X + crystalSizeRight [X, Y]; i++) {
						crystalID [i, Y + crystalSizeUp[X,Y] + 1] = crystalID [X, Y];
						DrowPixel (i, Y + crystalSizeUp[X,Y] + 1, ColorFormEnrg(X,Y));
					}
					crystalSizeUp [X, Y]++;
				}
				if (growSide == 1) {
					for (int i = X - crystalSizeLeft [X, Y]; i <= X + crystalSizeRight [X, Y]; i++) {
						crystalID [i, Y - crystalSizeDown[X,Y] - 1] = crystalID [X, Y];
						DrowPixel (i, Y - crystalSizeDown[X,Y] - 1, ColorFormEnrg(X,Y));
					}
					crystalSizeDown [X, Y]++;
				}
				if (growSide == 2) {
					for (int i = Y - crystalSizeDown [X, Y]; i <= Y + crystalSizeUp [X, Y]; i++) {
						crystalID [X-crystalSizeLeft[X,Y]-1, i] = crystalID [X, Y];
						DrowPixel (X-crystalSizeLeft[X,Y]-1, i, ColorFormEnrg(X,Y));
					}
					crystalSizeLeft [X, Y]++;
				}
				if (growSide == 3) {
					for (int i = Y - crystalSizeDown [X, Y]; i <= Y + crystalSizeUp [X, Y]; i++) {
						crystalID [X+crystalSizeRight[X,Y]+1, i] = crystalID [X, Y];
						DrowPixel (X+crystalSizeRight[X,Y]+1, i, ColorFormEnrg(X,Y));
					}
					crystalSizeRight [X, Y]++;
				}
				crystalSize [X, Y]++;
				OrientCrystal (X, Y);
			}
		}
	}

	private bool CheckGrowSide(int X, int Y,int side){
		if (side == 0) {
			for (int i = X - crystalSizeLeft [X, Y]-1; i <= X + crystalSizeRight [X, Y]+1; i++) {
				if (crystalID [i, Y + crystalSizeUp [X, Y] + 1] != 0) {
					return false;}
				if (crystalID [i, Y + crystalSizeUp [X, Y] + 2] != 0) {
					return false;}
			}
			return true;
		}
		if (side == 1) {
			for (int i = X - crystalSizeLeft [X, Y]-1; i <= X + crystalSizeRight [X, Y]+1; i++) {
				if (crystalID [i, Y - crystalSizeDown [X, Y] - 1] != 0) {
					return false;}
				if (crystalID [i, Y - crystalSizeDown [X, Y] - 2] != 0) {
					return false;}
			}
			return true;
		}
		if (side == 2) {
			for (int i = Y - crystalSizeDown [X, Y]-1; i <= Y + crystalSizeUp [X, Y]+1; i++) {
				if (crystalID [X-crystalSizeLeft[X,Y]-1, i] != 0) {
					return false;}
				if (crystalID [X-crystalSizeLeft[X,Y]-2, i] != 0) {
					return false;}
			}
			return true;
		}
		if (side == 3) {
			for (int i = Y - crystalSizeDown [X, Y]-1; i <= Y + crystalSizeUp [X, Y]+1; i++) {
				if (crystalID [X+crystalSizeRight[X,Y]+1, i] != 0) {
					return false;}
				if (crystalID [X+crystalSizeRight[X,Y]+2, i] != 0) {
					return false;}
			}
			return true;
		}
		return false;
	}


	private int CheckRandomSide(){
		while (2 > 1) {
			int rand = (int)Random.Range (0, 4);
			if (rand == 0) {
				if (canUp) {
					canUp = false;
					return 0;
				}
			}
			if (rand == 1) {
				if (canDown) {
					canDown = false;
					return 1;
				}
			}
			if (rand == 2) {
				if (canLeft) {
					canLeft = false;
					return 2;
				}
			}
			if (rand == 3) {
				if (canRight) {
					canRight = false;
					return 3;
				}
			}
		}
	}

	void OrientCrystal(int X, int Y){
		int size = crystalSize [X, Y];

		if (size != 0) {
			if (crystalType == 0) {
				if (crystalStart [X, Y] == 1) {
					for (int i = X - size; i <= X + size; i++) {
						ConnectPixel (i, Y + size, i, Y - size);
					}
				}
				if (crystalStart [X, Y] == 2) {
					for (int i = Y - size; i <= Y + size; i++) {
						ConnectPixel (X+size, i, X-size, i);
					}
				}
			}
			if (crystalType == 1) {
				if (crystalStart [X, Y] == 1) {
					for (int i = X - crystalSizeLeft[X,Y]; i <= X + crystalSizeRight[X,Y]; i++) {
						ConnectPixel (i, Y + crystalSizeUp[X,Y], i, Y - crystalSizeDown[X,Y]);
					}
				}
				if (crystalStart [X, Y] == 2) {
					for (int i = Y - crystalSizeDown[X,Y]; i <= Y + crystalSizeUp[X,Y]; i++) {
						ConnectPixel (X+crystalSizeRight[X,Y], i, X-crystalSizeLeft[X,Y], i);
					}
				}
			}
		}
	}

	void DrowPixel(int X, int Y, Color color){
		int x = X*10;
		int y = Y*10;
		for (int a1 =-1; a1<=1; a1++) {
			for (int b1 =-1; b1<=1; b1++) {
				textureNew.SetPixel (x + a1, y + b1, color);
			}}
		textureNew.SetPixel (x + 2, y, color);
		textureNew.SetPixel (x - 2, y, color);
		textureNew.SetPixel (x, y + 2, color);
		textureNew.SetPixel (x, y - 2, color);
	}
	void DrowPixelCentre(int X, int Y, Color color){
		int x = X*10;
		int y = Y*10;
		for (int a1 =-1; a1<=1; a1++) {
			for (int b1 =-1; b1<=1; b1++) {
				textureNew.SetPixel (x + a1, y + b1, color);
			}}
		textureNew.SetPixel (x + 2, y+1, color);
		textureNew.SetPixel (x + 2, y+2, color);
		textureNew.SetPixel (x + 1, y+1, color);
		textureNew.SetPixel (x - 2, y+1, color);
		textureNew.SetPixel (x - 2, y+2, color);
		textureNew.SetPixel (x - 1, y+1, color);
		textureNew.SetPixel (x + 2, y-1, color);
		textureNew.SetPixel (x + 2, y-2, color);
		textureNew.SetPixel (x + 1, y-1, color);
		textureNew.SetPixel (x - 2, y-1, color);
		textureNew.SetPixel (x - 2, y-2, color);
		textureNew.SetPixel (x - 1, y-1, color);

	}

	void ConnectPixel(int X1, int Y1, int X2, int Y2){
		X1 = X1 * 10;
		Y1 = Y1 * 10;
		oldPixel = new Vector2(X2*10,Y2*10);
		if(X1-oldPixel.x>=0 && Y1-oldPixel.y>=0){
			for(i=1;i<(X1-oldPixel.x);i++){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((Y1-oldPixel.y)/(X1-oldPixel.x)),grey);
			}
			for(i=1;i<(Y1-oldPixel.y);i++){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((X1-oldPixel.x)/(Y1-oldPixel.y)),(int)oldPixel.y+i,grey);
			}}
		if(X1-oldPixel.x>=0 && Y1-oldPixel.y<=0){
			for(i=1;i<(X1-oldPixel.x);i++){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((Y1-oldPixel.y)/(X1-oldPixel.x)),grey);
			}
			for(i=-1;i>(Y1-oldPixel.y);i--){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((X1-oldPixel.x)/(Y1-oldPixel.y)),(int)oldPixel.y+i,grey);
			}}
		if(X1-oldPixel.x<=0 && Y1-oldPixel.y>=0){
			for(i=-1;i>(X1-oldPixel.x);i--){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((Y1-oldPixel.y)/(X1-oldPixel.x)),grey);
			}
			for(i=1;i<(Y1-oldPixel.y);i++){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((X1-oldPixel.x)/(Y1-oldPixel.y)),(int)oldPixel.y+i,grey);
			}}
		if(X1-oldPixel.x<=0 && Y1-oldPixel.y<=0){
			for(i=-1;i>(X1-oldPixel.x);i--){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((Y1-oldPixel.y)/(X1-oldPixel.x)),grey);
			}
			for(i=-1;i>(Y1-oldPixel.y);i--){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((X1-oldPixel.x)/(Y1-oldPixel.y)),(int)oldPixel.y+i,grey);
			}}
	}

	private Color ColorFormEnrg(int X, int Y){
		float s = sigma * 0.5f;
		if (Enrg [X, Y] <= -2f * s) {
			return new Color (0,0,1);}
		if (Enrg [X, Y] > -2 * s && Enrg [X, Y] <= -1 * s) {
			return new Color (0,(Enrg[X,Y]/s+2f),1);}
		if (Enrg [X, Y] > -1 * s && Enrg [X, Y] <= 0) {
			return new Color (0,1,(Enrg[X,Y]/s*(-1)));}
		if (Enrg [X, Y] > 0 && Enrg [X, Y] <= s) {
			return new Color (Enrg[X,Y]/s,1,0);}
		if (Enrg [X, Y] > s && Enrg [X, Y] < 2*s) {
			return new Color (1,(-1f)*(Enrg[X,Y]/s-2),0);}
		if (Enrg [X, Y] >= 2 * s) {
			return new Color (1,0,0);}
		return new Color(0,0,0);
	}
	void ClearTexture(){
		for (int i = 0; i <= textureNew.width; i++) {
			for (int k = 0; k <= textureNew.height; k++) {
				textureNew.SetPixel (i,k,Color.white);
			}
		}
	}

	private bool RandomNormal(int chance){
		if ((int)Random.Range (0, chance) == 1) {
			return true;
		}else{
			return false;
		}
	}

	void CameraControl(){
		if (Input.mouseScrollDelta.y != 0) {
			if (MainCamera.orthographicSize > 0.15f ){
				if(Input.mouseScrollDelta.y>0){
					MainCamera.orthographicSize -= 0.1f;}}
			if(MainCamera.orthographicSize < 1.45f){
				if(Input.mouseScrollDelta.y<0){
					MainCamera.orthographicSize += 0.1f;}}

		}
		if (Input.GetMouseButton (0)) {
			XCamera = -(Input.mousePosition.x - XCameraOld)*0.001f;
			YCamera = -(Input.mousePosition.y - YCameraOld)*0.001f;
			//Debug.Log(Input.mousePosition.x);
			//if(!All || Input.mousePosition.x>230){
			//	MainCamera.transform.position = new Vector3(MainCamera.transform.position.x+XCamera,MainCamera.transform.position.y+YCamera,-10);}
		}

		XCameraOld = Input.mousePosition.x;
		YCameraOld = Input.mousePosition.y;
	}

	private float Gauss(){
		float j = Random.Range(0f,1f);
		float max;
		float min;
		float p;
		int o;
		if (j >= 0.5f) {
			min = 0f;
			max = 5;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1f+Erf(p/(1*1*1.414213f)))> j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}
		} else {
			max = 0f;
			min = -5;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1f+Erf(p/(1*1*1.414213f))) > j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}}

		return (min);
	}


	private float Erf(float x){
		if (x >= 0) {
			float y = 1.0f / (1.0f + 0.3275911f * x);
			return (1f - (((((+1.061405429f * y - 1.453152027f) * y + 1.421413741f) * y - 0.284496736f) * y + 0.254829592f) * y) * Mathf.Exp (-x * x));
		} else {
			float y = 1.0f / (1.0f + 0.3275911f * (-x));
			return (-(1f - (((((+1.061405429f * y - 1.453152027f) * y + 1.421413741f) * y - 0.284496736f) * y + 0.254829592f) * y) * Mathf.Exp (-x * x)));}
	}



	private float RndEnrg1(){
		float j = Random.Range(0f,1f);

		float max;
		float min;
		float p;
		int o;
		if (j >= 0.5f) {
			min = 0f;
			max = maxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1+Erf(p/(sigma*kT*1.414f)))> j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}
		} else {
			max = 0f;
			min = -maxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1+Erf(p/(sigma*kT*1.414f))) > j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}}
		//if (min < -M*sigma*1.414f * kT) {
		//	min=RndEnrg1();
		//}

		return (min);
	}

	private float RndEnrg1Crystal(){
		float j = Random.Range(0f,1f);

		float max;
		float min;
		float p;
		int o;
		if (j >= 0.5f) {
			min = 0f;
			max = maxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1+Erf(p/(sigma*sigmaCrystal*kT*1.414f)))> j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}
		} else {
			max = 0f;
			min = -maxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1+Erf(p/(sigma*sigmaCrystal*kT*1.414f))) > j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}}
		//if (min < -M*sigma*1.414f * kT) {
		//	min=RndEnrg1();
		//}

		return (min);
	}

}

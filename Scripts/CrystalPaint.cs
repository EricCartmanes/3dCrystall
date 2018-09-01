using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CrystalPaint : MonoBehaviour {

	public Texture2D textureNew;		//1800x1800
	public Camera MainCamera;
	private Color black = Color.black;
	private Color red = Color.red;
	private Color green = Color.green;
	private Color grey = Color.grey;
	private int X = 50;
	private int Y = 250;
	private int posX = 50;
	private int posY = 250;
	private float XCamera;
	private float YCamera;
	private float XCameraOld;
	private float YCameraOld;
	private int a=0;
	private int b=0;
	private float c=0;
	private float d=0;
	private int i;
	private int j;
	private Vector2 oldPixel;
	private int[] indexX = new int[93];
	private int[] indexY = new int[93];
	private int[] indexZ = new int[93];
	//private float[,] Enrg = new float[2000,2000];
	private int[,] crystalStart = new int[200,500];
	private float[,] crystalStartTime = new float[200,500];
	private int[,] crystalSize = new int[200,500];
	private int[,] crystalID = new int[200,500];
	private int[,] crystalSizeUp = new int[200,500];
	private int[,] crystalSizeDown = new int[200,500];
	private int[,] crystalSizeLeft = new int[200,500];
	private int[,] crystalSizeRight = new int[200,500];
	//private float[,] EnrgCrystalL = new float[200, 250];
	//private float[,] EnrgCrystalC = new float[200, 250];
	//private float[,] EnrgCrystalR = new float[200, 250];
	private float[,] Enrg = new float[200,500];
	private int[,] PosDetect = new int[200,500];
	private int[] crystalIDX = new int[10000];
	private int[] crystalIDY = new int[10000];
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
	public GameObject startExpUI;
	public GameObject startExpTextUI;
	public GameObject startExpSliderUI;


	private float maxEnrg;
	private float kT = 0.25f;
	private float sigma = 2.13f;
	private float limitGEnrg = 6;
	private float Et;
	private float sigmaCrystal=0.5f;

	private bool makeStat = false;
	private bool makeExp = true;
	private int expStep = 0;
	private int expStepLimit=100;		//количество экспериментов
	private int statStep = 0;
	private int statStepLimit=1000;
	private float[] v = new float[226];		//темпы перехода
	private float[,] v2 = new float[126,10];		//темпы перехода
	private int[] targePosX = new int[226];
	private int[] targePosY = new int[226];
	private float[] p = new float[226];		//вероятности перехода
	private float[] p2 = new float[1260];		//вероятности перехода
	private int RightLim=50;
	private int N=100;		//длина слоя на котором измеряем плотность
	private float E=0.055f; //0.025 = 0.1kT;		напряженность электрического поля
	private float r = 1;		//радиус решетки
	private float e=1;
	private int g;
	private bool borderError = false;
	private float tempSumm;
	private float[] tempSumm2 = new float[10];
	private float T;
	private float stepTime;
	private float stepTimeLocal;
	private float stepTimeCrystal;
	private int limit = 500000;
	private int limitConst = 3000000;
	private float[] K = new float[11000];
	private int[] centreNumb = new int[11000];
	private int[] chainNumb = new int[11000];
	private int[] lenghtChain = new int[11000];
	private float[] crystalPercent = new float[11000];
	private float[] time5 = new float[11000];
	private float[] time10 = new float[11000];
	private float[] time20 = new float[11000];
	private float[] time40 = new float[11000];
	private float[] time100 = new float[11000];
	private float[] time5Local = new float[11000];
	private float[] time10Local = new float[11000];
	private float[] time20Local = new float[11000];
	private float[] time40Local = new float[11000];
	private float[] time100Local = new float[11000];
	private float[] time5Crystal = new float[11000];
	private float[] time10Crystal = new float[11000];
	private float[] time20Crystal = new float[11000];
	private float[] time40Crystal = new float[11000];
	private float[] time100Crystal = new float[11000];
	private float middleTime5;
	private float middleTime5Rev;
	private float middleTime10;
	private float middleTime10Rev;
	private float middleTime20;
	private float middleTime20Rev;
	private float middleTime40;
	private float middleTime40Rev;
	private float middleTime100;
	private float middleTime100Rev;
	private float middleTime5Local;
	private float middleTime5RevLocal;
	private float middleTime10Local;
	private float middleTime10RevLocal;
	private float middleTime20Local;
	private float middleTime20RevLocal;
	private float middleTime40Local;
	private float middleTime40RevLocal;
	private float middleTime100Local;
	private float middleTime100RevLocal;
	private float middleTime5Crystal;
	private float middleTime5RevCrystal;
	private float middleTime10Crystal;
	private float middleTime10RevCrystal;
	private float middleTime20Crystal;
	private float middleTime20RevCrystal;
	private float middleTime40Crystal;
	private float middleTime40RevCrystal;
	private float middleTime100Crystal;
	private float middleTime100RevCrystal;
	private bool time5Bool = false;
	private bool time10Bool = false;
	private bool time20Bool = false;
	private bool time40Bool = false;
	private int l=0;
	private int J=-1;
	private float delE;
	private int A;
	private int xxx;
	private int yyy;
	private int nnn;
	private float frenqSmall = 10f;
	private float frenqBig = 10f;
	private int crystalIDTemp;
	private int crystalCenterX;
	private int crystalCenterY;
	private float middleL;
	private float middleH;
	private float middleV;
	private float middlePercent;
	private bool kvas = false;


	// Use this for initialization
	void Start () {
		limit = limitConst;
		maxEnrg = limitGEnrg * sigma * kT;
		Et = 1f*sigma;

		startExpUI.GetComponent<Button>().onClick.AddListener (StartExperiment);
		stepByStepUI.GetComponent<Button> ().onClick.AddListener (MakeStep);
		renderFullUI.GetComponent<Button> ().onClick.AddListener (RenderFull);
		resetCameraUI.GetComponent<Button> ().onClick.AddListener (ResetCamera);
		colorCentreUI.GetComponent<Toggle> ().onValueChanged.AddListener ((colorCentre)  => {
			reColorCentre();

		});

		//GenerateMap ();
		//Marker ();
		//chance = (int)density.GetComponent<Slider> ().value;


		//RenderFull ();

		ClearTexture ();
		for (int x = 0; x <= 199; x++) {
			for (int y = 0; y <= 499; y++) {
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
		for(int x=25;x<=175; x++){
			for(int y=25;y<=475; y++){
				Enrg [x, y] = RndEnrg1 ();

			}
		}
	}

	// Update is called once per frame
	void Update () {
		expStepLimit=((int)startExpSliderUI.GetComponent<Slider> ().value);
		startExpTextUI.GetComponent<Text>().text = "start Exp: "+l+"/"+expStepLimit;
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


		if (makeExp) {
			while (posX < 50 + N && posY > 50 && posY < 450) {
				if (!kvas) {
					if (crystalID [posX, posY] == 0) {//если (0;0) локализованное состояние
						Enrg[posX, posY]=Enrg[posX, posY]-sigma*kT;
					} else {
						crystalIDTemp = crystalID[posX,posY];
						crystalCenterX = crystalIDX[crystalIDTemp];
						crystalCenterY = crystalIDY[crystalIDTemp];

						if (crystalStart [crystalCenterX, crystalCenterY] == 2) {		//если ориентация горизонтальная
							for (int xx = -crystalSizeLeft [crystalCenterX, crystalCenterY]; xx <= crystalSizeRight [crystalCenterX, crystalCenterY]; xx++) {
								Enrg [posX+xx, posY] = Enrg [posX+xx, posY] - sigma * kT / 2f;
							}
						} else {
							for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {
								Enrg [posX, posY+yy] = Enrg [posX, posY+yy] - sigma * kT / 2f;
							}
						}
					}
					kvas = true;
				}
				//if(Input.anyKeyDown){
				g++;
				if (crystalID [posX, posY] == 0) {	//если прыжок НЕ из кристалла

					A = 0;
					for (int xx = -1; xx <= 1; xx++) {
						for (int yy = -1; yy <= 1; yy++) {
							if (xx != 0 || yy != 0) {
								c = xx * xx + yy * yy;
								delE = Enrg [posX + xx, posY + yy] - E * e * r * xx - Enrg [posX, posY];
								if (posY + yy < 50 || posY + yy > 450 || posX + xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c))+10f) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = posY + yy;
								//Debug.Log ("[" + (int)(X + xx) + "," + (int)(Y + yy) + "] = " + v [A]+"                 [delE]= "+delE+"   "+Mathf.Exp((delE + Mathf.Abs (delE))/ (-2 * kT)) );
								A++;
							}
						}
					}
					A--;
					c = 0;
					for (i = 0; i <= A; i++) {
						c += v [i];		//временная сумма всех темпов
					}
					tempSumm = c;

					for (i = 0; i <= A; i++) {
						p [i] = v [i] / c;
						//Debug.Log (i + ") " + p [i]);
					}

					d = Random.Range (0, 1f);
					i = 0;
					c = 0;
					while (c<=d) {
						c += p [i];
						i++;
					}
					i--;

					A=0;


					T = ((-Mathf.Log (Random.Range (0.0000001f, 1f))) / (tempSumm));
					stepTimeLocal += T;
				} else {	//если прыжок ИЗ кристалла

					crystalIDTemp = crystalID[posX,posY];
					crystalCenterX = crystalIDX[crystalIDTemp];
					crystalCenterY = crystalIDY[crystalIDTemp];
					float frenq=10;
					A = 0;
					if (crystalStart [crystalCenterX, crystalCenterY] == 2) {	//если ориентация горизорнтлаьная
						if ((posY != crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]) && (posY != crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY])) {		//Если НЕ крайняя цепь кристаллита
							for (int yy = -1; yy <= 1; yy++) {		//прыжок влево
								c = 1 * 1 + yy * yy;
								delE = Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1, posY + yy] - E * e * r * (-1) - Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY], posY];
								if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1) < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1;
								targePosY [A] = posY + yy;
								A++;
							}
							for (int yy = -1; yy <= 1; yy++) {
								if (yy != 0) {
									c = 1;
									delE = Enrg [crystalCenterX, posY + yy] - E * e * r * (0) - Enrg [crystalCenterX, posY];
									if (posY - yy < 50 || posY + yy > 450 || posX < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX;
									targePosY [A] = posY + yy;
									A++;
								}
							}
							for (int yy = -1; yy <= 1; yy++) {		//прыжок вправо
								c = 1 * 1 + yy * yy;
								delE = Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1, posY + yy] - E * e * r * (1) - Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY], posY];
								if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1) < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1;
								targePosY [A] = posY + yy;
								A++;
							}
						} else {		//Если крайняя цепь кристалла
							for (int yy = -1; yy <= 1; yy++) {		//прыжок влево
								c = 1 * 1 + yy * yy;
								delE = Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1, posY + yy] - E * e * r * (-1) - Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY], posY];
								if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1) < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] - 1;
								targePosY [A] = posY + yy;
								A++;
							}
							for (int yy = -1; yy <= 1; yy++) {		//прыжок вправо
								c = 1 * 1 + yy * yy;
								delE = Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1, posY + yy] - E * e * r * (1) - Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY], posY];
								if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1) < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + 1;
								targePosY [A] = posY + yy;
								A++;
							}
							if (posY == crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]) {		//если верхний край
								c = 1;
								delE = Enrg [crystalCenterX, posY - 1] - E * e * r * (0) - Enrg [crystalCenterX, posY];
								if (posY - 1 < 50 || posY - 1 > 450 || posX < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX;
								targePosY [A] = posY - 1;
								A++;
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вверх
									c = 1;
									delE = Enrg [crystalCenterX + xx, posY + 1] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
									if (posY + 1 < 50 || posY + 1 > 450 || crystalCenterX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX + xx;
									targePosY [A] = posY + 1;
									A++;
								}
							}
							if (posY == crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]) {		//если нижний край
								c = 1;
								delE = Enrg [crystalCenterX, posY + 1] - E * e * r * (0) - Enrg [crystalCenterX, posY];
								if (posY + 1 < 50 || posY + 1 > 450 || posX < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX;
								targePosY [A] = posY + 1;
								A++;
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вверх
									c = 1;
									delE = Enrg [crystalCenterX + xx, posY - 1] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
									if (posY - 1 < 50 || posY - 1 > 450 || crystalCenterX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX + xx;
									targePosY [A] = posY - 1;
									A++;
								}
							}
						}
					} else {		//Вертиклаьная ориентация
						if ((posX != crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY]) && (posX != crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY])) {		//Если НЕ крайняя цепь кристаллита
							for (int xx = -1; xx <= 1; xx++) {		//прыжок вниз
								c = 1 * 1 + xx * xx;
								delE = Enrg [posX + xx,crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1] - E * e * r * (xx) - Enrg [posX,crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]];
								if ((crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1) < 50 || (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1) > 450 || posX+xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1;
								A++;
							}
							for (int xx = -1; xx <= 1; xx++) {
								if (xx != 0) {
									c = 1;
									delE = Enrg [posX+xx,crystalCenterY] - E * e * r * (xx) - Enrg [posX,crystalCenterY];
									if (posY < 50 || posY > 450 || posX+xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY;
									A++;
								}
							}
							for (int xx = -1; xx <= 1; xx++) {		//прыжок верх
								c = 1 * 1 + xx * xx;
								delE = Enrg [posX + xx,crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1] - E * e * r * (xx) - Enrg [posX,crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]];
								if ((crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1) < 50 || (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1) > 450 || posX+xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1;
								A++;
							}
						} else {		//Если крайняя цепь кристалла
							for (int xx = -1; xx <= 1; xx++) {		//прыжок вниз
								c = 1 * 1 + xx * xx;
								delE = Enrg [posX + xx,crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1] - E * e * r * (xx) - Enrg [posX,crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]];
								if ((crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1) < 50 || (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1) > 450 || posX+xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] - 1;
								A++;
							}
							for (int xx = -1; xx <= 1; xx++) {		//прыжок верх
								c = 1 * 1 + xx * xx;
								delE = Enrg [posX + xx,crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1] - E * e * r * (xx) - Enrg [posX,crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]];
								if ((crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1) < 50 || (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1) > 450 || posX+xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + 1;
								A++;
							}
							if (posX == crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY]) {		//если правый край
								c = 1;
								delE = Enrg [posX-1,crystalCenterY] - E * e * r * (-1) - Enrg [crystalCenterX, posY];
								if (posY < 50 || posY > 450 || posX-1 < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX-1;
								targePosY [A] = crystalCenterY;
								A++;
								for (int yy = -crystalSizeDown[crystalCenterX, crystalCenterY]; yy <= crystalSizeUp[crystalCenterX, crystalCenterY]; yy++) {		//прыжок вправо
									c = 1;
									delE = Enrg [posX+1,crystalCenterY + yy] - E * e * r * (1) - Enrg [posX,crystalCenterY + yy];
									if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX+1 < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX+1;
									targePosY [A] = crystalCenterY + yy;
									A++;
								}
							}
							if (posY == crystalCenterY - crystalSizeLeft [crystalCenterX, crystalCenterY]) {		//если левый край
								c = 1;
								delE = Enrg [posX+1,crystalCenterY] - E * e * r * (1) - Enrg [posX,crystalCenterY];
								if (posY < 50 || posY > 450 || posX+1 < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX+1;
								targePosY [A] = crystalCenterY;
								A++;
								for (int yy = -crystalSizeDown[crystalCenterX, crystalCenterY]; yy <= crystalSizeUp[crystalCenterX, crystalCenterY]; yy++) {		//прыжок влево
									c = 1;
									delE = Enrg [posX-1,crystalCenterY + yy] - E * e * r * (-1) - Enrg [posX,crystalCenterY + yy];
									if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX-1 < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX-1;
									targePosY [A] = crystalCenterY + yy;
									A++;
								}
							}
						}
					}






					A--;
					c = 0;
					for (i = 0; i <= A; i++) {
						c += v [i];		//временная сумма всех темпов
						//Debug.Log("["+i+"]   ->   ["+targePosX[i]+" , "+targePosY[i]+"]         "+v[i]);
					}
					tempSumm = c;

					for (i = 0; i <= A; i++) {
						p [i] = v [i] / c;
						//Debug.Log (i + ") " + p [i]);
					}

					d = Random.Range (0, 1f);
					i = 0;
					c = 0;
					while (c<=d) {
						c += p [i];
						i++;
					}
					i--;

					A=0;




					T = ((-Mathf.Log (Random.Range (0.0000001f, 1f))) / (tempSumm)) / 10000000;
					stepTimeCrystal += T;
				}
				//g++;
				stepTime += T;

				posX = targePosX [i];
				posY = targePosY [i];
				if (posX > 55 && !time5Bool) {		//5
					time5Bool = true;
					time5 [l] = stepTime;
					time5Local [l] = stepTimeLocal;
					time5Crystal [l] = stepTimeCrystal;
				}
				if (posX > 60 && !time10Bool) {		//10
					time10Bool = true;
					time10 [l] = stepTime;
					time10Local [l] = stepTimeLocal;
					time10Crystal [l] = stepTimeCrystal;
				}
				if (posX > 70 && !time20Bool) {		//20
					time20Bool = true;
					time20 [l] = stepTime;
					time20Local [l] = stepTimeLocal;
					time20Crystal [l] = stepTimeCrystal;
				}
				if (posX > 90 && !time40Bool) {		//40
					time40Bool = true;
					time40 [l] = stepTime;
					time40Local [l] = stepTimeLocal;
					time40Crystal [l] = stepTimeCrystal;
				}
				if (crystalID [posX, posY] != 0) {
					crystalIDTemp = crystalID[posX,posY];
					crystalCenterX = crystalIDX[crystalIDTemp];
					crystalCenterY = crystalIDY[crystalIDTemp];

					if (crystalStart [crystalCenterX, crystalCenterY] == 2) {		//если ориентация горизонтальная
						for (int xx = -crystalSizeLeft [crystalCenterX, crystalCenterY]; xx <= crystalSizeRight [crystalCenterX, crystalCenterY]; xx++) {
							PosDetect [crystalCenterX+xx, posY]++;
						}
					} else {
						for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {
							PosDetect [posX, crystalCenterY+yy]++;
						}
					}
				} else {
					PosDetect [posX, posY]++;
				}
				if (g % 100 == 0) {
					//Debug.Log ("[" + g + "]   " + posX + "  ,  " + posY);
				}

				limit--;
				if (limit < 0) {
					posX = 180;
					Debug.Log ("!dolgo!");
				}


			}
			//if (Input.GetKeyDown ("space")) {
			//RenderFull();
			/*for(int x=25;x<=175; x++){
					for(int y=25;y<=225; y++){
						Enrg [x, y] = RndEnrg1 ();

					}
				}*/
			g = 0;
			//K [l] = stepTime / 1000;
			time100 [l] = stepTime;
			time100Local [l] = stepTimeLocal;
			time100Crystal [l] = stepTimeCrystal;
			l++;
			expStep++;
			Debug.Log (expStep+"       "+stepTime);
			kvas = false;
			stepTime = 0;
			stepTimeLocal = 0;
			stepTimeCrystal = 0;
			limit = limitConst;
			posX = 50;
			posY = 250;
			time5Bool = false;
			time10Bool = false;
			time20Bool = false;
			time40Bool = false;
			//if (expStep >= expStepLimit) {
				makeExp = false;
				renderBorders ();
				for (int i = 25; i <= 175; i++) {
					for (int j = 25; j <= 475; j++) {
						if(crystalID [i, j] != 0){
							crystalIDTemp = crystalID[i,j];
							crystalCenterX = crystalIDX[crystalIDTemp];
							crystalCenterY = crystalIDY[crystalIDTemp];
							if (crystalStart [crystalCenterX, crystalCenterY] == 2) {
							ConnectPixel (crystalCenterX-crystalSizeLeft [crystalCenterX, crystalCenterY], j, crystalCenterX+crystalSizeRight [crystalCenterX, crystalCenterY], j);
							}else{
							ConnectPixel (i, crystalCenterY-crystalSizeDown [crystalCenterX, crystalCenterY], i, crystalCenterY+crystalSizeUp [crystalCenterX, crystalCenterY]);
							}
						}
						if (PosDetect [i, j] >= 5) {
							DrowPixelBig (i, j, Color.red);
						} 
						if (PosDetect [i, j] >= 3 && PosDetect [i, j] <= 4) {
							DrowPixelBig (i, j, Color.yellow);
						}
						if (PosDetect [i, j] >= 1 && PosDetect [i, j] <= 2){
							DrowPixelBig (i, j, Color.green);
						}
					}
				}
				textureNew.Apply ();





			//}

			//}
		}
		/*
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
		*/
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


	void GenerateMap(){
		clasterNumb = 1;
		if (CheckChange ()) {
		}
		for (int x = 0; x <= 199; x++) {
			for (int y = 0; y <= 249; y++) {
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
		for(int x=25;x<=175; x++){
			for(int y=25;y<=475; y++){
				if (RandomNormal(chance)
					&& crystalStart [x-1, y+1]==0 && crystalStart [x, y+1]==0 && crystalStart [x+1, y+1]==0
					&& crystalStart [x-1, y]==0 && crystalStart [x+1, y]==0
					&& crystalStart [x-1, y-1]==0 && crystalStart [x, y-1]==0 && crystalStart [x+1, y-1]==0) {

					clasterNumb++;
					crystalStart [x, y] = (int)Random.Range(0,2)+1;		//orientation
					crystalSize[x,y] = 0;
					crystalID[x,y] = clasterNumb;
					Enrg [x, y] = RndEnrg1Crystal () - Et;
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
				}
			}
		}
		currentTimeStep = 0;

		while (currentTimeStep <= timeLimit) {
			Step ();
		}

		/*for (int x = 25; x <= 175; x++) {	//задание разных энергий разным цепям кристалла
			for (int y = 25; y <= 225; y++) {
				if (crystalStart [x, y] != 0) {
					if (crystalStart [x, y] == 1) {
						for (int I = x - crystalSizeLeft [x, y]; I <= x + crystalSizeRight [x, y]; I++) {
							float tempEnrg = RndEnrg1Crystal () - Et;
							for (int K = y - crystalSizeDown [x, y]; K <= y + crystalSizeUp [x, y]; K++) {
								Enrg [I, K] = tempEnrg;
							}
						}
					} else {
						for (int K = y - crystalSizeDown [x, y]; K <= y + crystalSizeUp [x, y]; K++) {
							float tempEnrg = RndEnrg1Crystal () - Et;
							for (int I = x - crystalSizeLeft [x, y]; I <= x + crystalSizeRight [x, y]; I++) {
								Enrg [I, K] = tempEnrg;
							}
						}
					}
				}
			}
		}*/

	}

	void NewSpace(){
		ClearTexture ();
		clasterNumb = 1;
		for (int x = 0; x <= 199; x++) {
			for (int y = 0; y <= 499; y++) {
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
		for(int x=25;x<=175; x++){
			for(int y=25;y<=475; y++){
				if (RandomNormal(chance)
					&& crystalStart [x-1, y+1]==0 && crystalStart [x, y+1]==0 && crystalStart [x+1, y+1]==0
					&& crystalStart [x-1, y]==0 && crystalStart [x+1, y]==0
					&& crystalStart [x-1, y-1]==0 && crystalStart [x, y-1]==0 && crystalStart [x+1, y-1]==0) {

					clasterNumb++;
					crystalStart [x, y] = (int)Random.Range(0,2)+1;		//orientation
					crystalSize[x,y] = 0;
					crystalID[x,y] = clasterNumb;
					crystalIDX [clasterNumb] = x;
					crystalIDY [clasterNumb] = y;
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
			for (int x = 25; x <= 175; x++) {
				for (int y = 25; y <= 475; y++) {
					if (crystalStart [x, y] != 0) {
						CheckGrowSpace (x, y);
					}
				}
			}
		}
		if (growType == 1) {
			for (int x = 25; x <= 175; x++) {
				for (int y = 25; y <= 475; y++) {
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

		for (int x = 25; x <= 175; x++) {
			for (int y = 25; y <= 475; y++) {
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

	void StartExperiment(){
		if (CheckChange ()) {
		}
		makeExp = true;
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


		for (int x = 50; x <= 150; x++) {
			for (int y = 50; y <= 450; y++) {
				if (crystalID [x, y] != 0) {
					crystalPercent [l]++;
				}
				if (crystalStart [x, y] != 0) {
					int a = 1;
					int b = 1;
					if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) != 0) {
						a = crystalSizeLeft [x, y] + crystalSizeRight [x, y] + 1;
					}
					if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) != 0) {
						b = crystalSizeUp [x, y] + crystalSizeDown [x, y] + 1;
					}
					centreNumb [l]=a*b;		//объем


					if (crystalStart[x,y] == 1) {
						if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) == 0) {
							chainNumb [l]=0;
							lenghtChain [l]=0;
						} else {
							lenghtChain[l]=(crystalSizeUp [x, y] + crystalSizeDown [x, y]);
							if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) != 0) {
								chainNumb[l]= (crystalSizeLeft [x, y] + crystalSizeRight [x, y]);
							} else {
								chainNumb [l] = 1;
							}
						}
					} else {
						if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) == 0) {
							chainNumb [l]=0;
							lenghtChain [l]=0;
						} else {
							lenghtChain[l]= (crystalSizeLeft [x, y] + crystalSizeRight [x, y]);
							if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) != 0) {
								chainNumb[l]= (crystalSizeUp [x, y] + crystalSizeDown [x, y]);
							} else {
								chainNumb [l]=1;
							}
						}
					}
				}
			}
		}
		crystalPercent [l] = (float)(crystalPercent [l] / 40000f);
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
						//ConnectPixel (i, Y + size, i, Y + size - 1);
						//ConnectPixel (i, Y - size, i, Y - size + 1);
					}
				}
				for (int i = Y - size; i <= Y + size; i++) {
					crystalID [X + size, i] = crystalID [X, Y];
					crystalID [X - size, i] = crystalID [X, Y];
					DrowPixel (X + size, i, ColorFormEnrg(X,Y));
					DrowPixel (X - size, i, ColorFormEnrg(X,Y));
					if (crystalStart [X, Y] == 2) {
						//ConnectPixel (X + size, i, X + size - 1, i);
						//ConnectPixel (X - size, i, X - size + 1, i);
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
				float tempEnrg =  RndEnrg1Crystal () - Et;
				if (growSide == 0) {
					for (int i = X - crystalSizeLeft [X, Y]; i <= X + crystalSizeRight [X, Y]; i++) {
						crystalID [i, Y + crystalSizeUp[X,Y] + 1] = crystalID [X, Y];
						if (crystalStart [X, Y] == 1) {
							Enrg [i, Y + crystalSizeUp [X, Y] + 1] = Enrg [i, Y];
						} else {
							Enrg [i, Y + crystalSizeUp [X, Y] + 1] = tempEnrg;
						}
						DrowPixel (i, Y + crystalSizeUp [X, Y] + 1, ColorFormEnrg (i, Y + crystalSizeUp [X, Y] + 1));
					}
					crystalSizeUp [X, Y]++;
				}
				if (growSide == 1) {
					for (int i = X - crystalSizeLeft [X, Y]; i <= X + crystalSizeRight [X, Y]; i++) {
						crystalID [i, Y - crystalSizeDown[X,Y] - 1] = crystalID [X, Y];
						if (crystalStart [X, Y] == 1) {
							Enrg [i, Y - crystalSizeDown [X, Y] - 1] = Enrg [i, Y];
						} else {
							Enrg [i, Y - crystalSizeDown [X, Y] - 1] = tempEnrg;
						}
						DrowPixel (i, Y - crystalSizeDown [X, Y] - 1, ColorFormEnrg (i, Y - crystalSizeDown [X, Y] - 1));
					}
					crystalSizeDown [X, Y]++;
				}
				if (growSide == 2) {
					for (int i = Y - crystalSizeDown [X, Y]; i <= Y + crystalSizeUp [X, Y]; i++) {
						crystalID [X-crystalSizeLeft[X,Y]-1, i] = crystalID [X, Y];
						if (crystalStart [X, Y] == 1) {
							Enrg [X-crystalSizeLeft[X,Y]-1, i] = tempEnrg;
						} else {
							Enrg [X-crystalSizeLeft[X,Y]-1, i] = Enrg [X, i];
						}
						DrowPixel (X-crystalSizeLeft[X,Y]-1, i, ColorFormEnrg (X-crystalSizeLeft[X,Y]-1, i));
					}
					crystalSizeLeft [X, Y]++;
				}
				if (growSide == 3) {
					for (int i = Y - crystalSizeDown [X, Y]; i <= Y + crystalSizeUp [X, Y]; i++) {
						crystalID [X+crystalSizeRight[X,Y]+1, i] = crystalID [X, Y];
						if (crystalStart [X, Y] == 1) {
							Enrg [X+crystalSizeRight[X,Y]+1, i] = tempEnrg;
						} else {
							Enrg [X+crystalSizeRight[X,Y]+1, i] = Enrg [X, i];
						}
						DrowPixel (X+crystalSizeRight[X,Y]+1, i, ColorFormEnrg (X+crystalSizeRight[X,Y]+1, i));
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
						//ConnectPixel (i, Y + size, i, Y - size);
					}
				}
				if (crystalStart [X, Y] == 2) {
					for (int i = Y - size; i <= Y + size; i++) {
						//ConnectPixel (X+size, i, X-size, i);
					}
				}
			}
			if (crystalType == 1) {
				if (crystalStart [X, Y] == 1) {
					for (int i = X - crystalSizeLeft[X,Y]; i <= X + crystalSizeRight[X,Y]; i++) {
						//ConnectPixel (i, Y + crystalSizeUp[X,Y], i, Y - crystalSizeDown[X,Y]);
					}
				}
				if (crystalStart [X, Y] == 2) {
					for (int i = Y - crystalSizeDown[X,Y]; i <= Y + crystalSizeUp[X,Y]; i++) {
						//ConnectPixel (X+crystalSizeRight[X,Y], i, X-crystalSizeLeft[X,Y], i);
					}
				}
			}
		}
	}
	void DrowPixelBig(int X, int Y, Color color){
		int x = X*5;
		int y = Y*5;
		textureNew.SetPixel (x, y , color);
		textureNew.SetPixel (x+1, y , color);
		textureNew.SetPixel (x-1, y , color);
		textureNew.SetPixel (x, y+1 , color);
		textureNew.SetPixel (x, y-1 , color);
	}
	void DrowPixel(int X, int Y, Color color){
		int x = X*5;
		int y = Y*5;
		textureNew.SetPixel (x, y , color);
		//textureNew.SetPixel (x+1, y , color);
		//textureNew.SetPixel (x-1, y , color);
		//textureNew.SetPixel (x, y+1 , color);
		//textureNew.SetPixel (x, y-1 , color);

		//for (int a1 =-1; a1<=1; a1++) {
		//	for (int b1 =-1; b1<=1; b1++) {
		//		textureNew.SetPixel (x + a1, y + b1, color);
		//	}}
		//textureNew.SetPixel (x + 2, y, color);
		//textureNew.SetPixel (x - 2, y, color);
		//textureNew.SetPixel (x, y + 2, color);
		//textureNew.SetPixel (x, y - 2, color);
	}
	void DrowPixelCentre(int X, int Y, Color color){
		int x = X*5;
		int y = Y*5;
		textureNew.SetPixel (x, y , color);
		/*for (int a1 =-1; a1<=1; a1++) {
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
		textureNew.SetPixel (x - 1, y-1, color);*/

	}
	void renderBorders(){
		for (int x = 1; x < 250; x++) {
			for (int y = 1; y < textureNew.height; y++) {
				textureNew.SetPixel (x, y, Color.black);
			}
		}
		for (int x = 750; x < 999; x++) {
			for (int y = 1; y < textureNew.height; y++) {
				textureNew.SetPixel (x, y, Color.black);
			}
		}
		for (int x = 1; x < textureNew.width; x++) {
			for (int y = 1; y < 250; y++) {
				textureNew.SetPixel (x, y, Color.black);
			}
		}
		for (int x = 1; x < textureNew.width; x++) {
			for (int y = 2250; y < 2499; y++) {
				textureNew.SetPixel (x, y, Color.black);
			}
		}
	}

	void ConnectPixel(int X1, int Y1, int X2, int Y2){
		X1 = X1 * 5;
		Y1 = Y1 * 5;
		oldPixel = new Vector2(X2*5,Y2*5);
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
	void Marker(){

		indexX [0] = 1;
		indexY [0] = 0;
		indexZ [0] = 0;

		indexX [1] = -1;
		indexY [1] = 0;
		indexZ [1] = 0;

		indexX [2] = 0;
		indexY [2] = 1;
		indexZ [2] = 0;

		indexX [3] = 0;
		indexY [3] = -1;
		indexZ [3] = 0;

		indexX [4] = 0;
		indexY [4] = 0;
		indexZ [4] = 1;

		indexX [5] = 0;
		indexY [5] = 0;
		indexZ [5] = -1;

		indexX [6] = -1;
		indexY [6] = 1;
		indexZ [6] = 0;

		indexX [7] = 1;
		indexY [7] = 1;
		indexZ [7] = 0;

		indexX [8] = -1;
		indexY [8] = -1;
		indexZ [8] = 0;

		indexX [9] = 1;
		indexY [9] = -1;
		indexZ [9] = 0;

		indexX [10] = 1;
		indexY [10] = 0;
		indexZ [10] = 1;

		indexX [11] = -1;
		indexY [11] = 0;
		indexZ [11] = 1;

		indexX [12] = 0;
		indexY [12] = 1;
		indexZ [12] = 1;

		indexX [13] = 0;
		indexY [13] = -1;
		indexZ [13] = 1;

		indexX [14] = 1;
		indexY [14] = 0;
		indexZ [14] = -1;

		indexX [15] = -1;
		indexY [15] = 0;
		indexZ [15] = -1;

		indexX [16] = 0;
		indexY [16] = 1;
		indexZ [16] = -1;

		indexX [17] = 0;
		indexY [17] = -1;
		indexZ [17] = -1;

		indexX [18] = 1;
		indexY [18] = 1;
		indexZ [18] = 1;

		indexX [19] = 1;
		indexY [19] = -1;
		indexZ [19] = 1;

		indexX [20] = -1;
		indexY [20] = -1;
		indexZ [20] = 1;

		indexX [21] = -1;
		indexY [21] = 1;
		indexZ [21] = 1;

		indexX [22] = 1;
		indexY [22] = 1;
		indexZ [22] = -1;

		indexX [23] = 1;
		indexY [23] = -1;
		indexZ [23] = -1;

		indexX [24] = -1;
		indexY [24] = -1;
		indexZ [24] = -1;

		indexX [25] = -1;
		indexY [25] = 1;
		indexZ [25] = -1;

		indexX [26] = 2;
		indexY [26] = 0;
		indexZ [26] = 0;

		indexX [27] = -2;
		indexY [27] = 0;
		indexZ [27] = 0;

		indexX [28] = 0;
		indexY [28] = 2;
		indexZ [28] = 0;

		indexX [29] = 0;
		indexY [29] = -2;
		indexZ [29] = 0;

		indexX [30] = 0;
		indexY [30] = 0;
		indexZ [30] = 2;

		indexX [31] = 0;
		indexY [31] = 0;
		indexZ [31] = -2;

		indexX [32] = 2;
		indexY [32] = 0;
		indexZ [32] = 1;

		indexX [33] = -2;
		indexY [33] = 0;
		indexZ [33] = 1;

		indexX [34] = 0;
		indexY [34] = 2;
		indexZ [34] = 1;

		indexX [35] = 0;
		indexY [35] = -2;
		indexZ [35] = 1;

		indexX [36] = 2;
		indexY [36] = 0;
		indexZ [36] = -1;

		indexX [37] = -2;
		indexY [37] = 0;
		indexZ [37] = -1;

		indexX [38] = 0;
		indexY [38] = 2;
		indexZ [38] = -1;

		indexX [39] = 0;
		indexY [39] = -2;
		indexZ [39] = -1;

		indexX [40] = 1;
		indexY [40] = 0;
		indexZ [40] = 2;

		indexX [41] = -1;
		indexY [41] = 0;
		indexZ [41] = 2;

		indexX [42] = 0;
		indexY [42] = 1;
		indexZ [42] = 2;

		indexX [43] = 0;
		indexY [43] = -1;
		indexZ [43] = 2;

		indexX [44] = 1;
		indexY [44] = 0;
		indexZ [44] = -2;

		indexX [45] = -1;
		indexY [45] = 0;
		indexZ [45] = -2;

		indexX [46] = 0;
		indexY [46] = 1;
		indexZ [46] = -2;

		indexX [47] = 0;
		indexY [47] = -1;
		indexZ [47] = -2;

		indexX [48] = 1;
		indexY [48] = 2;
		indexZ [48] = 0;

		indexX [49] = 2;
		indexY [49] = 1;
		indexZ [49] = 0;

		indexX [50] = 2;
		indexY [50] = -1;
		indexZ [50] = 0;

		indexX [51] = 1;
		indexY [51] = -2;
		indexZ [51] = 0;

		indexX [52] = -1;
		indexY [52] = -2;
		indexZ [52] = 0;

		indexX [53] = -2;
		indexY [53] = -1;
		indexZ [53] = 0;

		indexX [54] = -2;
		indexY [54] = 1;
		indexZ [54] = 0;

		indexX [55] = -1;
		indexY [55] = 2;
		indexZ [55] = 0;

		indexX [56] = 1;
		indexY [56] = 2;
		indexZ [56] = 1;

		indexX [57] = 2;
		indexY [57] = 1;
		indexZ [57] = 1;

		indexX [58] = 2;
		indexY [58] = -1;
		indexZ [58] = 1;

		indexX [59] = 1;
		indexY [59] = -2;
		indexZ [59] = 1;

		indexX [60] = -1;
		indexY [60] = -2;
		indexZ [60] = 1;

		indexX [61] = -2;
		indexY [61] = -1;
		indexZ [61] = 1;

		indexX [62] = -2;
		indexY [62] = 1;
		indexZ [62] = 1;

		indexX [63] = -1;
		indexY [63] = 2;
		indexZ [63] = 1;

		indexX [64] = 1;
		indexY [64] = 2;
		indexZ [64] = -1;

		indexX [65] = 2;
		indexY [65] = 1;
		indexZ [65] = -1;

		indexX [66] = 2;
		indexY [66] = -1;
		indexZ [66] = -1;

		indexX [67] = 1;
		indexY [67] = -2;
		indexZ [67] = -1;

		indexX [68] = -1;
		indexY [68] = -2;
		indexZ [68] = -1;

		indexX [69] = -2;
		indexY [69] = -1;
		indexZ [69] = -1;

		indexX [70] = -2;
		indexY [70] = 1;
		indexZ [70] = -1;

		indexX [71] = -1;
		indexY [71] = 2;
		indexZ [71] = -1;

		indexX [72] = 1;
		indexY [72] = 1;
		indexZ [72] = 2;

		indexX [73] = 1;
		indexY [73] = -1;
		indexZ [73] = 2;

		indexX [74] = -1;
		indexY [74] = -1;
		indexZ [74] = 2;

		indexX [75] = -1;
		indexY [75] = 1;
		indexZ [75] = 2;

		indexX [76] = 1;
		indexY [76] = 1;
		indexZ [76] = -2;

		indexX [77] = 1;
		indexY [77] = -1;
		indexZ [77] = -2;

		indexX [78] = -1;
		indexY [78] = -1;
		indexZ [78] = -2;

		indexX [79] = -1;
		indexY [79] = 1;
		indexZ [79] = -2;

		indexX [80] = 2;
		indexY [80] = 2;
		indexZ [80] = 0;

		indexX [81] = 2;
		indexY [81] = -2;
		indexZ [81] = 0;

		indexX [82] = -2;
		indexY [82] = -2;
		indexZ [82] = 0;

		indexX [83] = -2;
		indexY [83] = 2;
		indexZ [83] = 0;

		indexX [84] = 0;
		indexY [84] = 2;
		indexZ [84] = 2;

		indexX [85] = 2;
		indexY [85] = 0;
		indexZ [85] = 2;

		indexX [86] = 0;
		indexY [86] = -2;
		indexZ [86] = 2;

		indexX [87] = -2;
		indexY [87] = 0;
		indexZ [87] = 2;

		indexX [88] = 0;
		indexY [88] = 2;
		indexZ [88] = -2;

		indexX [89] = 2;
		indexY [89] = 0;
		indexZ [89] = -2;

		indexX [90] = 0;
		indexY [90] = -2;
		indexZ [90] = -2;

		indexX [91] = -2;
		indexY [91] = 0;
		indexZ [91] = -2;

	}
}

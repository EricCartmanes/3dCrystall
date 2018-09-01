using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CrystalTransport : MonoBehaviour {

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
	private int[,] crystalStart = new int[300,500];
	private float[,] crystalStartTime = new float[300,500];
	private int[,] crystalSize = new int[300,500];
	private int[,] crystalID = new int[300,500];
	private int[,] crystalSizeUp = new int[300,500];
	private int[,] crystalSizeDown = new int[300,500];
	private int[,] crystalSizeLeft = new int[300,500];
	private int[,] crystalSizeRight = new int[300,500];
	//private float[,] EnrgCrystalL = new float[200, 250];
	//private float[,] EnrgCrystalC = new float[200, 250];
	//private float[,] EnrgCrystalR = new float[200, 250];
	private float[,] Enrg = new float[300,500];
	private int[,] PosDetect = new int[300,500];
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
	private float sigma = 3.00f;
	private float limitGEnrg = 6;
	private float Et;
	private float sigmaCrystal=0.5f;

	private bool makeStat = false;
	private bool makeExp = false;
	private int expStep = 0;
	private int expStepLimit=100;		//количество экспериментов
	private int statStep = 0;
	private int statStepLimit=1000;
	private float[] v = new float[2260];		//темпы перехода
	private float[,] v2 = new float[126,10];		//темпы перехода
	private int[] targePosX = new int[2260];
	private int[] targePosY = new int[2260];
	private float[] p = new float[2260];		//вероятности перехода
	private float[] p2 = new float[1260];		//вероятности перехода
	private int RightLim=50;
	private int N=200;		//длина слоя на котором измеряем плотность
	private float E=0.0625f; //0.025 = 0.1kT;		напряженность электрического поля
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
	private float stepTimeLocal2Local;
	private float stepTimeLocal2Crystal;
	private float stepTimeCrystal2Local;
	private float stepTimeCrystal2Crystal;
	private int limit = 5000000;
	private int limitConst = 50000000;
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
	private float[] time200 = new float[11000];
	private float[] time5Local = new float[11000];
	private float[] time10Local = new float[11000];
	private float[] time20Local = new float[11000];
	private float[] time40Local = new float[11000];
	private float[] time100Local = new float[11000];
	private float[] time200Local = new float[11000];
	private float[] time5Crystal = new float[11000];
	private float[] time10Crystal = new float[11000];
	private float[] time20Crystal = new float[11000];
	private float[] time40Crystal = new float[11000];
	private float[] time100Crystal = new float[11000];
	private float[] time200Crystal = new float[11000];
	private float[] time5Local2Local = new float[11000];
	private float[] time10Local2Local = new float[11000];
	private float[] time20Local2Local = new float[11000];
	private float[] time40Local2Local = new float[11000];
	private float[] time100Local2Local = new float[11000];
	private float[] time200Local2Local = new float[11000];
	private float[] time5Local2Crystal = new float[11000];
	private float[] time10Local2Crystal = new float[11000];
	private float[] time20Local2Crystal = new float[11000];
	private float[] time40Local2Crystal = new float[11000];
	private float[] time100Local2Crystal = new float[11000];
	private float[] time200Local2Crystal = new float[11000];
	private float[] time5Crystal2Local = new float[11000];
	private float[] time10Crystal2Local = new float[11000];
	private float[] time20Crystal2Local = new float[11000];
	private float[] time40Crystal2Local = new float[11000];
	private float[] time100Crystal2Local = new float[11000];
	private float[] time200Crystal2Local = new float[11000];
	private float[] time5Crystal2Crystal = new float[11000];
	private float[] time10Crystal2Crystal = new float[11000];
	private float[] time20Crystal2Crystal = new float[11000];
	private float[] time40Crystal2Crystal = new float[11000];
	private float[] time100Crystal2Crystal = new float[11000];
	private float[] time200Crystal2Crystal = new float[11000];
	private float[] time5Numb = new float[11000];
	private float[] time10Numb = new float[11000];
	private float[] time20Numb = new float[11000];
	private float[] time40Numb = new float[11000];
	private float[] time100Numb = new float[11000];
	private float[] time200Numb = new float[11000];
	private float[] time5LocalNumb = new float[11000];
	private float[] time10LocalNumb = new float[11000];
	private float[] time20LocalNumb = new float[11000];
	private float[] time40LocalNumb = new float[11000];
	private float[] time100LocalNumb = new float[11000];
	private float[] time200LocalNumb = new float[11000];
	private float[] time5CrystalNumb = new float[11000];
	private float[] time10CrystalNumb = new float[11000];
	private float[] time20CrystalNumb = new float[11000];
	private float[] time40CrystalNumb = new float[11000];
	private float[] time100CrystalNumb = new float[11000];
	private float[] time200CrystalNumb = new float[11000];
	private float[] time5Local2LocalNumb = new float[11000];
	private float[] time10Local2LocalNumb = new float[11000];
	private float[] time20Local2LocalNumb = new float[11000];
	private float[] time40Local2LocalNumb = new float[11000];
	private float[] time100Local2LocalNumb = new float[11000];
	private float[] time200Local2LocalNumb = new float[11000];
	private float[] time5Local2CrystalNumb = new float[11000];
	private float[] time10Local2CrystalNumb = new float[11000];
	private float[] time20Local2CrystalNumb = new float[11000];
	private float[] time40Local2CrystalNumb = new float[11000];
	private float[] time100Local2CrystalNumb = new float[11000];
	private float[] time200Local2CrystalNumb = new float[11000];
	private float[] time5Crystal2LocalNumb = new float[11000];
	private float[] time10Crystal2LocalNumb = new float[11000];
	private float[] time20Crystal2LocalNumb = new float[11000];
	private float[] time40Crystal2LocalNumb = new float[11000];
	private float[] time100Crystal2LocalNumb = new float[11000];
	private float[] time200Crystal2LocalNumb = new float[11000];
	private float[] time5Crystal2CrystalNumb = new float[11000];
	private float[] time10Crystal2CrystalNumb = new float[11000];
	private float[] time20Crystal2CrystalNumb = new float[11000];
	private float[] time40Crystal2CrystalNumb = new float[11000];
	private float[] time100Crystal2CrystalNumb = new float[11000];
	private float[] time200Crystal2CrystalNumb = new float[11000];
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
	private float middleTime200;
	private float middleTime200Rev;
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
	private float middleTime200Local;
	private float middleTime200RevLocal;
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
	private float middleTime200Crystal;
	private float middleTime200RevCrystal;
	private float middleTime5Numb;
	private float middleTime10Numb;
	private float middleTime20Numb;
	private float middleTime40Numb;
	private float middleTime100Numb;
	private float middleTime200Numb;
	private float middleTime5LocalNumb;
	private float middleTime10LocalNumb;
	private float middleTime20LocalNumb;
	private float middleTime40LocalNumb;
	private float middleTime100LocalNumb;
	private float middleTime200LocalNumb;
	private float middleTime5CrystalNumb;
	private float middleTime10CrystalNumb;
	private float middleTime20CrystalNumb;
	private float middleTime40CrystalNumb;
	private float middleTime100CrystalNumb;
	private float middleTime200CrystalNumb;
	private float middleTime5Local2Local;
	private float middleTime5Local2LocalRev;
	private float middleTime10Local2Local;
	private float middleTime10Local2LocalRev;
	private float middleTime20Local2Local;
	private float middleTime20Local2LocalRev;
	private float middleTime40Local2Local;
	private float middleTime40Local2LocalRev;
	private float middleTime100Local2Local;
	private float middleTime100Local2LocalRev;
	private float middleTime200Local2Local;
	private float middleTime200Local2LocalRev;
	private float middleTime5Local2Crystal;
	private float middleTime5Local2CrystalRev;
	private float middleTime10Local2Crystal;
	private float middleTime10Local2CrystalRev;
	private float middleTime20Local2Crystal;
	private float middleTime20Local2CrystalRev;
	private float middleTime40Local2Crystal;
	private float middleTime40Local2CrystalRev;
	private float middleTime100Local2Crystal;
	private float middleTime100Local2CrystalRev;
	private float middleTime200Local2Crystal;
	private float middleTime200Local2CrystalRev;
	private float middleTime5Crystal2Local;
	private float middleTime5Crystal2LocalRev;
	private float middleTime10Crystal2Local;
	private float middleTime10Crystal2LocalRev;
	private float middleTime20Crystal2Local;
	private float middleTime20Crystal2LocalRev;
	private float middleTime40Crystal2Local;
	private float middleTime40Crystal2LocalRev;
	private float middleTime100Crystal2Local;
	private float middleTime100Crystal2LocalRev;
	private float middleTime200Crystal2Local;
	private float middleTime200Crystal2LocalRev;
	private float middleTime5Crystal2Crystal;
	private float middleTime5Crystal2CrystalRev;
	private float middleTime10Crystal2Crystal;
	private float middleTime10Crystal2CrystalRev;
	private float middleTime20Crystal2Crystal;
	private float middleTime20Crystal2CrystalRev;
	private float middleTime40Crystal2Crystal;
	private float middleTime40Crystal2CrystalRev;
	private float middleTime100Crystal2Crystal;
	private float middleTime100Crystal2CrystalRev;
	private float middleTime200Crystal2Crystal;
	private float middleTime200Crystal2CrystalRev;
	private float middleTime5Local2LocalNumb;
	private float middleTime20Local2LocalNumb;
	private float middleTime40Local2LocalNumb;
	private float middleTime10Local2LocalNumb;
	private float middleTime100Local2LocalNumb;
	private float middleTime200Local2LocalNumb;
	private float middleTime5Local2CrystalNumb;
	private float middleTime20Local2CrystalNumb;
	private float middleTime40Local2CrystalNumb;
	private float middleTime10Local2CrystalNumb;
	private float middleTime100Local2CrystalNumb;
	private float middleTime200Local2CrystalNumb;
	private float middleTime5Crystal2LocalNumb;
	private float middleTime20Crystal2LocalNumb;
	private float middleTime40Crystal2LocalNumb;
	private float middleTime10Crystal2LocalNumb;
	private float middleTime100Crystal2LocalNumb;
	private float middleTime200Crystal2LocalNumb;
	private float middleTime5Crystal2CrystalNumb;
	private float middleTime20Crystal2CrystalNumb;
	private float middleTime40Crystal2CrystalNumb;
	private float middleTime10Crystal2CrystalNumb;
	private float middleTime100Crystal2CrystalNumb;
	private float middleTime200Crystal2CrystalNumb;


	private float dispTime5;
	private float dispInvTime5;
	private float dispTime5Crystal;
	private float dispInvTime5Crystal;
	private float dispTime5local;
	private float dispInvTime5Local;
	private float disptimeNumb5;
	private float disptimeNumb5Crystal;
	private float disptimeNumb5Local;
	private float dispTime10;
	private float dispInvTime10;
	private float dispTime10Crystal;
	private float dispInvTime10Crystal;
	private float dispTime10local;
	private float dispInvTime10Local;
	private float disptimeNumb10;
	private float disptimeNumb10Crystal;
	private float disptimeNumb10Local;
	private float dispTime20;
	private float dispInvTime20;
	private float dispTime20Crystal;
	private float dispInvTime20Crystal;
	private float dispTime20local;
	private float dispInvTime20Local;
	private float disptimeNumb20;
	private float disptimeNumb20Crystal;
	private float disptimeNumb20Local;
	private float dispTime40;
	private float dispInvTime40;
	private float dispTime40Crystal;
	private float dispInvTime40Crystal;
	private float dispTime40local;
	private float dispInvTime40Local;
	private float disptimeNumb40;
	private float disptimeNumb40Crystal;
	private float disptimeNumb40Local;
	private float dispTime100;
	private float dispInvTime100;
	private float dispTime100Crystal;
	private float dispInvTime100Crystal;
	private float dispTime100local;
	private float dispInvTime100Local;
	private float disptimeNumb100;
	private float disptimeNumb100Crystal;
	private float disptimeNumb100Local;
	private float dispTime200;
	private float dispInvTime200;
	private float dispTime200Crystal;
	private float dispInvTime200Crystal;
	private float dispTime200local;
	private float dispInvTime200Local;
	private float disptimeNumb200;
	private float disptimeNumb200Crystal;
	private float disptimeNumb200Local;

	private int timeNumb;
	private int timeLocalNumb;
	private int timeCrystalNumb;
	private int timeLocal2LocalNumb;
	private int timeLocal2CrystalNumb;
	private int timeCrystal2LocalNumb;
	private int timeCrystal2CrystalNumb;
	private bool time5Bool = false;
	private bool time10Bool = false;
	private bool time20Bool = false;
	private bool time40Bool = false;
	private bool time100Bool = false;
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
	private bool error = false;
	private float percent = 50;		//~30%
	private int[] LocalCrystallID = new int[800];
	private float[] LocalCrystallEnrg = new float[800];
	private float[] LocalCrystallC = new float[800];
	private int[] LocalCrystallA = new int[800];
	private int B = 0;
	private bool saveTemp = false;
	private float[,] lenghtChainGlobal = new float[32,2000];
	private bool trueKvaz = false;
	private float trueKvazPercent =0.5f;	//Первый гаусс
	public AudioSource endMusic;
	private int CCJumpX2=0;
	private List<Vector3> allAviableState = new List<Vector3>();
	private List<Vector3> bestAviableState = new List<Vector3>();	//исключим темпы на однин и тот же сайт
	private List<Vector3> endAviableState = new List<Vector3>();	//исключим темпа на одну цепь кристаллита
	private bool live=true;


	// Use this for initialization
	void Start () {
		//LocalCrystallID [0] = 7;
		//LocalCrystallEnrg[2] = 12;
		//LocalCrystallC [3] = 5;
		//LocalCrystallA [8] = 3;
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

		GenerateMap ();
		Marker ();
		//chance = (int)density.GetComponent<Slider> ().value;


		ReRender ();
	}

	private void ReRender(){
		//RenderFull ();

		ClearTexture ();
		for (int x = 0; x <= 299; x++) {
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
		for(int x=25;x<=275; x++){
			for(int y=25;y<=475; y++){
				if (Random.Range(0,99)<percent) {
					Enrg [x, y] = RndEnrg1Crystal ()- Et * kT;
				} else {
					Enrg [x, y] = RndEnrg1 ();
				}

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
		if (Input.anyKeyDown) {
			live = true;
		}

		if (makeExp) {
			while (posX < 50 + N && posY > 50 && posY < 450 && !error) {
				if (!kvas) {
					//Enrg [posX, posY] = RndEnrg1Crystal () - Et * kT - sigma * sigmaCrystal * sigma * sigmaCrystal * kT;
					if (Random.Range (0f, 1f) < trueKvazPercent) {
						Enrg [posX, posY] = RndEnrg1() - sigma * sigma * kT;
					} else {
						Enrg [posX, posY] = RndEnrg1Crystal () - Et * kT - sigma * sigmaCrystal * sigma * sigmaCrystal * kT;
					}

					/*if (Random.Range (0f, 1f) < trueKvazPercent) {		//Если первый Гаусс
						if (crystalID [posX, posY] == 0) {		//Если локализованное состояние
							trueKvaz = true;
						} else {
							while (!trueKvaz) {
								ReRender ();
								if (crystalID [posX, posY] == 0) {
									trueKvaz = true;
								}
							}
						}
						Enrg [posX, posY] = Enrg [posX, posY] - sigma * sigma * kT;
					} else {	// Если второй Гаусс
						if (crystalID [posX, posY] != 0) {
							trueKvaz = true;
						} else {
							while (!trueKvaz) {
								ReRender ();
								if (crystalID [posX, posY] != 0) {
									trueKvaz = true;
								}
							}
						}

						crystalIDTemp = crystalID[posX,posY];
						crystalCenterX = crystalIDX[crystalIDTemp];
						crystalCenterY = crystalIDY[crystalIDTemp];

						if (crystalStart [crystalCenterX, crystalCenterY] == 2) {		//если ориентация горизонтальная
							for (int xx = -crystalSizeLeft [crystalCenterX, crystalCenterY]; xx <= crystalSizeRight [crystalCenterX, crystalCenterY]; xx++) {
								Enrg [posX+xx, posY] = Enrg [posX+xx, posY] - sigma * sigmaCrystal * sigma * sigmaCrystal * kT;
							}
						} else {
							for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {
								Enrg [posX, posY+yy] = Enrg [posX, posY+yy] - sigma * sigmaCrystal * sigma * sigmaCrystal * kT;
							}
						}
					}*/

					kvas = true;
				}
			//if(Input.anyKeyDown){
				g++;
				if (posX < 30 || posY < 30 || posY > 470) {
					posX = 275;
					stepTime = 0;
					stepTimeLocal = 0;
					stepTimeLocal2Local = 0;
					stepTimeLocal2Crystal = 0;
					stepTimeCrystal = 0;
					stepTimeCrystal2Local = 0;
					stepTimeCrystal2Crystal = 0;
					error = true;

				}



				if (crystalID [posX, posY] == 0 && !error) {	//если прыжок НЕ из кристалла
					timeLocalNumb++;
					timeNumb++;
					A = 0;
					allAviableState.Clear ();
					allAviableState.RemoveRange (0, allAviableState.Count);
					for (int xx = -2; xx <= 2; xx++) {
						for (int yy = -2; yy <= 2; yy++) {
							if (xx != 0 || yy != 0) {
								c = xx * xx + yy * yy;
								delE = Enrg [posX + xx, posY + yy] - E * e * r * xx - Enrg [posX, posY];
								if (posY + yy < 50 || posY + yy > 450 || posX + xx < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10f) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX + xx;
								targePosY [A] = posY + yy;
								//Debug.Log ("[" + (int)(X + xx) + "," + (int)(Y + yy) + "] = " + v [A]+"                 [delE]= "+delE+"   "+Mathf.Exp((delE + Mathf.Abs (delE))/ (-2 * kT)) );
								//Debug.Log ("1");
								allAviableState.Add (new Vector3 (targePosX [A], targePosY [A], v [A]));
								//CheckRepeat (posX + xx, posY + yy, c);
								A++;

							}
						}
					}
					A--;
					c = 0;


					CalculateTemp ();
						


					for (i = 0; i < endAviableState.Count; i++) {
						c += endAviableState [i].z;		//временная сумма всех темпов
						//Debug.Log(i+")   "+endAviableState [i].z);
					}
					tempSumm = c;

					for (i = 0; i < endAviableState.Count; i++) {
						p [i] = endAviableState [i].z / c;
						//Debug.Log (i + ") " + p [i]);
					}

					d = Random.Range (0, 1f);
					i = 0;
					c = 0;
					while (c <= d) {
						c += p [i];
						i++;
					}
					i--;

					A = 0;

					T = ((-Mathf.Log (Random.Range (0.0000001f, 1f))) / (tempSumm));
					stepTimeLocal += T;
					if (crystalID [(int)endAviableState [i].x, (int)endAviableState [i].y] == 0) {
						stepTimeLocal2Local += T;
						timeLocal2LocalNumb++;
					} else {
						stepTimeLocal2Crystal += T;
						timeLocal2CrystalNumb++;
					}
				} else if (!error) {	//если прыжок ИЗ кристалла
					timeCrystalNumb++;
					timeNumb++;
					allAviableState.Clear ();
					allAviableState.RemoveRange (0, allAviableState.Count);
					crystalIDTemp = crystalID [posX, posY];
					crystalCenterX = crystalIDX [crystalIDTemp];
					crystalCenterY = crystalIDY [crystalIDTemp];
					A = 0;
					if (crystalStart [crystalCenterX, crystalCenterY] == 2) {		//если ориентация горизорнтлаьная
						for (int xx0 = -crystalSizeLeft [crystalCenterX, crystalCenterY]; xx0 <= crystalSizeRight [crystalCenterX, crystalCenterY]; xx0++) {		//проходим по цепеочке
							for (int xx = -2; xx <= 2; xx++) {		//из каждой точки цепи кристаллита счиатем темпы
								for (int yy = -2; yy <= 2; yy++) {
									if ((yy != 0) || (crystalID [crystalCenterX + xx0 + xx, posY] != crystalIDTemp)) {		//не считаем с этой цепи на эту же цепь
										c = xx * xx + yy * yy;
										delE = Enrg [crystalCenterX + xx0 + xx, posY + yy] - E * e * r * (xx) - Enrg [crystalCenterX + xx0, posY];
										if (posY + yy < 50 || posY + yy > 450 || crystalCenterX + xx0 + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = crystalCenterX + xx0 + xx;
										targePosY [A] = posY + yy;
										allAviableState.Add (new Vector3 (targePosX [A], targePosY [A], v [A]));
										//CheckRepeat (crystalCenterX + xx, posY + yy, c);
										A++;

									}
								}
							}
						}
					} else {		//если ориентация вертиклаьная
						for (int yy0 = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy0 <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy0++) {		//проходим по цепеочке
							for (int xx = -2; xx <= 2; xx++) {		//из каждой точки цепи кристаллита счиатем темпы
								for (int yy = -2; yy <= 2; yy++) {
									if ((xx != 0) || (crystalID [posX, crystalCenterY+yy0+yy] != crystalIDTemp)) {		//не считаем с этой цепи на эту же цепь
										c = xx * xx + yy * yy;
										delE = Enrg [posX + xx, crystalCenterY + yy0+yy] - E * e * r * (xx) - Enrg [posX,crystalCenterY + yy0];
										if (crystalCenterY + yy0 +yy < 50 || crystalCenterY + yy0 +yy > 450 || posX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] =  posX + xx;
										targePosY [A] = crystalCenterY + yy0 + yy;
										allAviableState.Add (new Vector3 (targePosX [A], targePosY [A], v [A]));
										//CheckRepeat (crystalCenterX + xx, posY + yy, c);
										A++;
									}
								}
							}
						}
					}


					A--;
					c = 0;
					CalculateTemp ();


					for (i = 0; i < endAviableState.Count; i++) {
						c += endAviableState [i].z;		//временная сумма всех темпов
					}
					tempSumm = c;

					for (i = 0; i < endAviableState.Count; i++) {
						p [i] = endAviableState [i].z / c;
						//Debug.Log (i + ") " + p [i]);
					}

					d = Random.Range (0, 1f);
					i = 0;
					c = 0;
					while (c <= d) {
						c += p [i];
						i++;
					}
					i--;

					A = 0;

					T = ((-Mathf.Log (Random.Range (0.0000001f, 1f))) / (tempSumm));
					stepTimeCrystal += T;
					if (crystalID [(int)endAviableState [i].x, (int)endAviableState [i].y] == 0) {
						stepTimeCrystal2Local += T;
						timeCrystal2LocalNumb++;
					} else {
						stepTimeCrystal2Crystal += T;
						timeCrystal2CrystalNumb++;
					}




				}

				/*if (crystalID [posX, posY] == 0 && !error) {	//если прыжок НЕ из кристалла
					timeLocalNumb++;
					timeNumb++;
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
								//Debug.Log ("1");
								CheckRepeat (posX + xx,posY + yy,c);
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
					if (crystalID [targePosX [i], targePosY [i]] == 0) {
						stepTimeLocal2Local += T;
						timeLocal2LocalNumb++;
					} else {
						stepTimeLocal2Crystal += T;
						timeLocal2CrystalNumb++;
					}
				} else if(!error){	//если прыжок ИЗ кристалла
					timeCrystalNumb++;
					timeNumb++;
					crystalIDTemp = crystalID[posX,posY];
					crystalCenterX = crystalIDX[crystalIDTemp];
					crystalCenterY = crystalIDY[crystalIDTemp];
					float frenq=10;
					A = 0;
					if (crystalStart [crystalCenterX, crystalCenterY] == 2) {	//если ориентация горизорнтлаьная
						if ((posY != (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY])) && (posY != (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]))) {		//Если НЕ крайняя цепь кристаллита
							//for (int xx = -2; xx <= -1; xx++) {
								for (int yy = -2; yy <= 2; yy++) {		//прыжок влево
								int xx=-1;		
								c = xx * xx + yy * yy;
									delE = Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx, posY + yy] - E * e * r * xx - Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY], posY];
									if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx) < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] + xx;
									targePosY [A] = posY + yy;
									A++;
									//Debug.Log ("1");
									CheckRepeat (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx, posY + yy,c);
								//}
							}
							for (int yy = -1; yy <= 1; yy++) {
								if (yy != 0) {
									c = 1;
									delE = Enrg [crystalCenterX, posY + yy] - E * e * r * (0) - Enrg [crystalCenterX, posY];
									if (posY - yy < 50 || posY + yy > 450 || posX < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX+crystalSizeRight [crystalCenterX, crystalCenterY];
									targePosY [A] = posY + yy;
									A++;
								}
							}
							//for (int xx = 1; xx <= 2; xx++) {
								for (int yy = -2; yy <= 2; yy++) {		//прыжок вправо
								int xx=1;		
								c = xx * xx + yy * yy;
									delE = Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx, posY + yy] - E * e * r * (xx) - Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY], posY];
									if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx) < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx;
									targePosY [A] = posY + yy;
									A++;
									//Debug.Log ("1");
									CheckRepeat (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx, posY + yy,c);
								//}
							}
						} else {		//Если крайняя цепь кристалла
							//for (int xx = -2; xx <= -1; xx++) {
								for (int yy = -2; yy <= 2; yy++) {		//прыжок влево
								int xx=-1;		
								c = xx * xx + yy * yy;
									delE = Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx, posY + yy] - E * e * r * xx - Enrg [crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY], posY];
									if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx) < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] + xx;
									targePosY [A] = posY + yy;
									A++;
									//Debug.Log ("1");
									CheckRepeat (crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY] +xx, posY + yy,c);
								//}
							}
							//for (int xx = 1; xx <= 2; xx++) {
								for (int yy = -2; yy <= 2; yy++) {		//прыжок вправо
								int xx=1;	
								c = xx * xx + yy * yy;
									delE = Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx, posY + yy] - E * e * r * (xx) - Enrg [crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY], posY];
									if (posY + yy < 50 || posY + yy > 450 || (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx) < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx;
									targePosY [A] = posY + yy;
									A++;
									//Debug.Log ("1");
									CheckRepeat (crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY] + xx, posY + yy,c);
								//}
							}
							if ((posY == (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY])) && ((crystalSizeDown [crystalCenterX, crystalCenterY] != 0) && (crystalSizeUp [crystalCenterX, crystalCenterY] != 0))) {		//если верхний край
								c = 1;
								delE = Enrg [crystalCenterX, posY - 1] - E * e * r * (0) - Enrg [crystalCenterX, posY];
								if (posY - 1 < 50 || posY - 1 > 450 || posX < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX+crystalSizeRight [crystalCenterX, crystalCenterY];
								targePosY [A] = posY - 1;
								A++;
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вверх
									//for (int yy = 1; yy <= 2; yy++) {
									int yy=1;		
									c = yy*yy;
										delE = Enrg [crystalCenterX + xx, posY + yy] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
										if (posY + yy < 50 || posY + yy > 450 || crystalCenterX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = crystalCenterX + xx;
										targePosY [A] = posY + yy;
										A++;
										//Debug.Log ("1");
										CheckRepeat (crystalCenterX + xx, posY + yy,c);
									//}
								}
							}
							if ((posY == (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]))  && ((crystalSizeDown [crystalCenterX, crystalCenterY] != 0) && (crystalSizeUp [crystalCenterX, crystalCenterY] != 0))) {		//если нижний край
								c = 1;
								delE = Enrg [crystalCenterX, posY + 1] - E * e * r * (0) - Enrg [crystalCenterX, posY];
								if (posY + 1 < 50 || posY + 1 > 450 || posX < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = crystalCenterX+crystalSizeRight [crystalCenterX, crystalCenterY];
								targePosY [A] = posY + 1;
								A++;
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вниз
									//for (int yy = -2; yy <= -1; yy++) {
									int yy=-1;		
									c = yy * yy;
										delE = Enrg [crystalCenterX + xx, posY + yy] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
										if (posY + yy < 50 || posY + yy > 450 || crystalCenterX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = crystalCenterX + xx;
										targePosY [A] = posY + yy;
										A++;
										//Debug.Log ("1");
										CheckRepeat (crystalCenterX + xx, posY + yy, c);
									//}
								}
							}
							if ((crystalSizeDown [crystalCenterX, crystalCenterY] == 0) && (crystalSizeUp [crystalCenterX, crystalCenterY] == 0)) {		//Если цепь ВСЕГО одна
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вверх
									//for (int yy = 1; yy <= 2; yy++) {
									int yy=1;		
									c = xx*xx+yy*yy;
										delE = Enrg [crystalCenterX + xx, posY + yy] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
										if (posY + yy < 50 || posY + yy > 450 || crystalCenterX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = crystalCenterX + xx;
										targePosY [A] = posY + yy;
										//Debug.Log ("1");
										CheckRepeat (crystalCenterX + xx, posY + yy, c);
										A++;
									//}
								}
								for (int xx = -crystalSizeLeft[crystalCenterX, crystalCenterY]; xx <= crystalSizeRight[crystalCenterX, crystalCenterY]; xx++) {		//прыжок вниз
									//for (int yy = -2; yy <= -1; yy++) {
									int yy=-1;		
									c = xx*xx+yy*yy;
										delE = Enrg [crystalCenterX + xx, posY +yy] - E * e * r * (0) - Enrg [crystalCenterX + xx, posY];
										if (posY + yy < 50 || posY + yy > 450 || crystalCenterX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = crystalCenterX + xx;
										targePosY [A] = posY + yy;
										//Debug.Log ("1");
										CheckRepeat (crystalCenterX + xx, posY + yy, c);
										A++;
									//}
								}
							}
						}
					} else {		//Вертиклаьная ориентация
						if ((posX != crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY]) && (posX != crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY])) {		//Если НЕ крайняя цепь кристаллита
							for (int xx = -2; xx <= 2; xx++) {		//прыжок вниз
								//for (int yy = -2; yy <= -1; yy++) {
								int yy=-1;		
								c = yy * yy + xx * xx;
									delE = Enrg [posX + xx, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]];
									if ((crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy) < 50 || (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy) > 450 || posX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy;
									//Debug.Log ("1");
									CheckRepeat (posX + xx, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy, c);
									A++;
								//}
							}
							for (int xx = -1; xx <= 1; xx++) {
								if (xx != 0) {
									c = 1;
									delE = Enrg [posX+xx,crystalCenterY] - E * e * r * (xx) - Enrg [posX,crystalCenterY];
									if (posY < 50 || posY > 450 || posX+xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY;
									A++;
								}
							}
							for (int xx = -1; xx <= 1; xx++) {		//прыжок верх
								//for (int yy = 1; yy <= 2; yy++) {
								int yy=1;		
								c = yy * yy + xx * xx;
									delE = Enrg [posX + xx, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]];
									if ((crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy) < 50 || (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy) > 450 || posX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy;
									//Debug.Log ("1");
									CheckRepeat (posX + xx, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy, c);
									A++;
								//}
							}
						} else {		//Если крайняя цепь кристалла
							for (int xx = -2; xx <= 2; xx++) {		//прыжок вниз
								//for (int yy = -1; yy <= -2; yy++) {
								int yy=-1;		
								c = yy * yy + xx * xx;
									delE = Enrg [posX + xx, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]];
									if ((crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy) < 50 || (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy) > 450 || posX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy;
									//Debug.Log ("1");
									CheckRepeat (posX + xx, crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY] + yy, c);
									A++;
								//}
							}
							for (int xx = -1; xx <= 1; xx++) {		//прыжок верх
								//for (int yy = 1; yy <= 2; yy++) {
								int yy=1;	
								c = yy * yy + xx * xx;
									delE = Enrg [posX + xx, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY]];
									if ((crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy) < 50 || (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy) > 450 || posX + xx < 50) {
										v [A] = 0;
									} else {
										v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
									}
									targePosX [A] = posX + xx;
									targePosY [A] = crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy;
									//Debug.Log ("1");
									CheckRepeat (posX + xx, crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY] + yy, c);
									A++;
								//}
							}
							if ((posX == crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY]) && ((crystalSizeLeft [crystalCenterX, crystalCenterY] != 0) && (crystalSizeRight [crystalCenterX, crystalCenterY] != 0))) {		//если правый край
								c = 1;
								delE = Enrg [posX-1,crystalCenterY] - E * e * r * (-1) - Enrg [crystalCenterX, posY];
								if (posY < 50 || posY > 450 || posX-1 < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX-1;
								targePosY [A] = crystalCenterY;
								A++;
								//for (int xx = 1; xx <= 2; xx++) {
									for (int yy = -crystalSizeDown[crystalCenterX, crystalCenterY]; yy <= crystalSizeUp[crystalCenterX, crystalCenterY]; yy++) {		//прыжок вправо
									int xx=1;	
									c = xx*xx;
										delE = Enrg [posX+xx,crystalCenterY + yy] - E * e * r * (xx) - Enrg [posX,crystalCenterY + yy];
										if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX+xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = posX+xx;
										targePosY [A] = crystalCenterY + yy;
										//Debug.Log ("1");
										CheckRepeat (posX+xx,crystalCenterY + yy, c);
										A++;
									//}
								}
							}
							if ((posY == crystalCenterY - crystalSizeLeft [crystalCenterX, crystalCenterY]) && ((crystalSizeLeft [crystalCenterX, crystalCenterY] != 0) && (crystalSizeRight [crystalCenterX, crystalCenterY] != 0))) {		//если левый край
								c = 1;
								delE = Enrg [posX+1,crystalCenterY] - E * e * r * (1) - Enrg [posX,crystalCenterY];
								if (posY < 50 || posY > 450 || posX+1 < 50) {
									v [A] = 0;
								} else {
									v [A] = Mathf.Exp (-frenqBig * (Mathf.Sqrt (c)) + frenqBig) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
								}
								targePosX [A] = posX+1;
								targePosY [A] = crystalCenterY;
								A++;
								//for (int xx = -2; xx <= -1; xx++) {
									for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {		//прыжок влево
									int xx=-1;	
									c = xx*xx;
										delE = Enrg [posX + xx, crystalCenterY + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY + yy];
										if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = posX + xx;
										targePosY [A] = crystalCenterY + yy;
										//Debug.Log ("1");
										CheckRepeat (posX + xx, crystalCenterY + yy, c);
										A++;
									//}
								}
							}
							if ((crystalSizeLeft [crystalCenterX, crystalCenterY] == 0) && (crystalSizeRight [crystalCenterX, crystalCenterY] == 0)) {		//если цепь ВСЕГО одна
								//for (int xx = 1; xx <= 2; xx++) {
									for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {		//прыжок вправо
									int xx=1;	
									c = xx*xx;
										delE = Enrg [posX + xx, crystalCenterY + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY + yy];
										if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = posX + xx;
										targePosY [A] = crystalCenterY + yy;
										//Debug.Log ("1");
										CheckRepeat (posX + xx, crystalCenterY + yy, c);
										A++;
									//}
								}
								//for (int xx = -2; xx <= -1; xx++) {
									for (int yy = -crystalSizeDown [crystalCenterX, crystalCenterY]; yy <= crystalSizeUp [crystalCenterX, crystalCenterY]; yy++) {		//прыжок влево
										int xx=-1;
										c = xx*xx;
										delE = Enrg [posX + xx, crystalCenterY + yy] - E * e * r * (xx) - Enrg [posX, crystalCenterY + yy];
										if (crystalCenterY + yy < 50 || crystalCenterY + yy > 450 || posX + xx < 50) {
											v [A] = 0;
										} else {
											v [A] = Mathf.Exp (-10 * (Mathf.Sqrt (c)) + 10) * Mathf.Exp ((delE + Mathf.Abs (delE)) / (-2 * kT));
										}
										targePosX [A] = posX + xx;
										targePosY [A] = crystalCenterY + yy;
										//Debug.Log ("1");
										CheckRepeat (posX + xx, crystalCenterY + yy, c);
										A++;
									//}
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

					//if (targePosX [i] < 50) {
					//	Debug.Log ("Error i = " + i);
					//	crystalIDTemp = crystalID[posX,posY];
					//	crystalCenterX = crystalIDX[crystalIDTemp];
					//	crystalCenterY = crystalIDY[crystalIDTemp];
					//	Debug.Log ("Crystal center   ["+crystalCenterX+" , "+crystalCenterY+"]");
					//	if (crystalStart [crystalCenterX, crystalCenterY] == 2) {
					//		Debug.Log ("Horisontal");
					//	} else {
					//		Debug.Log ("Vertical");
					//	}
					//	Debug.Log ("Size UP = "+crystalSizeUp[crystalCenterX,crystalCenterY]);
					//	Debug.Log ("Size DOWN = "+crystalSizeDown[crystalCenterX,crystalCenterY]);
					//	Debug.Log ("Size LEFT = "+crystalSizeLeft[crystalCenterX,crystalCenterY]);
					//	Debug.Log ("Size RIGHT = "+crystalSizeRight[crystalCenterX,crystalCenterY]);
					//	Debug.Log ("Current Position   ["+posX+" , "+posY+"]");
					//	for (i = 0; i <= A; i++) {
					//		Debug.Log("["+i+"]   ->   ["+targePosX[i]+" , "+targePosY[i]+"]         "+v[i]);
					//	}
					//}
					A=0;




					T = ((-Mathf.Log (Random.Range (0.0000001f, 1f))) / (tempSumm));
					stepTimeCrystal += T;
					if (crystalID [targePosX [i], targePosY [i]] == 0) {
						stepTimeCrystal2Local += T;
						timeCrystal2LocalNumb++;
					} else {
						stepTimeCrystal2Crystal += T;
						timeCrystal2CrystalNumb++;
						if (Vector2.Distance (new Vector2(targePosX[i],targePosY[i]),new Vector2(posX,posY)) > 1.8f) {
							//Debug.Log ("=====================");
							//Debug.Log (" ");
							//Debug.Log ("Jump crystall -> crystall with range >= 2");

							crystalIDTemp = crystalID[posX,posY];
							crystalCenterX = crystalIDX[crystalIDTemp];
							crystalCenterY = crystalIDY[crystalIDTemp];

							if (crystalStart [crystalCenterX, crystalCenterY] == 2) {	//если ориентация горизорнтлаьная
								if ((posY == (crystalCenterY + crystalSizeUp [crystalCenterX, crystalCenterY])) || (posY == (crystalCenterY - crystalSizeDown [crystalCenterX, crystalCenterY]))) {	
									//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!");
									//Debug.Log ("Крайняя Цепь");
									//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!");
									CCJumpX2++;
								}
							}else {		//Вертиклаьная ориентация
								if ((posX == crystalCenterX + crystalSizeRight [crystalCenterX, crystalCenterY]) || (posX == crystalCenterX - crystalSizeLeft [crystalCenterX, crystalCenterY])) {
									//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!");
									//Debug.Log ("Крайняя Цепь");
									//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!");
									CCJumpX2++;
								}
							//Debug.Log (" ");
							//Debug.Log ("=====================");
							}
						}
					}
				}*/
				//g++;


				LocalCrystallID = new int[800];
				LocalCrystallEnrg = new float[800];
				LocalCrystallC = new float[800];
				LocalCrystallA = new int[800];
				B = 0;
				saveTemp = false;

				stepTime += T;
				if (posX < 30 || posY < 25 || posY > 475 || tempSumm==0 || stepTime>9999998f) {
					//posX = 275;
					stepTime = 0;
					stepTimeLocal = 0;
					stepTimeCrystal = 0;
					stepTimeLocal2Local = 0;
					stepTimeLocal2Crystal = 0;
					stepTimeCrystal2Local = 0;
					stepTimeCrystal2Crystal = 0;
					error = true;
					if (tempSumm == 0) {
						Debug.Log ("Error i = " + i);
						crystalIDTemp = crystalID[posX,posY];
						crystalCenterX = crystalIDX[crystalIDTemp];
						crystalCenterY = crystalIDY[crystalIDTemp];
						Debug.Log ("Crystal center   ["+crystalCenterX+" , "+crystalCenterY+"]");
						if (crystalStart [crystalCenterX, crystalCenterY] == 2) {
							Debug.Log ("Horisontal");
						} else {
							Debug.Log ("Vertical");
						}
						Debug.Log ("Size UP = "+crystalSizeUp[crystalCenterX,crystalCenterY]);
						Debug.Log ("Size DOWN = "+crystalSizeDown[crystalCenterX,crystalCenterY]);
						Debug.Log ("Size LEFT = "+crystalSizeLeft[crystalCenterX,crystalCenterY]);
						Debug.Log ("Size RIGHT = "+crystalSizeRight[crystalCenterX,crystalCenterY]);
						Debug.Log ("Current Position   ["+posX+" , "+posY+"]");
						for (i = 0; i <= A; i++) {
							Debug.Log("["+i+"]   ->   ["+targePosX[i]+" , "+targePosY[i]+"]         "+v[i]);
						}
					}
				}
				if (i < 200) {
					posX = (int)endAviableState [i].x;
					posY = (int)endAviableState [i].y;
				}
				//Debug.Log (posX + "  , " + posY);
				if (posX > 55 && !time5Bool) {		//5
					time5Bool = true;
					time5 [l] = stepTime;
					time5Local [l] = stepTimeLocal;
					time5Crystal [l] = stepTimeCrystal;
					time5Numb [l] = timeNumb;
					time5LocalNumb [l] = timeLocalNumb;
					time5CrystalNumb [l] = timeCrystalNumb;
					time5Local2Local [l] = stepTimeLocal2Local;
					time5Local2Crystal [l] = stepTimeLocal2Crystal;
					time5Crystal2Local [l] = stepTimeCrystal2Local;
					time5Crystal2Crystal [l] = stepTimeCrystal2Crystal;
					time5Local2LocalNumb [l] = timeLocal2LocalNumb;
					time5Local2CrystalNumb [l] = timeLocal2CrystalNumb;
					time5Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
					time5Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				}
				if (posX > 60 && !time10Bool) {		//10
					time10Bool = true;
					time10 [l] = stepTime;
					time10Local [l] = stepTimeLocal;
					time10Crystal [l] = stepTimeCrystal;
					time10Numb [l] = timeNumb;
					time10LocalNumb [l] = timeLocalNumb;
					time10CrystalNumb [l] = timeCrystalNumb;
					time10Local2Local [l] = stepTimeLocal2Local;
					time10Local2Crystal [l] = stepTimeLocal2Crystal;
					time10Crystal2Local [l] = stepTimeCrystal2Local;
					time10Crystal2Crystal [l] = stepTimeCrystal2Crystal;
					time10Local2LocalNumb [l] = timeLocal2LocalNumb;
					time10Local2CrystalNumb [l] = timeLocal2CrystalNumb;
					time10Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
					time10Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				}
				if (posX > 70 && !time20Bool) {		//20
					time20Bool = true;
					time20 [l] = stepTime;
					time20Local [l] = stepTimeLocal;
					time20Crystal [l] = stepTimeCrystal;
					time20Numb [l] = timeNumb;
					time20LocalNumb [l] = timeLocalNumb;
					time20CrystalNumb [l] = timeCrystalNumb;
					time20Local2Local [l] = stepTimeLocal2Local;
					time20Local2Crystal [l] = stepTimeLocal2Crystal;
					time20Crystal2Local [l] = stepTimeCrystal2Local;
					time20Crystal2Crystal [l] = stepTimeCrystal2Crystal;
					time20Local2LocalNumb [l] = timeLocal2LocalNumb;
					time20Local2CrystalNumb [l] = timeLocal2CrystalNumb;
					time20Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
					time20Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				}
				if (posX > 90 && !time40Bool) {		//40
					time40Bool = true;
					time40 [l] = stepTime;
					time40Local [l] = stepTimeLocal;
					time40Crystal [l] = stepTimeCrystal;
					time40Numb [l] = timeNumb;
					time40LocalNumb [l] = timeLocalNumb;
					time40CrystalNumb [l] = timeCrystalNumb;
					time40Local2Local [l] = stepTimeLocal2Local;
					time40Local2Crystal [l] = stepTimeLocal2Crystal;
					time40Crystal2Local [l] = stepTimeCrystal2Local;
					time40Crystal2Crystal [l] = stepTimeCrystal2Crystal;
					time40Local2LocalNumb [l] = timeLocal2LocalNumb;
					time40Local2CrystalNumb [l] = timeLocal2CrystalNumb;
					time40Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
					time40Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				}
				if (posX > 150 && !time100Bool) {		//100
					time100Bool = true;
					time100 [l] = stepTime;
					time100Local [l] = stepTimeLocal;
					time100Crystal [l] = stepTimeCrystal;
					time100Numb [l] = timeNumb;
					time100LocalNumb [l] = timeLocalNumb;
					time100CrystalNumb [l] = timeCrystalNumb;
					time100Local2Local [l] = stepTimeLocal2Local;
					time100Local2Crystal [l] = stepTimeLocal2Crystal;
					time100Crystal2Local [l] = stepTimeCrystal2Local;
					time100Crystal2Crystal [l] = stepTimeCrystal2Crystal;
					time100Local2LocalNumb [l] = timeLocal2LocalNumb;
					time100Local2CrystalNumb [l] = timeLocal2CrystalNumb;
					time100Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
					time100Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				}
				//PosDetect[posX,posY]++;
				if (g % 50 == 0) {
					//Debug.Log ("LOCAL " + stepTimeLocal);
					//Debug.Log ("CRYST " + stepTimeCrystal);
					//Debug.Log ("[" + g + "]   " + posX + "  ,  " + posY);
				}

				limit--;
				if (limit < 0) {
					posX = 280;
					Debug.Log ("!dolgo!");
					error = true;
				}

				//posX = 280;
			}
			//if (Input.GetKeyDown ("space")) {
				ReRender ();
				
				g = 0;
				//K [l] = stepTime / 1000;
				time200 [l] = stepTime;
				time200Local [l] = stepTimeLocal;
				time200Crystal [l] = stepTimeCrystal;
				time200Numb [l] = timeNumb;
				time200LocalNumb [l] = timeLocalNumb;
				time200CrystalNumb [l] = timeCrystalNumb;
				time200Local2Local [l] = stepTimeLocal2Local;
				time200Local2Crystal [l] = stepTimeLocal2Crystal;
				time200Crystal2Local [l] = stepTimeCrystal2Local;
				time200Crystal2Crystal [l] = stepTimeCrystal2Crystal;
				time200Local2LocalNumb [l] = timeLocal2LocalNumb;
				time200Local2CrystalNumb [l] = timeLocal2CrystalNumb;
				time200Crystal2LocalNumb [l] = timeCrystal2LocalNumb;
				time200Crystal2CrystalNumb [l] = timeCrystal2CrystalNumb;
				l++;
				expStep++;
				//Debug.Log (expStep+"       "+stepTime);
				if (error) {
					Debug.Log (expStep+")   ERROR");
					Debug.Log ("Jump "+g+" ["+posX+" , "+posY+"]    ");
					error = false;
					l--;
					expStep--;
					Debug.Log (tempSumm);
				}
				kvas = false;
				trueKvaz = false;
				stepTime = 0;
				stepTimeLocal = 0;
				stepTimeCrystal = 0;
				timeNumb = 0;
				timeLocalNumb = 0;
				timeCrystalNumb = 0;
				stepTimeLocal2Local = 0;
				stepTimeLocal2Crystal = 0;
				stepTimeCrystal2Local = 0;
				stepTimeCrystal2Crystal = 0;
				timeLocal2LocalNumb = 0;
				timeLocal2CrystalNumb = 0;
				timeCrystal2LocalNumb = 0;
				timeCrystal2CrystalNumb = 0;

				limit = limitConst;
				posX = 50;
				posY = 250;
				time5Bool = false;
				time10Bool = false;
				time20Bool = false;
				time40Bool = false;
				time100Bool = false;

				if (expStep >= expStepLimit) {
					makeExp = false;
					/*for (int i = 25; i <= 125; i++) {
						for (int j = 25; j <= 225; j++) {
							if (PosDetect [i, j] >= 1) {
								DrowPixel (i, j, Color.red);
							}
						}
					}*/
					textureNew.Apply ();

				for (int i = 0; i < l; i++) {
					//stepTime += K [i];
					middleTime5 += time5 [i];
					if(time5[i]!=0)middleTime5Rev += (float)(1 / time5 [i]);
					middleTime5Local += time5Local[i];
					if(time5Local[i]!=0)middleTime5RevLocal += (float)(1 / time5Local [i]);
					middleTime5Crystal += time5Crystal[i];
					if(time5Crystal[i]!=0)middleTime5RevCrystal += (float)(1 / time5Crystal [i]);
					middleTime5Numb += time5Numb [i];
					middleTime5LocalNumb += time5LocalNumb [i];
					middleTime5CrystalNumb += time5CrystalNumb [i];
					middleTime5Local2Local += time5Local2Local [i];
					if(time5Local2Local[i]!=0)middleTime5Local2LocalRev += (float)(1 / time5Local2Local [i]);
					middleTime5Local2Crystal += time5Local2Crystal [i];
					if(time5Local2Crystal[i]!=0)middleTime5Local2CrystalRev += (float)(1 / time5Local2Crystal [i]);
					middleTime5Crystal2Local += time5Crystal2Local [i];
					if(time5Crystal2Local[i]!=0)middleTime5Crystal2LocalRev += (float)(1 / time5Crystal2Local [i]);
					middleTime5Crystal2Crystal += time5Crystal2Crystal [i];
					if(time5Crystal2Crystal[i]!=0)middleTime5Crystal2CrystalRev += (float)(1 / time5Crystal2Crystal [i]);
					middleTime5Local2LocalNumb += time5Local2LocalNumb [i];
					middleTime5Local2CrystalNumb += time5Local2CrystalNumb [i];
					middleTime5Crystal2LocalNumb += time5Crystal2LocalNumb [i];
					middleTime5Crystal2CrystalNumb += time5Crystal2CrystalNumb [i];


					middleTime10 += time10 [i];
					if(time10[i]!=0)middleTime10Rev += (float)(1 / time10 [i]);
					middleTime10Local += time10Local[i];
					if(time10Local[i]!=0)middleTime10RevLocal += (float)(1 / time10Local [i]);
					middleTime10Crystal += time10Crystal[i];
					if(time10Crystal[i]!=0)middleTime10RevCrystal += (float)(1 / time10Crystal [i]);
					middleTime10Numb += time10Numb [i];
					middleTime10LocalNumb += time10LocalNumb [i];
					middleTime10CrystalNumb += time10CrystalNumb [i];
					middleTime10Local2Local += time10Local2Local [i];
					if(time10Local2Local[i]!=0)middleTime10Local2LocalRev += (float)(1 / time10Local2Local [i]);
					middleTime10Local2Crystal += time10Local2Crystal [i];
					if(time10Local2Crystal[i]!=0)middleTime10Local2CrystalRev += (float)(1 / time10Local2Crystal [i]);
					middleTime10Crystal2Local += time10Crystal2Local [i];
					if(time10Crystal2Local[i]!=0)middleTime10Crystal2LocalRev += (float)(1 / time10Crystal2Local [i]);
					middleTime10Crystal2Crystal += time10Crystal2Crystal [i];
					if(time10Crystal2Crystal[i]!=0)middleTime10Crystal2CrystalRev += (float)(1 / time10Crystal2Crystal [i]);
					middleTime10Local2LocalNumb += time10Local2LocalNumb [i];
					middleTime10Local2CrystalNumb += time10Local2CrystalNumb [i];
					middleTime10Crystal2LocalNumb += time10Crystal2LocalNumb [i];
					middleTime10Crystal2CrystalNumb += time10Crystal2CrystalNumb [i];

					middleTime20 += time20 [i];
					if(time20[i]!=0)middleTime20Rev += (float)(1 / time20 [i]);
					middleTime20Local += time20Local[i];
					if(time20Local[i]!=0)middleTime20RevLocal += (float)(1 / time20Local [i]);
					middleTime20Crystal += time20Crystal[i];
					if(time20Crystal[i]!=0)middleTime20RevCrystal += (float)(1 / time20Crystal [i]);
					middleTime20Numb += time20Numb [i];
					middleTime20LocalNumb += time20LocalNumb [i];
					middleTime20CrystalNumb += time20CrystalNumb [i];
					middleTime20Local2Local += time20Local2Local [i];
					if(time20Local2Local[i]!=0)middleTime20Local2LocalRev += (float)(1 / time20Local2Local [i]);
					middleTime20Local2Crystal += time20Local2Crystal [i];
					if(time20Local2Crystal[i]!=0)middleTime20Local2CrystalRev += (float)(1 / time20Local2Crystal [i]);
					middleTime20Crystal2Local += time20Crystal2Local [i];
					if(time20Crystal2Local[i]!=0)middleTime20Crystal2LocalRev += (float)(1 / time20Crystal2Local [i]);
					middleTime20Crystal2Crystal += time20Crystal2Crystal [i];
					if(time20Crystal2Crystal[i]!=0)middleTime20Crystal2CrystalRev += (float)(1 / time20Crystal2Crystal [i]);
					middleTime20Local2LocalNumb += time20Local2LocalNumb [i];
					middleTime20Local2CrystalNumb += time20Local2CrystalNumb [i];
					middleTime20Crystal2LocalNumb += time20Crystal2LocalNumb [i];
					middleTime20Crystal2CrystalNumb += time20Crystal2CrystalNumb [i];

					middleTime40 += time40 [i];
					if(time40[i]!=0)middleTime40Rev += (float)(1 / time40 [i]);
					middleTime40Local += time40Local[i];
					if(time40Local[i]!=0)middleTime40RevLocal += (float)(1 / time40Local [i]);
					middleTime40Crystal += time40Crystal[i];
					if(time40Crystal[i]!=0)middleTime40RevCrystal += (float)(1 / time40Crystal [i]);
					middleTime40Numb += time40Numb [i];
					middleTime40LocalNumb += time40LocalNumb [i];
					middleTime40CrystalNumb += time40CrystalNumb [i];
					middleTime40Local2Local += time40Local2Local [i];
					if(time40Local2Local[i]!=0)middleTime40Local2LocalRev += (float)(1 / time40Local2Local [i]);
					middleTime40Local2Crystal += time40Local2Crystal [i];
					if(time40Local2Crystal[i]!=0)middleTime40Local2CrystalRev += (float)(1 / time40Local2Crystal [i]);
					middleTime40Crystal2Local += time40Crystal2Local [i];
					if(time40Crystal2Local[i]!=0)middleTime40Crystal2LocalRev += (float)(1 / time40Crystal2Local [i]);
					middleTime40Crystal2Crystal += time40Crystal2Crystal [i];
					if(time40Crystal2Crystal[i]!=0)middleTime40Crystal2CrystalRev += (float)(1 / time40Crystal2Crystal [i]);
					middleTime40Local2LocalNumb += time40Local2LocalNumb [i];
					middleTime40Local2CrystalNumb += time40Local2CrystalNumb [i];
					middleTime40Crystal2LocalNumb += time40Crystal2LocalNumb [i];
					middleTime40Crystal2CrystalNumb += time40Crystal2CrystalNumb [i];

					middleTime100 += time100 [i];
					if(time100[i]!=0)middleTime100Rev += (float)(1 / time100 [i]);
					middleTime100Local += time100Local[i];
					if(time100Local[i]!=0)middleTime100RevLocal += (float)(1 / time100Local [i]);
					middleTime100Crystal += time100Crystal[i];
					if(time100Crystal[i]!=0)middleTime100RevCrystal += (float)(1 / time100Crystal [i]);
					middleTime100Numb += time100Numb [i];
					middleTime100LocalNumb += time100LocalNumb [i];
					middleTime100CrystalNumb += time100CrystalNumb [i];
					middleTime100Local2Local += time100Local2Local [i];
					if(time100Local2Local[i]!=0)middleTime100Local2LocalRev += (float)(1 / time100Local2Local [i]);
					middleTime100Local2Crystal += time100Local2Crystal [i];
					if(time100Local2Crystal[i]!=0)middleTime100Local2CrystalRev += (float)(1 / time100Local2Crystal [i]);
					middleTime100Crystal2Local += time100Crystal2Local [i];
					if(time100Crystal2Local[i]!=0)middleTime100Crystal2LocalRev += (float)(1 / time100Crystal2Local [i]);
					middleTime100Crystal2Crystal += time100Crystal2Crystal [i];
					if(time100Crystal2Crystal[i]!=0)middleTime100Crystal2CrystalRev += (float)(1 / time100Crystal2Crystal [i]);
					middleTime100Local2LocalNumb += time100Local2LocalNumb [i];
					middleTime100Local2CrystalNumb += time100Local2CrystalNumb [i];
					middleTime100Crystal2LocalNumb += time100Crystal2LocalNumb [i];
					middleTime100Crystal2CrystalNumb += time100Crystal2CrystalNumb [i];
					//Debug.Log (middleTime40+" +=  "+time40 [i]);
					//Debug.Log (middleTime40Rev+" +=  "+(float)(1 / time40 [i]));

					middleTime200 += time200 [i];
					if(time200[i]!=0)middleTime200Rev += (float)(1 / time200 [i]);
					middleTime200Local += time200Local[i];
					if(time200Local[i]!=0)middleTime200RevLocal += (float)(1 / time200Local [i]);
					middleTime200Crystal += time200Crystal[i];
					if(time200Crystal[i]!=0)middleTime200RevCrystal += (float)(1 / time200Crystal [i]);
					middleTime200Numb += time200Numb [i];
					middleTime200LocalNumb += time200LocalNumb [i];
					middleTime200CrystalNumb += time200CrystalNumb [i];
					middleTime200Local2Local += time200Local2Local [i];
					if(time200Local2Local[i]!=0)middleTime200Local2LocalRev += (float)(1 / time200Local2Local [i]);
					middleTime200Local2Crystal += time200Local2Crystal [i];
					if(time200Local2Crystal[i]!=0)middleTime200Local2CrystalRev += (float)(1 / time200Local2Crystal [i]);
					middleTime200Crystal2Local += time200Crystal2Local [i];
					if(time200Crystal2Local[i]!=0)middleTime200Crystal2LocalRev += (float)(1 / time200Crystal2Local [i]);
					middleTime200Crystal2Crystal += time200Crystal2Crystal [i];
					if(time200Crystal2Crystal[i]!=0)middleTime200Crystal2CrystalRev += (float)(1 / time200Crystal2Crystal [i]);
					middleTime200Local2LocalNumb += time200Local2LocalNumb [i];
					middleTime200Local2CrystalNumb += time200Local2CrystalNumb [i];
					middleTime200Crystal2LocalNumb += time200Crystal2LocalNumb [i];
					middleTime200Crystal2CrystalNumb += time200Crystal2CrystalNumb [i];

					middlePercent += crystalPercent [i];
					middleV += centreNumb [i];
					middleL += lenghtChain [i];
					middleH += chainNumb [i];

					for (int k = 0; k <= 20; k++) {
						lenghtChainGlobal [k, l] += lenghtChainGlobal [k, i];
					}
				}



				//stepTime = stepTime / expStepLimit * 1000f;
				//Debug.Log (stepTime);
				middleTime5 = middleTime5/expStepLimit;
				middleTime5Rev = middleTime5Rev/expStepLimit;
				middleTime5Local = middleTime5Local/expStepLimit;
				middleTime5RevLocal = middleTime5RevLocal/expStepLimit;
				middleTime5Crystal = middleTime5Crystal/expStepLimit;
				middleTime5RevCrystal =middleTime5RevCrystal/expStepLimit;
				middleTime5Numb = middleTime5Numb/expStepLimit;
				middleTime5LocalNumb = middleTime5LocalNumb/expStepLimit;
				middleTime5CrystalNumb = middleTime5CrystalNumb/expStepLimit;
				middleTime5Local2Local = middleTime5Local2Local/expStepLimit;
				middleTime5Local2LocalRev = middleTime5Local2LocalRev/expStepLimit;
				middleTime5Local2Crystal = middleTime5Local2Crystal/expStepLimit;
				middleTime5Local2CrystalRev = middleTime5Local2CrystalRev/expStepLimit;
				middleTime5Crystal2Local = middleTime5Crystal2Local/expStepLimit;
				middleTime5Crystal2LocalRev =  middleTime5Crystal2LocalRev/expStepLimit;
				middleTime5Crystal2Crystal = middleTime5Crystal2Crystal/expStepLimit;
				middleTime5Crystal2CrystalRev = middleTime5Crystal2CrystalRev/expStepLimit;
				middleTime5Local2LocalNumb = middleTime5Local2LocalNumb/expStepLimit;
				middleTime5Local2CrystalNumb = middleTime5Local2CrystalNumb/expStepLimit;
				middleTime5Crystal2LocalNumb = middleTime5Crystal2LocalNumb/expStepLimit;
				middleTime5Crystal2CrystalNumb = middleTime5Crystal2CrystalNumb/expStepLimit;

				middleTime10 = middleTime10/expStepLimit;
				middleTime10Rev = middleTime10Rev/expStepLimit;
				middleTime10Local = middleTime10Local/expStepLimit;
				middleTime10RevLocal = middleTime10RevLocal/expStepLimit;
				middleTime10Crystal = middleTime10Crystal/expStepLimit;
				middleTime10RevCrystal =middleTime10RevCrystal/expStepLimit;
				middleTime10Numb = middleTime10Numb/expStepLimit;
				middleTime10LocalNumb = middleTime10LocalNumb/expStepLimit;
				middleTime10CrystalNumb = middleTime10CrystalNumb/expStepLimit;
				middleTime10Local2Local = middleTime10Local2Local/expStepLimit;
				middleTime10Local2LocalRev = middleTime10Local2LocalRev/expStepLimit;
				middleTime10Local2Crystal = middleTime10Local2Crystal/expStepLimit;
				middleTime10Local2CrystalRev = middleTime10Local2CrystalRev/expStepLimit;
				middleTime10Crystal2Local = middleTime10Crystal2Local/expStepLimit;
				middleTime10Crystal2LocalRev =  middleTime10Crystal2LocalRev/expStepLimit;
				middleTime10Crystal2Crystal = middleTime10Crystal2Crystal/expStepLimit;
				middleTime10Crystal2CrystalRev = middleTime10Crystal2CrystalRev/expStepLimit;
				middleTime10Local2LocalNumb = middleTime10Local2LocalNumb/expStepLimit;
				middleTime10Local2CrystalNumb = middleTime10Local2CrystalNumb/expStepLimit;
				middleTime10Crystal2LocalNumb = middleTime10Crystal2LocalNumb/expStepLimit;
				middleTime10Crystal2CrystalNumb = middleTime10Crystal2CrystalNumb/expStepLimit;

				middleTime20 = middleTime20/expStepLimit;
				middleTime20Rev = middleTime20Rev/expStepLimit;
				middleTime20Local = middleTime20Local/expStepLimit;
				middleTime20RevLocal = middleTime20RevLocal/expStepLimit;
				middleTime20Crystal = middleTime20Crystal/expStepLimit;
				middleTime20RevCrystal =middleTime20RevCrystal/expStepLimit;
				middleTime20Numb = middleTime20Numb/expStepLimit;
				middleTime20LocalNumb = middleTime20LocalNumb/expStepLimit;
				middleTime20CrystalNumb = middleTime20CrystalNumb/expStepLimit;
				middleTime20Local2Local = middleTime20Local2Local/expStepLimit;
				middleTime20Local2LocalRev = middleTime20Local2LocalRev/expStepLimit;
				middleTime20Local2Crystal = middleTime20Local2Crystal/expStepLimit;
				middleTime20Local2CrystalRev = middleTime20Local2CrystalRev/expStepLimit;
				middleTime20Crystal2Local = middleTime20Crystal2Local/expStepLimit;
				middleTime20Crystal2LocalRev =  middleTime20Crystal2LocalRev/expStepLimit;
				middleTime20Crystal2Crystal = middleTime20Crystal2Crystal/expStepLimit;
				middleTime20Crystal2CrystalRev = middleTime20Crystal2CrystalRev/expStepLimit;
				middleTime20Local2LocalNumb = middleTime20Local2LocalNumb/expStepLimit;
				middleTime20Local2CrystalNumb = middleTime20Local2CrystalNumb/expStepLimit;
				middleTime20Crystal2LocalNumb = middleTime20Crystal2LocalNumb/expStepLimit;
				middleTime20Crystal2CrystalNumb = middleTime20Crystal2CrystalNumb/expStepLimit;

				middleTime40 = middleTime40/expStepLimit;
				middleTime40Rev = middleTime40Rev/expStepLimit;
				middleTime40Local = middleTime40Local/expStepLimit;
				middleTime40RevLocal = middleTime40RevLocal/expStepLimit;
				middleTime40Crystal = middleTime40Crystal/expStepLimit;
				middleTime40RevCrystal =middleTime40RevCrystal/expStepLimit;
				middleTime40Numb = middleTime40Numb/expStepLimit;
				middleTime40LocalNumb = middleTime40LocalNumb/expStepLimit;
				middleTime40CrystalNumb = middleTime40CrystalNumb/expStepLimit;
				middleTime40Local2Local = middleTime40Local2Local/expStepLimit;
				middleTime40Local2LocalRev = middleTime40Local2LocalRev/expStepLimit;
				middleTime40Local2Crystal = middleTime40Local2Crystal/expStepLimit;
				middleTime40Local2CrystalRev = middleTime40Local2CrystalRev/expStepLimit;
				middleTime40Crystal2Local = middleTime40Crystal2Local/expStepLimit;
				middleTime40Crystal2LocalRev =  middleTime40Crystal2LocalRev/expStepLimit;
				middleTime40Crystal2Crystal = middleTime40Crystal2Crystal/expStepLimit;
				middleTime40Crystal2CrystalRev = middleTime40Crystal2CrystalRev/expStepLimit;
				middleTime40Local2LocalNumb = middleTime40Local2LocalNumb/expStepLimit;
				middleTime40Local2CrystalNumb = middleTime40Local2CrystalNumb/expStepLimit;
				middleTime40Crystal2LocalNumb = middleTime40Crystal2LocalNumb/expStepLimit;
				middleTime40Crystal2CrystalNumb = middleTime40Crystal2CrystalNumb/expStepLimit;

				middleTime100 = middleTime100/expStepLimit;
				middleTime100Rev = middleTime100Rev/expStepLimit;
				middleTime100Local = middleTime100Local/expStepLimit;
				middleTime100RevLocal = middleTime100RevLocal/expStepLimit;
				middleTime100Crystal = middleTime100Crystal/expStepLimit;
				middleTime100RevCrystal =middleTime100RevCrystal/expStepLimit;
				middleTime100Numb = middleTime100Numb/expStepLimit;
				middleTime100LocalNumb = middleTime100LocalNumb/expStepLimit;
				middleTime100CrystalNumb = middleTime100CrystalNumb/expStepLimit;
				middleTime100Local2Local = middleTime100Local2Local/expStepLimit;
				middleTime100Local2LocalRev = middleTime100Local2LocalRev/expStepLimit;
				middleTime100Local2Crystal = middleTime100Local2Crystal/expStepLimit;
				middleTime100Local2CrystalRev = middleTime100Local2CrystalRev/expStepLimit;
				middleTime100Crystal2Local = middleTime100Crystal2Local/expStepLimit;
				middleTime100Crystal2LocalRev =  middleTime100Crystal2LocalRev/expStepLimit;
				middleTime100Crystal2Crystal = middleTime100Crystal2Crystal/expStepLimit;
				middleTime100Crystal2CrystalRev = middleTime100Crystal2CrystalRev/expStepLimit;
				middleTime100Local2LocalNumb = middleTime100Local2LocalNumb/expStepLimit;
				middleTime100Local2CrystalNumb = middleTime100Local2CrystalNumb/expStepLimit;
				middleTime100Crystal2LocalNumb = middleTime100Crystal2LocalNumb/expStepLimit;
				middleTime100Crystal2CrystalNumb = middleTime100Crystal2CrystalNumb/expStepLimit;

				middleTime200 = middleTime200/expStepLimit;
				middleTime200Rev = middleTime200Rev/expStepLimit;
				middleTime200Local = middleTime200Local/expStepLimit;
				middleTime200RevLocal = middleTime200RevLocal/expStepLimit;
				middleTime200Crystal = middleTime200Crystal/expStepLimit;
				middleTime200RevCrystal =middleTime200RevCrystal/expStepLimit;
				middleTime200Numb = middleTime200Numb/expStepLimit;
				middleTime200LocalNumb = middleTime200LocalNumb/expStepLimit;
				middleTime200CrystalNumb = middleTime200CrystalNumb/expStepLimit;
				middleTime200Local2Local = middleTime200Local2Local/expStepLimit;
				middleTime200Local2LocalRev = middleTime200Local2LocalRev/expStepLimit;
				middleTime200Local2Crystal = middleTime200Local2Crystal/expStepLimit;
				middleTime200Local2CrystalRev = middleTime200Local2CrystalRev/expStepLimit;
				middleTime200Crystal2Local = middleTime200Crystal2Local/expStepLimit;
				middleTime200Crystal2LocalRev =  middleTime200Crystal2LocalRev/expStepLimit;
				middleTime200Crystal2Crystal = middleTime200Crystal2Crystal/expStepLimit;
				middleTime200Crystal2CrystalRev = middleTime200Crystal2CrystalRev/expStepLimit;
				middleTime200Local2LocalNumb = middleTime200Local2LocalNumb/expStepLimit;
				middleTime200Local2CrystalNumb = middleTime200Local2CrystalNumb/expStepLimit;
				middleTime200Crystal2LocalNumb = middleTime200Crystal2LocalNumb/expStepLimit;
				middleTime200Crystal2CrystalNumb = middleTime200Crystal2CrystalNumb/expStepLimit;

				middlePercent = middlePercent / expStepLimit;
				middleV = middleV / expStepLimit;
				middleL = middleL / expStepLimit;
				middleH = middleH / expStepLimit;

				/*for (int i = 0; i < l; i++) {
					dispTime5 += Mathf.Pow ((time5 [i]-middleTime5),2)/expStepLimit;
					if(time5[i]!=0)dispInvTime5 += Mathf.Pow (((float)(1 / time5 [i])-middleTime5Rev),2)/expStepLimit;
					dispTime5local += Mathf.Pow ((time5Local [i]-middleTime5Local),2)/expStepLimit;
					if(time5Local[i]!=0)dispInvTime5Local += Mathf.Pow (((float)(1 / time5Local [i])-middleTime5RevLocal),2)/expStepLimit;
					dispTime5Crystal += Mathf.Pow ((time5Crystal [i]-middleTime5Crystal),2)/expStepLimit;
					if (time5Crystal [i] != 0)dispInvTime5Crystal += Mathf.Pow (((float)(1 / time5Crystal [i])-middleTime5RevCrystal),2)/expStepLimit;
					disptimeNumb5 += Mathf.Pow ((time5Numb [i]-middleTime5Numb),2)/expStepLimit;
					disptimeNumb5Local += Mathf.Pow ((time5LocalNumb [i]-middleTime5LocalNumb),2)/expStepLimit;
					disptimeNumb5Crystal += Mathf.Pow ((time5CrystalNumb [i]-middleTime5CrystalNumb),2)/expStepLimit;

					dispTime10 += Mathf.Pow ((time10 [i]-middleTime10),2)/expStepLimit;
					if(time10[i]!=0)dispInvTime10 += Mathf.Pow (((float)(1 / time10 [i])-middleTime10Rev),2)/expStepLimit;
					dispTime10local += Mathf.Pow ((time10Local [i]-middleTime10Local),2)/expStepLimit;
					if(time10Local[i]!=0)dispInvTime10Local += Mathf.Pow (((float)(1 / time10Local [i])-middleTime10RevLocal),2)/expStepLimit;
					dispTime10Crystal += Mathf.Pow ((time10Crystal [i]-middleTime10Crystal),2)/expStepLimit;
					if (time10Crystal [i] != 0)dispInvTime10Crystal += Mathf.Pow (((float)(1 / time10Crystal [i])-middleTime10RevCrystal),2)/expStepLimit;
					disptimeNumb10 += Mathf.Pow ((time10Numb [i]-middleTime10Numb),2)/expStepLimit;
					disptimeNumb10Local += Mathf.Pow ((time10LocalNumb [i]-middleTime10LocalNumb),2)/expStepLimit;
					disptimeNumb10Crystal += Mathf.Pow ((time10CrystalNumb [i]-middleTime10CrystalNumb),2)/expStepLimit;

					dispTime20 += Mathf.Pow ((time20 [i]-middleTime20),2)/expStepLimit;
					if(time20[i]!=0)dispInvTime20 += Mathf.Pow (((float)(1 / time20 [i])-middleTime20Rev),2)/expStepLimit;
					dispTime20local += Mathf.Pow ((time20Local [i]-middleTime20Local),2)/expStepLimit;
					if(time20Local[i]!=0)dispInvTime20Local += Mathf.Pow (((float)(1 / time20Local [i])-middleTime20RevLocal),2)/expStepLimit;
					dispTime20Crystal += Mathf.Pow ((time20Crystal [i]-middleTime20Crystal),2)/expStepLimit;
					if (time20Crystal [i] != 0)dispInvTime20Crystal += Mathf.Pow (((float)(1 / time20Crystal [i])-middleTime20RevCrystal),2)/expStepLimit;
					disptimeNumb20 += Mathf.Pow ((time20Numb [i]-middleTime20Numb),2)/expStepLimit;
					disptimeNumb20Local += Mathf.Pow ((time20LocalNumb [i]-middleTime20LocalNumb),2)/expStepLimit;
					disptimeNumb20Crystal += Mathf.Pow ((time20CrystalNumb [i]-middleTime20CrystalNumb),2)/expStepLimit;

					dispTime40 += Mathf.Pow ((time40 [i]-middleTime40),2)/expStepLimit;
					if(time40[i]!=0)dispInvTime40 += Mathf.Pow (((float)(1 / time40 [i])-middleTime40Rev),2)/expStepLimit;
					dispTime40local += Mathf.Pow ((time40Local [i]-middleTime40Local),2)/expStepLimit;
					if(time40Local[i]!=0)dispInvTime40Local += Mathf.Pow (((float)(1 / time40Local [i])-middleTime40RevLocal),2)/expStepLimit;
					dispTime40Crystal += Mathf.Pow ((time40Crystal [i]-middleTime40Crystal),2)/expStepLimit;
					if (time40Crystal [i] != 0)dispInvTime40Crystal += Mathf.Pow (((float)(1 / time40Crystal [i])-middleTime40RevCrystal),2)/expStepLimit;
					disptimeNumb40 += Mathf.Pow ((time40Numb [i]-middleTime40Numb),2)/expStepLimit;
					disptimeNumb40Local += Mathf.Pow ((time40LocalNumb [i]-middleTime40LocalNumb),2)/expStepLimit;
					disptimeNumb40Crystal += Mathf.Pow ((time40CrystalNumb [i]-middleTime40CrystalNumb),2)/expStepLimit;

					dispTime100 += Mathf.Pow ((time100 [i]-middleTime100),2)/expStepLimit;
					if(time100[i]!=0)dispInvTime100 += Mathf.Pow (((float)(1 / time100 [i])-middleTime100Rev),2)/expStepLimit;
					dispTime100local += Mathf.Pow ((time100Local [i]-middleTime100Local),2)/expStepLimit;
					if(time100Local[i]!=0)dispInvTime100Local += Mathf.Pow (((float)(1 / time100Local [i])-middleTime100RevLocal),2)/expStepLimit;
					dispTime100Crystal += Mathf.Pow ((time100Crystal [i]-middleTime100Crystal),2)/expStepLimit;
					if (time100Crystal [i] != 0)dispInvTime100Crystal += Mathf.Pow (((float)(1 / time100Crystal [i])-middleTime100RevCrystal),2)/expStepLimit;
					disptimeNumb100 += Mathf.Pow ((time100Numb [i]-middleTime100Numb),2)/expStepLimit;
					disptimeNumb100Local += Mathf.Pow ((time100LocalNumb [i]-middleTime100LocalNumb),2)/expStepLimit;
					disptimeNumb100Crystal += Mathf.Pow ((time100CrystalNumb [i]-middleTime100CrystalNumb),2)/expStepLimit;

					dispTime200 += Mathf.Pow ((time200 [i]-middleTime200),2)/expStepLimit;
					if(time200[i]!=0)dispInvTime200 += Mathf.Pow (((float)(1 / time200 [i])-middleTime200Rev),2)/expStepLimit;
					dispTime200local += Mathf.Pow ((time200Local [i]-middleTime200Local),2)/expStepLimit;
					if(time200Local[i]!=0)dispInvTime200Local += Mathf.Pow (((float)(1 / time200Local [i])-middleTime200RevLocal),2)/expStepLimit;
					dispTime200Crystal += Mathf.Pow ((time200Crystal [i]-middleTime200Crystal),2)/expStepLimit;
					if (time200Crystal [i] != 0)dispInvTime200Crystal += Mathf.Pow (((float)(1 / time200Crystal [i])-middleTime200RevCrystal),2)/expStepLimit;
					disptimeNumb200 += Mathf.Pow ((time200Numb [i]-middleTime200Numb),2)/expStepLimit;
					disptimeNumb200Local += Mathf.Pow ((time200LocalNumb [i]-middleTime200LocalNumb),2)/expStepLimit;
					disptimeNumb200Crystal += Mathf.Pow ((time200CrystalNumb [i]-middleTime200CrystalNumb),2)/expStepLimit;
				}*/

				for (int k = 0; k <= 20; k++) {
					lenghtChainGlobal [k, l] = lenghtChainGlobal [k, l]/expStepLimit;
				}
				Debug.Log (CCJumpX2);
				StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"Et="+(Et/sigma)+"_sigm="+sigma+"_D=1to"+chance+"_E="+E+"_N="+N+".txt");
				//StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"___STAT_MA_D=1|"+chance+"_sigm="+sigma+"_E="+E+"+_N="+N+".txt");
				str1.WriteLine("sigma1 = "+sigma+"kT;\t sigma2 = "+sigmaCrystal+"*sigma1;\t Et = "+Et+"*sigma1;\t eFa = "+E/kT+"kT;\t kT = "+kT);
				str1.WriteLine("\t");
				str1.WriteLine ("Плотность = 1/" + chance + "\t Время роста = "+timeLimit+"\t <плотность> = "+middlePercent*100f+"%");
				str1.WriteLine("\t <l> = "+middleL+"\t <h> = " + middleH + "\t <V> = "+middleV);
				str1.WriteLine("\t");
				str1.WriteLine("\t");
				str1.WriteLine("  \t    \t\t прямое"+" \t  обратное\t\tсобытий");
				str1.WriteLine("5]\t <t>\t\t <"+middleTime5+">\t <"+(1/middleTime5Rev)+">\t\t<"+middleTime5Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime5)+"}\t {"+1/Mathf.Sqrt(dispInvTime5)+"}\t\t{"+Mathf.Sqrt(disptimeNumb5)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime5Local+">\t <"+(1/middleTime5RevLocal)+">\t\t<"+middleTime5LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime5Local2Local+">\t <"+(1/middleTime5Local2LocalRev)+">\t\t<"+middleTime5Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime5Local2Crystal+">\t <"+(1/middleTime5Local2CrystalRev)+">\t\t<"+middleTime5Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime5local)+"}\t {"+1/Mathf.Sqrt(dispInvTime5Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb5Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime5Crystal+">\t <"+(1/middleTime5RevCrystal)+">\t\t<"+middleTime5CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime5Crystal2Local+">\t <"+(1/middleTime5Crystal2LocalRev)+">\t\t<"+middleTime5Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime5Crystal2Crystal+">\t <"+(1/middleTime5Crystal2CrystalRev)+">\t\t<"+middleTime5Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime5Crystal)+"}\t {"+1/Mathf.Sqrt(dispInvTime5Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb5Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("10]\t <t>\t\t <"+middleTime10+">\t <"+(1/middleTime10Rev)+">\t\t<"+middleTime10Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime10)+"}\t {"+1/Mathf.Sqrt(dispInvTime10)+"}\t\t{"+Mathf.Sqrt(disptimeNumb10)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime10Local+">\t <"+(1/middleTime10RevLocal)+">\t\t<"+middleTime10LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime10Local2Local+">\t <"+(1/middleTime10Local2LocalRev)+">\t\t<"+middleTime10Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime10Local2Crystal+">\t <"+(1/middleTime10Local2CrystalRev)+">\t\t<"+middleTime10Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime10local)+"}\t {"+1/Mathf.Sqrt(dispInvTime10Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb10Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime10Crystal+">\t <"+(1/middleTime10RevCrystal)+">\t\t<"+middleTime10CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime10Crystal2Local+">\t <"+(1/middleTime10Crystal2LocalRev)+">\t\t<"+middleTime10Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime10Crystal2Crystal+">\t <"+(1/middleTime10Crystal2CrystalRev)+">\t\t<"+middleTime10Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime10Crystal)+"}\t {"+1/Mathf.Sqrt(dispInvTime10Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb10Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("20]\t <t>\t\t <"+middleTime20+">\t <"+(1/middleTime20Rev)+">\t\t<"+middleTime20Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime20)+"}\t {"+1/Mathf.Sqrt(dispInvTime20)+"}\t\t{"+Mathf.Sqrt(disptimeNumb20)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime20Local+">\t <"+(1/middleTime20RevLocal)+">\t\t<"+middleTime20LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime20Local2Local+">\t <"+(1/middleTime20Local2LocalRev)+">\t\t<"+middleTime20Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime20Local2Crystal+">\t <"+(1/middleTime20Local2CrystalRev)+">\t\t<"+middleTime20Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime20local)+"}\t {"+1/Mathf.Sqrt(dispInvTime20Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb20Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime20Crystal+">\t <"+(1/middleTime20RevCrystal)+">\t\t<"+middleTime20CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime20Crystal2Local+">\t <"+(1/middleTime20Crystal2LocalRev)+">\t\t<"+middleTime20Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime20Crystal2Crystal+">\t <"+(1/middleTime20Crystal2CrystalRev)+">\t\t<"+middleTime20Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime20Crystal)+"}\t {"+1/Mathf.Sqrt(dispInvTime20Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb20Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("40]\t <t>\t\t <"+middleTime40+">\t <"+(1/middleTime40Rev)+">\t\t<"+middleTime40Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime40)+"}\t {"+Mathf.Sqrt(dispInvTime40)+"}\t\t{"+Mathf.Sqrt(disptimeNumb40)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime40Local+">\t <"+(1/middleTime40RevLocal)+">\t\t<"+middleTime40LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime40Local2Local+">\t <"+(1/middleTime40Local2LocalRev)+">\t\t<"+middleTime40Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime40Local2Crystal+">\t <"+(1/middleTime40Local2CrystalRev)+">\t\t<"+middleTime40Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime40local)+"}\t {"+Mathf.Sqrt(dispInvTime40Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb40Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime40Crystal+">\t <"+(1/middleTime40RevCrystal)+">\t\t<"+middleTime40CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime40Crystal2Local+">\t <"+(1/middleTime40Crystal2LocalRev)+">\t\t<"+middleTime40Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime40Crystal2Crystal+">\t <"+(1/middleTime40Crystal2CrystalRev)+">\t\t<"+middleTime40Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime40Crystal)+"}\t {"+Mathf.Sqrt(dispInvTime40Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb40Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("100]\t <t>\t\t <"+middleTime100+">\t <"+(1/middleTime100Rev)+">\t\t<"+middleTime100Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime100)+"}\t {"+1/Mathf.Sqrt(dispInvTime100)+"}\t\t{"+Mathf.Sqrt(disptimeNumb100)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime100Local+">\t <"+(1/middleTime100RevLocal)+">\t\t<"+middleTime100LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime100Local2Local+">\t <"+(1/middleTime100Local2LocalRev)+">\t\t<"+middleTime100Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime100Local2Crystal+">\t <"+(1/middleTime100Local2CrystalRev)+">\t\t<"+middleTime100Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime100local)+"}\t {"+1/Mathf.Sqrt(dispInvTime100Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb100Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime100Crystal+">\t <"+(1/middleTime100RevCrystal)+">\t\t<"+middleTime100CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime100Crystal2Local+">\t <"+(1/middleTime100Crystal2LocalRev)+">\t\t<"+middleTime100Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime100Crystal2Crystal+">\t <"+(1/middleTime100Crystal2CrystalRev)+">\t\t<"+middleTime100Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime100Crystal)+"}\t {"+1/Mathf.Sqrt(dispInvTime100Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb100Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("200]\t <t>\t\t <"+middleTime200+">\t <"+(1/middleTime200Rev)+">\t\t<"+middleTime200Numb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime200)+"}\t {"+1/Mathf.Sqrt(dispInvTime200)+"}\t\t{"+Mathf.Sqrt(disptimeNumb200)+"}");
				str1.WriteLine("  \t локализованно\t <"+middleTime200Local+">\t <"+(1/middleTime200RevLocal)+">\t\t<"+middleTime200LocalNumb+">");
				str1.WriteLine("  \t  локал->локал\t <"+middleTime200Local2Local+">\t <"+(1/middleTime200Local2LocalRev)+">\t\t<"+middleTime200Local2LocalNumb+">");
				str1.WriteLine("  \t  локал->крист\t <"+middleTime200Local2Crystal+">\t <"+(1/middleTime200Local2CrystalRev)+">\t\t<"+middleTime200Local2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime200local)+"}\t {"+1/Mathf.Sqrt(dispInvTime200Local)+"}\t\t{"+Mathf.Sqrt(disptimeNumb200Local)+"}");
				str1.WriteLine("  \t в кристаллах\t <"+middleTime200Crystal+">\t <"+(1/middleTime200RevCrystal)+">\t\t<"+middleTime200CrystalNumb+">");
				str1.WriteLine("  \t  крист->локал\t <"+middleTime200Crystal2Local+">\t <"+(1/middleTime200Crystal2LocalRev)+">\t\t<"+middleTime200Crystal2LocalNumb+">");
				str1.WriteLine("  \t  крист->крист\t <"+middleTime200Crystal2Crystal+">\t <"+(1/middleTime200Crystal2CrystalRev)+">\t\t<"+middleTime200Crystal2CrystalNumb+">");
				//str1.WriteLine("  \t\t\t {"+Mathf.Sqrt(dispTime200Crystal)+"}\t {"+1/Mathf.Sqrt(dispInvTime200Crystal)+"}\t\t{"+Mathf.Sqrt(disptimeNumb200Crystal)+"}");
				str1.WriteLine("\t");
				str1.WriteLine("===================================================================================");
				str1.WriteLine("\t");
				str1.WriteLine("\t lenghtStat");
				for (int k = 0; k <= 20; k++) {
					str1.WriteLine("  "+k+"  \t"+lenghtChainGlobal [k, l]);
				}


				str1.WriteLine("\t");
				str1.Close();


				StreamWriter str0 = new StreamWriter(@Application.streamingAssetsPath+"___FULL_STAT_MA_D=1to"+chance+"_sigm="+sigma+"_Et=-"+Et+"_E="+E+"_N="+N+".txt");
				str0.WriteLine("sigma1 = "+sigma+"kT;\t sigma2 = "+sigmaCrystal+"*sigma1;\t Et = "+Et+"*sigma1;\t eFa = "+E/kT+"kT;\t kT = "+kT);
				str0.WriteLine("\t");
				str0.WriteLine ("Плотность = 1/" + chance + "\t Время роста = "+timeLimit+"\t <плотность> = "+middlePercent*100f+"%");
				str0.WriteLine("\t <l> = "+middleL+"\t <h> = " + middleH + "\t <V> = "+middleV);
				str0.WriteLine("\t");
				str0.WriteLine("\t");

				for (int i = 0; i < l; i++) {
					str0.WriteLine(i+")\t    \t прямое"+" \t\t\tсобытий");
					str0.WriteLine("5]\t <t>\t\t <"+time5[i]+">\t\t<"+time5Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time5Local[i]+">\t\t<"+time5LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time5Crystal[i]+">\t\t<"+time5CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("10]\t <t>\t <"+time10[i]+">\t\t\t<"+time10Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time10Local[i]+">\t\t<"+time10LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time10Crystal[i]+">\t\t<"+time10CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("20]\t <t>\t <"+time20[i]+">\t\t\t<"+time20Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time20Local[i]+">\t\t<"+time20LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time20Crystal[i]+">\t\t<"+time20CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("40]\t <t>\t <"+time40[i]+">\t\t\t<"+time40Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time40Local[i]+">\t\t<"+time40LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time40Crystal[i]+">\t\t<"+time40CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("100]\t <t>\t <"+time100[i]+">\t\t\t<"+time100Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time100Local[i]+">\t\t<"+time100LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time100Crystal[i]+">\t\t<"+time100CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("200]\t <t>\t <"+time200[i]+">\t\t\t<"+time200Numb[i]+">");
					str0.WriteLine("  \t локализованно\t <"+time200Local[i]+">\t\t<"+time200LocalNumb[i]+">");
					str0.WriteLine("  \t в кристаллах\t <"+time200Crystal[i]+">\t\t<"+time200CrystalNumb[i]+">");
					str0.WriteLine("\t");
					str0.WriteLine("===================================================================================");
					str0.WriteLine("\t");
					str0.WriteLine("\t");
				}

				str0.WriteLine("\t");
				str0.Close();

				endMusic.Play ();
				}

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
		
	void CalculateTemp(){
		bestAviableState.Clear ();
		bestAviableState.RemoveRange (0, bestAviableState.Count);
	

		bestAviableState.Add (allAviableState [0]);
		bool badState = false;
		for (int i = 1; i < allAviableState.Count; i++) {
			badState = false;
			for (int k = 0; k < bestAviableState.Count; k++) {		//сравниваем с уже обработанными
				if ((Vector2)allAviableState [i] == (Vector2)bestAviableState [k]) {	//если пытаемся добавить темп перехода на уже отмеченный сайт
					if (allAviableState [i].z <= bestAviableState [k].z) {		//если новое расстояние ничем не лучше старого
						badState = true;
					} else {	// нужно заменить старое
						bestAviableState.RemoveAt(k);
					}
				}
			}
			if (!badState) {
				bestAviableState.Add (allAviableState[i]);
			}
		}		//получли список координат с наилучшими длинами

		//for (int i = 0; i < bestAviableState.Count; i++) {
		//	Debug.Log (i+")   "+(Vector2)bestAviableState[i]+"   -   "+bestAviableState[i].z*100f);
		//}
		endAviableState.Clear ();
		endAviableState.RemoveRange (0, endAviableState.Count);
		endAviableState.Add (bestAviableState [0]);

		for (int i = 1; i < bestAviableState.Count; i++) {
			badState = false;
			for (int k = 0; k < endAviableState.Count; k++) {
				if (crystalID [(int)bestAviableState [i].x, (int)bestAviableState [i].y] != 0) {		//если попали в кристалл
					if (crystalID [(int)bestAviableState [i].x, (int)bestAviableState [i].y] == crystalID [(int)endAviableState [k].x, (int)endAviableState [k].y]) {		//если попали в уже отмеченный кристаллит
						crystalIDTemp = crystalID [(int)endAviableState [k].x, (int)endAviableState [k].y];
						crystalCenterX = crystalIDX [crystalIDTemp];
						crystalCenterY = crystalIDY [crystalIDTemp];
						if (crystalStart [crystalCenterX, crystalCenterY] == 2) {		//если ориенатция горизонтальная
							if (bestAviableState [i].y == endAviableState [k].y) {		//если попали в ту же цепь
								if (bestAviableState [i].z <= endAviableState [k].z) {		//если новое расстояние ничем не лучше старого
									badState = true;
								} else {
									endAviableState.RemoveAt (k);
								}
							}
						} else {
							if (bestAviableState [i].x == endAviableState [k].x) {		//если попали в ту же цепь
								if (bestAviableState [i].z <= endAviableState [k].z) {		//если новое расстояние ничем не лучше старого
									badState = true;
								} else {
									endAviableState.RemoveAt (k);
								}
							}
						}
					}
				}
			}
			if (!badState) {
				endAviableState.Add (bestAviableState[i]);
			}
		}		//получли конченый список с координат и темпов

		//for (int i = 0; i < endAviableState.Count; i++) {
		//	Debug.Log (i+")   "+(Vector2)endAviableState[i]+"   -   "+endAviableState[i].z*100f);
		//}
	}

	void GenerateMap(){
		clasterNumb = 1;
		if (CheckChange ()) {
		}
		for (int x = 0; x <= 299; x++) {
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
		for(int x=25;x<=275; x++){
			for(int y=25;y<=475; y++){
				if (RandomNormal(chance)
					&& crystalStart [x-1, y+1]==0 && crystalStart [x, y+1]==0 && crystalStart [x+1, y+1]==0
					&& crystalStart [x-1, y]==0 && crystalStart [x+1, y]==0
					&& crystalStart [x-1, y-1]==0 && crystalStart [x, y-1]==0 && crystalStart [x+1, y-1]==0) {

					clasterNumb++;
					crystalStart [x, y] = (int)Random.Range(0,2)+1;		//orientation
					crystalSize[x,y] = 0;
					crystalID[x,y] = clasterNumb;
					Enrg [x, y] = RndEnrg1Crystal () - Et * kT;
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
	public void CheckRepeat(int xx, int yy, float cc){
		
		/*if (crystalID [xx, yy] != 0) {
			//Debug.Log (B+"         "+xx+" , "+yy);
			//Debug.Log (crystalID [xx, yy]);
			//Debug.Log (LocalCrystallID [B]);
			LocalCrystallID [B] = crystalID [xx, yy];
			LocalCrystallEnrg[B] = Enrg[xx, yy];
			LocalCrystallC [B] = cc;
			LocalCrystallA [B] = A;
			if (B == 0) {
				saveTemp = true;
			}
			if(B>=1){
				for(int i=0; i<B;i++){
					if (LocalCrystallID [i] == LocalCrystallID [B] && LocalCrystallEnrg [i] == LocalCrystallEnrg [B]) {
						if (cc < LocalCrystallC [i]) {
							v [LocalCrystallA [i]] = 0;
							saveTemp = true;
						}
					}
				}
			}
			if (saveTemp) {
				B++;
			} else {
				LocalCrystallID [B] = 0;
				v [A] = 0;
			}
			saveTemp = false;
		}*/
	}

	void NewSpace(){
		ClearTexture ();
		clasterNumb = 1;
		for (int x = 0; x <= 299; x++) {
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
		for(int x=25;x<=275; x++){
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
					Enrg [x, y] = RndEnrg1Crystal () - Et * kT;
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
			for (int x = 25; x <= 275; x++) {
				for (int y = 25; y <= 475; y++) {
					if (crystalStart [x, y] != 0) {
						CheckGrowSpace (x, y);
					}
				}
			}
		}
		if (growType == 1) {
			for (int x = 25; x <= 275; x++) {
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
		Et = EtUI.GetComponent<Slider> ().value * sigma;
		sigmaCrystal = sigmCrystUI.GetComponent<Slider> ().value;
		return change;
	}

	void reColorCentre(){
		if (colorCentre) {
			colorCentre = false;
		} else {
			colorCentre = true;
		}

		for (int x = 25; x <= 275; x++) {
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


		for (int x = 50; x <= 250; x++) {
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
							chainNumb [l]=1;
							lenghtChain [l]=1;
						} else {
							lenghtChain[l]=(crystalSizeUp [x, y] + crystalSizeDown [x, y])+1;
							if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) != 0) {
								chainNumb[l]= (crystalSizeLeft [x, y] + crystalSizeRight [x, y])+1;
							} else {
								chainNumb [l] = 1;
							}
						}
					} else {
						if ((crystalSizeLeft [x, y] + crystalSizeRight [x, y]) == 0) {
							chainNumb [l]=1;
							lenghtChain [l]=1;
						} else {
							lenghtChain[l]= (crystalSizeLeft [x, y] + crystalSizeRight [x, y])+1;
							if ((crystalSizeUp [x, y] + crystalSizeDown [x, y]) != 0) {
								chainNumb[l]= (crystalSizeUp [x, y] + crystalSizeDown [x, y])+1;
							} else {
								chainNumb [l]=1;
							}
						}
					}
					lenghtChainGlobal [lenghtChain [l], l]++;
				}
			}
		}
		crystalPercent [l] = (float)(crystalPercent [l] / 80000f);
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
				float tempEnrg =  RndEnrg1Crystal () - Et * kT;
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

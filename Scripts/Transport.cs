using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {


	public int N;		//длина слоя на котором измеряем плотность
	public float E;		//напряженность электрического поля
	public float r = 1;		//радиус решетки
	public float kT = 0.3f;		//k * температуру
	public int limit=20000;		
	public int LeftLim=100;		
	public int RightLim=30;		
	public float e=1;
	public float limitG = 0.00001f;
	private float[,] Enrg = new float[2000,2000];
	private bool[,] EnrgExist = new bool[2000,2000];
	private int[,] EnrgNumb = new int[2000, 2000];
	private int X = 1000;
	private int Y = 1000;
	private int a=0;
	private int b=0;
	private float c=0;
	private float d=0;
	private int i;
	private bool Refresh = false;

	private float MaxEnrg;
	private float[] v = new float[28];		//темпы перехода
	private float[] p = new float[28];		//вероятности перехода
	private int[] index2X = new int[28];
	private int[] index2Y = new int[28];
	public Texture2D textureNew;
	public Camera MainCamera;
	private Color color = Color.black;
	private Color colorR = Color.red;
	private Color colorO = new Color (1, 0.6f, 0, 1f);
	private Color colorY = Color.yellow;
	private Color colorG = Color.green;
	private Color colorDel = new Color (1, 1, 1, 0);
	private Vector2 oldPixel;
	private int BackLimit;
	private float Eplus1=0.01f; //0.025 = 0.1kT
	private float Eplus2=0f;
	private float XCamera;
	private float YCamera;
	private float XCameraOld;
	private float YCameraOld;
	public int Model=1;
	private bool All = true;
	public int Stat = 100;
	public bool ElPosEl = false;
	public float limitGEnrg;
	public float sigma;
	public int kvazline=1;
	public float Er=4;
	private float delE;
	private float Errr;
	public float M=3;
	private string Mstring="2.5";
	private string MEr="8";
	private string Msigma="2.12";
	public float zone = 5;
	private string MZone="5.0";


	void OnGUI () {
		if (All) {
			///GUI.Box (new Rect (230, 10, 200, 30), "Длина иссл. зоны: " + N);
			///N = (int)GUI.HorizontalSlider (new Rect (240, 45, 180, 20), N, 10, 150);
			///GUI.Box (new Rect (230, 60, 200, 30), "Расст. до левого электр.: " + LeftLim);
			///LeftLim = (int)GUI.HorizontalSlider (new Rect (240, 95, 180, 20), LeftLim, 0, 150);
			///GUI.Box (new Rect (230, 110, 200, 30), "Расст. до правого электр.: " + RightLim);
			///RightLim = (int)GUI.HorizontalSlider (new Rect (240, 145, 180, 20), RightLim, 0, 150);
			///GUI.Box (new Rect (230, 160, 200, 30), "Значение еEa, eEa/kT: " + (Eplus1 + Eplus2)/kT);
			///Eplus2 = GUI.HorizontalSlider (new Rect (240, 195, 180, 20), Eplus2, 0f, 1f);
			///Eplus1 = GUI.HorizontalSlider (new Rect (240, 215, 180, 20), Eplus1, 0.0000f, 0.05f);
			///GUI.Box (new Rect (30, 10, 200, 30), "Модель M-A");
			GUI.Box (new Rect (30, 100, 200, 30), "Глубина sigm*kT*1.41:   -" + M);
			///M = GUI.HorizontalSlider (new Rect (40, 135, 180, 20), M, -2f, 6f);
			Mstring=GUI.TextField(new Rect (40, 135, 180, 20), Mstring);
			M = float.Parse (Mstring);
			GUI.Box (new Rect (30, 230, 200, 30), "Значение kT: " + kT);
			kT = GUI.HorizontalSlider (new Rect (40, 265, 180, 20), kT, 0.001f, 0.05f);
			//GUI.Box (new Rect (30, 280, 200, 30), "Предел вычислений: " + limit);
			//limit = (int)GUI.HorizontalSlider (new Rect (40, 315, 180, 20), limit, 5000, 5000000);
			/*if (Model == 1) {
				GUI.Box (new Rect (30, 330, 200, 30), "Модель через Erf");
			} else if (Model == 2) {
				GUI.Box (new Rect (30, 330, 200, 30), "Модель через плотн. с конц.");
			} else {
				GUI.Box (new Rect (30, 330, 200, 30), "Модель через плотн. без конц.");
			}*/
	
			///kvazline = (int)GUI.HorizontalSlider(new Rect(330,450,200,30),kvazline, 1,2);
			///GUI.Box (new Rect (330, 470, 200, 30), kvazline+ "; 2-кваз.,1-нет");
			//Model = (int)GUI.HorizontalSlider (new Rect (40, 365, 180, 20), Model, 1, 3);
			//GUI.Box (new Rect (330, 350, 200, 30), "");

			///Er = GUI.HorizontalSlider (new Rect (30, 200, 190, 20), Er, 1f, 16f);
			GUI.Box (new Rect (30, 165, 200, 30), "Значение Er/kT = " + Er);
			MEr=GUI.TextField(new Rect (30, 200, 190, 20), MEr);
			Er = float.Parse (MEr);

			GUI.Box (new Rect (30, 290, 200, 30), "Радиус зоны выхода = " + zone);
			MZone=GUI.TextField(new Rect (30, 325, 190, 20), MZone);
			zone = float.Parse (MZone);
			//ElPosEl = GUI.Toggle(new Rect(335,355,200,30),ElPosEl,"Рожд. электр. на электроде");
			//GUI.Box (new Rect (330, 280, 200, 30), "прдел эн. Emax/(bkt) = " + limitGEnrg);
			//limitGEnrg = (int)GUI.HorizontalSlider (new Rect (340, 315, 180, 20), limitGEnrg, 1, 10f);
			GUI.Box (new Rect (30, 30, 200, 30), "значение сигма, b: " + sigma +"kT");
			///sigma = GUI.HorizontalSlider (new Rect (30, 85, 180, 20), sigma, 0.28f, 10f);
			Msigma=GUI.TextField(new Rect (30, 65, 180, 20), Msigma);
			sigma = float.Parse (Msigma);

			//if (GUI.Button (new Rect (30, 380, 200, 30), "Расчет пути")) {
			//	Refresh = true;
			//	Start ();
			//}
			//if(GUI.Button (new Rect (30, 420, 200, 30), "График плотн. с " + Stat + " эл.")){
			//	Start ();
			//	this.gameObject.GetComponent<Statistic>().Inicial = true;
			//}
			///if(GUI.Button (new Rect (30, 360, 200, 30), "быстрый прогон 1 экс.")){
			///	Stat=3;
			///	Start ();
			///	this.gameObject.GetComponent<StaticticF>().Inicial = true;
			///}
			if(GUI.Button (new Rect (30, 400, 200, 30), "вычисление с " + Stat + " экс.")){
				Start ();
				this.gameObject.GetComponent<StaticticF>().Inicial = true;
			}
			Stat = (int)GUI.HorizontalSlider (new Rect (40, 435, 180, 20), Stat, 100, 50000);

			if (GUI.Button (new Rect (30, 500, 30, 30), "+")) {
				if (MainCamera.orthographicSize > 0.15f) {
					MainCamera.orthographicSize -= 0.1f;
				}
			}
			if (GUI.Button (new Rect (70, 500, 30, 30), "-")) {
				if (MainCamera.orthographicSize < 1.5f) {
					MainCamera.orthographicSize += 0.1f;
				}
			}
			if (GUI.Button (new Rect (110, 500, 120, 30), "К началу")) {
				MainCamera.transform.position = new Vector3 (0.22f, 0f, -10);
			}
			if (GUI.Button (new Rect (30, 535, 120, 30), "Свернуть")) {
				All = false;
			}
		} else {
			if (GUI.Button (new Rect (30, 10, 120, 30), "Развернуть")) {
				All = true;
			}}


	}




	// Use this for initialization
	void Start () {
		Screen.SetResolution (640, 480, false);
		Errr = Er * kT;
		E = Eplus1 + Eplus2;
		BackLimit = limit;
		if (ElPosEl) {
			X = 1000 - LeftLim;
		} else {
			X=1000;}
		Y = 1000;

		MaxEnrg = limitGEnrg * sigma * kT;
		//Debug.Log (MaxEnrg);
		//Debug.Log (sigma*kT * Mathf.Sqrt (-2 * Mathf.Log (sigma*kT * 2.506628f*limitG) ));
		oldPixel = new Vector2(2000,2000);
		i = 0;
		int y = 0;
		while (y < textureNew.height) {
			int x = 0;
			while (x < textureNew.width) {
				textureNew.SetPixel (x, y, colorDel);
				if(x<2000-LeftLim*5 || x>2000+N*5+5*RightLim){
					textureNew.SetPixel (x, y, color);
				}
				x++;
			}
			y++;
		}
		y = 0;
		while (y < textureNew.height) {
			int x = 2000;
			textureNew.SetPixel (x, y, color);
			textureNew.SetPixel (x+N*5, y, color);

			y++;
		}
		EnrgNumb [1000-LeftLim, 1000] = 1;

		i = 0;
		for (b=1; b<=2; b++) {
			for (a=2; a>=-2; a--) {
				index2X [2 - a + (b - 1) * 5] = b;
				index2Y [2 - a + (b - 1) * 5] = a;}}
		index2X [10] = 3;
		index2Y [10] = 0;
		for (a=3; a>=1; a--) {
			index2X [14 - a] = 0;
			index2Y [14 - a] = a;}
		for (a=-1; a>=-3; a--) {
			index2X [13 - a] = 0;
			index2Y [13 - a] = a;}
		for (b=-1; b>=-2; b--) {
			for (a=2; a>=-2; a--) {
				index2X[19-a-(b+1)*5]=b;
				index2Y[19-a-(b+1)*5]=a;}}
		index2X [27] = -3;
		index2Y [27] = 0;

		textureNew.Apply();
			}
		


	// Update is called once per frame
	void Update () {
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
			if(!All || Input.mousePosition.x>230){
				MainCamera.transform.position = new Vector3(MainCamera.transform.position.x+XCamera,MainCamera.transform.position.y+YCamera,-10);}
		}


		if (Refresh){

			//while (X<1000+N+RightLim) {
			while(X<1000+20 && X>1000-20 && Y<1000+20 && Y>1000-20){
				Debug.Log(X +"  "+ Y);
				CalcEnrg (X, Y);
				oldPixel = new Vector2 (X + 1000, Y + 1000);
			
			
				/*for (a=0; a<=25; a++) {
					c=index2X[a] * index2X[a] + index2Y[a] * index2Y[a];
					delE=Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] -Enrg [X, Y];
					if(delE < 0){
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)))*Mathf.Exp (delE/(2*kT))*Mathf.Exp (-delE*delE/(4*Errr*kT));
					} else {
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)))*Mathf.Exp (-delE/(2*kT))*Mathf.Exp (-delE*delE/(4*Errr*kT));
						
					}}
					*/

				for (a=0; a<=25; a++) {
					c=index2X[a] * index2X[a] + index2Y[a] * index2Y[a];
					delE=Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] - Enrg [X, Y];
					if(Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] <Enrg [X, Y]){
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)));
					} else {
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c))) * Mathf.Exp ((delE) / (-kT));
						
					}}

				/*for (a=0; a<=25; a++) {
					c=index2X[a] * index2X[a] + index2Y[a] * index2Y[a];
					delE=Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] -Enrg [X, Y];
					
					v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)))*Mathf.Exp (-(Mathf.Pow((delE+Er),2))/(4*Er*kT));
				}*/

				if (X <= 1000 - LeftLim) {
					for (a=17; a<=27; a++) {
						v [a] = 0;
					}
				}

				c = 0;
				for (a=0; a<=27; a++) {
					c += v [a];		//временная сумма всех темпов
				}
				for (a=0; a<=27; a++) {
					p [a] = v [a] / c;
				}
		
				d = Random.Range (0, 1f);
				a = 0;
				c = 0;
				while (c<=d) {
					c += p [a];
					a++;
				}
				a--;
				X += index2X [a];
				Y += index2Y [a];
				EnrgNumb [X, Y]++;
				a = X - 1000;
				b = Y - 1000;

				DrowPixel ();
				ConnectPixel ();



				limit--;

				if (limit < 0) {
					X = 1500;

				}
				//Debug.Log (i+"     "+ X);
			}
	}
		//if (X >= (1000 + N) && Refresh == true) {
		if ((X>=1000+20 || X<=1000-20 || Y>=1000+20 || Y<=1000-20) && Refresh == true) {
			Refresh = false;
			textureNew.Apply();
			limit = BackLimit;
			for(a=940;a<1260;a++){
				for (b=0;b<=1999;b++){
					EnrgExist[a,b] = false;
					EnrgNumb[a,b] = 0;
				}
			}
			}
		XCameraOld = Input.mousePosition.x;
		YCameraOld = Input.mousePosition.y;
	}


	void CalcEnrg(int x, int y){		//Заполнение ближайших уровней энергий, если они не заполнены
		for (b=2; b>=-2; b--) {
			for (a=2; a>=-2; a--) {

				if(!EnrgExist[x+a,y+b]){
					Enrg [x + a, y + b] = RndEnrg0();
					EnrgExist[x + a, y + b] = true;}
			}
		}
		if (!EnrgExist[x + 3, y]){
			Enrg [x + 3, y] = RndEnrg0();
			EnrgExist[x + 3, y] = true;}
		if (!EnrgExist[x - 3, y]){
			Enrg [x - 3, y] = RndEnrg0();
			EnrgExist[x - 3, y] = true;}
		if (!EnrgExist[x, y + 3]){
			Enrg [x, y + 3] = RndEnrg0();
			EnrgExist[x, y + 3] = true;}
		if (!EnrgExist[x, y - 3]){
			Enrg [x, y - 3] = RndEnrg0();
			EnrgExist[x, y - 3] = true;}
	}

	private float RndEnrg0(){
		float l;
		if (Model == 1) {
			l = RndEnrg1 ();
		} else if (Model == 2) {
			l = RndEnrg2 ();
		} else {
			l=RndEnrg3();}
		return (l);}


	private float RndEnrg1(){
		float j = Random.Range(0f,1f);
		float max;
		float min;
		float p;
		int o;
		if (j >= 0.5f) {
			min = 0f;
			max = MaxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1f+Erf(p/(sigma*kT*1.414213f)))> j) {
					max = p;
					p = min + (max - min) / 2;
				} else {
					min = p;
					p = min + (max - min) / 2;
				}
			}
		} else {
			max = 0f;
			min = -MaxEnrg;
			p = min + (max - min) / 2;
			for (o=1; o<10; o++) {
				if (0.5f*(1f+Erf(p/(sigma*kT*1.414213f))) > j) {
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

	private float RndEnrg2(){
		float j = Random.Range (0f, 1f);
		float l = Random.Range (0f, MaxEnrg);
		bool ss = false;
		while(ss ==false){
			if (j < Mathf.Exp (-l * l / (2 *sigma* kT*sigma*kT))) {
				ss = true;
				//l=kT*Mathf.Sqrt(-2*Mathf.Log(j));
				float h = Random.Range(-1f,1f);
				l=l*(-1);
				if(h<0){
					l=l*(-1);
					j=1-j;
				}}
			else{
				ss = true;
				l=sigma*kT*Mathf.Sqrt(-2*Mathf.Log(j));
				float h = Random.Range(-1f,1f);
				l=l*(-1);
				if(h<0){
					l=l*(-1);
					j=1-j;
				}}
			
		}
		return(l);	
	}
	private float RndEnrg3(){
		float j = Random.Range (0f, 1f);
		float l = Random.Range (0f, MaxEnrg);
		bool ss = false;
		while(ss ==false){
			if (j < Mathf.Exp (-l * l / (2 *sigma* kT*sigma*kT))) {
				ss = true;
				//l=kT*Mathf.Sqrt(-2*Mathf.Log(j));
				float h = Random.Range(-1f,1f);
				l=l*(-1);

				if(h<0){
					l=l*(-1);

				}}
			else{
				j = Random.Range (0f, 1f);
				l = Random.Range (0f, MaxEnrg);
			}}
		return(l);	
	}

	void DrowPixel(){
		int x = X + a * 4+1000;
		int y = Y + b * 4+1000;
		if (EnrgNumb [X, Y]==1) {
			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, colorG);
				}}}
		if (EnrgNumb [X, Y]==2) {
			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, colorY);
				}}}
		if (EnrgNumb [X, Y]==3 || EnrgNumb [X, Y]==4) {
			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, colorO);
				}}}
		if (EnrgNumb [X, Y]>=5) {
			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, colorR);
				}}}
	}
	void ConnectPixel(){
		a=2000+a*5;
		b=2000+b*5;
		oldPixel = new Vector2(2000+5*(oldPixel.x-2000),2000+5*(oldPixel.y-2000));
		if(a-oldPixel.x>=0 && b-oldPixel.y>=0){
			for(i=1;i<(a-oldPixel.x);i++){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((b-oldPixel.y)/(a-oldPixel.x)),color);
			}
			for(i=1;i<(b-oldPixel.y);i++){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((a-oldPixel.x)/(b-oldPixel.y)),(int)oldPixel.y+i,color);
			}}
		if(a-oldPixel.x>=0 && b-oldPixel.y<=0){
			for(i=1;i<(a-oldPixel.x);i++){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((b-oldPixel.y)/(a-oldPixel.x)),color);
			}
			for(i=-1;i>(b-oldPixel.y);i--){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((a-oldPixel.x)/(b-oldPixel.y)),(int)oldPixel.y+i,color);
			}}
		if(a-oldPixel.x<=0 && b-oldPixel.y>=0){
			for(i=-1;i>(a-oldPixel.x);i--){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((b-oldPixel.y)/(a-oldPixel.x)),color);
			}
			for(i=1;i<(b-oldPixel.y);i++){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((a-oldPixel.x)/(b-oldPixel.y)),(int)oldPixel.y+i,color);
			}}
		if(a-oldPixel.x<=0 && b-oldPixel.y<=0){
			for(i=-1;i>(a-oldPixel.x);i--){
				textureNew.SetPixel((int)oldPixel.x+i,(int)oldPixel.y+i*(int)((b-oldPixel.y)/(a-oldPixel.x)),color);
			}
			for(i=-1;i>(b-oldPixel.y);i--){
				textureNew.SetPixel((int)oldPixel.x+i*(int)((a-oldPixel.x)/(b-oldPixel.y)),(int)oldPixel.y+i,color);
			}}
	}
}

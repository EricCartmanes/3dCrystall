using UnityEngine;
using System.Collections;
using System.IO;


public class Statistic : MonoBehaviour {
	private int N;		//длина слоя на котором измеряем плотность
	private float E;		//напряженность электрического поля
	private float r = 1;		//радиус решетки
	private float kT = 0.25f;		//k * температуру
	private int limit=20000;		
	private int LeftLim=150;		
	private int RightLim=150;		
	private float e=1;
	private float limitG = 0.00001f;
	private float[, ] Enrg = new float[16000,16000];
	private bool[,] EnrgExist = new bool[16000,16000];
	private int[,] EnrgNumb = new int[16000, 16000];
	private float[] FulTime = new float[16000];
	private int X = 8000;
	private int Y = 8000;
	private int Z = 8000;
	private int a=0;
	private int b=0;
	private float c=0;
	private float d=0;
	private int i;
	private bool Refresh = false;
	private float Er;
	
	private float MaxEnrg;
	private float[] v = new float[29];		//темпы перехода
	private float[] p = new float[29];		//вероятности перехода
	private int[] index2X = new int[29];
	private int[] index2Y = new int[29];
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
	private float Eplus1=0.003f;
	private float Eplus2=0f;
	private float XCamera;
	private float YCamera;
	private float XCameraOld;
	private float YCameraOld;
	private int Model=1;
	private bool All = true;
	private int Stat;
	public bool Inicial = false;
	private bool Work = false;
	private float tempSumm;
	private float NormTime;
	private float sigma;
	private float limitGEnrg;
	private bool ElPosEl;
	private float AbsolutTime = 0;
	private float StepTime;
	private float T;
	private float[] K = new float[300000];
	private int l=1;
	private int StatFirst;
	private float middleK;
	private float middleInvK;
	private int kvazline;
	private bool KvazilineExist =false;
	private int g=0;
	private float delE;
	
	void OnGUI () {
		GUI.Box (new Rect ((Screen.width)/2-100, 0, 200, 25), "Время вычисл.: " + AbsolutTime/60 + "мин.");
		if (Work) {
			GUI.Box (new Rect ((Screen.width)/2-100, 40, 200, 40), "Осталось " + Stat + " Вычислений");}
		
	}
	
	
	
	
	// Use this for initialization
	void Start () {
		ElPosEl = this.gameObject.GetComponent<Transport> ().ElPosEl;
		limitGEnrg = this.gameObject.GetComponent<Transport> ().limitGEnrg;
		sigma = this.gameObject.GetComponent<Transport> ().sigma;
		N = this.gameObject.GetComponent<Transport> ().N;
		E = this.gameObject.GetComponent<Transport> ().E;
		r = this.gameObject.GetComponent<Transport> ().r;
		kT = this.gameObject.GetComponent<Transport> ().kT;
		limit = this.gameObject.GetComponent<Transport> ().limit;
		LeftLim = this.gameObject.GetComponent<Transport> ().LeftLim;
		RightLim = this.gameObject.GetComponent<Transport> ().RightLim;
		e = this.gameObject.GetComponent<Transport> ().e;
		limitG = this.gameObject.GetComponent<Transport> ().limitG;
		Model = this.gameObject.GetComponent<Transport> ().Model;
		Stat = this.gameObject.GetComponent<Transport> ().Stat;
		kvazline = this.gameObject.GetComponent<Transport> ().kvazline;
		Er = this.gameObject.GetComponent<Transport> ().Er;


		Er = Er * kT;

		BackLimit = limit;
		if (ElPosEl) {
			X = 8000 - LeftLim;
		} else {
			X=8000;}
		Y = 8000;
		MaxEnrg = limitGEnrg * sigma * kT;



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
		//EnrgNumb [1000-LeftLim, 1000] = 1;
		
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
		textureNew.Apply ();
		StatFirst = Stat;

	}
	


	
	// Update is called once per frame
	void Update () {
		if (Inicial) {
			Start ();

			Inicial = false;
			for(int y=7900;y<8000+N;y++){
				FulTime[y]=0;}
			AbsolutTime=0;
			Work = true;
			Debug.Log(E);
			//StreamWriter str1 = new StreamWriter("outputTime.txt");
		}

		if (Stat == 0) {
			Work = false;
			Start ();


			NormTime=0;

			for(a=0;a<textureNew.height;a++){
				textureNew.SetPixel(a,2000,color);
				textureNew.SetPixel(a,2300,color);


			}
			for(a=8000;a<=8000+N/2;a++){
				NormTime+=FulTime[a];
			}
			NormTime=NormTime/(N/2);
			for(a=8000;a<=8000+N;a++){
				if((FulTime[a]/NormTime)<1.5f){
					FulTime[a]=FulTime[a]/NormTime;
				}
				else{
					FulTime[a]=0;}
			}
			NormTime=0;
			for(a=8000;a<=8000+N/2;a++){
				NormTime+=FulTime[a];
			}
			NormTime=NormTime/(N/2);
			StreamWriter str0 = new StreamWriter("Assets/StreamingAssets/output.txt");
			for(a=8000;a<=8000+N;a++){
				if((FulTime[a]/NormTime)<1.5f){
					FulTime[a]=FulTime[a]/NormTime;
					if(FulTime[a]!=0){
						str0.WriteLine(a-8000 + " "+ FulTime[a]);
						DrowPixel();}
				}

			}
			str0.Close();
			StreamWriter str1 = new StreamWriter("Assets/StreamingAssets/outputTime.txt");
			for(l=1;l<StatFirst;l++){
				middleK+=K[l];
				middleInvK+=(1/K[l]);
				str1.WriteLine(l+")   "+K[l]+ "   " +(1/K[l]));
			}
			middleK=middleK/l;
			middleInvK=middleInvK/l;
			str1.WriteLine("<"+middleK+">     <"+middleInvK+">");
			str1.Close();

			textureNew.Apply();
			Stat--;

			}

		if (Work){
			AbsolutTime+=Time.deltaTime;

					//while (X<8000+N+RightLim) {
					while(X<8000+5 && X>8000-5 && Y<8000+5 && Y>8000-5){
				//Debug.Log("l= "+l+";  ["+X+","+Y+"]");
						/*if(g==25){
							Debug.Log(X+" "+Y);
							g=0;
						}*/
						g++;
						CalcEnrg (X, Y);
						oldPixel = new Vector2 (X + 8000, Y + 8000);

				for (a=0; a<=27; a++) {
					c=index2X[a] * index2X[a] + index2Y[a] * index2Y[a];
					delE=Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] - Enrg [X, Y];
					if(Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] <Enrg [X, Y]){
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)));
					} else {
						v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c))) * Mathf.Exp ((delE) / (-kT));
						
					}}
				/*for (a=0; a<=27; a++) {
					c=index2X[a] * index2X[a] + index2Y[a] * index2Y[a];
					delE=Enrg[X+index2X[a],Y+index2Y[a]]-E*e*r*index2X[a] -Enrg [X, Y];
					
					v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)))*Mathf.Exp (-(Mathf.Pow((delE+Er),2))/(4*Er*kT));
				}*/

						if (X <= 8000 - LeftLim) {
							for (a=17; a<=27; a++) {
								v [a] = 0;
							}
						}
				
						c = 0;
						for (a=0; a<=27; a++) {
							c += v [a];		//временная сумма всех темпов
						}
						tempSumm = c;
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
				if(a>=27){a=27;}
				if(X>=8000-5 && X<= 8000+5 && Y>=8000-5 && Y<= 8000+5){
					T=((-Mathf.Log(Random.Range(0.00001f,1f)))/(tempSumm))/10000000;
					FulTime[X]+=T;
					StepTime+=T;
				}
						X += index2X [a];
						Y += index2Y [a];
						EnrgNumb [X, Y]++;
						a = X - 8000;
						b = Y - 8000;
				

				
				
						
						limit--;
						
						
						
						
						if (limit < 0) {
							X = 8900;
					
						}
						//Debug.Log (i+"     "+ X);
					}
				KvazilineExist = false;
				K[l]=StepTime/1000;
				StepTime =0;
				l++;
				Stat--;
				limit = BackLimit;
				//X = 8000 - LeftLim;
				X=8000;
				Y = 8000;
				for(a=7950;a<8050;a++){
					for (b=7950;b<=8050;b++){
						EnrgExist[a,b] = false;
						EnrgNumb[a,b] = 0;
					}
				}

	}


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

		if (kvazline == 2) {
			if(KvazilineExist==false){
				KvazilineExist=true;
				Enrg[8000,8000]=RndEnrgFirst();

			}}
	}
	
	private float RndEnrg0(){
		float l;
		if (Model == 1) {
			l = RndEnrg1 ();
		} else if (Model == 2) {
			l = RndEnrg2 ();
		} else {
			l=RndEnrg3();}
		//Debug.Log (l);
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
			min = -MaxEnrg;
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

	private float RndEnrgFirst(){
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
			min = -MaxEnrg;
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

		min =min - sigma * sigma *kT;

		if (min <= (-MaxEnrg)) {
			min=-MaxEnrg;}
		min = -2 * kT;
		return (min);
	}


	void DrowPixel(){
		int x = (a-1000)*5+2000;
		int y = (int)(FulTime[a]*300+2000);
		//Debug.Log (x + "  " + y);
			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, colorR);
				}}

	}

}
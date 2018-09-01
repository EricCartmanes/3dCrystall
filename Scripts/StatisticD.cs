using UnityEngine;
using System.Collections;
using System.IO;

public class StatisticD : MonoBehaviour {

	private int N;		//длина слоя на котором измеряем плотность
	private float E;		//напряженность электрического поля
	private float r = 1;		//радиус решетки
	private float kT = 0.25f;		//k * температуру
	private int limit=25000;		
	private int LeftLim=30;		
	private int RightLim=30;		
	private float e=1;
	private int X = 150;
	private int Y = 150;
	private int Z = 150;
	private bool Refresh = false;
	private float MaxEnrg;
	private int Model=1;
	private int Stat;
	public bool Inicial = false;
	private bool Work = false;
	private float sigma;
	private float limitGEnrg;
	private bool ElPosEl;
	private float StepTime;
	private float AbsolutTime = 0;
	private int kvazline;
	private int[] indexX = new int[126];
	private int[] indexY = new int[126];
	private int[] indexZ = new int[126];
	private float[] v = new float[126];		//темпы перехода
	private float[] p = new float[126];		//вероятности перехода
	private int i;
	private int StatFirst;
	private float[] FulTime = new float[2000];
	private float[,,] Enrg = new float[307,301,301];
	private bool[,,] EnrgExist = new bool[307,301,301];
	private int[,,] EnrgNumb = new int[307,301,301];
	private float NormTime=0;
	private int a;
	private int b;
	private float c;
	private float d;
	private float tempSumm;
	private float T;
	private bool KvazilineExist =false;
	private float[] K = new float[30000];
	private float[] K2 = new float[30000];
	private float[] K3 = new float[30000];
	private float[] K4 = new float[30000];
	private float[] KG = new float[30000];
	private float[] K2G = new float[30000];
	private float[] K3G = new float[30000];
	private float[] K4G = new float[30000];
	private float[] K5 = new float[30000];
	private float[] K6 = new float[30000];
	private float[] K7 = new float[30000];
	private int l=1;
	private int BackLimit;
	private float middleK;
	private float middleInvK;
	private float middleK2;
	private float middleInvK2;
	private float middleK3;
	private float middleInvK3;
	private float middleK4;
	private float middleInvK4;
	private float middleK5;
	private float middleK6;
	private int g=0;
	private int g1=0;
	private int g2=0;
	private int g3=0;
	private int g4=0;
	private int gLast=0;
	private float Er;
	private float delE;
	private float M = 3f;
	private float EnrgNiz;

	private float tMain=0;
	private float tLast=0;
	private float jumpF;
	private bool jumpFbool=true;
	private float exitFirst=0;
	private float exitTry=1;
	private bool saveNextTime=true;
	private bool saveNextJump = true;
	private float midDist=0;
	private float midEnrg=0;
	private float midDist2=0;
	private float midEnrg2=0;
	private int midNumb=0;
	private int jumpNumb=0;
	private int[] xCoord = new int[30000];
	private int[] yCoord = new int[30000];
	private int[] zCoord = new int[30000];
	private float[] jumpTime = new float[30000];
	private int returnPoint=0;
	private float walkTimer=0;
	private float walkTimer2=0;
	private float tNotAll;
	private bool borderError = false;
	private int borderErrors = 0;
	private bool backError = false;
	private int backErrors = 0;
	private int limitErrors = 0;
	private bool firstTimeLast = true;
	private bool saveLast = false;
	private float dispT;
	private float dispTInv;
	private float dispT2;
	private float dispTInv2;
	private float dispT3;
	private float dispTInv3;
	private float dispT4;
	private float dispTInv4;
	private float dispE;
	private bool limitError = false;
	private float stepTime1 = 0;	//0:0
	private float stepTime2 = 0;	//0:30
	private float stepTime3 = 0;	//30:0
	private float stepTime4 = 0;	//30:30
	private bool saveTime1 = true;
	private bool saveTime2 = true;
	private bool saveTime3 = true;
	private bool saveTime4 = true;
	private float middleKG = 0;
	private float middleKG2 = 0;
	private float middleKG3 = 0;
	private float middleKG4 = 0;
	private int returnLimit = 1000;
	int hh=0;





	void OnGUI () {
		//GUI.Box (new Rect ((Screen.width) / 2 - 100, 0, 200, 25), "Время вычисл.: " + AbsolutTime / 60 + "мин.");
		if (Work) {
			GUI.Box (new Rect ((Screen.width) / 2 - 100, 40, 200, 40), "Осталось " + Stat + " Вычислений");
		}
	}



	// Use this for initialization
	void Start () {

		ElPosEl = this.gameObject.GetComponent<Transport> ().ElPosEl;
		limitGEnrg = this.gameObject.GetComponent<Transport> ().limitGEnrg;		//6 т.е. 6=Emax/bKT
		sigma = this.gameObject.GetComponent<Transport> ().sigma;		//2.13....
		N = this.gameObject.GetComponent<Transport> ().N;
		E = this.gameObject.GetComponent<Transport> ().E;		//0, (eEa/kT = 1 => E= 0.025) 
		r = this.gameObject.GetComponent<Transport> ().r;		//1
		kT = this.gameObject.GetComponent<Transport> ().kT;		//0.025
		//limit = this.gameObject.GetComponent<Transport> ().limit;		//3.6KK
		limit = 25000;
		//LeftLim = this.gameObject.GetComponent<Transport> ().LeftLim;
		//LeftLim = 100;
		LeftLim = 0;
		RightLim = this.gameObject.GetComponent<Transport> ().RightLim;
		e = this.gameObject.GetComponent<Transport> ().e;		//1
		Model = this.gameObject.GetComponent<Transport> ().Model;
		Stat = this.gameObject.GetComponent<Transport> ().Stat;
		kvazline = this.gameObject.GetComponent<Transport> ().kvazline;
		Er = this.gameObject.GetComponent<Transport> ().Er;
		M = this.gameObject.GetComponent<Transport> ().M;

		
		Er = Er * kT;
	





		BackLimit = limit;
		if (ElPosEl) {
			X = 150 - LeftLim;
		} else {
			X=150;}
		Y = 150;
		Z = 150;
		MaxEnrg = limitGEnrg * sigma * kT;

		Marker ();
		StatFirst = Stat;



	}
	
	// Update is called once per frame
	void Update () {
		if (Inicial) {
			Start ();
			
			Inicial = false;
			//for(int i=1000;i<1000+N;i++){
			for(int i=1;i<306;i++){
				FulTime[i]=0;}
			AbsolutTime=0;
			Work = true;
			//Debug.Log(E);
		}
		if (Stat == 0) {
			Work = false;
			Start ();
			
		
			NormTime=0;
			
			/*
			//for(i=5;i<=45+N/2;i++){
			for(i=5;i<=45+N/2;i++){
				NormTime+=FulTime[i];
			}
			NormTime=NormTime/(N/2);
			for(i=5;i<=45+N;i++){
				if((FulTime[i]/NormTime)<1.5f){
					FulTime[i]=FulTime[i]/NormTime;
				}
				else{
					FulTime[i]=0;}
			}
			NormTime=0;
			for(i=5;i<=45+N/2;i++){
				NormTime+=FulTime[i];
			}
			NormTime=NormTime/(N/2);
			StreamWriter str0 = new StreamWriter("Assets/StreamingAssets/output.txt");
			for(i=1000;i<=1000+N;i++){
				if((FulTime[i]/NormTime)<1.5f){
					FulTime[i]=FulTime[i]/NormTime;
					if(FulTime[i]!=0){
						str0.WriteLine(i-1000 + " "+ FulTime[i]);
						}
				}
				
			}
			str0.Close();
			*/
			returnPoint++;

			/*
			//StreamWriter str2 = new StreamWriter("/TRACK_Mrc_Er="+Er/kT+"kT_sigm="+sigma+"kT_E0=-"+M+"sigmkTsqrt(2).txt");
			//StreamWriter str2 = new StreamWriter(@Application.streamingAssetsPath+"___TRACK_Mrc_Er="+Er/kT+"kT_sigm="+sigma+"kT_E0=-"+M+".txt");
			StreamWriter str2 = new StreamWriter(@Application.streamingAssetsPath+"___TRACK_MA_N="+N+"_sigm="+sigma+"kT_E=-"+E+".txt");
			//str2.WriteLine("Полный путь Модель Mrc\tEr="+Er/kT+"kT\tsigma="+sigma+"kT\tE0=-"+M+" sigma*kT*sqrt(2)\tиспытаний "+this.gameObject.GetComponent<Transport> ().Stat);
			str2.WriteLine("Полный путь Модель MA\tN="+N+"\tsigma="+sigma+"kT\tE=-"+E+" a*e*r\tиспытаний "+this.gameObject.GetComponent<Transport> ().Stat);
			str2.WriteLine("\t");
			str2.WriteLine("\t");
			str2.WriteLine("0)\t[50,50,50]\tЭнергия= "+Enrg[50,50,50]);
			str2.WriteLine("\t____________________________________________________________");
			str2.WriteLine("\t");
			for(l=1;l<jumpNumb;l++){

				walkTimer+=1/(jumpTime[l]*10000000/22);

				if(l==1){
					str2.WriteLine("\t\tЭнергии  \t "+Enrg [50,50,50]+"  ==>  "+Enrg [xCoord[l],yCoord[l],zCoord[l]]);
					str2.WriteLine("\t\tСостояния\t[50,50,50] ==> ["+(xCoord[l])+","+(yCoord[l])+","+(zCoord[l])+"]");
					str2.WriteLine(l+")\t["+(xCoord[l])+","+(yCoord[l])+","+(zCoord[l])+"]\tвремя на прыжок= "+jumpTime[l]*10000000/22+"\tразница энергий= "+(Enrg [xCoord[l],yCoord[l],zCoord[l]] - Enrg [50,50,50]));
				}else{
					str2.WriteLine("\t\tСостояния\t["+(xCoord[l-1])+","+(yCoord[l-1])+","+(zCoord[l-1])+"] ==> ["+(xCoord[l])+","+(yCoord[l])+","+(zCoord[l])+"]");
					str2.WriteLine("\t\tЭнергии  \t "+Enrg [xCoord[l-1],yCoord[l-1],zCoord[l-1]]+"  ==>  "+Enrg [xCoord[l],yCoord[l],zCoord[l]]);
					str2.WriteLine(l+")\t["+(xCoord[l])+","+(yCoord[l])+","+(zCoord[l])+"]\tвремя на прыжок= "+jumpTime[l]*10000000/22+"\tразница энергий= "+(Enrg [xCoord[l],yCoord[l],zCoord[l]] - Enrg [xCoord[l-1],yCoord[l-1],zCoord[l-1]]));}


				if(l==returnPoint){
					str2.WriteLine("\t");
					str2.WriteLine("\t____________________________________________________________");
					str2.WriteLine("\t точка невозврата\tвремя блужданий через обратное= "+(1/walkTimer));
					walkTimer2=(1/walkTimer);
					str2.WriteLine("\t____________________________________________________________");
					str2.WriteLine("\t");
				}
			}
			str2.WriteLine("\t____________________________________________________________");
			str2.WriteLine("\t область покинута\tбез конченых блужданий= "+walkTimer2);
			str2.WriteLine("\t____________________________________________________________");
			str2.Close();

			*/
			//StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"___STAT_Mrc_Er="+Er/kT+"kT_sigm="+sigma+"kT_E0=-"+M+".txt");
			StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"___STAT_MA_N="+N+"_sigm="+sigma+"_E=-"+E+"_kvaz="+kvazline+".txt");
			//StreamWriter str1 = new StreamWriter(@Application.streamingAssetsPath+"___STAT_Mrc_Er"+Er+"N="+N+"_sigm="+sigma+"_E=-"+E+"_kvaz="+kvazline+".txt");
			//StreamWriter str1 = new StreamWriter("Assets/StreamingAssets/MA_sigm="+sigma+"kT_E0=-"+M+"sigmkTsqrt(2).txt");
			for(l=1;l<StatFirst;l++){
				middleK+=K[l];
				middleInvK+=(1/K[l]);
				middleK2+=K2[l];
				middleInvK2+=(1/K2[l]);
				middleK3+=K3[l];
				middleInvK3+=(1/K3[l]);
				middleK4+=K4[l];
				middleInvK4+=(1/K4[l]);
				middleK5+=K5[l];
				middleKG+=KG[l];
				middleKG2+=K2G[l];
				middleKG3+=K3G[l];
				middleKG4+=K4G[l];
				//str1.WriteLine(l+")   "+K[l]+ "   " +(1/K[l]));
			}
			middleK=middleK/l;
			middleInvK=middleInvK/l;
			middleK2=middleK2/l;
			middleInvK2=middleInvK2/l;
			middleK3=middleK3/l;
			middleInvK3=middleInvK3/l;
			middleK4=middleK4/l;
			middleInvK4=middleInvK4/l;
			midEnrg=middleK5/l;
			middleKG=middleKG/l;
			middleKG2=middleKG2/l;
			middleKG3=middleKG3/l;
			middleKG4=middleKG4/l;

			for(i=1;i<StatFirst;i++){
				dispT+=(K[i]-middleK);
				dispTInv+=(1/K[i]-(middleInvK));
				dispT2+=(K2[i]-middleK2);
				dispTInv2+=(1/K2[i]-(middleInvK2));
				dispT3+=(K3[i]-middleK3);
				dispTInv3+=(1/K3[i]-(middleInvK3));
				dispT4+=(K4[i]-middleK4);
				dispTInv4+=(1/K4[i]-(middleInvK4));
				dispE+=(K5[i]-midEnrg);
			}
			dispT=Mathf.Sqrt(Mathf.Pow((dispT/22*10000),2)/l);
			dispTInv=Mathf.Sqrt(Mathf.Pow(((1/dispTInv)/22*10000),2)/l);
			dispT2=Mathf.Sqrt(Mathf.Pow((dispT2/22*10000),2)/l);
			dispTInv2=Mathf.Sqrt(Mathf.Pow(((1/dispTInv2)/22*10000),2)/l);
			dispT3=Mathf.Sqrt(Mathf.Pow((dispT3/22*10000),2)/l);
			dispTInv3=Mathf.Sqrt(Mathf.Pow(((1/dispTInv3)/22*10000),2)/l);
			dispT4=Mathf.Sqrt(Mathf.Pow((dispT4/22*10000),2)/l);
			dispTInv4=Mathf.Sqrt(Mathf.Pow(((1/dispTInv4)/22*10000),2)/l);
			dispE=Mathf.Sqrt(Mathf.Pow((dispE),2)/l);

		
			//str1.WriteLine("<"+middleK+">     <"+middleInvK+">");
			//str1.WriteLine("<"+(1/middleInvK)+">      <"+(1/middleInvK2)+">      <"+(middleK4)+">      <"+(exitFirst));
			//str1.WriteLine("Модель Mrc\tEr="+Er/kT+"kT\tsigma="+sigma+"kT\tE0=-"+M+" sigma*kT*sqrt(2)\tиспытаний "+this.gameObject.GetComponent<Transport> ().Stat);
			//str1.WriteLine("Модель Mrc\t Er="+Er+"kT\tN="+N+"\tsigma="+sigma+"kT\tE=-"+E+" квазиравновесие\t"+kvazline+"\tиспытаний "+GetComponent<Transport> ().Stat);
			str1.WriteLine("Модель MA\tN="+N+"\tsigma="+sigma+"kT\tE=-"+E+" квазиравновесие\t"+kvazline+"\tиспытаний "+GetComponent<Transport> ().Stat);
			//str1.WriteLine("Модель MA\tsigma="+sigma+"kT\tE0=-"+M+" sigma*kT*sqrt(2)\tиспытаний "+this.gameObject.GetComponent<Transport> ().Stat);
			str1.WriteLine("\t");
			str1.WriteLine("\t");
			str1.WriteLine("границы (0 : 0)\t");
			str1.WriteLine("число прыжков\t");
			str1.WriteLine("\t\t<"+middleKG+">");
			str1.WriteLine("прямое время\t");
			str1.WriteLine("\t\t<"+(middleK/22)*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispT)+"");
			str1.WriteLine("обратное время\t");
			str1.WriteLine("\t\t<"+(1/(middleInvK))/22*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispTInv)+"");
			str1.WriteLine("__________________________________________________");
			str1.WriteLine("\t");
			str1.WriteLine("границы (0 : 30)\t");
			str1.WriteLine("число прыжков\t");
			str1.WriteLine("\t\t<"+middleKG2+">");
			str1.WriteLine("прямое время\t");
			str1.WriteLine("\t\t<"+(middleK2/22)*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispT2)+"");
			str1.WriteLine("обратное время\t");
			str1.WriteLine("\t\t<"+(1/(middleInvK2))/22*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispTInv2)+"");
			str1.WriteLine("__________________________________________________");
			str1.WriteLine("\t");
			str1.WriteLine("границы (30 : 0)\t");
			str1.WriteLine("число прыжков\t");
			str1.WriteLine("\t\t<"+middleKG3+">");
			str1.WriteLine("прямое время\t");
			str1.WriteLine("\t\t<"+(middleK3/22)*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispT3)+"");
			str1.WriteLine("обратное время\t");
			str1.WriteLine("\t\t<"+(1/(middleInvK3))/22*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispTInv3)+"");
			str1.WriteLine("__________________________________________________");
			str1.WriteLine("\t");
			str1.WriteLine("границы (30 : 30)\t");
			str1.WriteLine("число прыжков\t");
			str1.WriteLine("\t\t<"+middleKG4+">");
			str1.WriteLine("прямое время\t");
			str1.WriteLine("\t\t<"+(middleK4/22)*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispT4)+"");
			str1.WriteLine("обратное время\t");
			str1.WriteLine("\t\t<"+(1/(middleInvK4))/22*10000+">");
			str1.WriteLine("\t\tДисперсия = "+(dispTInv4)+"");
			str1.WriteLine("__________________________________________________");
			str1.WriteLine("\t");
			str1.WriteLine("энергия\t");
			str1.WriteLine("\t\t<"+(midEnrg)+">");
			str1.WriteLine("\t\tДисперсия = "+(dispE)+"");
			str1.WriteLine("__________________________________________________");
			str1.WriteLine("\t");
			str1.WriteLine("эксперементов, где достигли края в 150\t");
			str1.WriteLine("\t\t"+borderErrors);
			str1.WriteLine("эксперементов, где прыжков более "+BackLimit+"\t");
			str1.WriteLine("\t\t"+limitErrors);
			str1.WriteLine("эксперементов, где возвращений более "+returnLimit+"\t");
			str1.WriteLine("\t\t"+backErrors);
			//str1.WriteLine("попыток освобождения <"+(middleK4)+">");
			//str1.WriteLine("% выхода с первой попытки "+(exitFirst/l*100)+"% ");
			//str1.WriteLine("средняя длина прыжков(все попытки)    <"+(midDist/midNumb)+">");
			//str1.WriteLine("средняя длина прыжков(те откуда ушел) <"+(middleK5)+">");
			//str1.WriteLine("средняя энергия прыжков(все попытки)    <"+(midEnrg/midNumb)+">");
			//str1.WriteLine("средняя энергия прыжков(те откуда ушел) <"+(middleK6)+">");
			//str1.WriteLine("<"+(middleK/22)+">      <"+(middleK2/22)+">      <"+(middleK4)+">      "+(exitFirst/l*100)+"%      <"+(midDist/midNumb)+">         <"+(midEnrg/midNumb)+">");
			str1.Close();
			//Debug.Log(borderErrors);

			Stat--;

		}

		if (Work){
			AbsolutTime+=Time.deltaTime;
			//while (X<1000+N+RightLim) {
			//while(X<25+5 && X>25-5 && Y<25+5 && Y>25-5){


			while(X<150+N+RightLim && X>=150-LeftLim-20 && Y<297 && Y>3 && Z<297 && Z>3){

				g++;
				CalcEnrg (X, Y, Z);


				for (i=0; i<=91; i++) {
					c=indexX[i] * indexX[i] + indexY[i] * indexY[i]+indexZ[i] * indexZ[i];
					if(Enrg[X+indexX[i],Y+indexY[i],Z+indexZ[i]]-E*e*r*indexX[i] <Enrg [X, Y, Z]){
						v [i] = Mathf.Exp (-10 * (Mathf.Sqrt (c)));
					} else {
						v [i] = Mathf.Exp (-10 * (Mathf.Sqrt (c))) * Mathf.Exp ((Enrg [X+indexX[i],Y+indexY[i],Z+indexZ[i]] - E * e * r * indexX[i] - Enrg [X,Y,Z]) / (-kT));

					}
					//Debug.Log(v[i]);
				}

				/*
				for (a=0; a<=91; a++) {
					c=indexX[a] * indexX[a] + indexY[a] * indexY[a]+ indexZ[a] * indexZ[a];
					delE=Enrg[X+indexX[a],Y+indexY[a],Z+indexZ[a]]-E*e*r*indexX[a] -Enrg [X, Y, Z];

					v [a] = Mathf.Exp (-10 * (Mathf.Sqrt (c)))*Mathf.Exp (-(Mathf.Pow((delE+Er),2))/(4*Er*kT));
					}
				*/

				/*if(X<53 || Y<7 || Y>193 || Z<7 || Z>193){
					borderError = true;}*/
			
				if(Y<=5){
					borderError = true;
					for (i=0; i<=91; i++) {
						if(indexY[i]<0){
							v[i]=0;}}}
				if(Y>=295){
					borderError = true;
					for (i=0; i<=91; i++) {
						if(indexY[i]>0){
							v[i]=0;}}}
				if(Z<=5){
					borderError = true;
					for (i=0; i<=91; i++) {
						if(indexZ[i]<0){
							v[i]=0;}}}
				if(Z>=295){
					borderError = true;
					for (i=0; i<=91; i++) {
						if(indexZ[i]>0){
							v[i]=0;}}}
				if(X<=150-LeftLim){
					//borderError = true;
					if(X<150){
						X = 290;
						limitError = true;
					}
					for (i=0; i<=91; i++) {
						if(indexX[i]<0){
							v[i]=0;}}}
				/*hh++;
				if(hh==20){
					hh=0;
					Debug.Log("ERROR  "+X+","+Y+"."+Z);
				}*/

				c = 0;
				for (i=0; i<=91; i++) {
					c += v [i];		//временная сумма всех темпов
				}
				tempSumm = c;
				//for (i=0; i<=119; i++) {
				for (i=0; i<=91; i++) {
					p [i] = v [i] / c;
				}

				//Debug.Log(i);
				if(i==92 || v[i]==0){
					d = Random.Range (0, 1f);
					i = 0;
					c = 0;
					while (c<=d) {
						c += p [i];
						i++;
					}
					i--;
					if(hh==15){
					//	Debug.Log(i);
					//	Debug.Log(p[i]);
					}
					if(i>91){i=92;}
				}



				T=((-Mathf.Log(Random.Range(0.00001f,1f)))/(tempSumm))/10000000;
				FulTime[X]+=T;
					
				if(saveTime1){
					g1++;
					stepTime1+=T;}
				if(X>=(150+N)){
					saveTime1=false;}
				if(X<=149 && saveTime1){
					stepTime1=0;}





				if(saveTime2 && !saveLast){
					g2++;
					stepTime2+=T;}
				if(X<=149 && saveTime2){
					stepTime2=0;}


				if(saveTime3){
					g3++;
					stepTime3+=T;}
				if(X>=(150+N)){
					saveTime3=false;}


				if(saveTime4 && !saveLast){
					g4++;
					stepTime4+=T;}


				if(saveLast){
					gLast++;
					tLast+=T;}


				if(X>=150+N && firstTimeLast){
					midEnrg = Enrg[X,Y,Z];
					saveTime2 = false;
					saveTime4 = false;
					saveLast=true;
					firstTimeLast = false;}
				if(X<150+N && saveLast){
					firstTimeLast = true;
					saveLast=false;
					stepTime2+=tLast;
					stepTime4+=tLast;
					g2+=gLast;
					g4+=gLast;
					tLast=0;
					gLast=0;
					saveTime2 = true;
					saveTime4 = true;
				}





					





			


				X += indexX [i];
				Y += indexY [i];
				Z += indexZ [i];
				jumpNumb++;
				//xCoord[jumpNumb]=X;
				//yCoord[jumpNumb]=Y;
				//zCoord[jumpNumb]=Z;
				//jumpTime[jumpNumb]=T;





				EnrgNumb [X, Y, Z]++;

				if(EnrgNumb [X, Y, Z]>returnLimit){
					backError = true;}
				
				limit--;

				if (limit < 0) {
					X = 290;
					limitError = true;}

			}
			if(tMain==0){
				exitFirst++;
			}
			tMain+=jumpF;
			KvazilineExist = false;
			K[l]=stepTime1;
			K2[l]=stepTime2;
			K3[l]=stepTime3;
			K4[l]=stepTime4;
			//Debug.Log(X+","+Y+"."+Z);
			//Debug.Log(borderErrors);
			//if(K[l]<0.0000001f){K[l]=0.0000001f;}
			//if(K2[l]<0.0000001f){K2[l]=0.0000001f;}
			//if(K3[l]<0.0000001f){K3[l]=0.0000001f;}
			//if(K4[l]<0.0000001f){K4[l]=0.0000001f;}
			KG[l]=g1;
			K2G[l]=g2;
			K3G[l]=g3;
			K4G[l]=g4;
			K5[l] = midEnrg;
			firstTimeLast = true;
			saveLast = false;
			stepTime1 = 0;
			stepTime2 = 0;
			stepTime3 = 0;
			stepTime4 = 0;
			tLast =0;
			saveTime1 = true;
			saveTime2 = true;
			saveTime3 = true;
			saveTime4 = true;
			g=0;
			g1=0;
			g2=0;
			g3=0;
			g4=0;
			StepTime =0;
			tMain =0;
			tLast =0;
			exitTry=1;
			midEnrg2=0;
			midEnrg=0;
			jumpFbool=true;
			if(borderError){
				borderErrors++;
				borderError=false;}
			l++;
			Stat--;
			if(backError){
				l--;
				Stat++;
				backErrors++;
				backError = false;}
			if(limitError){
				l--;
				Stat++;
				limitErrors++;
				limitError = false;}
			limit = BackLimit;
			//X = 1000 - LeftLim;
			X = 150;
			Y = 150;
			Z = 150;
			if(Stat!=0){
				jumpNumb=0;}
			for(a=0;a<305;a++){
				for (b=0;b<=300;b++){
					for(i=0;i<=300;i++){
						EnrgExist[a,b,i] = false;
						EnrgNumb[a,b,i] = 0;
					}
				}
			}
		}


	}

	void CalcEnrg(int xx, int yy, int zz){		//Заполнение ближайших уровней энергий, если они не заполнены
				
		if(!EnrgExist[xx,yy,zz]){
			Enrg[xx,yy,zz] = RndEnrg0();
			EnrgExist[xx,yy,zz] = true;
		}
		for (i=0; i<=91; i++) {
		//for (i=0; i<=25; i++) {
			//Debug.Log(xx+" "+yy+" "+zz);
			//Debug.Log(indexX[i]+" "+indexY[i]+" "+indexZ[i]);

			if(!EnrgExist[xx+indexX[i],yy+indexY[i],zz+indexZ[i]]){
				Enrg[xx+indexX[i],yy+indexY[i],zz+indexZ[i]] = RndEnrg0();
				EnrgExist[xx+indexX[i],yy+indexY[i],zz+indexZ[i]] = true;
			}}

		if (kvazline == 2) {
			if(KvazilineExist==false){
				KvazilineExist=true;
				Enrg[150,150,150]=RndEnrgFirst();
				
			}}

	}


	void MarkerClassic(){
		int nn = 0;
		for (int aa=-2; aa<=2; aa++) {
			for (int bb=-2; bb<=2; bb++) {
				for (int cc=-2; cc<=2; cc++) {
					if(aa!=0 && bb!=0 && cc!=0){
						indexX [nn] = aa;
						indexY [nn] = bb;
						indexZ [nn] = cc;
						nn++;
					}
				}
			}
		}
	
	
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
		
		
		/*for(i=0;i<=24;i++){
			indexX[i]=1;
		}
		Block (0);
		indexY [21] = 2;
		indexZ [21] = -2;
		indexY [22] = 2;
		indexZ [22] = 2;
		indexY [23] = -2;
		indexZ [23] = -2;
		indexY [24] = -2;
		indexZ [24] = 2;

		for(i=25;i<=45;i++){
			indexX[i]=2;
		}
		Block (25);
		indexX [46] = 3;
		indexY [46] = 0;
		indexZ [46] = 0;
		for(i=47;i<=72;i++){
			indexX[i]=-1;
		}
		BlockC (47);

		for(i=73;i<=97;i++){
			indexX[i]=-1;
		}
		Block (73);
		indexY [94] = 2;
		indexZ [94] = -2;
		indexY [95] = 2;
		indexZ [95] = 2;
		indexY [96] = -2;
		indexZ [96] = -2;
		indexY [97] = -2;
		indexZ [97] = 2;
		
		for(i=98;i<=118;i++){
			indexX[i]=-2;
		}
		Block (98);
		indexX [119] = -3;
		indexY [119] = 0;
		indexZ [119] = 0;
		*/
		StatFirst = Stat;
		
	}

	void Block(int u){
		indexY [0+u] = 1;
		indexZ [0+u] = 0;
		indexY [1+u] = 0;
		indexZ [1+u] = 0;
		indexY [2+u] = -1;
		indexZ [2+u] = 0;
		indexY [3+u] = 0;
		indexZ [3+u] = -1;
		indexY [4+u] = 0;
		indexZ [4+u] = 1;
		indexY [5+u] = 1;
		indexZ [5+u] = -1;
		indexY [6+u] = 1;
		indexZ [6+u] = 1;
		indexY [7+u] = -1;
		indexZ [7+u] = -1;
		indexY [8+u] = -1;
		indexZ [8+u] = 1;
		indexY [9+u] = 2;
		indexZ [9+u] = 0;
		indexY [10+u] = -2;
		indexZ [10+u] = 0;
		indexY [11+u] = 0;
		indexZ [11+u] = -2;
		indexY [12+u] = 0;
		indexZ [12+u] = 2;
		indexY [13+u] = 1;
		indexZ [13+u] = -2;
		indexY [14+u] = 1;
		indexZ [14+u] = 2;
		indexY [15+u] = -1;
		indexZ [15+u] = -2;
		indexY [16+u] = -1;
		indexZ [16+u] = 2;
		indexY [17+u] = 2;
		indexZ [17+u] = -1;
		indexY [18+u] = 2;
		indexZ [18+u] = 1;
		indexY [19+u] = -2;
		indexZ [19+u] = -1;
		indexY [20+u] = -2;
		indexZ [20+u] = 1;
		
	}
	
	void BlockC(int u){
		indexY [0+u] = 1;
		indexZ [0+u] = 0;
		indexY [1+u] = -1;
		indexZ [1+u] = 0;
		indexY [2+u] = 0;
		indexZ [2+u] = -1;
		indexY [3+u] = 0;
		indexZ [3+u] = 1;
		indexY [4+u] = 1;
		indexZ [4+u] = -1;
		indexY [5+u] = 1;
		indexZ [5+u] = 1;
		indexY [6+u] = -1;
		indexZ [6+u] = -1;
		indexY [7+u] = -1;
		indexZ [7+u] = 1;
		indexY [8+u] = 2;
		indexZ [8+u] = 0;
		indexY [9+u] = -2;
		indexZ [9+u] = 0;
		indexY [10+u] = 0;
		indexZ [10+u] = -2;
		indexY [11+u] = 0;
		indexZ [11+u] = 2;
		indexY [12+u] = 1;
		indexZ [12+u] = -2;
		indexY [13+u] = 1;
		indexZ [13+u] = 2;
		indexY [14+u] = -1;
		indexZ [14+u] = -2;
		indexY [15+u] = -1;
		indexZ [15+u] = 2;
		indexY [16+u] = 2;
		indexZ [16+u] = -1;
		indexY [17+u] = 2;
		indexZ [17+u] = 1;
		indexY [18+u] = -2;
		indexZ [18+u] = -1;
		indexY [19+u] = -2;
		indexZ [19+u] = 1;
		indexY [20+u] = 3;
		indexZ [20+u] = 0;
		indexY [21+u] = -3;
		indexZ [21+u] = 0;
		indexY [22+u] = 0;
		indexZ [22+u] = -3;
		indexY [23+u] = 0;
		indexZ [23+u] = 3;
		indexY [24+u] = 2;
		indexZ [24+u] = -2;
		indexY [25+u] = 2;
		indexZ [25+u] = 2;
		indexY [26+u] = -2;
		indexZ [26+u] = -2;
		indexY [27+u] = -2;
		indexZ [27+u] = 2;
	}
	/*void Marker(){
		
		indexX [0] = 1;
		indexY [0] = 0;
		indexZ [0] = 0;
		
		indexX [1] = 1;
		indexY [1] = 0;
		indexZ [1] = -1;
		
		indexX [2] = 1;
		indexY [2] = 0;
		indexZ [2] = 1;
		
		indexX [3] = 1;
		indexY [3] = 1;
		indexZ [3] = 0;
		
		indexX [4] = 1;
		indexY [4] = -1;
		indexZ [4] = 0;
		
		indexX [5] = 1;
		indexY [5] = 1;
		indexZ [5] = -1;
		
		indexX [6] = 1;
		indexY [6] = 1;
		indexZ [6] = 1;
		
		indexX [7] = 1;
		indexY [7] = -1;
		indexZ [7] = -1;
		
		indexX [8] = 1;
		indexY [8] = -1;
		indexZ [8] = 1;
		
		indexX [9] = 0;
		indexY [9] = 0;
		indexZ [9] = -1;
		
		indexX [10] = 0;
		indexY [10] = 0;
		indexZ [10] = 1;
		
		indexX [11] = 0;
		indexY [11] = 1;
		indexZ [11] = 0;
		
		indexX [12] = 0;
		indexY [12] = -1;
		indexZ [12] = 0;
		
		indexX [13] = 0;
		indexY [13] = 1;
		indexZ [13] = -1;
		
		indexX [14] = 0;
		indexY [14] = 1;
		indexZ [14] = 1;
		
		indexX [15] = 0;
		indexY [15] = -1;
		indexZ [15] = -1;
		
		indexX [16] = 0;
		indexY [16] = -1;
		indexZ [16] = 1;
		
		indexX [17] = -1;
		indexY [17] = 0;
		indexZ [17] = 0;
		
		indexX [18] = -1;
		indexY [18] = 0;
		indexZ [18] = -1;
		
		indexX [19] = -1;
		indexY [19] = 0;
		indexZ [19] = 1;
		
		indexX [20] = -1;
		indexY [20] = 1;
		indexZ [20] = 0;
		
		indexX [21] = -1;
		indexY [21] = -1;
		indexZ [21] = 0;
		
		indexX [22] = -1;
		indexY [22] = 1;
		indexZ [22] = -1;
		
		indexX [23] = -1;
		indexY [23] = 1;
		indexZ [23] = 1;
		
		indexX [24] = -1;
		indexY [24] = -1;
		indexZ [24] = -1;
		
		indexX [25] = -1;
		indexY [25] = 1;
		indexZ [25] = -1;
		
		
		/*for(i=0;i<=24;i++){
			indexX[i]=1;
		}
		Block (0);
		indexY [21] = 2;
		indexZ [21] = -2;
		indexY [22] = 2;
		indexZ [22] = 2;
		indexY [23] = -2;
		indexZ [23] = -2;
		indexY [24] = -2;
		indexZ [24] = 2;

		for(i=25;i<=45;i++){
			indexX[i]=2;
		}
		Block (25);
		indexX [46] = 3;
		indexY [46] = 0;
		indexZ [46] = 0;
		for(i=47;i<=72;i++){
			indexX[i]=-1;
		}
		BlockC (47);

		for(i=73;i<=97;i++){
			indexX[i]=-1;
		}
		Block (73);
		indexY [94] = 2;
		indexZ [94] = -2;
		indexY [95] = 2;
		indexZ [95] = 2;
		indexY [96] = -2;
		indexZ [96] = -2;
		indexY [97] = -2;
		indexZ [97] = 2;
		
		for(i=98;i<=118;i++){
			indexX[i]=-2;
		}
		Block (98);
		indexX [119] = -3;
		indexY [119] = 0;
		indexZ [119] = 0;
		*/
//		StatFirst = Stat;
		
//	}
//


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
		//if (min < -M*sigma*1.414f * kT) {
		//	min=RndEnrg1();
		//}

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

		//min = -M * kT * sigma*1.414f;
		return (min);
	}
}

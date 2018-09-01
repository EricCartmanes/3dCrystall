using UnityEngine;
using System.Collections;

 

public class GaussVSGauss : MonoBehaviour {


	float X=0.12f;
	float kT=0.025f;
	float MaxEnrg;
	float limitG = 0.000000001f;
	float p;
	int o;
	public Texture2D textureNew;
	private Color colorB = Color.blue;
	private Color colorR = Color.red;
	private Color colorO = new Color (1, 0.6f, 0, 1f);
	private Color colorG = Color.green;
	private Color color = Color.black;
	private Color colorDel = new Color (1, 1, 1, 0);
	private float[] Enrg2 = new float[10000];
	private float[] Enrg2Numb = new float[10000];
	private float[] Enrg2G = new float[201];
	private float Min;
	float z;
	float y;
	private float sigma = 3f;
	private float limitGEnrg = 6f;

	// Use this for initialization
	void Start () {
		MaxEnrg = limitGEnrg * sigma * kT;
		int y = 0;
		while (y < textureNew.height) {
			int x = 0;
			while (x < textureNew.width) {
				textureNew.SetPixel (x, y, colorDel);
				x++;
			}
			y++;
		}
		 y = 0;
		while (y < textureNew.height) {
			textureNew.SetPixel (1000, y, colorB);
			textureNew.SetPixel (y, 1400, colorB);
			textureNew.SetPixel (y, 1000, colorB);
			textureNew.SetPixel (1000+(int)(MaxEnrg*1000),y, colorB);
			textureNew.SetPixel (1000-(int)(MaxEnrg*1000),y, colorB);
			y++;
		}




		////////Debug.Log (MaxEnrg);

		//////Debug.Log (0.5f*(1+Erf(0.72f/(kT*1.414f))));
		/*
		for (int i=0; i<6000; i++) {
			float A = RndEnrg1();

		}*/

		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg1());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}
		
		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}
			
		}
		
		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
			//Debug.Log(Enrg2 [i+k]*10000);
			//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
			DrowPixelR ((int)((1000 + (Enrg2 [i]*1000))), 1000 + 200+ (int)(i/15));
			//}
		}

		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg1());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}
		
		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}
			
		}
		
		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
			//Debug.Log(Enrg2 [i+k]*10000);
			//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
			DrowPixelR ((int)((1000 - (Enrg2 [i]*1000))),200+  1000 - (int)(i/15));
			//}
		}

		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg3());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}

		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}

		}

		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
				//Debug.Log(Enrg2 [i+k]*10000);
				//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
				DrowPixel ((int)((1000 + (Enrg2 [i]*1000))), 1000 + 200+ (int)(i/15));
			//}
		}
		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg3());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}
		
		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}
			
		}
		
		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
			//Debug.Log(Enrg2 [i+k]*10000);
			//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
			DrowPixel ((int)((1000 - (Enrg2 [i]*1000))), 1000 + 200- (int)(i/15));
			//}
		}

		/*
		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg2());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}

		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}
			
		}

		
		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
			//Debug.Log(Enrg2 [i+k]*10000);
			//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
			DrowPixelG ((int)((1000 + (Enrg2 [i]*1000))),200+  1000 + (int)(i/15));
			//}
		}

	
		
		for (int i=0; i<3000; i++) {
			//float A = RndEnrg1();
			float B = Mathf.Abs(RndEnrg2());
			Enrg2[i]= B;
			//Enrg2Numb[i] = RndEnrg2NumbFun(B);
			//float C = RndEnrg3();
		}
		
		for(int i=0;i<2999;i++){
			Min = Enrg2[i];
			for(int k=(1+i);k<3000;k++){
				if(Min>Enrg2[k]){
					z=Enrg2[k];
					//y=(float)Enrg2Numb[k];
					Enrg2[k]=Enrg2[i];
					//Enrg2Numb[k] = Enrg2Numb[i];
					Enrg2[i]=Mathf.Abs(z);
					//Enrg2Numb[i]=y;
					Min = z;
				}
			}
			
		}
		
		for (int i=0; i<3000; i++) {
			//for (int k=0; k<=2; k++) {
			//Debug.Log(Enrg2 [i+k]*10000);
			//Debug.Log((int)(1000 + (Enrg2 [i+k]*10000)));
			DrowPixelG ((int)((1000 - (Enrg2 [i]*1000))),200+  1000 - (int)(i/15));
			//}
		}



		*/
















		textureNew.Apply ();

	}
	
	// Update is called once per frame
	void Update () {
	
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

	float RndEnrg2NumbFun(float l){
		float j;
		j= Mathf.Exp(-l*l/(2*kT*kT));
		j=j/2;
		if (l <= 0) {
			j=1-j;}



		return(j);
		
	}


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
		
	
		//DrowPixelR ((int)(1000 + min * 400),(int)( 1000 + j * 400));
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

	void DrowPixel(int x, int y){

			for (int a1 =-1; a1<=1; a1++) {
				for (int b1 =-1; b1<=1; b1++) {
					textureNew.SetPixel (x + a1, y + b1, color);
				}}

	}
	void DrowPixelR(int x, int y){
		
		for (int a1 =-1; a1<=1; a1++) {
			for (int b1 =-1; b1<=1; b1++) {
				textureNew.SetPixel (x + a1, y + b1, colorR);
			}}
		
	}
	void DrowPixelG(int x, int y){
		
		for (int a1 =-1; a1<=1; a1++) {
			for (int b1 =-1; b1<=1; b1++) {
				textureNew.SetPixel (x + a1, y + b1, colorG);
			}}
		
	}

}

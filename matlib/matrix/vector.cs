// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using static System.Math;
public partial class vector{

private double[] data;
public int size{ get{return data.Length;} }

public double this[int i]{
	get{
		if (i<0){return data[data.Length+i];} 
		return data[i];}
	set{if (i<0)
			data[data.Length+i]=value; 
		else 
			data[i]=value;}
}

public vector this[int i, int j]{
	get{
		if (i<0)i = data.Length+i; 
		if (j<0)j = data.Length+j;
		if (i>j) return new vector(0);
		vector r = new vector(j-i);
		for(int k = i;k<j;k++){
			r[k] = data[k];
		}
		return r;
	}
		
}

public vector(int n){data=new double[n];}
public vector(double a){data=new double[]{a};}
public vector(double[] a){data=a;}
public vector(double a, double b)
	{ data = new double[]{a,b}; }
public vector(double a, double b, double c)
	{ data = new double[]{a,b,c}; }
public vector(double a, double b, double c, double d)
	{ data = new double[]{a,b,c,d}; }

public static implicit operator vector (double[] a){ return new vector(a); }
public static implicit operator double[] (vector v){ return v.data; }

public void print(string s=""){
	System.Console.Write(s+"[");
	for(int i=0;i<size;i++) System.Console.Write("{0:f3} ",this[i]);
	System.Console.Write("]\n");

}

public static vector operator+(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]+u[i];
	return r; }

public static vector operator+(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]+a;
	return r; }

public static vector operator+(double a, vector v){
	return v+a; }


public static vector operator-(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]-u[i];
	return r; }

public static vector operator*(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=a*v[i];
	return r; }

public static vector operator*(double a, vector v){
	return v*a; }

public static vector operator*(vector v1, vector v2){
	if (v1.size != v2.size)
		throw new System.ArgumentException ($"Vectors must have equal length, but had length {v1.size} and {v2.size}");
	vector r=new vector(v1.size);
	for(int i=0;i<v1.size;i++)r[i]=v1[i]*v2[i];
	return r; }


public static vector operator/(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/a;
	return r; }

public static vector operator/(vector v1, vector v2){
	if (v1.size != v2.size)
		throw new System.ArgumentException ($"Vectors must have equal length, but had length {v1.size} and {v2.size}");
	vector r=new vector(v1.size);
	for(int i=0;i<v1.size;i++)r[i]=v1[i]/v2[i];
	return r; }

public double dot(vector o){
	double sum=0;
	for(int i=0;i<size;i++)sum+=this[i]*o[i];
	return sum;
	}

public double norm(){
	double meanabs=0;
	for(int i=0;i<size;i++)meanabs+=Abs(this[i]);
	if(meanabs==0)meanabs=1;
	meanabs/=size;
	double sum=0;
	for(int i=0;i<size;i++)sum+=(this[i]/meanabs)*(this[i]/meanabs);
	return meanabs*Sqrt(sum);
	}

public vector copy(){
	vector b = new vector(this.size);
	for(int i=0;i<this.size;i++)b[i]=this[i];
	return b;
}

public static bool approx(double x, double y, double acc=1e-9, double eps=1e-9){
	if(Abs(x-y)<acc)return true;
	if(Abs(x-y)/Max(Abs(x),Abs(y))<eps)return true;
	return false;
	}

public bool approx(vector o){
	for(int i=0;i<size;i++)
		if(!approx(this[i],o[i]))return false;
	return true;
	}


// Lukas kode

public static vector pow(vector v,double n){
	vector r = new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=Pow(v[i],n);
	return r; 
}

public static vector pow(vector v,int n){return pow(v,(double)n); }

public static vector sqrt(vector v){
	return pow(v,1.0/2);
}

public static vector abs(vector v){
	vector r = new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=Abs(v[i]);
	return r; 
}

public static double min(vector v){
	double min = v[0];
	for(int i=1;i<v.size;i++){
		if (v[i]<min)
			min = v[i];
	}
	return min; 
}
public static double max(vector v){
	double max = v[0];
	for(int i=1;i<v.size;i++){
		if (v[i]>max)
			max = v[i];
	}
	return max; 
}


public static vector linspace(double z0, double zend, int N){
	var z = new vector(N);
	for(int i=0;i<N;i++){
        z[i] = z0+(zend-z0)/(N-1)*i;
	}
	return z;
}

}//vector

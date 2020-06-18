using System;
using static System.Math;
using static System.Console;
using static vector;

public partial class quasi_random_mc{
// private static Random rand = new Random(); 
private static vector haltonx(int n, vector a,vector b){
    vector x =  new vector(a.size);
    vector haltonQrandom = halton(n,a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+haltonQrandom[i]*(b[i]-a[i]);
    return x;
}
private static vector latticex(int n, vector a,vector b){
    vector x =  new vector(a.size);
    vector latticeQrandom = lattice(n,a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+latticeQrandom[i]*(b[i]-a[i]);
    return x;
}

public static vector haltonmc(Func<vector,double> f, vector a,vector b,int N){
   double volume=1; 
   for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
   double sum1=0,sum2=0;
     int nhalf = (int) N/2;
   for(int i=0;i<nhalf;i++) sum1 +=f(haltonx(i+1,a,b));
   for(int i=nhalf;i<N;i++) sum2 +=f(haltonx(i+1,a,b));
   double mean = (sum1+sum2)/N;                // <f_i>
   double err = Abs(sum1/nhalf-sum2/(N-nhalf));          //Error estimate
   return new vector(mean*volume, err*volume); //Vector with integral and error estimate
}
public static vector latticemc(Func<vector,double> f, vector a,vector b,int N){
   double volume=1; 
   for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
   double sum1=0,sum2=0;
    int nhalf = (int) N/2;
   for(int i=0;i<nhalf;i++) sum1 +=f(latticex(i+1,a,b));
   for(int i=nhalf;i<N;i++) sum2 +=f(latticex(i+1,a,b));
   double mean = (sum1+sum2)/N;                // <f_i>
   double err = Abs(sum1/nhalf-sum2/(N-nhalf));          //Error estimate
   return new vector(mean*volume, err*volume); //Vector with integral and error estimate
}

public static vector combinedmc(Func<vector,double> f, vector a,vector b,int N){
   double volume=1; 
   for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
   double sum1=0,sum2=0;
   int nhalf = (int) N/2;
   for(int i=0;i<nhalf;i++) sum1 +=f(latticex(i+1,a,b));
   for(int i=nhalf;i<N;i++) sum2 +=f(haltonx(i+1,a,b));
   double mean = (sum1+sum2)/N;                // <f_i>
   double err = Abs(sum1/nhalf-sum2/(N-nhalf));          //Error estimate
   return new vector(mean*volume, err*volume); //Vector with integral and error estimate
}



public static double corput(int n,int baseVal){
    double bk = 1;
    double q = 0;
    while(n>0){
        bk /=baseVal;
        double dk = (n%baseVal);
        q+=dk*bk;
        n /=baseVal;
    }
    return q;
}

public static vector halton(int n,int dim){
    int[] baseVals = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
    int maxdim = baseVals.Length;
    if (maxdim<dim) {
			throw new System.ArgumentException ($"dim = {dim} larger than maxdim = {maxdim}","dim");
	}
    vector x = new vector(dim);
    for(int i = 0;i<dim;i++){
        x[i] = corput(n,baseVals[i]);
    }
    return x;
}


private static double frac(double x){ return x-Floor(x);}
public static vector lattice(int n, int dim) {
    double pi = 3.1415926535897932384626433832795028841971693993751;
    // WriteLine("pi {0}",pi);
    double alpha;
    vector x = new vector(dim);
    for(int i=0;i<dim;i++){
        alpha = Sqrt(pi+i);
        // WriteLine("alpha {0}",alpha);

        x[i] = frac(n*alpha);
    }
    return x;
}
}
using System;
using static System.Math;
using static System.Console;
using static vector;

public partial class quasi_random_mc{

//Pic the nth points i the haltons sequences, and place it corresponding to a and b in multiple dimensions. 

private static vector haltonx(int n, vector a,vector b){
    vector x =  new vector(a.size);
    vector haltonQrandom = halton(n,a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+haltonQrandom[i]*(b[i]-a[i]);
    return x;
}

//Pic the nth points i the lattice sequences, and place it corresponding to a and b in multiple dimensions. 
private static vector latticex(int n, vector a,vector b){
    vector x =  new vector(a.size);
    vector latticeQrandom = lattice(n,a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+latticeQrandom[i]*(b[i]-a[i]);
    return x;
}

// Integrator using Halton sequences
public static vector haltonmc(Func<vector,double> f, vector a,vector b,int N){
    double volume=1; 
    for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
    double sum1=0,sum2=0;
    int nhalf = (int) N/2;
    // Sum function values with two different sequences each of size 1/2N to allow for error estimate 
    for(int i=0;i<nhalf;i++) sum1 +=f(haltonx(i+1,a,b));
    for(int i=nhalf;i<N;i++) sum2 +=f(haltonx(i+1,a,b));
    double mean = (sum1+sum2)/N;                 // Mean function value
    double err = Abs(sum1/nhalf-sum2/(N-nhalf)); // Unscaled error estimate
    return new vector(mean*volume, err*volume);  //Vector with integral and error estimate
}

// Integrator using lattice sequences
public static vector latticemc(Func<vector,double> f, vector a,vector b,int N){
    double volume=1; 
    for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
    double sum1=0,sum2=0;
    int nhalf = (int) N/2;
    // Sum function values with two different sequences each of size 1/2N to allow for error estimate 
    for(int i=0;i<nhalf;i++) sum1 +=f(latticex(i+1,a,b)); 
    for(int i=nhalf;i<N;i++) sum2 +=f(latticex(i+1,a,b));  
    double mean = (sum1+sum2)/N;                 // Mean function value
    double err = Abs(sum1/nhalf-sum2/(N-nhalf)); // Unscaled error estimate
    return new vector(mean*volume, err*volume); //Vector with integral and error estimate
}

// Calculate Corput number given n and base
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

// Calculate Corput numbers in different bases corresponding to the number of dimensions, given n
public static vector halton(int n,int dim){
    int[] baseVals = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
    int maxdim = baseVals.Length;
    if (maxdim<dim) {
			throw new System.ArgumentException ($"dim = {dim} larger than maxdim = {maxdim}","dim");
	}
    vector x = new vector(dim);
    // Calculate Corput number for each dimension
    for(int i = 0;i<dim;i++){
        x[i] = corput(n,baseVals[i]);
    }
    return x;
}

//Define frac (number above decimal)
private static double frac(double x){ return x-Floor(x);}

// Create Lattice number given n and dimensions
public static vector lattice(int n, int dim) {
    double alpha;
    vector x = new vector(dim);
    for(int i=0;i<dim;i++){
        alpha = Sqrt(PI+i);     //Create new irrational number for this dimension
        x[i] = frac(n*alpha);   //Get number n in sequence
    }
    return x;
}
}
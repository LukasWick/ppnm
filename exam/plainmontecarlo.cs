using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using static vector;

public partial class montecarlo{

//Pic a random point between a and b in multiple dimensions. 
private static vector randomx(vector a,vector b,Random rnd){
    vector x = new vector(a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+rnd.NextDouble()*(b[i]-a[i]);
    return x;
}

//Integrate with plain phsudo random integrator
public static vector plainmc(Func<vector,double> f, vector a,vector b,int N,Random rnd){
   double volume=1; for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
   double sum=0,sum2=0;
   for(var i=0;i<N;i++){double fx=f(randomx(a,b,rnd)); sum += fx; sum2 += fx*fx;}
   double mean = sum/N;                           // <f_i>
   double sigma = Sqrt(sum2/N - mean*mean);       // sigma² = <(f_i)²> - <f_i>²
   double SIGMA = sigma/Sqrt(N);                  // SIGMA² = <Q²> - <Q>²
   return new vector(mean*volume, SIGMA*volume);  //Vector with integral and uncertanty
}

}
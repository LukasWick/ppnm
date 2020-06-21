using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using static vector;

public partial class montecarlo{
private static Random rand = new Random(); 
private static vector randomx(vector a,vector b){
    vector x = new vector(a.size);
    for(int i=0; i<a.size;i++) x[i] = a[i]+rand.NextDouble()*(b[i]-a[i]);
    return x;
}
public static vector plainmc(Func<vector,double> f, vector a,vector b,int N){
   double volume=1; for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
   double sum=0,sum2=0;
   for(var i=0;i<N;i++){double fx=f(randomx(a,b)); sum += fx; sum2 += fx*fx;}
   double mean = sum/N;                             // <f_i>
   double sigma = Sqrt(sum2/N - mean*mean);    // sigma² = <(f_i)²> - <f_i>²
   double SIGMA = sigma/Sqrt(N);               // SIGMA² = <Q²> - <Q>²
   return new vector(mean*volume, Abs(SIGMA*volume)); //Vector with integral and uncertanty
}

//Calls stratta which calls itself continuesly
public static vector stratifiedmc(Func<vector,double> f, vector a,vector b, double acc =1e-3, double eps =1e-3, int N=-1){
    if (N<0) {N=64*a.size;};
    double volume=1; for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
    vector fxs = new vector(N);
    for(var i=0;i<N;i++){
        fxs[i]=f(randomx(a,b));
    }  
    vector oldstat = stats(fxs);
    vector res = strata(f,a,b,acc,eps,N,oldstat,volume);

    return  new vector(res[0],res[1]);
}
//Integrates by calling itself continuesly
private static vector strata(Func<vector,double> f, vector a,vector b, double acc, double eps, int N, vector oldstat, double V){
    vector fxs = new vector(N);
    matrix xs = new matrix(a.size,N);
    for(int i=0;i<N;i++){
        xs[i] = randomx(a,b);
        fxs[i]=f(xs[i]);
    }  
    vector stat = stats(fxs);
    double oldN = oldstat[2];
    double integ =V*(stat[0]*N+oldstat[0]*oldN)/(N+oldN);
    double error = Abs(V*Sqrt(stat[0]*N+oldstat[0]*oldN)/(N+oldN));


    if (error<acc+eps*Abs(integ)){
        return new vector(integ,error,N+oldN);
    }
    else{
        double varmax = -1;
        int kmax = 0;
        vector oldstatl =new vector(3),oldstatr=new vector(3);
        vector statsl,statsr;
        for(int k=0;k<a.size;k++){
            List<double> fxsl = new List<double>();
            List<double> fxsr = new List<double>();
            for(int i=0;i<N;i++){
                if (xs[k,i] <(a[k]+b[k])/2) {fxsl.Add(fxs[i]);}
                else {fxsr.Add(fxs[i]);}
            }
            statsl = stats(fxsl);
            statsr = stats(fxsr);
            double v = Abs(statsl[0]-statsr[0]);
            if(varmax < v){
                varmax = v;
                kmax = k;
                oldstatl = statsl;
                oldstatr = statsr;
            }
        }
        vector a2 = a.copy();
        a2[kmax] = (a[kmax]+b[kmax])/2.0;
        vector b2 = b.copy();
        b2[kmax] = (a[kmax]+b[kmax])/2.0;

        vector resl = strata(f, a,b2, acc/Sqrt(2), eps, N, oldstatl, V/2);
        vector resr = strata(f, a2,b, acc/Sqrt(2), eps, N, oldstatr, V/2);
        integ = resl[0]+resr[0];
        error = Sqrt(resl[1]*resl[1]+resr[1]*resr[1]);
        return new vector(integ,error);
    }
}


public static vector stats(vector fxs){
    int n = fxs.size;
    double sum = 0;
    double sum2 = 0;
    for(int i=0;i<n;i++){
        sum +=fxs[i];
        sum2 +=fxs[i]*fxs[i];
    }
    double mean = sum/n;                     // <f_i>
    double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
    return new vector(mean,variance,n);
}
public static vector stats(List<double> fxs){
    int n = fxs.Count;
    double sum = 0;
    double sum2 = 0;
    for(int i=0;i<n;i++){
        sum +=fxs[i];
        sum2 +=fxs[i]*fxs[i];
    }
    double mean = sum/n;                     // <f_i>
    double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
    return new vector(mean,variance,n);
}


}
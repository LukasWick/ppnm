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
   return new vector(mean*volume, SIGMA*volume); //Vector with integral and uncertanty
}


// public static vector stratifiedmc(Func<vector,double> f, vector a,vector b, double acc =1e-2, double eps =1e-2, int N=100){
//     double volume=1; for(int i=0; i<a.size;i++) volume*=b[i]-a[i];
//     vector fxs = new vector(N);
//     for(var i=0;i<N;i++){
//         fxs[i]=f(randomx(a,b));
//     }  
//     vector oldstat = stats(fxs);
//     vector res = strata(f,a,b,acc,eps,N,oldstat,volume);

//     return  new vector(res[0],res[1]);
// }

// private static vector strata(Func<vector,double> f, vector a,vector b, double acc, double eps, int N, vector oldstat, double V){
//     vector fxs = new vector(N);
//     matrix xs = new matrix(a.size,N);
//     for(int i=0;i<N;i++){
//         xs[i] = randomx(a,b);
//         fxs[i]=f(xs[i]);
//     }  
//     vector stat = stats(fxs);
    
//     double integ =V*(stat[0]*N+oldstat[0]*oldstat[2])/(N+oldstat[2]);
//     double error = V*Sqrt(stat[0]*stat[2]+oldstat[0]*oldstat[2])/(N+oldstat[2]);
//     // WriteLine($"error = {error}");
//     // WriteLine($"tol = {acc+eps*Abs(integ)}");

//     if (error<acc+eps*Abs(integ)){
//         return new vector(integ,error,N+oldstat[2]);
//     }
//     else{
//         double varmax = -1;
//         int kmax = 0;
//         vector oldstatl =new vector(3),oldstatr=new vector(3);
//         vector statsl,statsr;
//         for(int k=0;k<a.size;k++){
//             List<double> fxsl = new List<double>();
//             List<double> fxsr = new List<double>();
//             for(int i=0;i<N;i++){
//                 if (xs[k,i] <(a[k]+b[k])/2) {fxsl.Add(fxs[i]);}
//                 else {fxsr.Add(fxs[i]);}
//             }
//             statsl = stats(fxsl);
//             statsr = stats(fxsr);
//             double v = Abs(statsl[0]-statsr[0]);
//             if(varmax < v){
//                 varmax = v;
//                 kmax = k;
//                 oldstatl = statsl;
//                 oldstatr = statsr;
//             }
//         }
//         vector a2 = a.copy();
//         a2[kmax] = (a[kmax]+b[kmax])/2.0;
//         vector b2 = b.copy();
//         b2[kmax] = (a[kmax]+b[kmax])/2.0;

//         vector resl = strata(f, a,b2, acc/Sqrt(2), eps, N, oldstatl, V/2);
//         vector resr = strata(f, a2,b, acc/Sqrt(2), eps, N, oldstatr, V/2);
//         return new vector(resl[0]+resr[0],Sqrt(resl[1]*resl[1]+resr[1]*resr[1]));
//     }
// }


// public static vector stats(vector fxs){
//     int n = fxs.size;
//     double sum = 0;
//     double sum2 = 0;
//     for(int i=0;i<n;i++){
//         sum +=fxs[i];
//         sum2 +=fxs[i]*fxs[i];
//     }
//     double mean = sum/n;                     // <f_i>
//     double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
//     return new vector(mean,variance,n);
// }
// public static vector stats(List<double> fxs){
//     int n = fxs.Count;
//     double sum = 0;
//     double sum2 = 0;
//     for(int i=0;i<n;i++){
//         sum +=fxs[i];
//         sum2 +=fxs[i]*fxs[i];
//     }
//     double mean = sum/n;                     // <f_i>
//     double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
//     return new vector(mean,variance,n);
// }





public static vector stratifiedmc(Func<vector,double> f, vector a,vector b, double acc =1e-2, double eps =1e-2, int N=42){
    double V=1; for(int i=0; i<a.size;i++) V*=b[i]-a[i];
    vector fxs = new vector(N);
    for(var i=0;i<N;i++){
        fxs[i]=f(randomx(a,b));
    }  
    vector oldstat = stats(fxs);
    vector res = strata(f,a,b,acc,eps,N,oldstat,V);

    int Ncalls = (int) (res[2]+oldstat[2]);
    double Vfxsum = (res[2]*res[0]+V/2*oldstat[0])/(Ncalls);
    double Vfxsum2 = (res[2]*res[2]*res[1]+V*V*oldstat[1])/(Ncalls*Ncalls);
    double Vsigma = Sqrt(Vfxsum2*Ncalls-Vfxsum*Vfxsum);
    double error = Vsigma/Sqrt(Ncalls);
    double integ = Vfxsum;

    return  new vector(integ,error,Ncalls);
}



private static vector strata(Func<vector,double> f, vector a,vector b, double acc, double eps, int N, vector oldstat, double V){
    vector fxs = new vector(N);
    matrix xs = new matrix(a.size,N);
    for(int i=0;i<N;i++){
        xs[i] = randomx(a,b);
        fxs[i]=f(xs[i]);
    }  
    vector stat = stats(fxs);
    
    // double integ =V*(stat[0]*N+oldstat[0]*oldstat[2])/(N+oldstat[2]);
    // // double error =V*Sqrt((stat[1]*N+oldstat[1]*oldstat[2]))/(N+oldstat[2]);
    // double variance = (stat[1]*(N-1)+oldstat[1]*(oldstat[2]-1))/(N+oldstat[2]-2);
    // double error = V*Sqrt(variance)/(N+oldstat[2]);

    int Ncalls = (int) (stat[2]+oldstat[2]);
    // double Vfxsum  = *(stat[0]+oldstat[0]);
    // double Vfxsquaresum = stat[1]+oldstat[1];
    //  double mean = fxsum/Ncalls;                     // <f_i>
    // double variance = fxsquaresum/Ncalls - mean*mean;
    // double error = V*Sqrt(variance)/(Ncalls);
    // double integ = mean*V;

    
    double Vfxsum = (V*oldstat[0]+V*stat[0])/Ncalls;
    double Vfxsum2 = (V*V*oldstat[1]+V*V*stat[1])/Pow(Ncalls,2);
    // WriteLine($"Vfxsum2={Vfxsum2}");
    // WriteLine($"Vfxsum^2={Vfxsum}");
    
    double Vsigma = Sqrt(Vfxsum2*Ncalls-Vfxsum*Vfxsum);
    
    double error = Vsigma/Sqrt(Ncalls);
    double integ = Vfxsum;
    
    // WriteLine($"iteg = {integ}");
    // WriteLine($"error = {error}");
    // WriteLine($"eps*int = {eps*Abs(integ)}");
    // WriteLine($"acc = {acc}");

    if (error<acc+eps*Abs(integ)){
        // return new vector(integ,variance,N+oldstat[2]);
        return new vector(V*stat[0]/stat[2] ,V*V*stat[1]/(stat[2]*stat[2]),stat[2]);
    }
    else{
        double vmax = -1;
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
            if(vmax < v){
                vmax = v;
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
        // integ = (V*(oldstat[0]*oldstat[2])+(resl[0]*resl[2]+resr[0]*resr[2]))/Ncalls;
        // variance = (oldstat[1]*oldstat[2]+resl[1]*resl[2]+resr[1]*resr[2])/(Ncalls-3);
        // Ncalls = (int) (resl[2]+resr[2]+oldstat[2]);
        // fxsum  = resl[0]+resr[0]+oldstat[0];
        // fxsquaresum = resl[1]+resr[1]+oldstat[1];
        double Nl =  (resl[2]+oldstatl[2]);
        double Nr = (resr[2]+oldstatr[2]);
        
        Vfxsum = (resl[2]*resl[0]+V/2*oldstatl[0])/(Nl)+(resr[2]*resr[0]+V/2*oldstatr[0])/(Nr);
        Vfxsum2 = (resl[2]*resl[2]*resl[1]+V*V/4*oldstatl[1])/(Nl*Nl)+(resl[2]*resl[2]*resl[1]+V*V/4*oldstatl[1])/(Nl*Nl);
        
        return new vector(Vfxsum,Vfxsum2,Nl+Nr);
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
    // double mean = sum/n;                     // <f_i>
    // double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
    return new vector(sum,sum2,n);
}
public static vector stats(List<double> fxs){
    int n = fxs.Count;
    double sum = 0;
    double sum2 = 0;
    for(int i=0;i<n;i++){
        sum +=fxs[i];
        sum2 +=fxs[i]*fxs[i];
    }
    // double mean = sum/n;                     // <f_i>
    // double variance = sum2/n - mean*mean;    // sigma² = <(f_i)²> - <f_i>²
    return new vector(sum,sum2,n);
}

}
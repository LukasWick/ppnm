using static System.Console;
using static System.Math;
using System;
using static montecarlo;
using static quasi_random_mc;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){


    // //Sphere one or zero
    // double r = Pow(3.0/2,1.0/3);
    // Func<vector,double> f = (x) => {
    //     if(x.dot(x)<=r*r) return 1;
    //     else return 0;
    // };

    // vector a = new vector(-1.5,-1.5,-1.5);
    // vector b = new vector(1.5,1.5,1.5);
    // double exact = 2*PI;
    // System.IO.StreamWriter outputfile0 = new System.IO.StreamWriter("out.plot.PHL.SphereXYZ.data",append:false);
    // for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
    //     vector resP = plainmc(f,a,b,N,new Random(1));
    //     double errorP = Abs(resP[0]-exact);
    //     vector resH = haltonmc(f,a,b,N);
    //     double errorH = Abs(resH[0]-exact);
    //     vector resL = latticemc(f,a,b,N);
    //     double errorL = Abs(resL[0]-exact);        
    //     outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    // }
    // outputfile0.Close();

    //Circle one or zero
    double r = 1;
    Func<vector,double> f = (x) => {
        if(x.dot(x)<=r*r) return 1;
        else return 0;
    };

    vector a = new vector(-1.1,-1.1);
    vector b = new vector(1.1,1.1);
    double exact = PI;
    System.IO.StreamWriter outputfile0 = new System.IO.StreamWriter("out.plot.PHL.Circle.data",append:false);
    for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
        vector resP = plainmc(f,a,b,N,new Random());
        double errorP = Abs(resP[0]-exact);
        vector resH = haltonmc(f,a,b,N);
        double errorH = Abs(resH[0]-exact);
        vector resL = latticemc(f,a,b,N);
        double errorL = Abs(resL[0]-exact);        
        outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    }
    outputfile0.Close();


    // Half sphere Z given from x,y
    r = Pow(3.0/2,1.0/3);
    f = (x) => {
        if(x.dot(x)<=r*r) return Sqrt(r*r-x.dot(x));
        else return 0;
    };

    a = new vector(-1.5,-1.5);
    b = new vector(1.5,1.5);
    exact = PI;
    outputfile0 = new System.IO.StreamWriter("out.plot.PHL.Sphere.data",append:false);
    for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
        vector resP = plainmc(f,a,b,N,new Random());
        double errorP = Abs(resP[0]-exact);
        vector resH = haltonmc(f,a,b,N);
        double errorH = Abs(resH[0]-exact);
        vector resL = latticemc(f,a,b,N);
        double errorL = Abs(resL[0]-exact);        
        outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    }
    outputfile0.Close();


    // // 1/xyz 
    // f = (x) => {
    //     return 1/(Sqrt(x[0]*x[1]*x[2]));
    // };

    // a = new vector(0,0,0);
    // b = new vector(1,1,1);
    // exact = 8;
    // outputfile0 = new System.IO.StreamWriter("out.plot.PHL.1xyz.data",append:false);
    // for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
    //     vector resP = plainmc(f,a,b,N,new Random(1));
    //     double errorP = Abs(resP[0]-exact);
    //     vector resH = haltonmc(f,a,b,N);
    //     double errorH = Abs(resH[0]-exact);
    //     vector resL = latticemc(f,a,b,N);
    //     double errorL = Abs(resL[0]-exact);        
    //     outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    // }
    // outputfile0.Close();


    // Hard integral
    f = (x) => {return 1.0/(PI*PI*PI)/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};
    exact = 1.3932039296856768591842462603255;
    a = new vector(0,0,0);
    b = new vector(PI,PI,PI);
    outputfile0 = new System.IO.StreamWriter("out.plot.PHL.InverseCos.data",append:false);
    for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
        vector resP = plainmc(f,a,b,N,new Random());
        double errorP = Abs(resP[0]-exact);
        vector resH = haltonmc(f,a,b,N);
        double errorH = Abs(resH[0]-exact);
        vector resL = latticemc(f,a,b,N);
        double errorL = Abs(resL[0]-exact);        
        outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    }
    outputfile0.Close();

    // sin
    f = (x) => {return Sin(x[0])*Sin(x[1])*Sin(x[2]);};
    exact = 8;
    a = new vector(0,0,0);
    b = new vector(PI,PI,PI);
    outputfile0 = new System.IO.StreamWriter("out.plot.PHL.SimpleSin.data",append:false);
    for(int N =(int)10; N<(int) 2e6; N=(int) (1.25*N)){        
        vector resP = plainmc(f,a,b,N,new Random());
        double errorP = Abs(resP[0]-exact);
        vector resH = haltonmc(f,a,b,N);
        double errorH = Abs(resH[0]-exact);
        vector resL = latticemc(f,a,b,N);
        double errorL = Abs(resL[0]-exact);        
        outputfile0.WriteLine($"{N} {1/Sqrt(N)} {resP[1]} {errorP}  {resH[1]} {errorH} {resL[1]} {errorL}");
    }
    outputfile0.Close();

}
}
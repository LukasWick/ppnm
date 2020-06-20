using static System.Console;
using static System.Math;
using System;
using static ode_integrator;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    
    WriteLine("\n\nQuestion A:");
    WriteLine("Implenetation of ODE45");
   
    Func<double,vector,vector> f = (X,Y)=> new vector(Y[1],-Y[0]);


    int n = 20;
    double a = 0.0;
    vector y0 = new vector(0,1);
    double b = 2*PI;
    double h = (b-a)/(n*10.0);
    double acc = 0.001;
    double eps =  0.0001;

    vector res = driver(f,a,y0,b,h,acc,eps);
    double exact = Sin(b);
    WriteLine("Solve u''=-u: sin");
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",exact);
    WriteLine("Expected error         : {0}",Abs(exact*eps)+acc);
    WriteLine("Actual error           : {0}",Abs(exact-res[0]));

    Func<double,vector,vector> g = (X,Y)=> new vector(Y[0]);
    vector y0exp = new vector(1.0);
    res = driver(g,a,y0exp,b,h,acc,eps);
    exact = Exp(b);
    WriteLine("Solve u'=u: exp");
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",exact);
    WriteLine("Expected error         : {0}",Abs(exact*eps)+acc);
    WriteLine("Actual error           : {0}",Abs(exact-res[0]));

    System.IO.StreamWriter outputfile1 = new System.IO.StreamWriter("out.plotA1.data",append:false);
    System.IO.StreamWriter outputfile2 = new System.IO.StreamWriter("out.plotA2.data",append:false);
    System.IO.StreamWriter outputfile3 = new System.IO.StreamWriter("out.plotA3.data",append:false);


    // Integrate from y(n) to y(n+1) from y(0) to y(N)
    vector t = linspace(a,b,n);
    vector y = y0.copy();
    outputfile1.WriteLine("{0} {1} {2}",t[0],y[0],y0[1]);
    for(int i = 1; i<t.size;i++){
        y = ode_integrator.driver(f,t[i-1],y,t[i],h,acc,eps);
        outputfile1.WriteLine("{0} {1} {2}",t[i],y[0],y[1]);
    }


    // Plot 2 
    List<double> ts=new List<double>();
    List<vector> ys=new List<vector>();
    driver(f,a,y0,b,ts,ys,h,acc,eps);
    for(int i=0;i<ts.Count;i++){
        outputfile2.WriteLine("{0} {1} {2}",ts[i],ys[i][0],ys[i][1]);
    }
    outputfile2.Close();

    // Plot 3
    matrix yres = ode_integrator.driver(f, t, y, h, acc, eps);
    for(int i = 0; i<t.size;i++){
        outputfile3.WriteLine("{0} {1} {2}",t[i],yres[i][0],yres[i][1]);
    }
    

    outputfile3.Close();

    WriteLine("Three ways of getting y values:");
    WriteLine("PlotA1.svg: Integrate from y(n) to y(n+1) from y(0) to y(N)"); 
    WriteLine("PlotA2.svg: The points found during integration");
    WriteLine("PlotA3.svg: Interplolate yvalues from points found during integration");
    WriteLine("\n\n");

}
}

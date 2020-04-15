using static System.Console;
using static System.Math;
using System;
using static ode_integrator;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    Func<double,vector,vector> f = (X,Y)=> new vector(Y[1],-Y[0]);

    System.IO.StreamWriter outputfile2 = new System.IO.StreamWriter("out.plotA2.txt",append:false);
    

    List<double> ts=new List<double>();
    List<vector> ys=new List<vector>();
    ode_integrator.driver(f,0.0,new vector(0,1),2*PI,0.1,0.01,0.01,ts,ys);
    // List<double> ts = res.Item1;
    // List<vector> ys = res.Item2;

    
    for(int i=0;i<ts.Count;i++){
        outputfile2.WriteLine("{0} {1} {2}",ts[i],ys[i][0],ys[i][1]);
    }
    outputfile2.Close();


    int n = 20;
    vector t = linspace(0,2*PI,n);
    vector y = new vector(0,1);
    double h = (t[-1]-t[0])/(n*10.0);
    double acc = 0.01;
    double eps = 0.01;
    WriteLine("{0} {1} {2}",t[0],y[0],Sin(t[0]),y[1]);
    for(int i = 1; i<t.size;i++){
        y = ode_integrator.driver(f,t[i-1],y,t[i],h,acc,0.01);
        WriteLine("{0} {1} {2}",t[i],y[0],y[1]);
    }

    System.IO.StreamWriter outputfile3 = new System.IO.StreamWriter("out.plotA3.txt",append:false);

    matrix yres = ode_integrator.driver(f, t, y, h, acc, eps);
    yres.print("yres");
    for(int i = 0; i<t.size;i++){
        outputfile3.WriteLine("{0} {1} {2}",t[i],yres[i][0],yres[i][1]);
    }

    outputfile3.Close();

}
}

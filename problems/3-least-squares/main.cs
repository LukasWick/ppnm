using static System.Console;
using static System.Math;
using System;
using static least_squares;

class main{
static void Main(){
       
    var t = new vector(new double[]{1,2,3,4,6,9,10,13,15});
    var y = new vector(new double[]{117 ,100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1});
    // var dy = new vector(new double[]{1 ,1, 1, 1, 1,0.1, 0.1, 0.1, 0.1});
    var dy = y/20;
    Func<double,double>[] f = {(x)=>1,(x)=>-x};

    var logy = new vector(y.size);
    var logdy = new vector(dy.size);
    for(int i =0; i<y.size; i++){
        logy[i] = Log(y[i]);
        logdy[i] = dy[i]/y[i];
    }

    least_squares  fit = new least_squares(t, logy, logdy,  f);
    vector c = fit.c;
    vector Delta_c = fit.Delta_c;

    double a = Exp(c[0]);
    double lambda = c[1];
    
    double Delta_a = Exp(Delta_c[0]);
    double Delta_lambda = Delta_c[1];

    System.IO.StreamWriter  outA = new System.IO.StreamWriter("outB.txt",append:false);
    outA.WriteLine("Fit: a = {0} +-{1}, og lambda = {2}+-{3} 1/d",a,Delta_a,lambda,Delta_lambda);
    outA.WriteLine("Falf life time: {0} +- {1} d",Log(2)/lambda,Delta_lambda*Log(2)/(lambda*lambda));
    outA.WriteLine("Table value: 3.6319(23) d");
    outA.Close();


    matrix sigma = fit.sigma;
    

    System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out.tydata.txt",append:false);
        for(int i=0;i<y.size;i++){
            outputfile.WriteLine("{0} {1} {2}",t[i],y[i],dy[i]);
        }
   	outputfile.Close();


    vector ts = vector.linspace(t[0],t[-1],200);
    double yfit,delta_y_plus_c0,delta_y_plus_c1,delta_y_minus_c0,delta_y_minus_c1;

    System.IO.StreamWriter outputfile_fitA = new System.IO.StreamWriter("out.plotA.txt",append:false);
    
    for(int i=0;i<ts.size;i++){
            yfit = a*Exp(-lambda*ts[i]);
            
            outputfile_fitA.WriteLine("{0} {1}",ts[i],yfit);
    }
    outputfile_fitA.Close();


    System.IO.StreamWriter outputfile_fitC = new System.IO.StreamWriter("out.plotC.txt",append:false);
    for(int i=0;i<ts.size;i++){
            yfit = Exp(c[0]-c[1]*ts[i]);
            delta_y_plus_c0 = Exp(c[0]+Delta_c[0]-c[1]*ts[i]);
            delta_y_minus_c0 = Exp(c[0]-Delta_c[0]-c[1]*ts[i]);
            delta_y_plus_c1 = Exp(c[0]-(c[1]+Delta_c[1])*ts[i]);
            delta_y_minus_c1 = Exp(c[0]-(c[1]-Delta_c[1])*ts[i]);
            outputfile_fitC.WriteLine("{0} {1} {2} {3} {4} {5}",ts[i],yfit,delta_y_plus_c0,delta_y_minus_c0,delta_y_plus_c1,delta_y_minus_c1);
    }
    outputfile_fitC.Close();
}
}

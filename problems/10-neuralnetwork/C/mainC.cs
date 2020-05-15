using static System.Console;
using static System.Math;
using System;
using static nnde;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    
    nnde network = new nnde(2);

    Func<double,double,double,double,double> phi = (x,y,ym,ymm) =>y-ymm;
    double a = -PI,b=PI;
    double c = -PI;
    double yc = Sin(c); 
    double ymc = Cos(c); 

    // c = 1;
    // yc = Exp(c);
    // ymc = Exp(c);
    


    network.train(phi, a, b,c, yc, ymc);

    vector xs = linspace(a,b,100);
    
    System.IO.StreamWriter outputfile = new System.IO.StreamWriter("out.solvedFun.sin.txt",append:false);

    for (int i = 0; i<100;i++){
        outputfile.WriteLine("{0} {1} {2}",xs[i],network.feedforwad(xs[i]),Sin(xs[i]));
                // outputfile.WriteLine("{0} {1} {2}",xs[i],network.feedforwad(xs[i]),Exp(xs[i]));

    }
    outputfile.Close();


}
}
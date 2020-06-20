using static System.Console;
using static System.Math;
using System;
using static ann;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    
    ann network = new ann(5);

    // X^2 
    // Func<double,double> f = (X) => {return 10-X*X;};
    Func<double,double> f = (X) => {return Sin(X);};

    int N = 5;
    double a = -PI+10,b=PI+10;
    vector x = linspace(a,b,N);
    x.print("x");
    vector y = new vector(N);
    double meanx = sum(x)/N;
    double sum2 = x.dot(x)/N;
    WriteLine(meanx);
    WriteLine(sum2);
    double std = Sqrt(sum2-meanx*meanx);

    vector xnormalized = (x-meanx)/std;
    
    System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out.tabfun.sin.txt",append:false);
    for (int i = 0; i<N;i++){
        y[i] = f(x[i]);
        outputfile.WriteLine($"{x[i]} {y[i]}");
    }
    outputfile.Close();

    network.train(xnormalized,y);

    vector xs = linspace(a,b,100);
    outputfile = new System.IO.StreamWriter("out.fitfun.sin.txt",append:false);

    for (int i = 0; i<100;i++){
        double xnormal = (xs[i]-meanx)/std;
        outputfile.WriteLine("{0} {1}",xs[i],network.feedforwad(xnormal));
    }
    outputfile.Close();


}
}
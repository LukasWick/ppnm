using static System.Console;
using static System.Math;
using System;
using static ann;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    
    ann network = new ann(10);

    // X^2 
    // Func<double,double> f = (X) => {return 10-X*X;};
    

    int N = 15;
    double a = -PI,b=PI;
    vector x = linspace(a,b,N);
    vector y = new vector(N);
    
    Func<double,double> f = (X) => 0.1*X*X+Sin(X);
    Func<double,double> fm = (X) => 0.2*X+Cos(X);
    Func<double,double> F = (X) => 1.0/30*X*X*X-Cos(X) - 1.0/30*a*a*a+Cos(a);
    

    System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out.tabfun.sin.txt",append:false);
    for (int i = 0; i<N;i++){
        // WriteLine($"f(x) = {f(x[i])}");
        y[i] = f(x[i]);
        outputfile.WriteLine($"{x[i]} {y[i]}");
    }
    outputfile.Close();

    network.train(x,y);

    vector xs = linspace(a,b,100);
    outputfile = new System.IO.StreamWriter("out.fitfun.sin.txt",append:false);
    System.IO.StreamWriter outputfile_exact = new System.IO.StreamWriter("out.exactfun.sin.txt",append:false);

    for (int i = 0; i<100;i++){
        outputfile.WriteLine("{0} {1} {2} {3}",xs[i],network.feedforwad(xs[i]),network.derivative(xs[i]),network.antiderivative(xs[i],xs[0]));
        outputfile_exact.WriteLine("{0} {1} {2} {3}",xs[i],f(xs[i]),fm(xs[i]),F(xs[i]));
    }
    outputfile.Close();
    outputfile_exact.Close();


}
}
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

    Tuple<vector,vector,matrix>  fit = least_squares_fit(t, logy, logdy,  f);
    vector c = fit.Item1;
    vector Delta_c = fit.Item2;

    double a = Exp(c[0]);
    double lambda = c[1];
    
    double Delta_a = Exp(Delta_c[0]);
    double Delta_lambda = Delta_c[1];

    WriteLine("Fit: a = {0} +-{1}, og lambda = {2}+-{3} d",a,Delta_a,lambda,Delta_lambda);

    matrix sigma = fit.Item3;
    

    System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out.tydata.txt",append:false);
        for(int i=0;i<y.size;i++){
            outputfile.WriteLine("{0} {1} {2}",t[i],y[i],dy[i]);
        }
   	outputfile.Close();

    vector ts = vector.linspace(t[0],t[-1],200);
    System.IO.StreamWriter outputfile_fit = new System.IO.StreamWriter("out.plot.txt",append:false);
    double yfit,sigma_y;
    for(int i=0;i<ts.size;i++){
            yfit = a*Exp(-lambda*ts[i]);
            sigma_y = Sqrt(Pow(yfit/a*Delta_a,2)+Pow(-lambda*yfit*Delta_lambda,2)+yfit/a*(-lambda*yfit)*sigma[0,1]);
            outputfile_fit.WriteLine("{0} {1} {2}",ts[i],yfit,sigma_y);
    }
    outputfile_fit.Close();
}
}

using static System.Console;
using static System.Math;
using System;
using static ode_integrator;
using static vector;
using System.Collections.Generic;
class main{
    static private Func<double,vector,vector> diff(vector param){
        double N = param[0]; 
        double TC = param[1];
        double TR = param[2];
        return (X,Y)=> new vector(-Y[1]*Y[0]/(N*TC),Y[1]*Y[0]/(N*TC)-Y[1]/TR,Y[1]/TR);
    }
static void Main(){
    // double N = 5.822e6; 
    // double TC = 5.0/2;
    // double TR = 6;
    // double R0 = 900;
    // double I0 = 3000-R0;
    // double S0 = N-I0-R0;

    // Func<double,vector,vector> f = (X,Y)=> new vector(-Y[1]*Y[0]/(N*TC),Y[1]*Y[0]/(N*TC)-Y[1]/TR,Y[1]/TR);

    


    int days = 100;
    int resulution = 5; 
    vector t = linspace(0,days,(int) (resulution*(days+1)));
    double h = 0.1;
    double acc = 0.001;
    double eps = 0.001;

    System.IO.StreamWriter outputfile = new System.IO.StreamWriter("out.plotB.txt",append:false);


    double N = 5.822e6; 
    double TC = 5.0/2;
    double TR = 6;
    double R0 = 900;
    double I0 = 3000-R0;
    double S0 = N-I0-R0;

    vector y = new vector(S0,I0,R0);
    Func<double,vector,vector> f = diff(new vector(N,TC,TR));
    matrix yres = ode_integrator.driver(f, t, y, h, acc, eps);
    for(int i = 0; i<t.size;i++){
        outputfile.WriteLine("{0} {1} {2} {3}",t[i],yres[i][0],yres[i][1],yres[i][2]);
    }
    outputfile.Close();





}
}

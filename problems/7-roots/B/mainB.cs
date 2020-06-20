using static System.Console;
using static System.Math;
using System;
using static rootfinder;
using static vector;
using static matrix;

using static ode_integrator;
using System.Collections.Generic;
class main{

static private vector auxiliaryFun(vector e, double rmax,double r0){
        Func<double,vector,vector> diff = (r,f)=> new vector(f[1],-2*(1/r+e[0])*f[0]);
        double f0 = r0-r0*r0;
        double f0merke = 1-2*r0;
        vector fstart  = new vector(f0,f0merke);
        vector F = driver(diff,r0,fstart,rmax,h:1e-3,acc:1e-4,eps:1e-4);
        return new vector(F[0]);
    } 
static void Main(){    
    double epsilon = 1e-4;
    double rmax = 10; 
    double r0 =1e-6;

    Func<vector,vector> M = (e) => auxiliaryFun(e, rmax,r0);

    vector e0 = new vector(-1.0);
    vector e_found = newton(M,e0,epsilon);
    vector e_exact = new vector(-1.0/2);
    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question B\n Bound states of hydrogen atom with shooting method for boundary value problems");
    WriteLine("See plot in PlotB.svg");


    WriteLine("First Bound states of hydrogen atom found with the shooting method");
    e_found.print("Numericaly found root        : ");
    e_exact.print("Exact root                   : ");
    WriteLine("Error goal                   : {0}",epsilon);
    WriteLine("Actual error (norm at rooot) : {0}",M(e_found).norm());    
    WriteLine("__________________________________________________________________________________________________________\n");



    
    Func<double,vector,vector> diff = (r,f)=> new vector(f[1],-2*(1/r+e_found[0])*f[0]);
    vector rs = linspace(r0,rmax,100);
    double f0 = r0-r0*r0;
    double f0merke = 1-2*r0;
    vector fstart  = new vector(f0,f0merke);
    matrix F = driver(diff,rs,fstart,h:1e-3,acc:1e-4,eps:1e-4);

    System.IO.StreamWriter outputfile = new System.IO.StreamWriter("out.plotB.txt",append:false);
    for(int i = 0; i<rs.size;i++){
        outputfile.WriteLine("{0} {1} {2}",rs[i],rs[i]*Exp(-rs[i]),F[i][0]);
    }
    outputfile.Close();


    

}
}
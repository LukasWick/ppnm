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
static private matrix Ffun(vector e, double rmax,double r0,vector rs){
    Func<double,vector,vector> diff = (r,f)=> new vector(f[1],-2*(1/r+e[0])*f[0]);
    double f0 = r0-r0*r0;
    double f0merke = 1-2*r0;
    vector fstart  = new vector(f0,f0merke);
    matrix F = driver(diff,rs,fstart,h:1e-3,acc:1e-4,eps:1e-4);
    return F;
} 
static void Main(){    
    double epsilon = 1e-4;
    double r0 =1e-6;
    vector e_exact = new vector(-1.0/2);
    vector e0 = new vector(-1.0);
    System.IO.StreamWriter outputfile = new System.IO.StreamWriter("out.plotC.txt",append:false);
    System.IO.StreamWriter outputfileE = new System.IO.StreamWriter("out.plotC.E.txt",append:false);

    for(double rmax=3; rmax<11;rmax++){
        Func<vector,vector> M = (e) => auxiliaryFun(e, rmax,r0);
        vector e_found_s = newton(M,e0,epsilon);

        M = (e) => auxiliaryFun(e, rmax,r0)-(new vector(rmax*Exp(-Sqrt(-2*e[0])*rmax)));
        vector e_found_c = newton(M,e0,epsilon);



        vector rs = linspace(r0,rmax,100);
        matrix F_simple = Ffun(e_found_s, rmax,r0,rs);

        matrix F_complex = Ffun(e_found_c, rmax,r0,rs);

        for(int i = 0; i<rs.size;i++){            
            outputfile.WriteLine("{0} {1} {2} {3}",rs[i],rs[i]*Exp(-rs[i]),F_simple[i][0],F_complex[i][0]);
        }
        outputfileE.WriteLine("{0} {1} {2} {3}",rmax,e_exact[0],e_found_s[0],e_found_c[0]);
        outputfile.WriteLine("");
        outputfile.WriteLine("");

    }
    
    outputfileE.Close();
    outputfile.Close();
}
}
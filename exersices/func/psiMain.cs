using System;
using static System.Console;
using static System.Math;

class main{
static double inf = System.Double.PositiveInfinity;
static double nan = System.Double.NaN;
    static double psiNorm(double a){
        return quad.o8av((x)=>Exp(-a*x*x),-inf,inf,acc:1e-6,eps:1e-6);
    }
    static double psiH0psi(double a){
       	Func<double,double> f= (x)=>(-Pow(a*x,2)/2+a/2+x*x/2)*Exp(-a*x*x);
        return quad.o8av(f,-inf,inf,acc:1e-6,eps:1e-6);
    }
    static int Main(){
        for(double a=0.1;a<2;a+=0.02)
            Write("{0} {1}\n",a,psiH0psi(a)/psiNorm(a));
        return 0;
    }
}
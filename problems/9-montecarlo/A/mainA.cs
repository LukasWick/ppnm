using static System.Console;
using static System.Math;
using System;
using static montecarlo;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    // sin(x)sin(y)
    Func<vector,double> f = (x) => {return Sin(x[0])*Sin(x[1]);};

    int N = (int) 1e6;
    WriteLine($"\n ∫0π dx  ∫0π dy sin(x)*sin(y). With N = {N}");
    vector a = new vector(0,0);
    vector b = new vector(PI,PI);
    vector res = plainmc(f,a,b,N);
    double accurate = 4;
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",accurate);
    WriteLine("Error estimate         : {0}",res[1]);
    WriteLine("Actual error           : {0}",Abs(accurate-res[0]));

    // integral of a half sphere
    double r = Pow(3.0/2,1.0/3);
    f = (x) => {
        if(x[0]*x[0]+x[1]*x[1]<=r*r) return Sqrt(r*r-x[0]*x[0]-x[1]*x[1]);
        else return 0;
    };

    N = (int) 1e6;
    WriteLine($"\nIntegral of circle with radius {r}. With N = {N}");
    a = new vector(-r,-r);
    b = new vector(r,r);
    res = plainmc(f,a,b,N);
    accurate = PI;
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",accurate);
    WriteLine("Error estimate         : {0}",res[1]);
    WriteLine("Actual error           : {0}",Abs(accurate-res[0]));    

    // given integral
    f = (x) => {return 1.0/(PI*PI*PI)/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};

    N = (int) 1e6;
    WriteLine($"\nIntegral: ∫0π  dx/π ∫0π  dy/π ∫0π  dz/π [1-cos(x)cos(y)cos(z)]^-1 = Γ(1/4)4/(4π3).\nWith N = {N}");
    a = new vector(0,0,0);
    b = new vector(PI,PI,PI);
    res = plainmc(f,a,b,N);
    accurate = 1.3932039296856768591842462603255;
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",accurate);
    WriteLine("As it is a difficult singular integral\nWe expect the errors to be large");
    WriteLine("Error estimate         : {0}",res[1]);
    WriteLine("Actual error           : {0}",Abs(accurate-res[0]));    



}
}
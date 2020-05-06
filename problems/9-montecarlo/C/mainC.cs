using static System.Console;
using static System.Math;
using System;
using static montecarlo;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    // sin(x)sin(y)
    int callCount = 0;
    Func<vector,double> f = (x) => {callCount++;return Sin(x[0])*Sin(x[1]);};

    WriteLine($"\n ∫0π dx  ∫0π dy sin(x)*sin(y). ");
    vector a = new vector(0,0);
    vector b = new vector(PI,PI);
    vector res = stratifiedmc(f,a,b);
    double accurate = 4;
    WriteLine("Numerical integration   : {0}",res[0]);
    WriteLine("Analytical integration  : {0}",accurate);
    WriteLine("Error estimate          : {0}",res[1]);
    WriteLine("Actual error            : {0}",Abs(accurate-res[0]));
    WriteLine("Number of function calls: {0}",callCount);
    // WriteLine("Number of function calls: {0}",res[2]);

    // integral of a half sphere
    callCount = 0;
    double r = Pow(3.0/2,1.0/3);
    f = (x) => {
        callCount++;
        if(x[0]*x[0]+x[1]*x[1]<=r*r) return Sqrt(r*r-x[0]*x[0]-x[1]*x[1]);
        else return 0;
    };

    WriteLine($"\nIntegral of circle with radius {r}.");
    a = new vector(-r,-r);
    b = new vector(r,r);
    res = stratifiedmc(f,a,b);
    accurate = PI;
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",accurate);
    WriteLine("Error estimate         : {0}",res[1]);
    WriteLine("Actual error           : {0}",Abs(accurate-res[0]));    
    WriteLine("Number of function calls: {0}",callCount);
    // WriteLine("Number of function calls: {0}",res[2]);
    // given integral
    callCount = 0;
    f = (x) => {callCount++;return 1.0/(PI*PI*PI)/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};

    WriteLine($"\nIntegral: ∫0π  dx/π ∫0π  dy/π ∫0π  dz/π [1-cos(x)cos(y)cos(z)]^-1 = Γ(1/4)4/(4π3).");
    a = new vector(0,0,0);
    b = new vector(PI,PI,PI);
    res = stratifiedmc(f,a,b);
    accurate = 1.3932039296856768591842462603255;
    WriteLine("Numerical integration  : {0}",res[0]);
    WriteLine("Analytical integration : {0}",accurate);
    WriteLine("As it is a difficult singular integral\nWe expect the errors to be large");
    WriteLine("Error estimate         : {0}",res[1]);
    WriteLine("Actual error           : {0}",Abs(accurate-res[0]));    
    WriteLine("Number of function calls: {0}",callCount);
    // WriteLine("Number of function calls: {0}",res[2]);


}
}
using static System.Console;
using static System.Math;
using System;
using static montecarlo;
using static quasi_random_mc;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    // sin(x)sin(y)
    Func<vector,double> f = (x) => {return Sin(x[0])*Sin(x[1]);};

    int N = (int) 1e5;
    WriteLine($"\n ∫0π dx  ∫0π dy sin(x)*sin(y). With N = {N}");
    vector a = new vector(0,0);
    vector b = new vector(PI,PI);
    double accurate = 4;

    vector resH = haltonmc(f,a,b,N);
    vector resL = latticemc(f,a,b,N);
    vector resC = combinedmc(f,a,b,N);

    WriteLine("Halton integration     : {0}",resH[0]);
    WriteLine("Lattice integration    : {0}",resL[0]);
    WriteLine("Combined integration   : {0}",resC[0]);
    WriteLine("Analytical integration : {0}",accurate);

    WriteLine("Halton error estimate  : {0}",resH[1]);
    WriteLine("Lattice error estimate : {0}",resL[1]);
    WriteLine("Combined error estimate: {0}",resC[1]);
    WriteLine("Actual H error         : {0}",Abs(accurate-resH[0]));
    WriteLine("Actual L error         : {0}",Abs(accurate-resL[0]));
    WriteLine("Actual C error         : {0}",Abs(accurate-resC[0]));
    
    // integral of a sphere
    double r = Pow(3.0/2,1.0/3);
    f = (x) => {
        if(x.dot(x)<=r*r) return 1;
        else return 0;
    };

    N = (int) 1e6;
    WriteLine($"\nIntegral of sphere with radius {r}. With N = {N}");
    accurate = 2*PI;

    a = new vector(-1.5,-1.5,-1.5);
    b = new vector(1.5,1.5,1.5);
    resH = haltonmc(f,a,b,N);
    resL = latticemc(f,a,b,N);
    resC = combinedmc(f,a,b,N);

    WriteLine("Halton integration     : {0}",resH[0]);
    WriteLine("Lattice integration    : {0}",resL[0]);
    WriteLine("Combined integration   : {0}",resC[0]);
    WriteLine("Analytical integration : {0}",accurate);

    WriteLine("Halton error estimate  : {0}",resH[1]);
    WriteLine("Lattice error estimate : {0}",resL[1]);
    WriteLine("Combined error estimate: {0}",resC[1]);
    WriteLine("Actual H error         : {0}",Abs(accurate-resH[0]));
    WriteLine("Actual L error         : {0}",Abs(accurate-resL[0]));
    WriteLine("Actual C error         : {0}",Abs(accurate-resC[0]));


    // integral of a half sphere
    r = Pow(3.0/2,1.0/3);
    f = (x) => {
        if(x.dot(x)<=r*r) return Sqrt(r*r-x.dot(x));
        else return 0;
    };

    N = (int) 1e6;
    WriteLine($"\nIntegral of half sphere with radius {r}. With N = {N}");
    accurate = PI;


    a = new vector(-1.5,-1.5);
    b = new vector(1.5,1.5);
    resH = haltonmc(f,a,b,N);
    resL = latticemc(f,a,b,N);
    resC = combinedmc(f,a,b,N);

    WriteLine("Halton integration     : {0}",resH[0]);
    WriteLine("Lattice integration    : {0}",resL[0]);
    WriteLine("Combined integration   : {0}",resC[0]);
    WriteLine("Analytical integration : {0}",accurate);

    WriteLine("Halton error estimate  : {0}",resH[1]);
    WriteLine("Lattice error estimate : {0}",resL[1]);
    WriteLine("Combined error estimate: {0}",resC[1]);
    WriteLine("Actual H error         : {0}",Abs(accurate-resH[0]));
    WriteLine("Actual L error         : {0}",Abs(accurate-resL[0]));
    WriteLine("Actual C error         : {0}",Abs(accurate-resC[0]));

    // given integral
    f = (x) => {return 1.0/(PI*PI*PI)/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};

    N = (int) 1e6;
    WriteLine($"\nIntegral: ∫0π  dx/π ∫0π  dy/π ∫0π  dz/π [1-cos(x)cos(y)cos(z)]^-1 = Γ(1/4)4/(4π3).\nWith N = {N}");
    accurate = 1.3932039296856768591842462603255;

    a = new vector(0,0,0);
    b = new vector(PI,PI,PI);
    resH = haltonmc(f,a,b,N);
    resL = latticemc(f,a,b,N);
    resC = combinedmc(f,a,b,N);
    WriteLine("Halton integration     : {0}",resH[0]);
    WriteLine("Lattice integration    : {0}",resL[0]);
    WriteLine("Combined integration   : {0}",resC[0]);
    WriteLine("Analytical integration : {0}",accurate);

    WriteLine("Halton error estimate  : {0}",resH[1]);
    WriteLine("Lattice error estimate : {0}",resL[1]);
    WriteLine("Combined error estimate: {0}",resC[1]);
    WriteLine("Actual H error         : {0}",Abs(accurate-resH[0]));
    WriteLine("Actual L error         : {0}",Abs(accurate-resL[0]));
    WriteLine("Actual C error         : {0}",Abs(accurate-resC[0]));



}
}
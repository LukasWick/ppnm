using static System.Console;
using static System.Math;
using System;
using static montecarlo;
using static vector;
using System.Collections.Generic;
class main{
static void Main(){
    // sin(x)sin(y)
    double r = Pow(3.0/2,1.0/3);
    Func<vector,double> f = (x) => {
        if(x[0]*x[0]+x[1]*x[1]<=r*r) return Sqrt(r*r-x[0]*x[0]-x[1]*x[1]);
        else return 0;
    };
    vector a = new vector(-r,-r);
    vector b = new vector(r,r);
    double accurate = PI;

    for(int N =(int)3e3; N<(int) 2e6; N=(int) (1.3*N)){        
        vector res = plainmc(f,a,b,N);
        double error = Abs(res[0]-accurate);
        WriteLine($"{1/Sqrt(N)} {res[1]} {error}");
    }
}
}
using static System.Console;
using static System.Math;
using System;
using static integrator;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    Func<double,double> f = (x) => {callCount++;return Sqrt(x);};
    
    WriteLine("\n∫01 dx √(x) = 2/3 .");
    WriteLine("Numerical integration: {0}",adapt(f,0,1,1e-3,1e-3));
    WriteLine("Called function {0} times",callCount);
    callCount = 0;

    f = (x) => {callCount++;return 4*Sqrt(1-x*x);};
    
    WriteLine("\n∫01 dx 4√(1-x²) = π = {0}",PI);
    WriteLine("Numerical integration: {0}",adapt(f,0,1,1e-3,1e-3));
    WriteLine("Called function {0} times",callCount);
    callCount = 0;


}
}
    

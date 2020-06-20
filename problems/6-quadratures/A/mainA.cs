using static System.Console;
using static System.Math;
using System;
using static integrator;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    Func<double,double> f = (x) => {callCount++;return Sqrt(x);};
    WriteLine("\n__________________________________________________________________________________________________________");

    WriteLine("Question A\nRecursive adaptive integrator");
    WriteLine("Tested on different integrals");
    WriteLine("\n∫01 dx √(x) = 2/3 .");
    double acc = 1e-4;
    double eps = 0;
    double q = adapt(f,0,1,acc,eps);
    double exact = 2.0/3;
    WriteLine("Numerical integration: {0}",q);
    WriteLine("Error goal           : {0}",acc+Abs(exact)*eps);
    WriteLine("Actual error         : {0}",Abs(q-exact));    
    WriteLine("Called function {0} times",callCount);
    callCount = 0;

    f = (x) => {callCount++;return 4*Sqrt(1-x*x);};
    
    WriteLine("\n∫01 dx 4√(1-x²) = π = {0}",PI);
    acc = 0;
    eps = 1e-4;
    q = adapt(f,0,1,acc,eps);
    exact = PI;
    WriteLine("Numerical integration: {0}",q);
    WriteLine("Error goal           : {0}",acc+Abs(exact)*eps);
    WriteLine("Actual error         : {0}",Abs(q-exact));    
    WriteLine("Called function {0} times",callCount);
    callCount = 0;
    WriteLine("__________________________________________________________________________________________________________\n");


}
}
    

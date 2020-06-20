using static System.Console;
using static System.Math;
using System;
using static integrator;
using static quad;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    Func<double,double> f = (x) => {callCount++;return 1/Sqrt(x);};

    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question B\nOpen quadrature with Clenshaw–Curtis variable transformation");
    WriteLine("Tested on different integrals");

    WriteLine("\n∫01 dx 1/√(x) = 2 .");

    WriteLine("With clenshaw curtis: {0}",clenshaw_curtis(f,0,1,1e-2,1e-2));
    WriteLine("Called function {0} times",callCount);
    callCount =0;
    WriteLine("Without clenshaw curtis: {0}",adapt(f,0,1,1e-2,1e-2));
    WriteLine("Called function {0} times",callCount);
    callCount =0;
    
    
    f = (x) => {callCount++;return Log(x)/Sqrt(x);};
    WriteLine("\n∫01 dx ln(x)/√(x) = -4 .");
    WriteLine("With clenshaw curtis: {0}",clenshaw_curtis(f,0,1,2e-2,2e-2));
    WriteLine("Called function {0} times",callCount);
    callCount =0;
    
    WriteLine("Without clenshaw curtis: {0}",adapt(f,0,1,2e-2,2e-2));// Lowest value that did not give stack overflow 
    WriteLine("Called function {0} times",callCount);
    callCount =0;
    


    f = (x) => {callCount++;return 4*Sqrt(1-x*x);};
    
    WriteLine("\nTesting ∫01 dx 4√(1-x²) = π = {0}",PI);
    WriteLine("With clenshaw curtis: {0}",clenshaw_curtis(f,0,1,1e-15,1e-15));
    WriteLine("Called function {0} times",callCount);
    callCount =0;

    WriteLine("Without clenshaw curtis: {0}",adapt(f,0,1,1e-15,1e-15));
    WriteLine("Called function {0} times",callCount);
    callCount =0;

    WriteLine("With o8av: {0}",o8av(f,0,1,1e-15,1e-15));
    WriteLine("Called function {0} times",callCount);

    WriteLine("__________________________________________________________________________________________________________\n");

}
}
    

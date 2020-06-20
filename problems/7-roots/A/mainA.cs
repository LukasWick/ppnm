using static System.Console;
using static System.Math;
using System;
using static rootfinder;
using static vector;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    Func<vector,vector> f = (x) => {callCount++;return new vector(x[0]*x[0]-4);};
    double epsilon = 1e-3;
    vector x0 = new vector(1.0);
    vector root = newton(f,x0,epsilon);
    vector exact = new vector(2.0);

    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question A\n Newton's method with numerical Jacobian and back-tracking linesearch");

    WriteLine("Test function f(x)=x^2-4, root = +-2 (only looking for 2)");
    root.print("Numericaly found root        : ");
    exact.print("Exact root                   : ");
    f(root).print("Function value at root       : ");
    WriteLine("Error goal                   : {0}",epsilon);
    WriteLine("Actual error (norm at rooot) : {0}",f(root).norm());    
    WriteLine("Number of func call          : {0}",callCount);
    WriteLine("");
    callCount = 0;
    // Gradient 
    // Func<vector,vector> f = (x) => {callCount++;return new vector(pow(1-x,2)+100,);};
    
    f = (x) => {
        callCount++;
        double dx = -2*(1-x[0])-400*x[0]*(x[1]-x[0]*x[0]);
        double dy = 200*(x[1]-x[0]*x[0]);
        return new vector(dx,dy);};

    epsilon = 1e-3;
    x0 = new vector(1.1,0.5);
    root = newton(f,x0,epsilon);
    exact = new vector(1.0,1.0);
    WriteLine("Test function extreem of Rosenbrock's valley function");
    root.print("Numericaly found root        : ");
    exact.print("Exact root                   : ");
    f(root).print("Function value at root       : ");
    WriteLine("Error goal                   : {0}",epsilon);
    WriteLine("Actual error (norm at rooot) : {0}",f(root).norm());    
    WriteLine("Number of func call          : {0}",callCount);
    WriteLine("__________________________________________________________________________________________________________\n");
    callCount = 0;
}
}
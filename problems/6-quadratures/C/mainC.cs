using static System.Console;
using static System.Math;
using System;
using static integrator;
using static quad;
using System.Collections.Generic;
class main{
 
static void Main(){
    // Func<double,double> f = (x) => {callCount++;return 1/Sqrt(x);};
    double err = 0;

    Func<double,double> f = (x) => 4*Sqrt(1-x*x);
    WriteLine("\n∫01 dx 4√(1-x²) = π = {0}",PI);
    double Q = adapt(f,0,1,ref err,1e-6,1e-6);
    WriteLine("Numerical integration: {0}",Q);
    WriteLine("Calculated error: {0}",err);
    WriteLine("Actual error:     {0}",Abs(Q-PI));
    
    int callCount = 0;
    f= (x) => {callCount++;return Exp(-x*x);};

    WriteLine("\n∫-inf,inf dx exp(-x²) = √π = {0}",Sqrt(PI));
    Q = integrate(f,double.NegativeInfinity,double.PositiveInfinity,1e-3,1e-3);
    WriteLine("My integration: {0}",Q);
    WriteLine("My error:       {0}",Abs(Q-Sqrt(PI)));
    WriteLine("My #calls:      {0}",callCount);
    callCount = 0;

    Q = o8av(f,double.NegativeInfinity,double.PositiveInfinity,1e-3,1e-3);
    WriteLine("o8av integration: {0}",Q);
    WriteLine("o8av error:       {0}",Abs(Q-Sqrt(PI)));
    WriteLine("o8av #calls:      {0}",callCount);
    callCount = 0;

    f= (x) => {callCount++;return 1/(1+x*x);};
    WriteLine("\n∫0,inf dx 1/(1+x²) = π/2 = {0}",PI/2);
    Q = integrate(f,0,double.PositiveInfinity,1e-3,1e-3);
    WriteLine("My integration: {0}",Q);
    WriteLine("My error:       {0}",Abs(Q-PI/2));
    WriteLine("My #calls:      {0}",callCount);
    callCount = 0;

    Q = o8av(f,0,double.PositiveInfinity,1e-3,1e-3);
    WriteLine("o8av integration: {0}",Q);
    WriteLine("o8av error:       {0}",Abs(Q-PI/2));
    WriteLine("o8av #calls:      {0}",callCount);
    callCount = 0;

    f= (x) => {callCount++;return 1/(1+x*x);};
    WriteLine("\n∫-inf,0 dx  1/(1+x²) = π/2 = {0}",PI/2);
    Q = integrate(f,double.NegativeInfinity,0,1e-3,1e-3);
    WriteLine("My integration: {0}",Q);
    WriteLine("My error:       {0}",Abs(Q-PI/2));
    WriteLine("My #calls:      {0}",callCount);
    callCount = 0;

    Q = o8av(f,double.NegativeInfinity,0,1e-3,1e-3);
    WriteLine("o8av integration: {0}",Q);
    WriteLine("o8av error:       {0}",Abs(Q-PI/2));
    WriteLine("o8av #calls:      {0}",callCount);
    callCount = 0;
}
}
    

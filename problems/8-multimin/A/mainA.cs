using static System.Console;
using static System.Math;
using System;
using static minimization;
using static vector;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    Func<vector,double> f = (X) => Pow(1-X[0],2)+100*Pow(X[1]-X[0]*X[0],2);
    double epsilon = 1e-4;
    vector x = new vector(1.5,0.5);
    int num_of_steps = qnewton(f,ref x,epsilon);
    vector x_exact = new vector(1.0,1.0);
    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question A\n Quasi-Newton method with numerical gradient, back-tracking linesearch, and rank-1 update");
    
    WriteLine("Test function f(x,y)=(1-x)^2+100(y-x^2)^2, min at 1,1");
    x.print("Numericaly found min x       : ");
    x.print("Exact min x                  : ");
    WriteLine("Function value at min        : {0}",f(x));
    WriteLine("Gradient tollerence          : {0}",epsilon);
    WriteLine("Actual gradient              : {0}",gradient(f,x).norm());
    WriteLine("Actual error                 : {0}",Abs(f(x)-f(x_exact)));    
    WriteLine("Number of steps              : {0}",num_of_steps);
    WriteLine("");
    
    f = (X) => Pow(X[0]*X[0]+X[1]-11,2)+Pow(X[0]+X[1]*X[1]-7,2);
    epsilon = 1e-4;
    x = new vector(2.4,2.4);
    num_of_steps = qnewton(f,ref x,epsilon);
    x_exact = new vector(3,2);
    WriteLine("");
    WriteLine("Test function f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2, min at 3,2");
    x.print("Numericaly found min x       : ");
    x.print("Exact min x                  : ");
    WriteLine("Function value at min        : {0}",f(x));
    WriteLine("Gradient tollerence          : {0}",epsilon);
    WriteLine("Actual gradient              : {0}",gradient(f,x).norm());
    WriteLine("Actual error                 : {0}",Abs(f(x)-f(x_exact)));    
    WriteLine("Number of steps              : {0}",num_of_steps);
    WriteLine("__________________________________________________________________________________________________________\n");
}
}
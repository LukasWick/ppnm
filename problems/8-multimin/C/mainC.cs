using static System.Console;
using static System.Math;
using System;
using static simplex_minimization;
using static vector;
using System.Collections.Generic;
class main{
 
static void Main(){
    int callCount = 0;
    
    Func<vector,double> f = (X) => {callCount++;return X.norm();};
    double tol = 1e-3;
    matrix simplex = new matrix("1.5 0.5 2;0.5 0.2 0.2");
    int minindex = simplexmin(f,simplex,tol);
    vector x = simplex[minindex];
    vector x_exact = new vector(0,0);
    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question C\n Simplex");
    
    WriteLine("Test function f(x,y)=x^2+y^2, min at 0,0");
    x.print("Numericaly found min x       : ");
    x_exact.print("Exact min x                  : ");
    simplex.print("Found simplex                :");
    WriteLine("Tollerated simplex size      : {0}",tol);
    WriteLine("Function value at found min  : {0}",f(x));
    WriteLine("Error                        : {0}",Abs(f(x)-f(x_exact)));    
    WriteLine("Number of calls              : {0}",callCount);
    WriteLine("");

    callCount = 0;
    var rnd = new Random();
    simplex = new matrix(5,7);
    for(int i=0;i<simplex.size1;i++){
        for(int j=0;j<simplex.size2;j++){
            simplex[i,j]=10*(rnd.NextDouble()-0.5); 
        }
    }
    WriteLine("Test function f(x)=|x|, for 5D vector with random init");
    simplex.print("Initial simplex              :");
    tol = 1e-3;
    minindex = simplexmin(f,simplex,tol);
    x = simplex[minindex];
    x_exact = new vector(5);
    x.print("Numericaly found min x       : ");
    x_exact.print("Exact min x                  : ");
    simplex.print("Found simplex                :");
    WriteLine("Tollerated simplex size      : {0}",tol);
    WriteLine("Function value at found min  : {0}",f(x));
    WriteLine("Error                        : {0}",Abs(f(x)-f(x_exact)));    
    WriteLine("Number of calls              : {0}",callCount);
    WriteLine("");
    callCount = 0;
    f = (X) => {callCount++;return Pow(1-X[0],2)+100*Pow(X[1]-X[0]*X[0],2);};


    simplex = new matrix("1 1 2;1 0 0.1");

    WriteLine("Test function f(x,y)=(1-x)^2+100(y-x^2)^2, min at 1,1");
    simplex.print("Initial simplex              :");
    tol = 1e-3;
    minindex = simplexmin(f,simplex,tol);
    x = simplex[minindex];
    x_exact = new vector(1,1);
    x.print("Numericaly found min x       : ");
    x_exact.print("Exact min x                  : ");
    simplex.print("Found simplex                :");
    WriteLine("Tollerated simplex size      : {0}",tol);
    WriteLine("Function value at found min  : {0}",f(x));
    WriteLine("Error                        : {0}",Abs(f(x)-f(x_exact)));    
    WriteLine("Number of calls              : {0}",callCount);
    WriteLine("__________________________________________________________________________________________________________\n");
}
}
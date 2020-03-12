using static System.Console;
using static System.Math;
using static cmath;
using System;

class main{
    static int Main(){
        Write("sqrt(2)={0}\n", sqrt(2.0));
        complex I = new complex(0,1);
        Write("exp(i)={0}\n", exp(I));
        Write("exp(i*pi)={0}\n", exp(I*PI));
        Write("i^i={0}\n", I.pow(I));
        Write("sin(i)={0}\n", sin(I*PI));
        
        return 0;
    }

}
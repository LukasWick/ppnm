using static System.Math;
using System;
using static System.Console;
using System.Collections.Generic;
public partial class integrator{
    public static double adapt24(Func<double,double> f, double a, double b, double delta, double eps,double f2,double f3,ref double error)
        {
        double f1 = f(a+(b-a)*1/6);
        double f4 = f(a+(b-a)*5/6);
        double Q = (b-a)/6*(2*f1+f2+f3+2*f4);
        double q = (b-a)/4*(f1+f2+f3+f4);
        double err=Abs(Q-q);

        if (err < delta+eps*Abs(Q)){
            error = error+err;
            return Q;
        } 
        else{
            return adapt24(f,a,(a+b)/2,delta/Sqrt(2),eps,f1,f2,ref error)+ adapt24(f,(a+b)/2,b,delta/Sqrt(2),eps,f3,f4,ref error);
            }
        }

    public static double adapt(Func<double,double> f, double a, double b, double delta= 1e-3, double eps = 1e-3){
            double err =0;
            return adapt(f,a, b, ref err, delta,eps);
        }

    public static double adapt(Func<double,double> f, double a, double b,ref double err, double delta= 1e-3, double eps = 1e-3){
        double f2 = f(a+(b-a)*2/6);
        double f3 = f(a+(b-a)*4/6);
        double Q = adapt24(f,a,b,delta,eps,f2,f3,ref err);

        return Q;
        }
    
    public static double clenshaw_curtis(Func<double,double> f, double a, double b, double delta, double eps){
        Func<double,double> h = (x) => 1.0/2*(b-a)*f(1.0/2*(a+b)+1.0/2*(b-a)*x); //Function with same intergral from -1 to 1
        Func<double,double> g = (theta) => h(Cos(theta))*Sin(theta); 
        
        return adapt(g, 0, PI, delta, eps);
    }

    public static double integrate(Func<double,double> f, double a, double b, double delta, double eps){
        double posInf = double.PositiveInfinity;
        double negInf = double.NegativeInfinity;
        Func<double,double> g;
        if (a == negInf){
            if (b == posInf){
                g = (t) => f(t/(1-t*t))*(1+t*t)/Pow(1-t*t,2);
                return adapt(g, -1, 1, delta, eps);
            }
            else{
                g = (t) => f(b+t/(1+t))/Pow(1+t,2);
                return adapt(g, -1, 0, delta, eps);
            }
        }
        else if (b == posInf) {
            g = (t) => f(a+t/(1-t))/Pow(1-t,2);
            return adapt(g, 0, 1, delta, eps);
        }
        else{
            return adapt(f,a, b,delta,eps);
        }
    }


}
using System;
using static System.Math;
public static class nonef{
public static vector euler_spiral(double L){
	double a=0,b=L;
	Func<double,double> Cdiff = (s) => Cos(s*s);
	Func<double,double> Sdiff = (s) => Sin(s*s);

	double x = quad.o8av(Cdiff,a,b,acc:1e-6,eps:1e-6);
	double y = quad.o8av(Sdiff,a,b,acc:1e-6,eps:1e-6);
	return new vector(x,y);
	}
}
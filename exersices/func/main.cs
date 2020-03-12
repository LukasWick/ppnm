using System;
using static System.Console;
using static System.Math;

class main{
static double inf = System.Double.PositiveInfinity;
static double nan = System.Double.NaN;
static double gamma(double z){
	if (z<0) return PI/Sin(PI*z)/gamma(1-z);
	if (z<1) return 1/z*gamma(z+1);
	if (z>2) return (z-1)*gamma(z-1); 
	
	Func<double,double> f = delegate(double x){
		return Pow(x,z-1)*Exp(-x);
	};
	return quad.o8av(f,0,inf,acc:1e-6,eps:1e-6);
}
static double integrate(Func<double,double> f,double a, double b){return quad.o8av(f,a,b,acc:1e-6,eps:1e-6);}
static int Main(){
	Func<double,double> f = (x) => Log(x)/Sqrt(x);
	double a=0,b=1,result;
	result=quad.o8av(f,a,b,acc:1e-6,eps:1e-6);
	Write("int_0^1 dx ln(x)/sqrt(x) = {0} (~ -4)\n",result);
	Write("int_-inf^inf dx exp(-x^2) = {0} (sqrt(pi)={1})\n",integrate((x) => Exp(-x*x),-inf,inf),Sqrt(PI));
	for(double p=0.5;p<9;p+=0.5)
		Write("p = {1:f1}: int_-0^1 dx (ln(1/x))^p = {0:f3} (gamma({1:f1}+1) = {2:f3}) \n",integrate((x) => Pow((Log(1/x)),p),0,1),p,gamma(p+1));
	
	result = integrate((x)=>Sqrt(x)*Exp(-x),0,inf);
	Write("int_0^inf dx sqrt(x)*exp(-x) = {0} (~ 1/2sqrt(pi) = {1})\n", result, 1/2.0 * Sqrt(PI));
	result = integrate((x)=>Pow(x,3)/(Exp(x)-1),0,inf);
	Write("int_0^inf dx x^3/(e^x-1) = {0} (~ pi^4/15 = {1})\n", result,Pow(PI,4)/15);
	// result = integrate((x)=>Sin(x)/x,0,inf);
	// Write("int_0^inf dx sin(x)/x = {0} (~ pi/2 = {1})\n",result ,PI/2);
	
	return 0;
	}
}

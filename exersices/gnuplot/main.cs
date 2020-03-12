using static System.Console;
using static System.Math;
class main{
static int Main(){
double eps=1.0/32, dx=1.0/16;
for(double x=-4+eps;x<=6;x+=dx)
	WriteLine("{0,10:f3} {1,15:f8}",x,math.gamma(x));

for(double x=-3+eps;x<=3;x+=dx)
	Error.WriteLine("{0,10:f3} {1,15:f8}",x,math.erf(x));


return 0;
}//Main
}//main
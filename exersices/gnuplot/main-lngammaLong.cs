using static System.Console;
using static System.Math;
class main{
static int Main(){
double eps=1.0/32, dx=1.0/16;
for(double x=0+eps;x<=1000;x+=dx)
	WriteLine("{0,10:f3} {1,15:f8}",x,math.lngamma(x));
return 0;
}//Main
}//main
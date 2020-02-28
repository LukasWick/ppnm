#include<math.h>
#include<stdio.h>
int main(){
for(double x= 0.5;x<=1000;x+=100)
	printf("%g %g\n",x,gamma(x));
return 0;
}

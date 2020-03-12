#include<math.h>
#include<stdio.h>
int main(){
for(double x= -5.5;x<=5.5;x+=3)
	printf("%g %g\n",x,gamma(x));
return 0;
}

using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
class main{
static int Main(string[] args){
	double xa = 0;
	double xb = 3;
	vector ya = new vector(0.5);
	List<double> xs = new List<double>();
	List<vector> ys = new List<vector>();

	Func<double,vector,vector> ydiff = delegate(double x, vector y){
		var res = new vector(y[0]*(1-y[0]));
		return res;
	};
	vector yb = ode.rk23(ydiff,xa,ya,xb,xs,ys);
	
	double logistic = 0;
	for(int i=0;i<xs.Count;i++){
		logistic = 1/(1+Exp(-xs[i]));
		WriteLine($"{xs[i]} {ys[i][0]} {logistic}");
	}
		
	return 0;
}
}

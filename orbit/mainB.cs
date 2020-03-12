using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
class main{
static int Main(string[] args){
	int N = 2;
	double phia = 0;
	double phib = N*2*PI;
	double u0 = 1.0;
	double udiff0 = 1e-3; 
	double eps = 0;

	foreach(string s in args){
		string[] ws = s.Split('=');
		if(ws[0]=="a") phia =double.Parse(ws[1]);
		if(ws[0]=="b") phib =double.Parse(ws[1]);
		if(ws[0]=="u0") u0 =double.Parse(ws[1]);
		if(ws[0]=="udiff0") udiff0 =double.Parse(ws[1]);
		if(ws[0]=="eps") eps =double.Parse(ws[1]);
		if(ws[0]=="N") {N = int.Parse(ws[1]); phib = N*2*PI;}
	} 

	vector ua = new vector(u0,udiff0); // start vector
	List<double> phis = new List<double>();
	List<vector> us = new List<vector>();

	Func<double,vector,vector> ydiff = delegate(double x, vector y){
		var res = new vector(y[1],1-y[0]+eps*y[0]*y[0]);
		return res;
	};
	vector ub = ode.rk23(ydiff,phia,ua,phib,phis,us,acc:1e-6,eps:1e-5,h:1e-5,limit:10000);
	
	for(int i=0;i<phis.Count;i++){
		WriteLine($"{phis[i]} {us[i][0]} {us[i][1]}");
	}
		
	return 0;
}
}

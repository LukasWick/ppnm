using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

using static vector;
using static matrix;
public partial class minimization{
    public static readonly double EPS=1.0/4194304;
    public static vector gradient(Func<vector,double> f,vector x){
        vector gradientF = new vector(x.size);
        vector x_temp;
        double dx;
        double f_res = f(x);
        for(int i=0;i<x.size;i++){
            dx=Abs(x[i])*EPS;
            if(Abs(x[i])<Sqrt(EPS)) dx=EPS;

            x_temp = x.copy();
            x_temp[i] += dx; 
            gradientF[i] = (f(x_temp)-f_res)/dx;
        }
        return gradientF;
    }

    public static int qnewton(
	Func<vector,double> f, /* objective function */
	ref vector x, /* starting point */
	double eps /* accuracy goal, on exit |gradient| should be <eps */
    ){
        int maxStep = 1000;
        matrix B,DeltaB;
        vector deltaX,gradientFx,gradientFxs,s,y,u;
        double lam = 1,fx;
        double alpha = 1e-4;
        B = new matrix(x.size,x.size);
        B.set_unity();
        gradientFx = gradient(f,x);
        int numSteps =0;
        while(numSteps<maxStep){
            numSteps ++;
            deltaX = -B*gradientFx;
            if(deltaX.norm()<EPS*x.norm()){
			    Error.Write($"SR1: |Dx|<EPS*|x|\n");
			    break;
			}
		    if(gradientFx.norm()<eps){
			    Error.Write($"SR1: |gx|<acc\n");
			    break;
			}
            if(numSteps==maxStep){
			    Error.Write($"SR1: Ended after maxsteps = {0}\n",maxStep);
			}

            lam = 1.0;
            s = lam*deltaX;
            fx = f(x);
            while(true){
                lam /=2;
                s = lam*deltaX;
                if (f(x+s)<fx)         break;
                if (lam<EPS){
                    B.set_unity();
                    break;
                }
            }
            x +=s;
            gradientFxs = gradient(f,x);
            y = gradientFxs-gradientFx;
            u = s-B*y;
            double uTy =u.dot(y); 
            if (Abs(uTy)>1e-6){
                B = B + outer(u, u)/uTy;
            }
            gradientFx = gradientFxs;
        }

    return numSteps;


    }
}
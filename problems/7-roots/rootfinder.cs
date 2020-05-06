using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

using static vector;
using static matrix;
using static qr_gs;
public partial class rootfinder{
    public static vector newton(
	Func<vector,vector> f,
	vector x,
	double epsilon=1e-3,
	double dx=1e-7)
    {
        vector x_temp,f_res,delta_x,dfdx;
        double lam;
        x = x.copy();
        int n = x.size;
        matrix J = new matrix(n,n);
        matrix R;
        f_res = f(x);
        
        delta_x = new vector(n);
        delta_x[0] = 10+dx*2;
        while(f_res.norm()>epsilon && max(abs(delta_x))>dx){
            for(int k=0;k<n;k++){
                x_temp = x.copy();
                x_temp[k] += dx; 
                dfdx = (f(x_temp)-f_res)/dx;
                // J[k] = dfdx;
                for(int i=0;i<n;i++){
                    J[i,k] = dfdx[i];
                }    
            }

            R = new matrix(n,n);
            qr_gs_decomp(J,R);
            delta_x = qr_gs_solve(J,R,-1.0*f_res);

            lam = 1.0;
            while(f(x+lam*delta_x).norm()>(1-lam/2)*f_res.norm() && lam>1.0/64){
                lam /=2;
            }
            x = x+lam*delta_x;
            f_res = f(x);
            }

            return x;
    }
}
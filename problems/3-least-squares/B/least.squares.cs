using static System.Math;
using System;
using static System.Console;
using static qr_gs;

public partial class least_squares{
    // Part A
    static public Tuple<vector,vector,matrix> least_squares_fit(vector x, vector y, vector dy, Func<double,double>[] f){
        var A = new matrix(x.size,f.Length);

        for(int i=0;i<x.size;i++){
            for(int j=0;j<f.Length;j++){
                A[i,j] = f[j](x[i])/dy[i];
            }
        }
        var b = y/dy;
        
        var R = new matrix(A.size2,A.size2);
        qr_gs_decomp(A,R);
        
        var inverse_R = new matrix(R.size1,R.size2);
        var e = new vector(R.size2);
        A.print("Q");
        (A.T*A).print("QTQ");
        R.print("R");
        
        for(int i = 0; i<R.size1;i++){
                e[i]=1;
                backsub(R, e).print("R back");
                inverse_R[i] = backsub(R, e);
                e[i]=0;
        }
        inverse_R.print("R_inverse");

        var sigma = inverse_R*inverse_R.T;
        // var sigma = R.T*R;
        // var R_sigma = new matrix(sigma.size1,sigma.size2);
        // qr_gs.qr_gs_decomp(sigma,R_sigma);
        
        // sigma = qr_gs.qr_gs_inverse(sigma,R_sigma);

        var c = qr_gs_solve(A,R,b);
        var Delta_c = new vector(c.size);
        for(int i =0;i<c.size;i++) 
            Delta_c[i] = Sqrt(sigma[i,i]);
        return Tuple.Create(c,Delta_c,sigma);
    }



}


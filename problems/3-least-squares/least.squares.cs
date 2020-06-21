using static System.Math;
using System;
using static System.Console;
using static qr_gs;

public partial class least_squares{
    public vector c;
    public vector Delta_c;
    public matrix sigma;
    // Part A
    public least_squares(vector x, vector y, vector dy, Func<double,double>[] f){
        var A = new matrix(x.size,f.Length);

        // Featuretransform scaled by uncertainty
        for(int i=0;i<x.size;i++){
            for(int j=0;j<f.Length;j++){
                A[i,j] = f[j](x[i])/dy[i];
            }
        }
        var b = y/dy;
        
        var R = new matrix(A.size2,A.size2);
        qr_gs_decomp(A,R); // A is now Q in QR decomposition
        
        var inverse_R = new matrix(R.size1,R.size2);
        var e = new vector(R.size2); // Unitvector for inverting R
        
        // Calculate iverse of A
        for(int i = 0; i<R.size1;i++){
                e[i]=1;
                inverse_R[i] = backsub(R, e);
                e[i]=0;
        }

        sigma = inverse_R*inverse_R.T; //covariance matrix
        c = qr_gs_solve(A,R,b); //Best fit
        Delta_c = new vector(c.size); //Fit uncertatny
        for(int i =0;i<c.size;i++) 
            Delta_c[i] = Sqrt(sigma[i,i]);
    }




}


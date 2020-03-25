using static System.Math;
using System;
using static System.Console;

public class least_squares{
    // Part A
    static public vector least_squares_fit(vector x, vector y, vector dy, Func<double,double>[] f){
        var A = new matrix(x.size,f.Length);

        for(int i=0;i<x.size;i++){
            for(int j=0;j<f.Length;j++){
                A[i,j] = f[j](x[i])/dy[i];
            }
        }
        var b = y/dy;
        A.print("A ");
        b.print("b: ");
        
        var R = new matrix(A.size1,A.size2);
        qr_gs.qr_gs_decomp(A,R);
        var c = qr_gs.qr_gs_solve(A,R,b);
        c.print("c: ");

        return c;

    }

}


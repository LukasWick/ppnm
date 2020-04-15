using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){

    // Part A

    WriteLine("Part 1: Test");
	var A= new matrix("3 2 4;2 0 2;4 2 3");
    // var A= new matrix("3 2 4;2 0 2;4 2 12");
    var V = new matrix(A.size1,A.size2);
    A.print("A");
    
    matrix D = diag_cyclic_complete(A,V);
    (V*D*V.T).print("VDVT");

    V.print("V");
    D.print("D");

    (V.T*A*V).print("VTAV");
    
    // part 2
    int n=100;
    double s=1.0/(n+1);
    matrix H = new matrix(n,n);
    for(int i=0;i<n-1;i++){
        H[i,i]=-2;
        H[i,i+1]=1;
        H[i+1,i]=1;
    }
    H[n-1,n-1]=-2;
    H = H/(-s*s);
    V = new matrix(n,n);
    vector eigenvals = diag_cyclic(H,V);
    
    System.IO.StreamWriter outputfile_A_E = new System.IO.StreamWriter("out.plotA.E.txt",append:false);
    for (int k=0; k < n/3; k++){
        double exact = PI*PI*(k+1)*(k+1);
        double calculated = eigenvals[k];
        outputfile_A_E.WriteLine($"{k} {calculated} {exact}");
    }
    outputfile_A_E.Close();


    System.IO.StreamWriter outputfile_A_psi = new System.IO.StreamWriter("out.plotA.psi.txt",append:false);
    double psi=0;

    for(int k=0;k<5;k++){
        outputfile_A_psi.WriteLine($"{0} {0} {0}");
        for(int i=0;i<n;i++){
            double factor=Sign(V[0,k])/Sqrt(s);
            outputfile_A_psi.WriteLine($"{(i+1.0)/(n+1)} {V[i,k]*factor} {psi}");
        }
        outputfile_A_psi.WriteLine($"{1} {0} {0}\n");


        
  }
  outputfile_A_psi.Close();
  }
}

using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){

    // Part A
    var rnd = new Random();
    var A = new matrix(3,3);
    for(int i=0;i<A.size1;i++){
        for(int j=i;j<A.size2;j++){
            double rndnumber = 10*(rnd.NextDouble()-0.5); 
            A[i,j]=rndnumber;
            A[j,i]=rndnumber;
        }
    }
	// var A= new matrix("3 2 4;2 0 2;4 2 3");
    // var A= new matrix("3 2 4;2 0 2;4 2 12");
    var V = new matrix(A.size1,A.size2);
    WriteLine("\nQuestion A \nPart 1: Test Jacobi diagonalization with cyclic sweeps");
    WriteLine("Random symmetric matrix:");

    A.print("A = ");
    matrix D = diag_cyclic_complete(A,V);
    WriteLine("Do diagonalization with cyclic sweeps:");
    WriteLine("Eigen values");
    D.print("D");
    WriteLine("Eigenvectors:");
    V.print("V");   
    WriteLine("Test decomposition:");
    (V*D*V.T).print("A=VDV^T");
    WriteLine("Test eigen vectors:");
    (V.T*A*V).print("VTAV");
    WriteLine("\n -----------------------------\nPart 2:");
    WriteLine("See the energy as function of n for the hamiltonian in PlotA.E.svg and the eigenfunction (wavefunctions) in PlotA.psi.svg.");
    WriteLine("\n\n");
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
    vector eigenvals = new vector(n);
    diag_cyclic(H,V,eigenvals);
    
    System.IO.StreamWriter outputfile_A_E = new System.IO.StreamWriter("out.plotA.E.txt",append:false);
    for (int k=0; k < n/3; k++){
        double exact = PI*PI*(k+1)*(k+1);
        double calculated = eigenvals[k];
        outputfile_A_E.WriteLine($"{k} {calculated} {exact}");
    }
    outputfile_A_E.Close();


    System.IO.StreamWriter outputfile_A_psi = new System.IO.StreamWriter("out.plotA.psi.txt",append:false);

    for(int k=0;k<5;k++){
        outputfile_A_psi.WriteLine($"{0} {0} {0}");
        for(int i=0;i<n;i++){
            double factor=Sign(V[0,k])/Sqrt(s);
            double psi=V[i,k]*factor;
            double x = (i+1.0)/(n+1);
            outputfile_A_psi.WriteLine($"{x} {psi}");
        }
        outputfile_A_psi.WriteLine($"{1} {0} \n");


        
  }
  outputfile_A_psi.Close();
  }
}

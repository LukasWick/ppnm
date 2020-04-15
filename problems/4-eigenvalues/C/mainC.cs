using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){
  
    // Part 2
    var rnd = new Random(1);
    int n = 5;
    matrix v_c = new matrix(n,n);
    matrix v_classic = new matrix(n,n);

    var A_c = new matrix(n,n);
    for(int i=0;i<n;i++){
        for(int j=i;j<n;j++){
            A_c[i,j]=2*(rnd.NextDouble()-0.5); 
            A_c[j,i]=A_c[i,j];
        }
    }

    var A_classic = A_c.copy();
    vector e_c = diag_cyclic(A_c,v_c);
    vector e_classic = diag_classic(A_classic,v_classic);
    A_c.print("A c transformed");
    A_classic.print("A classic transformed");
    e_c.print("Eigenvalues calculated with cyclic:");
    e_classic.print("Eigenvalues calculated with classic:");
    
    v_c.print("Eigenvectors calculated with cyclic:");
    v_classic.print("Eigenvectors calculated with classic:");


    // Part 3 and 4
    System.IO.StreamWriter outputfile_C  = new System.IO.StreamWriter("out.plotC.txt",append:false);

    var N = 200;
    var n0 = 20;
    for(n=n0;n<N;n+=5){
        Stopwatch sw_c = new Stopwatch();
        Stopwatch sw_classic = new Stopwatch();

        v_c = new matrix(n,n);
        v_classic = new matrix(n,n);

        A_c = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                A_c[i,j]=2*(rnd.NextDouble()-0.5); 
                A_c[j,i]=A_c[i,j];
            }
        }

        A_classic = A_c.copy();

        sw_c.Start();
        diag_cyclic(A_c,v_c);
        sw_c.Stop();

        sw_classic.Start();
        diag_classic(A_classic,v_classic);
        sw_classic.Stop();

        outputfile_C.WriteLine("{0} {1} {2}",Log(n),Log(sw_c.ElapsedMilliseconds),Log(sw_classic.ElapsedMilliseconds));

    }
    outputfile_C.Close();

}
}

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
    vector e_c = new vector(n);
    vector e_classic = new vector(n);
    var A_c = new matrix(n,n);
    for(int i=0;i<n;i++){
        for(int j=i;j<n;j++){
            A_c[i,j]=2*(rnd.NextDouble()-0.5); 
            A_c[j,i]=A_c[i,j];
        }
    }
    WriteLine("\n\nQuestion C:");
    WriteLine("Random matrix");
    A_c.print("A = ");

    var A_classic = A_c.copy();
    diag_cyclic(A_c,v_c,e_c);
    diag_classic(A_classic,v_classic,e_classic);


    A_c.print("A cyclic transformed");
    A_classic.print("A classic transformed");
    e_c.print("Eigenvalues calculated with cyclic       :  ");
    e_classic.print("Eigenvalues calculated with classic:  ");
    
    v_c.print("Eigenvectors calculated with cyclic       : ");
    v_classic.print("Eigenvectors calculated with classic: ");


    // Part 3 and 4
    System.IO.StreamWriter outputfile_C_cyc  = new System.IO.StreamWriter("out.plotC.time.data",append:false);
    System.IO.StreamWriter outputfile_C_rot  = new System.IO.StreamWriter("out.plotC.rotations.data",append:false);

    var N = 200;
    var n0 = 20;
    for(n=n0;n<N;n+=(int) Floor(n*0.25)){
        Stopwatch sw_c = new Stopwatch();
        Stopwatch sw_classic = new Stopwatch();

        v_c = new matrix(n,n);
        v_classic = new matrix(n,n);
        e_c = new vector(n);
        e_classic = new vector(n);
        
        A_c = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                A_c[i,j]=2*(rnd.NextDouble()-0.5); 
                A_c[j,i]=A_c[i,j];
            }
        }

        A_classic = A_c.copy();

        sw_c.Start();
        int rotations_cyclyc = diag_cyclic(A_c,v_c,e_c);
        sw_c.Stop();

        sw_classic.Start();
        int rotations_classic = diag_classic(A_classic,v_classic,e_classic);
        sw_classic.Stop();

        outputfile_C_cyc.WriteLine("{0} {1} {2}",n,sw_c.ElapsedMilliseconds,sw_classic.ElapsedMilliseconds);
        outputfile_C_rot.WriteLine("{0} {1} {2}",n,rotations_cyclyc,rotations_classic);

    }
    outputfile_C_cyc.Close();
    outputfile_C_rot.Close();

    WriteLine("From fig PlotC.svg it can be seen that the classic version is sligthly faster");
}
}

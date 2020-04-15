using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){
  
    // Part 2
     WriteLine("Opg b,2");

    var rnd = new Random(1);
    int n = 5;
    matrix v_c = new matrix(n,n);
    matrix v_vbv = new matrix(n,n);

    var A_c = new matrix(n,n);
    for(int i=0;i<n;i++){
        for(int j=i;j<n;j++){
            A_c[i,j]=2*(rnd.NextDouble()-0.5); 
            A_c[j,i]=A_c[i,j];
        }
    }

    var A_vbv = A_c.copy();
    vector e_c = diag_cyclic(A_c,v_c);
    vector e_vbv = diag_lowest(A_vbv,v_vbv,3);

    WriteLine("Opg B,b");
    A_c.print("A c transformed");
    A_vbv.print("A value by value transformed");
    
    e_c[0,3].print("Eigenvalues calculated with cyclic:");
    A_vbv.print("Eigenvalues calculated with value by value:");
    
    v_c.print("Eigenvectors calculated with cyclic:");
    v_vbv.print("Eigenvectors calculated with value by value:");
    WriteLine("As the cyclic method gives the eigenvalues sorted by the lowest first and value by value gives the same, value by value gives the lowest eigenvalues first");


    // Part 3 and 4
    System.IO.StreamWriter outputfile_B  = new System.IO.StreamWriter("out.plotB34.txt",append:false);

    var N = 100;
    var n0 = 15;
    for(n=n0;n<N;n+=2){
        Stopwatch sw_c = new Stopwatch();
        Stopwatch sw_vbv1 = new Stopwatch();
        Stopwatch sw_vbvn = new Stopwatch();

        v_c = new matrix(n,n);
        var v_vbv1 = new matrix(n,n);
        var v_vbvn = new matrix(n,n);

        A_c = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                A_c[i,j]=2*(rnd.NextDouble()-0.5); 
                A_c[j,i]=A_c[i,j];
            }
        }

        var A_vbv1 = A_c.copy();
        var A_vbvn = A_c.copy();

        sw_c.Start();
        diag_cyclic(A_c,v_c);
        sw_c.Stop();

        sw_vbv1.Start();
        diag_lowest(A_vbv1,v_vbv1,1);
        sw_vbv1.Stop();

        sw_vbvn.Start();
        diag_lowest(A_vbvn,v_vbvn,n);
        sw_vbvn.Stop();

        outputfile_B.WriteLine("{0} {1} {2} {3}",n,Log(sw_c.ElapsedMilliseconds),Log(sw_vbv1.ElapsedMilliseconds),Log(sw_vbvn.ElapsedMilliseconds));
        }
        outputfile_B.Close();

        // Part 5
        n = 4;
        var A_lowest_first = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                A_lowest_first[i,j]=2*(rnd.NextDouble()-0.5); 
                A_lowest_first[j,i]=A_lowest_first[i,j];
            }
        }
        matrix v_lowest = new matrix(n,n);
        matrix v_highest= new matrix(n,n);

        var A_highest_first = A_lowest_first.copy();
        vector e_lowest = diag_cyclic(A_lowest_first,v_lowest);
        vector e_highest = diag_cyclic_highest_first(A_highest_first,v_highest);
        WriteLine("Opg b,5");
        e_lowest.print("D sorted by lovest firts");
        e_highest.print("D sorted by highest firts");


}
}

using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){
    WriteLine("\n\nQuestion B");
    WriteLine("Question B 1: See figure PlotB1.svg and PlotB2.svg");

    // Part 2
    WriteLine("Question B 2");
    WriteLine("Question B 2,a");
    WriteLine("Random matrix A:");

    Random rnd = new Random(1);
    int n = 4;
    matrix v_c = new matrix(n,n);
    matrix v_vbv1 = new matrix(n,n);
    matrix v_vbv2 = new matrix(n,n);

    matrix A = new matrix(n,n);
    for(int i=0;i<n;i++){
        for(int j=i;j<n;j++){
            A[i,j]=2*(rnd.NextDouble()-0.5); 
            A[j,i]=A[i,j];
        }
    }
    vector e_c = new vector(A.size1);
    vector e_vbv1 = new vector(A.size1);
    vector e_vbv2 = new vector(A.size1);

    A.print("A = ");
    matrix A_c = A.copy();
    matrix A_vbv1 = A.copy();
    matrix A_vbv2 = A.copy();
    diag_cyclic(A_c,v_c,e_c);
    diag_frist_n(A_vbv1,v_vbv1,e_vbv1,1);
    diag_frist_n(A_vbv2,v_vbv2,e_vbv2,2);

    A_vbv1.print("A with first row eliminated ");
    WriteLine("Eigenvalues calculated with value by value:  {0}",e_vbv1[0]);

    A_c.print("A cyclic transformed");
    WriteLine("First eigenvalue calculated with cyclic:     {0}", e_c[0]);
    
    WriteLine("The first Eigenvector dose also agree");
    v_c[0].print("Eigenvectors calculated with cyclic:           ");
    v_vbv1[0].print("Eigenvector calculated with value by value: ");

    WriteLine("Question B 2,b");
    A_vbv2.print("A with first and second row eliminated ");
    WriteLine("Second eigenvalue calculated with value by value:  {0}",e_vbv2[1]);
    WriteLine("Second eigenvalue calculated with cyclic:          {0}", e_c[1]);
    WriteLine("You will only change the row above via line 2 and 3 in equation 10, and these only depend on the values of the row above wich are zero");



    WriteLine("As the cyclic method gives the eigenvalues sorted by the lowest first and value by value gives the same, value by value gives the lowest eigenvalues first");


    // Part 3 and 4
    System.IO.StreamWriter outputfile_Bvbv1  = new System.IO.StreamWriter("out.plotB34vbv1.data",append:false);
    System.IO.StreamWriter outputfile_Bvbvn  = new System.IO.StreamWriter("out.plotB34vbvn.data",append:false);
    System.IO.StreamWriter outputfile_Bcyclic  = new System.IO.StreamWriter("out.plotB34cyclic.data",append:false);

    var N1val = 600;
    var Nvalbyval = 90;
    var Ncyclic = 300;
    var N01val = 40;
    var N0valbyval = 20;
    var N0cyclic = 20;

    
    for(n=Min(Min(N01val,N0valbyval),N0cyclic);n<Max(Max(N1val,Nvalbyval),Ncyclic);n+=(int) Floor(n*0.20)){
        Stopwatch sw_c = new Stopwatch();
        Stopwatch sw_vbv1 = new Stopwatch();
        Stopwatch sw_vbvn = new Stopwatch();


        e_c = new vector(n);
        e_vbv1 = new vector(n);
        vector e_vbvn = new vector(n);

        v_c = new matrix(n,n);
        v_vbv1 = new matrix(n,n);
        var v_vbvn = new matrix(n,n);

        A_c = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                A_c[i,j]=2*(rnd.NextDouble()-0.5); 
                A_c[j,i]=A_c[i,j];
            }
        }

        A_vbv1 = A_c.copy();
        var A_vbvn = A_c.copy();

        


        if(n<Nvalbyval&&n>N0valbyval){ //As the runtime is very slow this is only evaluated on matrices of size below 90
            sw_vbvn.Start();
            diag_frist_n(A_vbvn,v_vbvn,e_vbvn,n);
            sw_vbvn.Stop();
            outputfile_Bvbvn.WriteLine("{0} {1}",n,sw_vbvn.ElapsedMilliseconds);
        }

        if(n<Ncyclic&&n>N0cyclic){ //As the runtime is very slow this is only evaluated on matrices of size below 90
            sw_c.Start();
            diag_cyclic(A_c,v_c,e_c);
            sw_c.Stop();
            outputfile_Bcyclic.WriteLine("{0} {1}",n,sw_c.ElapsedMilliseconds);
        }

        if(n<N1val&&n>N01val){
            sw_vbv1.Start();
            diag_frist_n(A_vbv1,v_vbv1,e_vbv1,1);
            sw_vbv1.Stop();
            outputfile_Bvbv1.WriteLine("{0} {1}",n,sw_vbv1.ElapsedMilliseconds);
        }
        }
        outputfile_Bvbv1.Close();
        outputfile_Bvbvn.Close();
        outputfile_Bcyclic.Close();

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
        vector e_lowest = new vector(n);
        vector e_highest = new vector(n);
        diag_cyclic(A_lowest_first,v_lowest,e_lowest);
        diag_cyclic(A_highest_first,v_highest,e_highest,true);
        WriteLine("Opg for B 3 and 4 see figure PlotB34.svg");

        WriteLine("Opg B 5");
        WriteLine("Change both signs in the atan2. When the first parameter is zero this gives pi instead of 0, wich makes it sort in the opposite direction");
        e_lowest.print("D sorted by lowest first:  ");
        e_highest.print("D sorted by highest first: ");
        WriteLine("\n\n");

}
}

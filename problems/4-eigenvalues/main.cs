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
    A.print("A transformed");

    A.print("A back");
    (V*D*V.T).print("VDVT");

    V.print("V");
    D.print("D");

    (V.T*A*V).print("VTAV");

    WriteLine("Part 2: Particel in a box");
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
    
    for (int k=0; k < n/3; k++){
        double exact = PI*PI*(k+1)*(k+1);
        double calculated =eigenvals[k];
        WriteLine($"{k} {calculated} {exact}");
    
    }

    System.IO.StreamWriter outputfile_A = new System.IO.StreamWriter("out.plotA.txt",append:false);
    double psi=0;

    for(int k=0;k<5;k++){
        outputfile_A.WriteLine($"{0} {0} {0}");
        for(int i=0;i<n;i++){
            // if((k+1)%2 ==0)
            //     psi = Sqrt(2.0/20)*Sin(PI*(k+1)*((i+1.0)/(n+1)+0.5));
            // if((k)%2 ==0)
            //     psi = Sqrt(2.0/20)*Cos(PI*(k+1)*((i+1.0)/(n+1)-0.5));
            outputfile_A.WriteLine($"{(i+1.0)/(n+1)} {V[i,k]*Sign(V[0,k])} {psi}");
        }
        outputfile_A.WriteLine($"{1} {0} {0}\n");


        
  }
  outputfile_A.Close();
  
    // Part B
    System.IO.StreamWriter outputfile_B = new System.IO.StreamWriter("out.plotB1.txt",append:false);

    Stopwatch sw = new Stopwatch();
    var rnd = new Random(1);
    var N = 100;
    var n0 = 15;
    for(n=n0;n<N;n+=2){
        sw.Start();
        matrix v = new matrix(n,n);
        var Arnd = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                Arnd[i,j]=2*(rnd.NextDouble()-0.5); 
                Arnd[j,i]=Arnd[i,j];
            }
        }
        vector e = diag_cyclic(Arnd,v);

        sw.Stop();

        outputfile_B.WriteLine("{0} {1} {2} {3}",Log(n),Log(sw.ElapsedMilliseconds),3*(Log(n)-Log(n0)),4*(Log(n)-Log(n0)));

    }
    outputfile_B.Close();





}
}

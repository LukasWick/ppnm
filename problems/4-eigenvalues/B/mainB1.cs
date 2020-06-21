using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){
  
    // Part B
    System.IO.StreamWriter outputfile_B = new System.IO.StreamWriter("out.plotB1.data",append:false);
    var rnd = new Random(1);
    var N = 100;
    var n0 = 25;
    for(int n=n0;n<N;n+=2){
        Stopwatch sw = new Stopwatch();
        matrix v = new matrix(n,n);
        var Arnd = new matrix(n,n);
        for(int i=0;i<n;i++){
            for(int j=i;j<n;j++){
                Arnd[i,j]=2*(rnd.NextDouble()-0.5); 
                Arnd[j,i]=Arnd[i,j];
            }
        }
        vector d = new vector(Arnd.size1);
        sw.Start();
        int rotations = diag_cyclic(Arnd,v,d);
        sw.Stop();

        outputfile_B.WriteLine("{0} {1} {2} {3}",n,sw.ElapsedMilliseconds,Pow(10,3*(Log10(n)-Log10(n0))),rotations);

    }
    outputfile_B.Close();



}
}

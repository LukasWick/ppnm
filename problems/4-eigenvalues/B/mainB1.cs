using static System.Console;
using static System.Math;

using System;
using System.Diagnostics;

using static jacobi;
class main{
static void Main(){
  
    // Part B
    System.IO.StreamWriter outputfile_B = new System.IO.StreamWriter("out.plotB1.txt",append:false);
    var rnd = new Random(1);
    var N = 100;
    var n0 = 15;
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
        sw.Start();
        vector e = diag_cyclic(Arnd,v);
        sw.Stop();

        outputfile_B.WriteLine("{0} {1} {2}",Log(n),Log(sw.ElapsedMilliseconds),3*(Log(n)-Log(n0)));

    }
    outputfile_B.Close();



}
}

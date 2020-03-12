using static System.Math;
using static System.Console;
class main{
    public static int Main(){
        
        int n = 14;
        double x1 = 0, xend = 2*2*PI; 

        vector xs = new vector(n);
        vector ys = new vector(n);

        System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out-xydata.txt",append:false);
        for(int i=0;i<n;i++){
            xs[i] = x1+(xend-x1)/(n-1)*i;
            ys[i] = Cos(xs[i]);
            outputfile.WriteLine("{0} {1}",xs[i],ys[i]);
        }
   		outputfile.Close();

        int N = 2000;
        double z=0;
        double z1 = x1;
        double zend = xend;
        System.IO.StreamWriter  outputfileLinterp = new System.IO.StreamWriter("out-linterp.txt",append:false);
        System.IO.StreamWriter  outputfileIntegral = new System.IO.StreamWriter("out-integral.txt",append:false);

        for(int i=0;i<N;i++){
            z = z1+(zend-z1)/(N-1)*i;
            outputfileLinterp.WriteLine("{0} {1}",z,linspline.linterp(xs,ys,z));
            outputfileIntegral.WriteLine("{0} {1} {2}",z,linspline.linterpInteg(xs,ys,z),Sin(z));
        }
        outputfileLinterp.Close();
        outputfileIntegral.Close();
        
    return 0;

    }
}

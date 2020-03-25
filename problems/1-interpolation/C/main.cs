using static System.Math;
using static System.Console;
class main{
    public static int Main(){
        
        int n = 6;
        double x1 = 0, xend = 2*PI; 

        vector xs = new vector(n);
        vector ys = new vector(n);

        System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out-xydata.txt",append:false);
        for(int i=0;i<n;i++){
            xs[i] = x1+(xend-x1)/(n-1)*i;
            ys[i] = Sin(xs[i]);
            outputfile.WriteLine("{0} {1}",xs[i],ys[i]);
        }
   		outputfile.Close();

        int N = 2000;
        double z=0;
        double z1 = x1;
        double zend = xend;
        System.IO.StreamWriter  outputfileSpline = new System.IO.StreamWriter("out-Cspline.txt",append:false);
        System.IO.StreamWriter  outputfileIntegral = new System.IO.StreamWriter("out-CIntegral.txt",append:false);
        System.IO.StreamWriter  outputfileDerivative = new System.IO.StreamWriter("out-CDerivative.txt",append:false);

        cspline spline = new cspline(xs,ys);
        for(int i=0;i<N;i++){
            z = z1+(zend-z1)/(N-1)*i;
            outputfileSpline.WriteLine("{0} {1} {2}",z,spline.spline(z),Sin(z));
            outputfileIntegral.WriteLine("{0} {1} {2}",z,spline.integral(z),1-Cos(z));
            outputfileDerivative.WriteLine("{0} {1} {2}",z,spline.derivative(z),Cos(z));
        }
        outputfileSpline.Close();
        outputfileIntegral.Close();
        outputfileDerivative.Close();
    return 0;

    }
}

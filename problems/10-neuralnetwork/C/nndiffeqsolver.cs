using System;
using static System.Math;
using static System.Console;

using System.Collections.Generic;
using static vector;
using static minimization;
using static integrator;

public partial class  nnde {
	int n; /* number of hidden neurons */
	Func<double,double> f; /* activation function */
    Func<double,double> fm; /* deriv activation function */
    Func<double,double> fmm; /* second. deriv activation function */
    vector param; /* (a,b,w) */
    int nxs; /* Number of integration points */
	double[] xs; /* Integration points */
    private void init(int m){
        Random rand = new Random();
        n = m;
        nxs = 64;
        xs  = new double[nxs];
        // f = (x) => Cos(5*x)*Exp(-x*x);
        f = (x) => x*Exp(-x*x);
        fm = (x) => Exp(-x*x)-x*2*x*Exp(-x*x);
        fmm = (x) => -2*x*Exp(-x*x)-4*x*Exp(-x*x)+4*x*x*x*Exp(-x*x);
        
        param = new vector(n*3);
        for(int i = 0; i<n*3;i++){
            double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0-rand.NextDouble();
            double randStdNormal = Sqrt(-2.0 * Log(u1)) *Sin(2.0 * PI * u2);//random normal(0,1)
            param[i] = randStdNormal*0.5;
        }     
    }
    public nnde(int m){
        init(m);
        }

	public double feedforwad(double x){
        double y = 0;
        for(int i=0;i<n;i++)
            y += f((x-param[i*3])/param[i*3+1])*param[i*3+2];
        return y;
    } /* apply the network to input parameter x */
	public void train(Func<double,double,double,double,double> phi, double a, double b,double c, double yc, double ymc){
        
        for(int i = 0; i<n;i++){
            double ai=a+(b-a)*i/(n-1);
            double bi=5;
            double wi=1;
            param[3*i+0]=ai;
            param[3*i+1]=bi;
            param[3*i+2]=wi;
        }
        for(int i =0;i<nxs;i++){
            xs[i] =  a+(b-a)*i/(nxs-1);
        }
        
        Func<vector,double> cost = (p) => {
            param = p;
            Func<double,double> integrant = (x) => 
                Pow(phi(x,feedforwad(x),derivative(x),secondderivative(x)),2);
            
            // double costSum  = adapt(integrant,a,b,1e-4,1e-4);
            double costSum = 0;
            for(int i =0;i<nxs;i++){
                costSum += integrant(xs[i]);
            }
            costSum *= (b-a)/(n-1);
            costSum += Pow(feedforwad(c)-yc,2)*(b-a);
            costSum += Pow(derivative(c)-ymc,2)*(b-a);
            // for(int i=0;i<n;i++)
            //     costSum += 0.001*(Pow(1/param[i*3+1],2) + Pow(param[i*3+2],2));//Weight decay (makes sure the varibels don't go crazy)
            WriteLine($"Cost: {costSum}");
            return costSum;
        };
        double eps = 1e-5;
        vector pa = param.copy();
        qnewton(cost, ref pa,eps);
        param = pa;

    } /* train to interpolate the given table {x,y} */
    public double derivative(double x){
        double y = 0;
        for(int i=0;i<n;i++){
            double a=param[3*i+0];
            double b=param[3*i+1];
            double w=param[3*i+2];
            	y += w*fm((x-a)/b)/b;
		}
        return y;
    }
    
    public double secondderivative(double x){
        double y = 0;
        for(int i=0;i<n;i++){
            double a=param[3*i+0];
            double b=param[3*i+1];
            double w=param[3*i+2];
            y += w*fmm((x-a)/b)/b/b;
		}
        return y;
    }
    


}
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
    vector param;
    private void init(int m){
        Random rand = new Random();
        n = m; 
        // f = (x) => Cos(5*x)*Exp(-x*x);
        f = (x) => Exp(-x*x);
        
        param = new vector(n*3);
        for(int i = 0; i<n*3;i++){
            double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0-rand.NextDouble();
            double randStdNormal = Sqrt(-2.0 * Log(u1)) *Sin(2.0 * PI * u2);//random normal(0,1)
            param[i] = randStdNormal*0.5;
        }     
    }
    public nnde(int m){init(m);}

	public double feedforwad(double x){
        double y = 0;
        for(int i=0;i<n;i++)
            y += f((x-param[i*3])/param[i*3+1])*param[i*3+2];
        return y;
    } /* apply the network to input parameter x */
	public void train(Func<double,double,double,double,double> phi, double a, double b,double c, double yc, double ymc){
        
        Func<vector,double> cost = (p) => {
            param = p;
            Func<double,double> integrant = (x) => Pow(phi(x,feedforwad(x),derivative(x),doublederivative(x)),2);
            
            double costSum  = adapt(integrant,a,b,1e-3,1e-3);
            
            costSum += Pow(feedforwad(c)-yc,2)*(b-a);
            costSum += Pow(derivative(c)-ymc,2)*(b-a);
            // for(int i=0;i<n;i++)
            //     costSum += 0.001*(Pow(1/param[i*3+1],2) + Pow(param[i*3+2],2));//Weight decay (makes sure the varibels don't go crazy)
            WriteLine(costSum);
            return costSum;
        };
        double eps = 1e-15;
        vector pa = param.copy();
        qnewton(cost, ref pa,eps);
        param = pa;

    } /* train to interpolate the given table {x,y} */
	public double derivative(double x){
        double y = 0;
        for(int i=0;i<n;i++)
            y += -2*(x-param[i*3])/(Pow(param[i*3+1],2))*param[i*3+2]* f((x-param[i*3])/param[i*3+1]);
        return y;
    }
    
    public double doublederivative(double x){
        double y = 0;
        for(int i=0;i<n;i++)
            y += param[i*3+2]*f((x-param[i*3])/param[i*3+1])*(-2/Pow(param[i*3+1],2) +4*Pow((x-param[i*3])/(Pow(param[i*3+1],2)),2));
        return y;
    }
    


}
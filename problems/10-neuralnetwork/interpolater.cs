using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using static vector;
using static minimization;
public partial class  ann {
	int n; /* number of hidden neurons */
	Func<double,double> f; /* activation function */
  	Func<double,double> fm; /* derivative of activation function */
	Func<double,double> F; /* anti derivative of activation function */

    vector param;
    private void init(int m){
        Random rand = new Random();
        n = m; 
        // f = (x) => Cos(5*x)*Exp(-x*x);
        f = (x) => x*Exp(-x*x);
        fm = (x) => Exp(-x*x)-x*2*x*Exp(-x*x);
        F = (x) => -1.0/2*Exp(-x*x);

        param = new vector(n*3);
        for(int i = 0; i<n*3;i++){
            double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0-rand.NextDouble();
            double randStdNormal = Sqrt(-2.0 * Log(u1)) *Sin(2.0 * PI * u2);//random normal(0,1)
            param[i] = randStdNormal*0.5;
        }     
    }
    public ann(int m){init(m);}
    public ann() {init(3);}
    

	public double feedforwad(double x){
        double y = 0;
        for(int i=0;i<n;i++)
            y += f((x-param[i*3])/param[i*3+1])*param[i*3+2];
        return y;
    } /* apply the network to input parameter x */
	public void train(vector x,vector y){
         for(int i = 0; i<n;i++){
            double ai=x[0]+(x[-1]-x[0])*i/(x.size-1);
            double bi=5;
            double wi=1;
            param[3*i+0]=ai;
            param[3*i+1]=bi;
            param[3*i+2]=wi;
        }

        Func<vector,double> cost = (p) => {
            param = p;
            double costSum = 0;
            for (int i =0; i<x.size;i++){
                double ypred = feedforwad(x[i]);
                costSum += Pow(ypred-y[i],2);
            }
            // for(int i=0;i<n;i++)
            //     costSum += 0.001*(Pow(1/param[i*3+1],2) + Pow(param[i*3+2],2)); //Weight decay (makes sure the varibels don't go crazy)
            return costSum;
        };
                double eps = 1e-7;
        vector pa = param.copy();
        qnewton(cost, ref pa,eps);
        param = pa;

    } /* train to interpolate the given table {x,y} */
	public double derivative(double x){
        double y = 0;
        for(int i=0;i<n;i++){
            // y += -2*(x-param[i*3])/(Pow(param[i*3+1],2))*param[i*3+2]* f((x-param[i*3])/param[i*3+1]);
            double a=param[3*i+0];
            double b=param[3*i+1];
            double w=param[3*i+2];
            y+=w*fm((x-a)/b)/b;
            } 
        return y;
    }
    
    public double antiderivative(double x,double x0){
        double y = 0;
        for(int i=0;i<n;i++){
            // double erf1 = erf((x0-param[i*3])/param[i*3+1]);
            // double erf2 = erf((x-param[i*3])/param[i*3+1]);
            // y += param[i*3+1]*param[i*3+2]*Sqrt(PI)/2*(-erf1+erf2);
            double a=param[3*i+0];
            double b=param[3*i+1];
            double w=param[3*i+2];
            y += w*F((x-a)/b)*b-w*F((x0-a)/b)*b;
        }
        return y;
    // }
    // public static double erf(double x){
    //     /// single precision error function (Abramowitz and Stegun, from Wikipedia)
    //     if(x<0) return -erf(-x);
    //     double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
    //     double t=1/(1+0.3275911*x);
    //     double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
    // return 1-sum*Exp(-x*x);
    } 


}
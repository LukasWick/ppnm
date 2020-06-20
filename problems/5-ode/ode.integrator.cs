using static System.Math;
using System;
using static System.Console;
using System.Collections.Generic;
using static vector;
using static cspline;
public partial class ode_integrator{
    static void rkstep12(
	Func<double,vector,vector> f, /* the right-hand-side of dydt=f(t,y) */
	double t,                     /* the current value of the variable */
	vector yt,                    /* the current value y(t) of the sought function */
	double h,                     /* the step to be taken */
	vector yh,                   /* output: y(t+h) */
	vector err                    /* output: error estimate dy */
    ){
        vector k0 = f(t,yt);
        vector k1_2 = f(t+h/2.0,yt+1.0/2*h*k0);
        for(int i = 0; i<yt.size;i++){
            yh[i] =  yt[i]+h*k1_2[i];
            err[i] = h*(k1_2[i]-k0[i]);
        }
    }

    public static void rkstep45(
	Func<double,vector,vector> f, /* the right-hand-side of dydt=f(t,y) */
	double t,                     /* the current value of the variable */
	vector yt,                    /* the current value y(t) of the sought function */
	double h,                     /* the step to be taken */
	vector yh,                   /* output: y(t+h) */
	vector err                    /* output: error estimate dy */
    ){ 

        vector k1 = f(t, yt);
        vector k2 = f(t + 0.25 * h, yt + h * 0.25 * k1);
        vector k3 = f(t + 3.0/8 * h, yt + h *(3.0/32 * k1 + 9.0/32 * k2));
        vector k4 = f(t + 12.0/13 * h, yt + h * (1932.0/2197 * k1 - 7200.0/2197 * k2 + 7296.0/2197 * k3 ));
        vector k5 = f(t + h, yt + h * (439.0/216 * k1 - 8.0 * k2 + 3680.0/513 * k3 - 845.0/4104 * k4));
        vector k6 = f(t + 0.5 * h, yt + h * (-8.0/27 * k1 + 2.0 * k2 - 3544.0/2565 * k3 + 1859.0/4104 * k4 - 11.0/40 * k5));
        vector K_res = h*( 16.0/135 * k1 + 6656.0/12825  * k3 + 28561.0/56430 * k4 + -9.0/50 * k5 + 2.0/55 * k6);
        vector e = K_res-h*(25.0/216 * k1 + 1408.0/2565 * k3 + 2197.0/4104 * k4 -1.0/5 * k5);
        for(int i = 0; i<yt.size;i++){
            yh[i] =  yt[i]+ K_res[i];
            err[i] = e[i];
        }

    }
   
    
    // Allows one to keep alle steps in the calculation

    public static void driver(
	Func<double,vector,vector> f, /* right-hand-side of dydt=f(t,y) */
	double a,                     /* the start-point a */
	vector y,                     /* y(a) */
	double b,                     /* the end-point of the integration */
	List<double> ts,              /*List for used t values*/
    List<vector> ys,              /*List for found y values*/
    double h=1e-1,                /* initial step-size */
	double acc=1e-2,              /* absolute accuracy goal */
	double eps=1e-2               /* relative accuracy goal */                   
    ){
    vector tau = new vector(y.size);
    vector err = new vector(y.size);
    vector yt = y.copy();
    vector yh =  new vector(y.size);
    double t = a;
    double tol;
    ts.Add(t);
    ys.Add(yt.copy());
    while(t<b){
        if (b<t+h)
            h = b-t;
        rkstep45(f,t,yt,h,yh,err);
        err = abs(err); //Elementwise abs
        tau = (eps*abs(yh)+acc)*Sqrt(h/(b-a));
        tol = min(tau/err); //Elementwise division
        
        if(tol>1){
            yt = yh.copy();
            t +=h;
            ts.Add(t);
            ys.Add(yt.copy());
        }
        double factor = Min(Pow(tol,0.25)*0.95,2);
        h *= factor;
        
    }
    }

     // Only returns the last value
     // Runs the driver giving a list and only returns the last value
    public static vector driver(
	Func<double,vector,vector> f, /* right-hand-side of dydt=f(t,y) */
	double a,                     /* the start-point a */
	vector y,                     /* y(a) */
	double b,                     /* the end-point of the integration */
	double h=1e-1,                      /* initial step-size */
	double acc=1e-2,                   /* absolute accuracy goal */
	double eps=1e-2                    /* relative accuracy goal */
    ){ /* return y(b) */
        List<double> ts = new List<double>();
        List<vector> ys = new List<vector>();
        driver(f,a, y, b,ts,ys,h,acc,eps);
        vector yh = new vector(y.size);
        for(int i = 0; i<y.size;i++){
            yh[i] = ys[ys.Count-1][i];
        }
        return yh;
    }

    //Provide vector of times at wich you want the value. 
    //Runs the ODE from first to last point while saving the points interpolates to give the values in y 
    public static matrix driver(
	Func<double,vector,vector> f, /* right-hand-side of dydt=f(t,y) */
	vector ts,                     /* points return y at these points */
	vector y,                     /* starting y */
	double h=1e-1,                      /* initial step-size */
	double acc=1e-2,                   /* absolute accuracy goal */
	double eps=1e-2                    /* relative accuracy goal */
    ){ /* return y(b) */

        List<double> ts_found= new List<double>();
        List<vector> ys_found = new List<vector>();
        driver(f,ts[0], y, ts[-1],ts_found,ys_found,h, acc,eps); //ts[-1] is last elemnt in vector
        vector[] ys_vectors = new vector[y.size];

        vector ts_vector =  new vector(ts_found.Count);
        
        for(int i=0;i<y.size;i++){
            ys_vectors[i] = new vector(ts_found.Count);
        }
        for(int i=0;i<ts_found.Count;i++){
            ts_vector[i] = ts_found[i];

            for(int j=0;j<y.size;j++){
                ys_vectors[j][i] = ys_found[i][j];
            }
        }  

        cspline cs; 
        matrix ys_res = new matrix(y.size,ts.size);

        for(int i=0;i<y.size;i++){
            cs = new cspline(ts_vector, ys_vectors[i]);
            for(int j = 0; j<ts.size;j++){
                ys_res[j][i] = cs.spline(ts[j]);
            }
        }

        return ys_res;
    }
}
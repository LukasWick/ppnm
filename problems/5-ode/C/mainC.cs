using static System.Console;
using static System.Math;
using System;
using static ode_integrator;
using static vector;
using System.Collections.Generic;
class main{
    static private Func<double,vector,vector> diff(vector param){
       return (X,Y)=> diffExplesit(X,Y,param);
    }
    // Differential equation of 3 body system
    static private vector diffExplesit(double t,vector Y, vector param){
        // Y = [r1x,r1y,r1x',r1y',r2x,r2y,r2x',r2y',r3x,r3y,r4x',r5y']
        // i = [ 0 , 1 ,  2 ,  3 , 4 , 5 , 6  , 7  , 8 , 9 , 10 , 11 ]
        vector Ydiff = new vector(Y.size);
        double m1 = param[0];
        double m2 = param[1];
        double m3 = param[2];
        double G = 1;

        for(int i=0;i<3;i++){
            Ydiff[i*4] = Y[i*4+2];
            Ydiff[i*4+1] = Y[i*4+3];
        }
        double d12inv = Pow(Pow(Y[0]-Y[4],2)+Pow(Y[1]-Y[5],2),-3.0/2);
        double d13inv = Pow(Pow(Y[0]-Y[8],2)+Pow(Y[1]-Y[9],2),-3.0/2);
        double d23inv = Pow(Pow(Y[4]-Y[8],2)+Pow(Y[5]-Y[9],2),-3.0/2);

        // double d13inv = Pow((new vector(Y[0]-Y[8],Y[1]-Y[9])).norm(),-3);
        // double d23inv = Pow((new vector(Y[4]-Y[8],Y[5]-Y[9])).norm(),-3);

        Ydiff[2] = -G*m2*(Y[0]-Y[4])*d12inv-G*m3*(Y[0]-Y[8])*d13inv;
        Ydiff[3] = -G*m2*(Y[1]-Y[5])*d12inv-G*m3*(Y[1]-Y[9])*d13inv;
        Ydiff[6] = -G*m3*(Y[4]-Y[8])*d23inv-G*m1*(Y[4]-Y[0])*d12inv;
        Ydiff[7] = -G*m3*(Y[5]-Y[9])*d23inv-G*m1*(Y[5]-Y[1])*d12inv;
        Ydiff[10] = -G*m1*(Y[8]-Y[0])*d13inv-G*m2*(Y[8]-Y[4])*d23inv;
        Ydiff[11] = -G*m1*(Y[9]-Y[1])*d13inv-G*m2*(Y[9]-Y[5])*d23inv;
        return Ydiff;
    }

static void Main(){
    
    int tend = 100;
    double resulution = 1; // steps pr time unit for plot (dose not effect ODE)
    vector t = linspace(0,tend,(int) (resulution*(tend+1)));
    double h = 1e-5;
    double acc = 1e-4;
    double eps = 1e-4;
  

    
    // Initial coniditions found at: https://arxiv.org/pdf/math/0011268.pdf
    double x10 = 0.97000436;
    double y10 = -0.24308753;
    double x20 = -x10;
    double y20 = -y10;
    double x30 = 0;
    double y30 = 0;

    double v3x0 = -0.93240737;
    double v3y0 = -0.86473146;
    double v2x0 = -0.5 * v3x0;
    double v2y0 = -0.5 * v3y0;
    double v1x0 = v2x0;
    double v1y0 = v2y0;

    vector Y0 = new vector(new double[]{x10,y10,v1x0,v1y0,x20,y20,v2x0,v2y0,x30,y30,v3x0,v3y0});

    vector param = new vector(1,1,1);
    Func<double,vector,vector> f = (T,Y) => diffExplesit(T,Y, param);

    matrix yres = ode_integrator.driver(f, t, Y0, h, acc, eps);

    System.IO.StreamWriter outputfile = new System.IO.StreamWriter("out.plotC.txt",append:false);

    for(int i = 0; i<t.size;i++){
        outputfile.WriteLine("{0} {1} {2} {3} {4} {5} {6}",t[i],yres[i][0],yres[i][1],yres[i][4],yres[i][5],yres[i][8],yres[i][9]);
    }
    outputfile.Close();





}
}

using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

using static vector;
using static matrix;
public partial class simplex_minimization{
    // Returns index of lowest value
    public static int simplexmin(
	Func<vector,double> f, /* objective function */
    matrix simplex,        /*Start simplex to be optimized*/
	double tol             /*Acceptable size of simplex*/
    ){
       do{
           int highIndex = 0;
           int lowIndex = 0;
           double lowVal = f(simplex[0]);
           double highVal = f(simplex[0]);
           for(int i=1;i<simplex.size2;i++){
               double val_i = f(simplex[i]);
               if (val_i<lowVal){
                   lowIndex =i;
                   lowVal = val_i;
               }
                if (val_i>highVal){
                   highIndex =i;
                   highVal = val_i;
               }
           }

           vector pce = centroid(simplex, highIndex);
           vector reflected = reflection(simplex[highIndex],pce);
           double fref = f(reflected);
           if (fref<lowVal){
                vector expanded = expansion(simplex[highIndex],pce);
                double fexp = f(expanded);
                if (fexp<fref) {simplex[highIndex]= expanded;}
                else {simplex[highIndex] = reflected;}           
            }
           else{
                if (fref<highVal) {simplex[highIndex] = reflected;}
                else{
                    vector contracted  = contraction(simplex[highIndex],pce);
                    double fcon = f(contracted);
                    if (fcon<highVal) {simplex[highIndex]= contracted;}
                    else {reduction(simplex,lowIndex);};
            }
        }
           
       } while(size(simplex)>tol);
        // Find new lowest index for output
        int lowestIndex = 0;
        double lowestVal = f(simplex[0]);
        for(int i=1;i<simplex.size2;i++){
            double val_i = f(simplex[i]);
            if (val_i<lowestVal){
                lowestIndex =i;
                lowestVal = val_i;
            }
        }
       return lowestIndex;
    }
    public static double size(matrix simplex){
        double thesize = 0;
        for(int k=1;k<simplex.size2;k++){
            double dist = distance(simplex[0],simplex[k]);
            if (dist>thesize) thesize = dist;
        }        
        return thesize;
    }

    public static double distance(vector v1,vector v2){
        return (v1-v2).norm();
    }

    public static vector reflection(vector phigh, vector pce){
        return pce+(pce-phigh);
    }
    public static vector expansion(vector phigh, vector pce){
        return pce+2*(pce-phigh);
    }

    public static vector contraction(vector phigh, vector pce){
        return pce+0.5*(pce-phigh);
    }
    public static vector centroid(matrix P, int highestIndex){
        int n = P.size2-1;
        vector pce = new vector(P.size1);
        for(int k=0;k<n+1;k++){
            if(k!=highestIndex){
                pce = 1.0/n*P[k];
            }
        }
        return pce;
    }
        
    public static void reduction(matrix P, int lowestIndex){
        vector plow = P[lowestIndex];
        for(int k=0;k<P.size2;k++){
            if(k!=lowestIndex){
                P[k] = 0.5*(P[k]+plow);
            }
        }
    }
}

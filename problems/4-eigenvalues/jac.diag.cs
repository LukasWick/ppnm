using static System.Math;
using System;
using static System.Console;

public partial class jacobi{
    // Part A
    static public int diag_cyclic(matrix A,matrix V,vector d, bool highestFirst=false){
        V.set_unity();
        for (int i = 0; i<d.size;i++){
            d[i] = A[i,i];
        }
        double phi, s,c;
        vector old_d = d.copy();
        double diff;
        double absT0l= 0;
        int rotations = 0;
        int highestFirstVal = 1;
        if (highestFirst) highestFirstVal=-1;
        do {
            for(int p =0;p<A.size1;p++){
                for(int q =p+1;q<A.size2;q++){
                    rotations++;
                    double app = d[p];
                    double aqq = d[q];
                    double apq = A[p,q];
                    phi = 1.0/2*Atan2(highestFirstVal*2*apq,highestFirstVal*(aqq-app)); // Added - on both arguments makes it sort by highest eigenvalue first
                    s = Sin(phi);
                    c = Cos(phi);
                    
                    // Calculate new values 
                    for(int i =0;i<A.size1;i++){
                        if (i>p){
                            double api = A[p,i];
                            if (i>q){
                                double aqi = A[q,i];
                                A[p,i] = c*api-s*aqi;
                                A[q,i] = s*api+c*aqi;
                            }
                            else if (i<q){
                                double aqi = A[i,q];
                                A[p,i] = c*api-s*aqi;
                                A[i,q] = s*api+c*aqi;
                            }
                        }
                        else if (i<p){
                            double api = A[i,p];
                            if (i>q){
                                double aqi = A[q,i];
                                A[i,p] = c*api-s*aqi;
                                A[q,i] = s*api+c*aqi;
                            }
                            else if (i<q){
                                double aqi = A[i,q];
                                A[i,p] = c*api-s*aqi;
                                A[i,q] = s*api+c*aqi;
                            }
                        }
                    }
                    d[p] = c*c*app -2*s*c*apq+s*s*aqq;
                    d[q] = s*s*app +2*s*c*apq+c*c*aqq;
                    A[p,q] =0;
                    for(int i =0;i<A.size1;i++){
                        double vip = V[i,p];
                        double viq = V[i,q];
                        V[i,p] = c*vip-s*viq;
                        V[i,q] = s*vip+c*viq;
                    }
                }
            }
            vector diff_d = old_d-d;
            diff = 0;
            for(int i=0;i<diff_d.size;i++)
                diff += Abs(diff_d[i]);
            old_d = d.copy();
        } while (diff > absT0l);
        return rotations;
    }



    // Help function. Makes sure A is symetric after evaluation and returns the diagonal matrix
    static public matrix diag_cyclic_complete(matrix A,matrix V){
        vector d = new vector(A.size1);
        diag_cyclic(A, V, d);
        for(int i = 0;i<A.size1;i++){
           for(int j=i+1;j<A.size2;j++){
            A[i,j] = A[j,i];
        }
        }
        matrix D = new matrix(A.size1,A.size2);
        for (int i = 0; i<d.size;i++){
            D[i,i] = d[i];
        }
        return D;
    }
static public int diag_frist_n(matrix A,matrix V,vector d,int n, bool highestFirst=false){
        V.set_unity();
        for (int i = 0; i<d.size;i++){
            d[i] = A[i,i];
        }
        double phi, s,c;
        vector old_d = d.copy();
        double diff;
        double absT0l= 0;
        int rotations = 0;
        int highestFirstVal = 1;
        if (highestFirst) highestFirstVal=-1;
        for(int p =0;p<n;p++){
            do {
                for(int q =p+1;q<A.size2;q++){
                
                    double app = d[p];
                    double aqq = d[q];
                    double apq = A[p,q];
                    phi = 1.0/2*Atan2(highestFirstVal*2*apq,highestFirstVal*(aqq-app)); // Added - on both arguments makes it sort by highest eigenvalue first
                    s = Sin(phi);
                    c = Cos(phi);
                    
                    // Calculate new values 
                    for(int i =0;i<A.size1;i++){
                        if (i>p){
                            double api = A[p,i];
                            if (i>q){
                                double aqi = A[q,i];
                                A[p,i] = c*api-s*aqi;
                                A[q,i] = s*api+c*aqi;
                            }
                            else if (i<q){
                                double aqi = A[i,q];
                                A[p,i] = c*api-s*aqi;
                                A[i,q] = s*api+c*aqi;
                            }
                        }
                        else if (i<p){
                            double api = A[i,p];
                            if (i>q){
                                double aqi = A[q,i];
                                // A[i,p] = c*api-s*aqi;
                                A[q,i] = s*api+c*aqi;
                            }
                            else if (i<q){
                                double aqi = A[i,q];
                                // A[i,p] = c*api-s*aqi;
                                A[i,q] = s*api+c*aqi;
                            }
                        }
                    }
                    d[p] = c*c*app -2*s*c*apq+s*s*aqq;
                    d[q] = s*s*app +2*s*c*apq+c*c*aqq;
                    A[p,q] =0;
                    for(int i =0;i<A.size1;i++){
                        double vip = V[i,p];
                        double viq = V[i,q];
                        V[i,p] = c*vip-s*viq;
                        V[i,q] = s*vip+c*viq;
                    }
                }
                diff = old_d[p]-d[p];
                old_d = d.copy();
            } while (diff > absT0l);
        }
        return rotations;
    }

    // Qustion C
    static public int diag_classic(matrix A,matrix V, vector d, bool highestFirst=false){
        V.set_unity();
        for (int i = 0; i<d.size;i++){
            d[i] = A[i,i];
        }
        double phi, s,c;
        vector old_d = d.copy();
        double diff;
        double absT0l= 0;
        int rotations = 0;
        int highestFirstVal = 1;
        if (highestFirst) highestFirstVal=-1;
        do {
            for(int p =0;p<A.size1-1;p++){
                int q = p+1;

                // find index q of largest value
                double val_largest = Abs(A[p,q]);
                for(int q_it =p+2;q_it<A.size2;q_it++){
                    if (val_largest<=Abs(A[p,q_it])){
                        val_largest = Abs(A[p,q_it]);
                        q = q_it;
                    }
                }
                
                rotations++;
                double app = d[p];
                double aqq = d[q];
                double apq = A[p,q];
                phi = 1.0/2*Atan2(highestFirstVal*2*apq,highestFirstVal*(aqq-app)); // Added - on both arguments makes it sort by highest eigenvalue first
                s = Sin(phi);
                c = Cos(phi);
                
                // Calculate new values 
                for(int i =0;i<A.size1;i++){
                    if (i>p){
                        double api = A[p,i];
                        if (i>q){
                            double aqi = A[q,i];
                            A[p,i] = c*api-s*aqi;
                            A[q,i] = s*api+c*aqi;
                        }
                        else if (i<q){
                            double aqi = A[i,q];
                            A[p,i] = c*api-s*aqi;
                            A[i,q] = s*api+c*aqi;
                        }
                    }
                    else if (i<p){
                        double api = A[i,p];
                        if (i>q){
                            double aqi = A[q,i];
                            A[i,p] = c*api-s*aqi;
                            A[q,i] = s*api+c*aqi;
                        }
                        else if (i<q){
                            double aqi = A[i,q];
                            A[i,p] = c*api-s*aqi;
                            A[i,q] = s*api+c*aqi;
                        }
                    }
                }
                d[p] = c*c*app -2*s*c*apq+s*s*aqq;
                d[q] = s*s*app +2*s*c*apq+c*c*aqq;
                A[p,q] =0;
                for(int i =0;i<A.size1;i++){
                    double vip = V[i,p];
                    double viq = V[i,q];
                    V[i,p] = c*vip-s*viq;
                    V[i,q] = s*vip+c*viq;
                }
            
            }
            vector diff_d = old_d-d;
            diff = 0;
            for(int i=0;i<diff_d.size;i++)
                diff += Abs(diff_d[i]);
            old_d = d.copy();
        } while (diff > absT0l);
        return rotations;
    }

}


using static System.Math;
using System;
using static System.Console;

public partial class jacobi{
    // Part A
    static public vector diag_cyclic(matrix A,matrix V){
        vector d = new vector(A.size1);
        for (int i = 0; i<d.size;i++){
            V[i,i] = 1;
            d[i] = A[i,i];
        }
        double phi, s,c;
        vector old_d = d.copy();
        double diff;
        double absT0l= 0;
        do {
            for(int p =0;p<A.size1;p++){
                for(int q =p+1;q<A.size2;q++){
                    phi = 1.0/2*Atan2(2*A[p,q],d[q]-d[p]);
                    s = Sin(phi);
                    c = Cos(phi);
                    //Save last itterations values
                    vector Ap = A[p].copy();
                    vector Aq = A[q].copy();
                    Ap[p] = d[p];
                    Aq[q] = d[q];
                    for(int i =p+1 ;i<A.size1;i++){
                            Ap[i] = A[p,i];
                    }
                    for(int i =q+1 ;i<A.size1;i++){
                            Aq[i] = A[q,i];
                    }

                    // Calculate new values 
                    for(int i =0;i<A.size1;i++){
                        if (i<p)
                            A[i,p] = c*Ap[i]-s*Aq[i];
                        if (i>p)
                            A[p,i] = c*Ap[i]-s*Aq[i];

                        if (i<q)
                            A[i,q] = s*Ap[i]+c*Aq[i];
                        if (i>q)
                            A[q,i] = s*Ap[i]+c*Aq[i];
                    }
                    d[p] = c*c*Ap[p] -2*s*c*Ap[q]+s*s*Aq[q];
                    d[q] = s*s*Ap[p] +2*s*c*Ap[q]+c*c*Aq[q];                
                    A[p,q] = s*c*(Ap[p]-Aq[q])+(c*c-s*s)*Ap[q];

                    vector Vp = V[p].copy();
                    vector Vq = V[q].copy();
                    for(int i =0;i<A.size1;i++){
                        V[i,p] = c*Vp[i]-s*Vq[i];
                        V[i,q] = s*Vp[i]+c*Vq[i];
                    }
                }
            }
            vector diff_d = old_d-d;
            diff = 0;
            for(int i=0;i<diff_d.size;i++)
                diff += Abs(diff_d[i]);
            old_d = d.copy();
        } while (diff > absT0l);

        
        return d;
    }
    // Help function
    static public matrix diag_cyclic_complete(matrix A,matrix V){
        
        vector d = diag_cyclic(A, V);
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

}


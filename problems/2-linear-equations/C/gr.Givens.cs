using static System.Math;
public class qr_givens{
    // Part A
    //Save R in upper triangular part of A and rotation (theta) in lower part
    static public void qr_givens_QR(matrix A){
        for(int p=0;p<A.size2;p++){
            for(int q=p+1;q<A.size1;q++){
                double theta = Atan2(A[q,p],A[p,p]);
                for(int k= p; k<A.size2;k++){
                    double xq = A[q,k], xp = A[p,k];
                    A[p,k] = xp*Cos(theta) +xq*Sin(theta);
                    A[q,k] = -xp*Sin(theta)+xq*Cos(theta);
                }
                A[q,p] = theta;
            }
        }
    }
    // Calculate QT v
    static public void qr_givens_QTvec(matrix QR,vector v){
        for(int p=0;p<QR.size2;p++){
            for(int q=p+1;q<QR.size1;q++){
            double theta = QR[q,p];
            double vq = v[q], vp = v[p];
            v[p] = vp*Cos(theta) +vq*Sin(theta);
            v[q] = -vp*Sin(theta)+vq*Cos(theta);
            }
        }
    }
    static public void qr_givens_solve(matrix QR,vector b){
        qr_givens_QTvec(QR,b);
        for(int i=b.size-1;i>=0;i--){
            double sum = 0;
            // calculate QR*b
            for(int k = i+1;k<QR.size1;k++){
                sum += QR[i,k]*b[k];
            }
            b[i] = 1/QR[i,i]*(b[i]-sum);
        }
    }
    static public matrix qr_givens_inverse(matrix QR){
        var B = new matrix(QR.size1,QR.size2);
        var e = new vector(QR.size1); //Unitverctor
        for(int i = 0; i<QR.size1;i++){
                e[i]=1;
                qr_givens_solve(QR, e);
                B[i]= e.copy();
                e=new vector(QR.size1);
        }
    return B;
}
}





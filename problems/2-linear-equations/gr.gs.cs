using static System.Math;
public partial class qr_gs{

    // Part A
    // Do decomposition
    static public void qr_gs_decomp(matrix A, matrix R){
        int m = A.size2;
        for(int i = 0; i<m;i++){
            R[i,i] = Sqrt(A[i].dot(A[i])); // norm of A_i
            A[i] = A[i]/R[i,i];
            for(int j=i + 1;j<m;j++){
                R[i,j] = A[i].dot(A[j]);
                A[j] = A[j]-A[i]*R[i,j];
            }
        }

    }
    // Solve given Ax = QRx = b
    static public vector qr_gs_solve(matrix Q, matrix R, vector b){
        vector x = Q.T*b;
        return backsub(R, x);
    }

    // Part B
    // Calculate inverse of A = QR
    static public matrix qr_gs_inverse(matrix Q, matrix R){
        var B = new matrix(Q.size1,Q.size2);
        var e = new vector(Q.size1); //Unitverctor
        for(int i = 0; i<Q.size1;i++){
                e[i]=1;
                B[i]=qr_gs_solve(Q, R, e);
                e[i]=0;
        }
    return B;

}
// Do bacsubstitution on upper trianguler matrix U
static public vector backsub(matrix U, vector x){
    vector c = x.copy();
    for(int i=c.size-1;i>=0;i--){
        double s = c[i];
        for(int k = i+1;k<c.size;k++){
            s -= U[i,k]*c[k];
        }
        c[i] = s/U[i,i];
    }
    return c;
}
    


}


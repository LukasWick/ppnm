using static System.Console;
using System;
class main{
static void Main(){
    var rnd = new Random();
    var A = new matrix(3,3);
    for(int i=0;i<A.size1;i++){
        for(int j=0;j<A.size2;j++){
            A[i,j]=10*(rnd.NextDouble()-0.5); 
        }
    }
    var Q = A.copy();
    var R = new matrix(Q.size2,Q.size2);
    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question B:\nMatrix inverse by Gram-Schmidt QR factorization");
    WriteLine("Random matrix");
	A.print("A = ");
    qr_gs.qr_gs_decomp(Q,R);
    WriteLine("Factorized into");
	Q.print("Q = ");
    R.print("R = ");
    
    var B = qr_gs.qr_gs_inverse(Q,R);

    WriteLine("Inverse of A:");
    B.print("A^-1=B=");
    WriteLine("Product should give identity");

    (A*B).print("I=AB=");
    WriteLine("__________________________________________________________________________________________________________\n");

}
}

using static System.Console;
using System;

class main{
static void Main(){
    var rnd = new Random();
    var A = new matrix(5,3);
    for(int i=0;i<5;i++){
        for(int j=0;j<3;j++){
            A[i,j]=10*(rnd.NextDouble()-0.5); 
        }
    }
    WriteLine("\n\n");
    WriteLine("A: Solving linear equations using QR-decomposition by modified Gram-Schmidt orthogonalization");

    WriteLine("__________________________________________________\nPart 1:");
	// var A=new matrix("1 2 4;5 6 3;8 7 10;3 6 1;4 5 2");
    WriteLine("Random tall (n>m) matrix A:");
	A.print("A = ");
    var R = new matrix(A.size2,A.size2);
    WriteLine("Factorized into");
    qr_gs.qr_gs_decomp(A,R); // A is now Q
	A.print("Q = ");
    WriteLine("and upper triangular");
    R.print("R = ");
    
    WriteLine("Q^TQ should give identity:");
    (A.T*A).print("Q^TQ");
    WriteLine("And QR = A:");

    (A*R).print("QR");

    WriteLine("\n__________________________________________________\nPart 2:");
    A = new matrix(3,3);
    for(int i=0;i<3;i++){
        for(int j=0;j<3;j++){
            A[i,j]=10*(rnd.NextDouble()-0.5); 
        }
    }
    var b = new vector(3);
    for(int i=0;i<3;i++){
        b[i]=10*(rnd.NextDouble()-0.5); 
    }

    // var A2 = new matrix("1 2;3 4");
    // var Q = new matrix("1 2;3 4");
    var Q = A.copy();
    var R2 = new matrix(Q.size2,Q.size2);
    // var b = new vector(5.0,6,4);
    A.print("A = ");
    b.print("b = ");

    qr_gs.qr_gs_decomp(Q,R2);
    var x = qr_gs.qr_gs_solve(Q,R2,b);
    WriteLine("Solve Ax = b:");
    x.print("x =");
    WriteLine("Ch eck result:");
    (A*x).print("Ax= ");
    WriteLine("\n\n");

}
}

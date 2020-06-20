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
    var b = new vector(3);
    for(int i=0;i<3;i++){
        b[i]=10*(rnd.NextDouble()-0.5); 
    }
    WriteLine("\n__________________________________________________________________________________________________________");

    WriteLine("Question C: \nTest QR-decomposition by Givens rotations:");
    var QR = A.copy();
    A.print("A = ");

    qr_givens.qr_givens_QR(QR);
    WriteLine("QR-decomposition");
    QR.print("QR = ");
    WriteLine("Random vector");
    b.print("b = ");
    qr_givens.qr_givens_solve(QR,b);
    WriteLine("Solve Ax = b:");
    b.print("x = ");
    WriteLine("Check result:");
    (A*b).print("Ax= ");
    WriteLine("__________________________________________________________________________________________________________\n");

}
}

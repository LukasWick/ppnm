using static System.Console;
using System;

class main{
static void Main(){
    WriteLine("Part 1:");
	var A=new matrix("1 2 4;5 6 3;8 7 10;3 6 1;4 5 2");
	A.print("A = ");
    var R = new matrix(A.size2,A.size2);
    R.print("R not init = ");
    qr_gs.qr_gs_decomp(A,R); // A is now Q
	A.print("Q = ");
    R.print("R = ");
    (A.T*A).print("Q^TQ");
    (A*R).print("QR");

    WriteLine("\nPart 2:");
    var A2 = new matrix("1 2;3 4");
    var Q = new matrix("1 2;3 4");
    var R2 = new matrix(Q.size2,Q.size2);
    var b = new vector(5.0,6);
    A2.print("A = ");
    b.print("b = ");


    qr_gs.qr_gs_decomp(Q,R2);
    var x = qr_gs.qr_gs_solve(Q,R2,b);
    x.print("x=");
    (A2*x).print("Ax=");


}
}

using static System.Console;
class main{
static void Main(){
    WriteLine("Test Givens:");
    var A = new matrix("1 2 3;2 7 4;6 7 9");
    var QR = A.copy();
    A.print("A = ");

    qr_givens.qr_givens_QR(QR);
    QR.print("QR = ");

    var b = new vector(5.0,6,4);
    b.print("b = ");
    qr_givens.qr_givens_solve(QR,b);
    b.print("x=");
    (A*b).print("Ax=");
}
}

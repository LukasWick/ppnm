using static System.Console;
class main{
static void Main(){
    var A = new matrix("1 2;3 4");
    var Q = new matrix("1 2;3 4");
    var R = new matrix(Q.size2,Q.size2);
    A.print("A = ");
    
    qr_gs.qr_gs_decomp(Q,R);
    var B = qr_gs.qr_gs_inverse(Q,R);
    B.print("B=");

    (A*B).print("AB=");
}
}

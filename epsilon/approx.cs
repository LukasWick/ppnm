using static System.Math;

public class approx{
	public static bool Approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
        return Abs(a-b)<tau || Abs(a-b)/(Abs(a)+Abs(b))<epsilon/2;
    }
}
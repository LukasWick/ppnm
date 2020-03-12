using static System.Console;
class main{
static void Main(){
	for(double L=-10;L<=10;L+=0.01){
		vector v = nonef.euler_spiral(L);
		WriteLine($"{v[0]} {v[1]}");
	}
}
}
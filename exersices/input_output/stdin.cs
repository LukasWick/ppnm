using System; 
class stdin{
	static int Main(){
		do{
			string s = Console.In.ReadLine();
			if (s==null)break;
			string[] words = s.Split(' ',',','\t');
			foreach(var word in words){
                double x = double.Parse(word);
                Console.WriteLine("{0} {1} {2}",x,Math.Sin(x),Math.Cos(x));
			}
		}while(true);
		return 0;
	}
}

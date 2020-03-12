using System; 
class stdin{
	static int Main(string[] args){
		System.IO.StreamReader  inputfile = new System.IO.StreamReader(args[0]);
		System.IO.StreamWriter  outputfile = new System.IO.StreamWriter(args[1],append:false);

		do{
			string s = inputfile.ReadLine();
			if (s==null)break;
			string[] words = s.Split(' ',',','\t');
			foreach(var word in words){
			double x = double.Parse(word);
			outputfile.WriteLine("{0} {1} {2}",x,Math.Sin(x),Math.Cos(x));
			}
		}while(true);
		outputfile.Close();

		return 0;
	}
}


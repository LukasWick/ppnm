using static System.Console;
using static System.Math;
using System;
using static minimization;
using static vector;
using System.Collections.Generic;
class main{
    static List<double> energy = new List<double>();
    static List<double> crossSection = new List<double>();
    static List<double> error = new List<double>();
static double breit_wigner(vector par,double E){
    return par[2]/(Pow(E-par[0],2)+par[1]*par[1]/4.0);
}
static double chiSquared(vector par){
    double chi2 = 0;
    for(int i = 0; i<energy.Count;i++)
        chi2 += Pow((breit_wigner(par,energy[i])-crossSection[i])/error[i],2);
    return chi2;
}
static void Main(){
    System.IO.StreamReader inputfile = new System.IO.StreamReader("higgs_data.data");
    string s = inputfile.ReadLine();
    while(!(s==null)){
		string[] values = s.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
        energy.Add(double.Parse(values[0]));
        crossSection.Add(double.Parse(values[1]));
        error.Add(double.Parse(values[2]));
        s = inputfile.ReadLine();
    }

    double epsilon = 1e-7;
    vector param = new vector(120,4.83,20);
    int num_of_steps = qnewton(chiSquared,ref param,epsilon);
    WriteLine("\n__________________________________________________________________________________________________________");
    WriteLine("Question A\n Higgs discovery");
    WriteLine("Fit of higgs mass:");
    WriteLine("m = {0}, Γ = {1}, A = {2}",param[0],param[1],param[2]);
    WriteLine("Expected mass: {0}", 125.3);
    WriteLine("Number of steps              : {0}",num_of_steps);
    WriteLine("See fit in PlotB.svg");
    WriteLine("__________________________________________________________________________________________________________\n");

    System.IO.StreamWriter  outputfile = new System.IO.StreamWriter("out.fitfun.txt",append:false);
    vector Es = linspace(101,159,400);
    for(int i = 0; i<400;i++)
        outputfile.WriteLine("{0} {1}",Es[i],breit_wigner(param,Es[i]));
    outputfile.Close();
    }
}
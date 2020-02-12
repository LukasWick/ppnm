using static System.Console;
using static System.Math;
using System;
using static approx;

class main{
    static int Main(){
        // OpgA
        // int i=1; while(i+1>i) {i++;}
        // Write("my max int = {0}\n",i);
        // Write("system max int = {0}\n",int.MaxValue);

        // i=1; while(i-1<i) {i--;}
        // Write("my min int = {0}\n",i);
        // Write("system min int = {0}\n",int.MinValue);
        
        // OpgB
        // double x=1; while(1+x!=1){x/=2;} x*=2;
        // float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;
        // Write("my double eps = {0}\n",x);
        // Write("dimitri double eps= {0}\n",System.Math.Pow(2,-52));
        // Write("my float eps = {0}\n",y);
        // Write("dimitri float eps= {0}\n",System.Math.Pow(2,-23));
        
        // OpgC
        // int max=int.MaxValue/3;
        // float float_sum_up = 0;
        // float float_sum_down = 0;

        // int i = 0;

        // for (i=1; i<max+1;i++){
        //     float_sum_up += 1F/i;
        // }
        // for (i=max; i>0;i--){
        //     float_sum_down += 1F/i;
        // }
        // Write("Float upsum= {0}\n",float_sum_up);
        // Write("Float dowmsum= {0}\n",float_sum_down);
       

        // ii because we are using float and add small numbers to a large number, it dosent contain enough significant figures to add the small walues
        // But when we start at a low number we start at a small exponent and are able to add small other small numbers in increasing sice.
        // iii it dose not converge as at some point it dosent matter how high max is, you would still get exactly the same value 

        // double double_sum_up = 0.0;
        // double double_sum_down = 0.0;

        // for (i=1; i<max+1;i++){
        //     double_sum_up += 1.0/i;
        // }
        // for (i=max; i>0;i--){
        //     double_sum_down += 1.0/i;
        // }
        // Write("Double upsum= {0}\n",double_sum_up);
        // Write("Double dowmsum= {0}\n",double_sum_down);

        // Opgave D
        Write("approx ikke ens: {0}\n", Approx(1+0.001,1));
        Write("approx \"ens\": {0}\n", Approx(1+0.000000000001,1));
        
        return 0;
    }
    


}
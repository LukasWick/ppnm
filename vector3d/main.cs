class main{
	static int Main(){
		vector3d v = new vector3d(1,2,3);
		vector3d u = new vector3d(5,4,3);
		v.print("v= ");
		u.print("u= ");
		(v+u).print("u+v= ");
		(v-u).print("u-v= ");
		(2*v).print("2*v= ");
		System.Console.Write("v.x = {0}\n",v.x);
		v.x = 5.0;
		System.Console.Write("v.x = {0}\n",v.x);
		
		System.Console.Write("v@u= {0}\n",v.dot_product(u));
		v.vector_product(u).print("v cross u= ");
		System.Console.Write("|v|= {0}\n",v.magnitude());
		return 0;
	}
}


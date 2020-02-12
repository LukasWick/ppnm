using static System.Math;
public struct vector3d : ivector3d<vector3d>{
	private  double _x,_y,_z;
	public double x {get{return _x;}set{_x = value;}}
	public double y {get{return _y;}set{_y = value;}}
    public double z {get{return _z;}set{_z = value;}}   	
	
	//constructors
	public vector3d(double a,double b,double c){_x=a;_y=b;_z=c;}
	
	//operators
	public static vector3d operator*(vector3d v, double c){return new vector3d(c*v.x,c*v.y,c*v.z);}
	public static vector3d operator*(double c, vector3d v){return v*c;}
	public static vector3d operator+(vector3d u, vector3d v){return new vector3d(u.x+v.x,u.y+v.y,u.z+v.z);}
	public static vector3d operator-(vector3d u, vector3d v){return new vector3d(u.x-v.x,u.y-v.y,u.z-v.z);}
	//methods
	public double dot_product(vector3d other){return this.x*other.x+this.y+other.y+this.z+other.z;}
	public static double dot_product(vector3d first, vector3d other){return first.dot_product(other);}
	public vector3d vector_product(vector3d other){
		return new vector3d(
		this.y*other.z - this.z*other.y,
		this.z*other.x - this.x*other.z,
		this.x*other.y - this.y*other.x);
	}

	public double magnitude(){
		double max = Max(Abs(x),Max(Abs(y),Abs(z)));
		return max*Sqrt(Pow(x,2)/Pow(max,2)+Pow(y,2)/Pow(max,2)+Pow(z,2)/Pow(max,2));
	}

	public void print(string s=""){
		System.Console.Write("{0}({1}, {2}, {3})\n",s,x,y,z);
	}

	public override string ToString(){
		return string.Format("({0}, {1}, {2})",x,y,z);
	} 


}

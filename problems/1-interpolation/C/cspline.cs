using static System.Console;
using static System.Math;

public class cspline {
	vector x, y, b, c, d;
	public cspline (vector xs, vector ys) {
		/* calculate b and c */
		x = xs;
		y = ys;
		int n = xs.size;
		vector h = new vector (n - 1);
		vector p = new vector (n - 1);

		for (int i = 0; i < n - 1; i++) {
			h[i] = (x[i + 1] - x[i]);
			p[i] = (y[i + 1] - y[i]) / h[i];
		}
		vector D = new vector (n);
		D[0] = 2;
		D[-1] = 2;

		vector Q = new vector (n-1);
		Q[0] = 1;
		vector B = new vector (n);
		B[0] = 3*p[0];
		B[-1] = 3*p[-1];
		for (int i = 0; i < n - 2; i++) {
			D[i+1] = 2*h[i]/h[i+1]+2;
			Q[i+1] = h[i]/h[i+1];
			B[i+1] = 3*(p[i]+p[i+1]*h[i]/h[i+1]);
		}
		vector Dt = new vector(n);
		Dt[0] = D[0];
		vector Bt = new vector(n);
		Bt[0] = B[0];
		for (int i = 1; i < n; i++) {
			Dt[i] = D[i]-Q[i-1]/Dt[i-1];
			Bt[i] = B[i]-Bt[i-1]/Dt[i-1];
		}
		b = new vector(n);
		b[-1] = Bt[-1]/D[-1];
		for (int i = n-2; i >= 0; i--){
			b[i] = (Bt[i]-Q[i]*b[i+1])/Dt[i];
		}
		c = new vector (n - 1);
		// c[0] = 0;
		d = new vector (n - 1);
		// d[0] = (b[1]+b[1+1]-2*p[1])/(h[1]*h[1]);
		for(int i =0;i<n-1;i++){
			c[i] = (-2*b[i]-b[i+1]+3*p[i])/h[i];
			d[i] = (b[i]+b[i+1]-2*p[i])/(h[i]*h[i]);
		}
		
	}
	public double spline (double z) { 
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0, j = x.size - 1;
		while (j - i > 1) {
			int m = (i + j) / 2;
			if (z > x[m])
				i = m;
			else
				j = m;
		}
		double DeltaX =(z-x[i]);
		return y[i] + b[i]*DeltaX+c[i]*Pow(DeltaX,2)+d[i]*Pow(DeltaX,3);

	}

	public double derivative (double z) { 
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0, j = x.size - 1;
		while (j - i > 1) {
			int m = (i + j) / 2;
			if (z > x[m])
				i = m;
			else
				j = m;
		}
		double DeltaX =(z-x[i]);

		return b[i]+2*c[i]*DeltaX+3*d[i]*DeltaX*DeltaX;
	}
	public double integral (double z) {
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0;
		double integral = 0;
		double DeltaX =0;
		while (z > x[i+1]) {
			DeltaX = (x[i + 1] - x[i]);
			integral += y[i] * DeltaX+ 1.0 / 2 *b[i]*DeltaX*DeltaX+1.0/3*c[i]*Pow(DeltaX,3)+1.0/4*d[i]*Pow(DeltaX,4);
			i++;
		}
		DeltaX = (z - x[i]);
		integral += y[i] * DeltaX+ 1.0 / 2 *b[i]*DeltaX*DeltaX+1.0/3*c[i]*Pow(DeltaX,3)+1.0/4*d[i]*Pow(DeltaX,4);
		return integral;
	}
}
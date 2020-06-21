using static System.Console;

public class qspline {
	vector x, y, b, c;
	public qspline (vector xs, vector ys) {
		/* calculate b and c */
		x = xs;
		y = ys;
		int n = xs.size;
		vector p = new vector (n - 1);
		vector cForward = new vector (n - 1);
		vector cBackward = new vector (n - 1);
		vector DeltaX = new vector (n - 1);
		cForward[0] = 0;
		cBackward[-1] = 0;
		for (int i = 0; i < n - 1; i++) {
			DeltaX[i] = (x[i + 1] - x[i]);
			double DeltaY = (y[i+1] - y[i]);
			p[i] = DeltaY / DeltaX[i]; //First order gradient
		}
		for (int i = 0; i <= n - 3; i++) {
			cForward[i+1] = 1.0/DeltaX[i+1] * (p[i+1] - p[i] - cForward[i] * DeltaX[i]);
		}
		for (int i = n - 3; i >= 0; i--) {
			cBackward[i] = 1.0 / DeltaX[i] * (p[i + 1] - p[i] - cBackward[i + 1] * DeltaX[i + 1]);
		}
		
		c = 1.0/2.0 * (cForward + cBackward);
		b = p - c * DeltaX;
	}
	public double spline (double z) { 
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0, j = x.size - 1;
		// Binary search
		while (j - i > 1) {
			int m = (i + j) / 2;
			if (z > x[m])
				i = m;
			else
				j = m;
		}
		double dx = (z-x[i]);
		return y[i] + b[i]*dx+c[i]*dx*dx;

	}

	public double derivative (double z) { 
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0, j = x.size - 1;
		// Binary search
		while (j - i > 1) {
			int m = (i + j) / 2;
			if (z > x[m])
				i = m;
			else
				j = m;
		}
 		double dx = (z-x[i]);
		return b[i]+2*c[i]*dx;
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
			integral += y[i] * DeltaX+ 1.0 / 2.0 *b[i]*DeltaX*DeltaX+1.0/3.0*c[i]*DeltaX*DeltaX*DeltaX;
			i++;
		}
		DeltaX = (z - x[i]);
		integral += y[i] * DeltaX+ 1.0 / 2.0 *b[i]*DeltaX*DeltaX+1.0/3.0*c[i]*DeltaX*DeltaX*DeltaX;
		return integral;
	}
}
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
		cBackward[n - 2] = 0;
		for (int i = 0; i < n - 2; i++) {
			DeltaX[i] = (x[i + 1] - x[i]);
			p[i] = (y[i + 1] - y[i]) / DeltaX[i];

		}
		for (int i = 0; i < n - 2; i++) {
			cForward[i + 1] = 1.0 / DeltaX[i + 1] * (p[i + 1] - p[i] - cForward[i] * DeltaX[i]);
		}
		for (int i = n - 3; i >= 0; i--) {
			cBackward[i] = 1.0 / DeltaX[i] * (p[i + 1] - p[i] - cBackward[i + 1] * DeltaX[i + 1]);
		}
		c = 1.0/2 * (cForward + cBackward);
		b = p - c * DeltaX;
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
		return y[i] + b[i]*(z-x[i])+c[i]*(z-x[i])*(z-x[i]);

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
		return b[i]+2*c[i]*(z - x[i]);
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
			integral += y[i] * DeltaX+ 1.0 / 2 *b[i]*DeltaX+1.0/3*c[i]*DeltaX*DeltaX*DeltaX;
			i++;
		}
		DeltaX = (z - x[i]);
		integral += y[i] * DeltaX+ 1.0 / 2 *b[i]*DeltaX+1.0/3*c[i]*DeltaX*DeltaX*DeltaX;
		return integral;
	}
}
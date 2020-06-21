public class linspline {
	static public double linterp (vector x, vector y, double z) {
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
		double dx =  (z - x[i]);
		double DeltaX = (x[i + 1] - x[i]);
		double DeltaY =  (y[i + 1] - y[i]);
		return y[i] +DeltaY/ DeltaX *dx;

	}

	// Given x and y return integrated value at Z
	static public double linterpInteg (vector x, vector y, double z) {
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0;
		double integral = 0;
		double DeltaX = 0;
		double DeltaY =  0;
		while (z > x[i+1]) {
			DeltaX = (x[i + 1] - x[i]);
			DeltaY =  (y[i + 1] - y[i]);
			integral += y[i] * DeltaX + 0.5 * (y[i + 1] - y[i]) * DeltaX;
			i++;
		}
		double dX = (z - x[i]);
		DeltaX = (x[i + 1] - x[i]);
		DeltaY =  (y[i + 1] - y[i]);
		integral += y[i] * dX + 0.5 * DeltaY / DeltaX * dX * dX;
		return integral;
	}

}
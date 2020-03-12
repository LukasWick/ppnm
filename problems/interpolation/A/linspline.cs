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
		return y[i] + (y[i + 1] - y[i]) / (x[i + 1] - x[i]) * (z - x[i]);

	}

	static public double linterpInteg (vector x, vector y, double z) {
		if (z < x[0] || z > x[x.size - 1]) {
			throw new System.ArgumentException ($"z = {z} is out of range x form {x[0]} to {x[x.size - 1]}", "z");
		}
		int i = 0;
		double integral = 0;

		while (z > x[i+1]) {
			integral += y[i] * (x[i + 1] - x[i]) + 1.0 / 2 * (y[i + 1] - y[i]) * (x[i + 1] - x[i]);
			i++;
		}

		integral += y[i] * (z - x[i]) + 1.0 / 2 * (y[i + 1] - y[i]) / (x[i + 1] - x[i]) * (z - x[i]) * (z - x[i]);
		return integral;
	}

}
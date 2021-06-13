using System;
using System.Numerics;

namespace FourierTransform
{
	public class FFT
	{
		/// <summary>
		/// Calculate of rotated module e^(-i*2*PI*k/N)
		/// </summary>
		/// <param name="k"></param>
		/// <param name="N"></param>
		/// <returns></returns>
		private static Complex w(int k, int N)
		{
			if (k % N == 0) return 1;
			double arg = -2 * Math.PI * k / N;
			return new Complex(Math.Cos(arg), Math.Sin(arg));
		}

		/// <summary>
		/// Perform Fourier Transformation
		/// </summary>
		/// <param name="sourceData">Array of <see cref="Complex"/> numbers</param>
		/// <returns>Resulted array of <see cref="Complex"/> numbers</returns>
		public static Complex[] FastFourierTransform(Complex[] sourceData)
		{
			Complex[] X;
			var N = sourceData.Length;
			if (N == 2)
			{
				/*X = new Complex[1];
				X[0] = sourceData[0];*/
				
				X = new Complex[2];
				X[0] = sourceData[0] + sourceData[1];
				X[1] = sourceData[0] - sourceData[1];
			}
			else
			{
				Complex[] x_even = new Complex[N / 2];
				Complex[] x_odd = new Complex[N / 2];
				for (int i = 0; i < N / 2; i++)
				{
					x_even[i] = sourceData[2 * i];
					x_odd[i] = sourceData[2 * i + 1];
				}

				Complex[] X_even = FastFourierTransform(x_even);
				Complex[] X_odd = FastFourierTransform(x_odd);
				X = new Complex[N];
				for (int i = 0; i < N / 2; i++)
				{
					X[i] = X_even[i] + w(i, N) * X_odd[i];
					X[i + N / 2] = X_even[i] - w(i, N) * X_odd[i];
				}
			}

			return X;
		}

		/// <summary>
		/// Centered a values got from FastFourierTransform
		/// </summary>
		/// <param name="X">Array of values got by FastFourierTransform calculation</param>
		/// <returns></returns>
		public static Complex[] nfft(Complex[] X)
		{
			int N = X.Length;
			Complex[] X_n = new Complex[N];
			for (int i = 0; i < N / 2; i++)
			{
				X_n[i] = X[N / 2 + i];
				X_n[N / 2 + i] = X[i];
			}

			return X_n;
		}
	}
}

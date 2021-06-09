using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using OxyPlot;
using OxyPlot.Series;

namespace FFTPresentation.View
{
	public class ViewPlotModel
	{
		public PlotModel MyModel { get; set; }
		public PlotModel ResultModel { get; set; }
		public PlotModel ColumnResultModel { get; set; }
		public Complex[] result { get; set; }

		public int N { get; set; }

		//private int N => (int)Math.Pow(2, 11);
		private double Arrange => 0.1;
		private double Step = 0.0001;// => Arrange / N;

		public ViewPlotModel()
		{
			MyModel = new PlotModel { Title = "x(t)" };
			//MyModel.Series.Add(new FunctionSeries(sourceFunction, 0, 0.5, 0.001, "sin(2pi*x)"));

			ResultModel = new PlotModel {Title = "A(t)"};
			//ResultModel.Series.Add(new FunctionSeries( Magnitude, 0, Arrange - Step, Step));

			ColumnResultModel = new PlotModel { Title = "Phase(t)" };

			//ColumnResultModel.Series.Add(new FunctionSeries(Phase, 0, Arrange - Step, Step));
		}

		private double Phase(double index)
		{
			int i = (int)(index / (Arrange / N));
			return result[i].Imaginary;
		}

		private double Magnitude(double index)
		{
			int i = (int)(index / (Arrange / N));
			return result[i].Magnitude;
		}

		private Complex[] GetResult()
		{
			
			var X = new Complex[N];
			for (int i = 0; i < N; i++)
			{
				X[i] = new Complex(sourceFunction(i/100.0), 0.0); 
			}

			return FourierTransform.FFT.FastFourierTransform(X);
		}

		private double sourceFunction(double x)
		{
			double result = 0.0;
			for (int i = 1; i < 6; i++)

			{
				result = result + i * Math.Cos(i * 10 * 2 * Math.PI * x);
			}

			return result;
			//return Math.Sin(10 * 2 * Math.PI * x) + 0.5 * Math.Sin(5 * 2 * Math.PI*x);
			//return Math.Sin(2 * Math.PI * x);
		}

		private IEnumerable<ColumnItem> CountList()
		{
			return result.Select(i => new ColumnItem(i.Magnitude));
		}
	}
}
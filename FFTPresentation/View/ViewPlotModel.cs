using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Numerics;

namespace FFTPresentation.View
{
	public class ViewPlotModel
	{
		public PlotModel SourcePlotModel { get; set; }
		public PlotModel MagnitudePlotModel { get; set; }
		public PlotModel PhasePlotModel { get; set; }
		public Complex[] ResultData { get; set; }
		public Complex[] SourceData { get; set; }
		public string FunctionName { get; set; }
		public int N { get; set; }
		public string Expression { get; set; }

		private double Arrange => 1;
		private double Step => Arrange / N;
		private int currentStep = 0;


		public ViewPlotModel()
		{
			GetInitResult();
			SourcePlotModel = new PlotModel { Title = "x(t)" };
			SourcePlotModel.Series.Add(new FunctionSeries(SourceFunction, 0, 0.5, 0.001, FunctionName));

			MagnitudePlotModel = new PlotModel {Title = "A(t)"};
			MagnitudePlotModel.Series.Add(new FunctionSeries(Magnitude, 0, Arrange - Step, Step));
			
			PhasePlotModel = new PlotModel { Title = "Phase(t)" };
			PhasePlotModel.Series.Add(new FunctionSeries(Phase, 0, Arrange - Step, Step));
		}

		public void UpdateSourcePlotModel()
		{
			for (int i = 0; i < N; i++)
			{
				SourceData[i] = new Complex(SourceFunction(i / 100.0), 0.0);
			}
			
			SourcePlotModel.InvalidatePlot(true);
		}

		public void UpdateResultedMode()
		{
			MagnitudePlotModel.InvalidatePlot(true);
			PhasePlotModel.InvalidatePlot(true);
		}

		private double Phase(double index)
		{
			int i = (int)(index / (Arrange / N));
			return ResultData[i].Imaginary;
		}

		private double Magnitude(double index)
		{
			int i = (int)(index / (Arrange / N));
			return ResultData[i].Magnitude;
		}

		private double Source(double index)
		{
			if (currentStep < SourceData.Length)
			{
				currentStep++;
				return SourceData[currentStep-1].Real;

			}

			currentStep = 0;
			return 0;
		}

		private void GetInitResult()
		{
			N = (int)Math.Pow(2, 11);
			Expression = "sum(i,1,6, i * cos(i * 10 * 2 * pi * x))";
			SourceData = new Complex[N];
			for (int i = 0; i < N; i++)
			{
				SourceData[i] = new Complex(SourceFunction(i/100.0), 0.0);
			}

			ResultData = FourierTransform.FFT.FastFourierTransform(SourceData);
		}

		private double SourceFunction(double x)
		{
			Function Ft = new Function($"Ft(x) = {Expression}");
			Argument xArgument = new Argument($"x = {x}");
			Expression ex = new Expression("Ft(x)", Ft, xArgument);
			return ex.calculate();
			//return result;
			//return Math.Sin(10 * 2 * Math.PI * x) + 0.5 * Math.Sin(5 * 2 * Math.PI*x);
			//return Math.Sin(2 * Math.PI * x);
			//result = result + i * Math.Cos(i * 10 * 2 * Math.PI * x);
		}
	}
 }
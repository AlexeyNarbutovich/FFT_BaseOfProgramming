using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Documents;
using FFTPresentation.View;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using LineSeries = OxyPlot.Series.LineSeries;

namespace FFTPresentation.Controller
{
	public class FFTCalculator
	{
		private readonly ViewPlotModel plotModel = new ViewPlotModel();
		public string FunctionText { get; set; }
		public string DiscreteRealDataText { get; set; }
		public string DiscreteImgDataText { get; set; }
		public string SamplesNumberText { get; set; }
		private int size => int.Parse(SamplesNumberText);
		private const double Arrange = 0.1;
		private Complex[] data;

		public void BuildSourceGraph()
		{
			if (!DiscreteRealDataText.Equals(string.Empty))
			{
				var discreteData = ConvertStringToDoubleList(DiscreteRealDataText, DiscreteImgDataText);
				data = ParseDiscreteNumbers(discreteData);
				List<DataPoint> dataPoints = new List<DataPoint>();
				var index = 0;
				foreach (var complex in data)
				{
					dataPoints.Add(new DataPoint(index, complex.Real));
					index++;
				}
				
			}
			else
			{
				ParseFunction(FunctionText);
			}

			//var result = InitAndProcessNumbers(data);
		}

		private double GetSource(double index)
		{
			int i = (int)(index / (Arrange / size));
			return data[i].Real;
		}

		public Complex[] InitAndProcessNumbers(Complex[] sourceData)
		{
			return FourierTransform.FFT.FastFourierTransform(sourceData);
		}

		public Complex[] ParseDiscreteNumbers(IEnumerable<(double,double)> sourceData)
		{
			var X = new List<Complex>();

			foreach (var numbers in sourceData)
			{
				X.Add(new Complex(numbers.Item1, numbers.Item2));
			}

			return X.ToArray();
		}

		public void BuildResultedGraph()
		{
			throw new NotImplementedException();
		}

		private void ParseFunction(string sourceData)
		{
			plotModel.Expression = sourceData;
			plotModel.UpdateSourcePlotModel();
			plotModel.UpdateResultedMode();
		}

		private List<double> processText(string text)
		{
			return text.Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.RemoveEmptyEntries)
				.Where(i => !i.Equals(string.Empty))
				.Select(s => double.Parse(s)).ToList();
		}

		private List<(double, double)> ConvertStringToDoubleList(string realValue, string imgValue)
		{
			var realData = processText(realValue);
			var imgData = processText(imgValue);
			int maxIndex = realData.Count > imgData.Count ? realData.Count : imgData.Count;

			var tuple = new List<(double, double)>();
			for (var i = 0; i < maxIndex; i++)
			{
				if (i >= realData.Count)
					realData.Add(0.0);
				if (i >= imgData.Count)
					imgData.Add(0.0);
				tuple.Insert(i, (realData[i], imgData[i]));
			}

			return tuple;
		}


		

	}
}
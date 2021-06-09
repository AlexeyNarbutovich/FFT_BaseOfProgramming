using FFTPresentation.View;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FFTPresentation.Models;

namespace FFTPresentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ViewPlotModel MyModel { get; private set; }
		public DiscreteData discreteDataList = new DiscreteData();
		
		public MainWindow()
		{
			InitializeComponent();
			tcStatus.Text = "Wait source data...";
		}

		private void DrawAxis()
		{/*
			const double margin = 10;
			double xMin = margin;
			double xMax = cnvCanvas.Width - margin;
			double yMin = margin;
			double yMax = cnvCanvas.Height - margin;
			const double step = 10;

			GeometryGroup xAxisGeom = new GeometryGroup();
			xAxisGeom.Children.Add(new LineGeometry(new Point(0, yMax), new Point(cnvCanvas.Width, yMax)));
			for (double x = xMin + step; x <= cnvCanvas.Width - step; x += step)
			{
				xAxisGeom.Children.Add(new LineGeometry(
					new Point(x, yMax - margin / 2),
					new Point(x, yMax + margin / 2)));
			}

			Path xAxisPath = new Path();
			xAxisPath.StrokeThickness = 1;
			xAxisPath.Stroke = Brushes.Black;
			xAxisPath.Data = xAxisGeom;

			cnvCanvas.Children.Add(xAxisPath);

			GeometryGroup yAxisGeom = new GeometryGroup();
			yAxisGeom.Children.Add(new LineGeometry(new Point(xMin, 0), new Point(xMin, cnvCanvas.Height)));
			for (double y = step; y <= cnvCanvas.Height - step; y += step)
			{
				yAxisGeom.Children.Add(new LineGeometry(
					new Point(xMin - margin / 2, y),
					new Point(xMin + margin / 2, y)));
			}

			Path yAxisPath = new Path();
			yAxisPath.StrokeThickness = 1;
			yAxisPath.Stroke = Brushes.Black;
			yAxisPath.Data = yAxisGeom;

			cnvCanvas.Children.Add(yAxisPath);*/
		}

		private void btnRun_Click(object sender, RoutedEventArgs e)
		{
			int.TryParse(cbSampleNumber.Text, out var sampleNumbers);

			Complex[] data;

			if (!txRealNumber.Text.Equals(string.Empty))
			{
				data = ParseDiscreteNumbers(txRealNumber.Text, txImaginaryNumber.Text);
			}
			else
			{
				data = ParseFunction(tbFunction.Text, sampleNumbers);
			}

			var result = InitAndProcessNumbers(data);
			var centeredResult = FourierTransform.FFT.nfft(result);

			PointCollection points = new PointCollection();
			for (int i = 0; i < result.Length; i++)
			{
				points.Add(new Point(i, result[i].Magnitude));
			}

			Polyline polyLine = new Polyline
			{
				StrokeThickness = 0.5,
				Stroke = Brushes.Blue,
				Points = points
			};
		}

		private void btnBuildGraph_Click(object sender, RoutedEventArgs e)
		{

		}

		private Complex[] InitAndProcessNumbers(Complex[] sourceData)
		{
			return FourierTransform.FFT.FastFourierTransform(sourceData);
		}

		private Complex[] ParseDiscreteNumbers(string realData, string imaginaryData)
		{
			var splitRealData = processText(realData);
			var size = splitRealData.Length;

			var X = new Complex[size];
			for (int i = 0; i < size; i++)
			{
				X[i] = new Complex(0.0, 0.0);
			}

			var splitImaginaryData = processText(imaginaryData);

			for (int i = 0; i < size; i++)
			{
				var imgData = "";

				if (i < splitImaginaryData.Length)
					imgData = splitImaginaryData[i];
				else 
					imgData = "0.0";

				X[i] = new Complex(double.Parse(splitRealData[i]), double.Parse(imgData));
			}

			return X;
		}

		private Complex[] ParseFunction(string data, int number)
		{
			var X = new Complex[number];
			for (var i = 0; i < number; i++)
			{
				X[i] = new Complex(Math.Cos(2 * Math.PI) * i, 0.0);
			}
			return X;
		}

		private string[] processText(string text)
		{
			return text.Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}

using FFTPresentation.Controller;
using System.Windows;

namespace FFTPresentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly FFTCalculator calculator;
		
		public MainWindow()
		{
			InitializeComponent();
			
			calculator = new FFTCalculator();
			tcStatus.Text = "Wait source data...";
		}

		private void btnRun_Click(object sender, RoutedEventArgs e)
		{
			UpdateData();
			calculator.BuildSourceGraph();
			calculator.BuildResultedGraph();
			sourcePlot.InvalidatePlot(true);
			phasePlot.InvalidatePlot(true);
			attPlot.InvalidatePlot(true);

		}

		private void btnBuildGraph_Click(object sender, RoutedEventArgs e)
		{
			UpdateData();
			calculator.BuildSourceGraph();
			sourcePlot.InvalidatePlot(true);
		}

		private void UpdateData()
		{
			calculator.FunctionText = tbFunction.Text;
			calculator.DiscreteRealDataText = txRealNumber.Text;
			calculator.DiscreteImgDataText = txImaginaryNumber.Text;
			calculator.SamplesNumberText = cbSampleNumber.Text;
		}
	}
}

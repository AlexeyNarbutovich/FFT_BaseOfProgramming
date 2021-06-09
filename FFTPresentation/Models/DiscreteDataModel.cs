using System.Collections;
using System.Collections.Generic;

namespace FFTPresentation.Models
{
	public class DiscreteDataModel
	{
		public string RealInput { get; set; }
		public string ImaginaryInput { get; set; }
	}

	public class DiscreteData
	{
		public List<DiscreteDataModel> DiscreteDataCollection => new List<DiscreteDataModel>();

		public void Add(DiscreteDataModel data)
		{
			DiscreteDataCollection.Add(data);
		}
	}
}
using System.Data;


namespace FFTPresentation.View
{
	public class FourierSourceViewModel
	{
		private DataTable dataTable;
		public DataTable SourceDataTable
		{
			get
			{
				if (dataTable == null)
					dataTable = CreateDataTable();
				return dataTable;
			}
		}

		private DataTable CreateDataTable()
		{
			var localDataTable = new DataTable();

			localDataTable.Columns.Add(new DataColumn("Real Input", typeof(string)));
			localDataTable.Columns.Add(new DataColumn("Imaginary Input", typeof(string)));
			var row = localDataTable.NewRow();
			row["Real Input"] = "0";
			row["Imaginary Input"] = "i";

			return localDataTable;
		}
	}
}
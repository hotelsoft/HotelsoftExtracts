using Advantage.Data.Provider;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtracts
{
	public class Extractor<T>
	{
		readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

		public void Extract(string query, CsvWriter writer, Func<IDataReader, T> mapper)
		{
			AdsDataReader rd = AdsHelper.ExecuteReader(connectionString, CommandType.Text, query);
			while (rd.Read())
			{

			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtracts
{
	public static class Extensions
	{
		#region IDataReader Extensios
		public static string GetString(this IDataReader rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? String.Empty : Convert.ToString(rd[col]);
		}

		public static DateTime GetDateTime(this IDataReader rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? DateTime.MinValue : Convert.ToDateTime(rd[col]);
		}

		public static double GetDouble(this IDataReader rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? 0 : Convert.ToDouble(rd[col]);
		}

		public static int GetInt16(this IDataReader rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? 0 : Convert.ToInt16(rd[col]);
		}
		#endregion

		#region DataRow Extensions
		public static string GetString(this DataRow rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? String.Empty : Convert.ToString(rd[col]);
		}

		public static DateTime GetDateTime(this DataRow rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? DateTime.MinValue : Convert.ToDateTime(rd[col]);
		}

		public static double GetDouble(this DataRow rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? 0 : Convert.ToDouble(rd[col]);
		}

		public static int GetInt16(this DataRow rd, string col)
		{
			return Convert.IsDBNull(rd[col]) ? 0 : Convert.ToInt16(rd[col]);
		}
		#endregion
	}
}

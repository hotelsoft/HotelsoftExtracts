using System;
using System.Data;

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

		#region Folder Extensions
		public static void Empty(this System.IO.DirectoryInfo directory)
		{
			foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
			foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
		}
		#endregion
	}
}

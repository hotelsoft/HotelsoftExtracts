using Advantage.Data.Provider;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataExtracts
{
	public class GroupsExtract
	{
		private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
		readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
		readonly string query = @"
		SELECT 
			CURRENT_DATE() AS EXTRACTDATE,
			DATUM, ALTDESC, KAT, ALLOTTED, PICKUP, SALES, CUTOFF, RATE1, RATE2, RATE3
		FROM GALLOTT
		WHERE ALLOTTED > 0 AND KAT NOT IN ('PM', 'PF', 'PX')
		";

		public void Extract(string fileName)
		{
			LOGGER.Trace("Extract: {0}", fileName);

			var utf8WithoutBom = new UTF8Encoding(false);
			var config = new CsvConfiguration { TrimFields = true, TrimHeaders = true, Encoding = utf8WithoutBom, QuoteAllFields = true };
			config.RegisterClassMap<GroupsRecordMap>();
			LOGGER.Trace("Executing query: {0}", query);

			//Execute query and load the results into data reader
			AdsDataReader rd = AdsHelper.ExecuteReader(connectionString, CommandType.Text, query);
			DataTable allotdt = new DataTable();
			allotdt.Load(rd);
			rd.Close();

			var group = from allot in allotdt.AsEnumerable()
						group allot by allot.Field<string>("ALTDESC") into grp
						select new
						{
							ExtractDate = grp.Select(r => r.Field<DateTime>("EXTRACTDATE")).First(),
							GroupCode = grp.Key,
							Checkin = grp.Min(r => r.Field<DateTime>("DATUM")),
							Checkout = grp.Max(r => r.Field<DateTime>("DATUM")),
							Cutoff = grp.Select(r => r.IsNull("CUTOFF") ? null : r["CUTOFF"]).First(),
							Rates = grp.Select<DataRow, XElement>(r => new XElement("Rate",
							new XAttribute("BEDTYPE", r.GetString("KAT")),
							new XAttribute("STARTDATE", r.GetDateTime("DATUM").ToString("yyyy-MM-dd")),
							new XAttribute("INTROOMS", r.GetInt16("ALLOTTED")),
							new XAttribute("ROOMSUSED", r.GetInt16("PICKUP")),
							new XAttribute("ROOMSREL", 0),
							new XAttribute("RATE1", r.GetDouble("RATE1")),
							new XAttribute("RATE2", r.GetDouble("RATE2"))))
						};

			using (var streamWriter = new StreamWriter(Path.Combine(Path.GetTempPath(), fileName), false, utf8WithoutBom) { AutoFlush = true })
			using (var csv = new CsvWriter(streamWriter, config))
			{
				csv.WriteHeader<GroupsRecord>();
				int counter = 0;
				foreach (var obj in group)
				{
					var record = new GroupsRecord()
					{
						ExtractDate = obj.ExtractDate,
						Code = obj.GroupCode,
						Nane = obj.GroupCode,
						Checkin = obj.Checkin,
						Checkout = obj.Checkout,
						CreateDate = obj.Checkin,
						Rates = new XElement("Rates", obj.Rates).ToString(),
						Status = "A"
					};
					if (obj.Cutoff != null)
					{
						record.Cutoff = Convert.ToDateTime(obj.Cutoff);
					}
					csv.WriteRecord(record);
					counter++;
				}
				LOGGER.Info("Wrote {0} records", counter);
			}

			#region Fileupload
			try
			{
				var task = Task.Run(async () => await DropboxUploader.UploadFile(fileName));
				task.Wait();
			}
			catch (Exception ex)
			{
				LOGGER.Warn(ex, "Error in uploading the file");
				File.Copy(Path.Combine(Path.GetTempPath(), fileName), Path.Combine(ConfigurationManager.AppSettings["LocalDropboxFoler"], fileName), true);
				File.Delete(Path.Combine(Path.GetTempPath(), fileName));
			}
			#endregion
		}
	}
}

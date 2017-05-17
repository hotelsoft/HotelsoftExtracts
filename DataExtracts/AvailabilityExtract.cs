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

namespace DataExtracts
{
	public class AvailabilityExtract
	{
		private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
		readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

		const string availabilitysql = @"SELECT  CURRENT_DATE() AS EXTRACTDATE, DATUM AS FOREDATE, KAT AS ROOMTYPE, FREI AS ROOMS,
			0  AS STAYOVERS, ZB AS CHECKINS, ZIM6 AS BLOCKED, OOO AS OOOS,
			(select count(*) from GRES2 R where G.DATUM >= R.ANREISE AND G.DATUM < R.ABREISE AND R.PCODE IN ('COMP', 'COMPBB') AND R.KAT =  G.KAT) AS COMPLIMENTARY,
			(select count(*) from GRES2 R where G.DATUM >= R.ANREISE AND G.DATUM < R.ABREISE AND R.PCODE IN ('HOUSE') AND R.KAT =  G.KAT ) AS OWNERRELATED 
		FROM GFORB G 
			WHERE GENERIC = 'HTL' AND DATUM BETWEEN CURDATE() AND CURDATE()+366";

		public void Extract(string fileName)
		{
			LOGGER.Trace("Extract: {0}", fileName);

			var utf8WithoutBom = new UTF8Encoding(false);
			var config = new CsvConfiguration { TrimFields = true, TrimHeaders = true, Encoding = utf8WithoutBom, QuoteAllFields = true };
			config.RegisterClassMap<AvailabilityRecordMap>();
			LOGGER.Trace("Executing query: {0}", availabilitysql);

			AdsDataReader rd = AdsHelper.ExecuteReader(connectionString, CommandType.Text, availabilitysql);
			using (var streamWriter = new StreamWriter(Path.Combine(Path.GetTempPath(), fileName), false, utf8WithoutBom) { AutoFlush = true })
			using (var csv = new CsvWriter(streamWriter, config))
			{
				csv.WriteHeader<AvailabilityRecord>();

				int counter = 0;
				#region DataReaderLoop
				while (rd.Read())
				{
					#region AvailabilityRecordConstructor
					var record = new AvailabilityRecord
					{
						ExtractDate = rd.GetDateTime("EXTRACTDATE"),
						StayDate = rd.GetDateTime("FOREDATE"),
						RoomType = rd.GetString("ROOMTYPE"),
						Rooms = rd.GetInt16("ROOMS"),
						CheckIns = rd.GetInt16("CHECKINS"),
						OOOS = rd.GetInt16("OOOS"),
						Comps = rd.GetInt16("COMPLIMENTARY"),
						Owners = rd.GetInt16("OWNERRELATED"),
						Blocked = rd.GetInt16("BLOCKED")
					};
					#endregion

					csv.WriteRecord(record);
					counter++;
				}
				rd.Close();
				#endregion
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

using Advantage.Data.Provider;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtracts
{
	public class ReservationsExtract
	{
		private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
		readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
		readonly string query = @"
		SELECT 
			CURRENT_DATE() AS EXTRACTDATE
			, G.REGISTER AS RESERVATIONID, G.AM AS CREATIONDT, G.ANREISE AS CHECKIN, G.ABREISE AS CHECKOUT, G.PREIS AS ROOMRATE
			, G.PREIS AS ENDRATE, CASE YEAR(G.STORNO) > 2000 WHEN True THEN 'C' ELSE 'R' END  AS RESERVESTATUS, G.ERW AS ADULTS, G.KIN AS CHILD, 1 AS NIGHTS
			, '' AS RATE_REQ, G.KAT AS ROOMTYPE, G.ANZ AS ROOMS, G.MARKET AS MARKETCD, M.TEXT AS MARKETNAME, G.ALOTM AS  BLOCKID, '' AS GROUPNAME
			, G.SCODE AS SOURCE, S.TEXT AS SOURCENAME, G.COMPANY AS COMPANY, G.RT7 AS COMPANYNAME, G.TRAVEL AS TRAVELID, G.RT4 AS TRAVELNAME
			, G.GASTNR AS GUESTID, T.ANREDE AS PREFIX, T.VORNAME AS FIRSTNAME, T.NAME AS LASTNAME, T.EMAIL AS EMAIL, T.STRASSE1 AS STREET, T.ORT AS CITY
			, T.TELEFON AS PHONE1, '' AS PHONE2, T.PLZ AS ZIPCODE, T.LAND AS COUNTRY, G.RTXT AS NOTES 
		FROM GRES2 G 
			INNER JOIN MARKETS M ON M.MARKET = G.MARKET 
			INNER JOIN GAESTEST T ON T.NUMMER = G.GASTNR 
			INNER JOIN SOURCES S ON S.SCODE = G.SCODE 
		AND G.KAT NOT IN ('PM', 'PF', 'PX') ";

		public void Extract(string fileName)
		{
			LOGGER.Trace("Extract");

			var utf8WithoutBom = new UTF8Encoding(false);
			var config = new CsvConfiguration { TrimFields = true, TrimHeaders = true, Encoding = utf8WithoutBom, QuoteAllFields = true };
			config.RegisterClassMap<ReservationRecordMap>();

			LOGGER.Trace("Executing query: {0}", query);
			AdsDataReader rd = AdsHelper.ExecuteReader(connectionString, CommandType.Text, query);
			using (var streamWriter = new StreamWriter(Path.Combine(Path.GetTempPath(), fileName), false, utf8WithoutBom) { AutoFlush = true })
			using (var csv = new CsvWriter(streamWriter, config))
			{
				csv.WriteHeader<ReservationRecord>();

				int counter = 0;
				#region DataReaderLoop
				while (rd.Read())
				{
					#region ReservationRecordConstructor
					var record = new ReservationRecord
					{
						ExtractDate = rd.GetDateTime("EXTRACTDATE"),
						ReservationId = rd.GetString("RESERVATIONID"),
						CreationDate = rd.GetDateTime("CREATIONDT"),
						Checkin = rd.GetDateTime("CHECKIN"),
						Checkout = rd.GetDateTime("CHECKOUT"),
						RoomRate = rd.GetDouble("ROOMRATE"),
						EndRate = rd.GetDouble("ROOMRATE"),
						Status = rd.GetString("RESERVESTATUS"),
						Adults = rd.GetInt16("ADULTS"),
						Children = rd.GetInt16("CHILD"),
						RoomType = rd.GetString("ROOMTYPE"),
						Rooms = rd.GetInt16("ROOMS"),
						MarketCode = rd.GetString("MARKETCD"),
						MarketName = rd.GetString("MARKETNAME"),
						SourceCode = rd.GetString("SOURCE"),
						SourceName = rd.GetString("SOURCENAME"),
						AgentCode = rd.GetString("TRAVELID"),
						AgentName = rd.GetString("TRAVELNAME"),
						CompanyCode = rd.GetString("COMPANY"),
						CompanyName = rd.GetString("COMPANYNAME"),
						GroupCode = rd.GetString("BLOCKID"),
						GuestId = rd.GetString("GUESTID"),
						FirstName = rd.GetString("FIRSTNAME"),
						LastName = rd.GetString("LASTNAME"),
						Email = rd.GetString("EMAIL"),
						Street1 = rd.GetString("STREET"),
						City = rd.GetString("CITY"),
						ZipCode = rd.GetString("ZIPCODE"),
						Country = rd.GetString("COUNTRY"),
						Phone1 = rd.GetString("PHONE1"),
						Notes = rd.GetString("NOTES")
					};
					#endregion

					if (record.CreationDate == DateTime.MinValue)
					{
						record.CreationDate = record.Checkin;
					}
					if (record.Checkout == DateTime.MinValue)
					{
						record.Checkout = record.Checkin.AddDays(1);
					}
					record.Nights = (record.Checkout - record.Checkin).Days;
					csv.WriteRecord(record);
					counter++;
				}
				rd.Close();
				#endregion
				LOGGER.Info("Wrote {0} records", counter);
			}

			LOGGER.Info("Done writing Reservation extract");
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

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExtracts;
using System.Configuration;
using System.Data.Odbc;
using System.Data;
using System.Xml.Linq;
using Dropbox.Api;
using System.IO;


namespace ConsoleApp
{
	class Program
	{
		private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			string archiveFile = Path.Combine(ConfigurationManager.AppSettings["ArchiveFolder"],
				$"{DateTime.Now.AddDays(-1).ToString("MMdd")}_001.BAC").ToString();
			bool isSuccess = BackupExtractor.Extract(archiveFile, ConfigurationManager.AppSettings["DBFolder"]);
			if (!isSuccess)
			{
				return;
			}
			if (ConfigurationManager.AppSettings["ReservationsFile"] != null)
			{
				try
				{
					var extract = new ReservationsExtract();
					extract.Extract(string.Format(ConfigurationManager.AppSettings["ReservationsFile"], DateTime.Now));
				}
				catch (Exception ex)
				{
					LOGGER.Error(ex, "Error in Reservation Extract");
				}
			}
			if (ConfigurationManager.AppSettings["HistReservationFile"] != null)
			{
				try
				{
					var extract = new HistoryExtraction();
					extract.Extract(string.Format(ConfigurationManager.AppSettings["HistReservationFile"], DateTime.Now));
				}
				catch (Exception ex)
				{
					LOGGER.Error(ex, "Error in History Extract");
				}
			}
			if (ConfigurationManager.AppSettings["AvailabilityFile"] != null)
			{
				try
				{
					var extract = new AvailabilityExtract();
					extract.Extract(string.Format(ConfigurationManager.AppSettings["AvailabilityFile"], DateTime.Now));
				}
				catch (Exception ex)
				{
					LOGGER.Error(ex, "Error in Availability Extract");
				}
			}
			if (ConfigurationManager.AppSettings["GroupsFile"] != null)
			{
				try
				{
					var extract = new GroupsExtract();
					extract.Extract(string.Format(ConfigurationManager.AppSettings["GroupsFile"], DateTime.Now));

				}
				catch (Exception ex)
				{
					LOGGER.Error(ex, "Error in Groups data");
				}
			}
			DirectoryInfo di = new DirectoryInfo(ConfigurationManager.AppSettings["DBFolder"]);
			di.Empty();
		}
	}
}

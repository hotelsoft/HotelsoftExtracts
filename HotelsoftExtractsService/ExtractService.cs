using DataExtracts;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelsoftExtractsService
{
	public partial class ExtractService : ServiceBase
	{
		private static Logger LOGGER = LogManager.GetCurrentClassLogger();

		private Timer Schedular;
		public ExtractService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			this.ScheduleService();
		}

		protected override void OnStop()
		{
		}

		public void ScheduleService()
		{

			try
			{
				Schedular = new Timer(new TimerCallback(SchedularCallback));
				string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
				LOGGER.Info("Simple Service Mode: {0}", mode);

				//Set the Default Time.
				DateTime scheduledTime = DateTime.MinValue;

				if (mode == "DAILY")
				{
					//Get the Scheduled Time from AppSettings.
					scheduledTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
					if (DateTime.Now > scheduledTime)
					{
						//If Scheduled Time is passed set Schedule for the next day.
						scheduledTime = scheduledTime.AddDays(1);
					}
				}

				if (mode.ToUpper() == "INTERVAL")
				{
					//Get the Interval in Minutes from AppSettings.
					int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

					//Set the Scheduled Time by adding the Interval to Current Time.
					scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
					if (DateTime.Now > scheduledTime)
					{
						//If Scheduled Time is passed set Schedule for the next Interval.
						scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
					}
				}

				TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
				LOGGER.Info("Simple Service scheduled to run after: {0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)",
					timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

				//Get the difference in Minutes between the Scheduled and Current Time.
				int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

				//Change the Timer's Due Time.
				Schedular.Change(dueTime, Timeout.Infinite);
			}
			catch (Exception ex)
			{
				LOGGER.Error(ex);

				//Stop the Windows Service.
				using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("HotelsoftExtracts"))
				{
					serviceController.Stop();
				}
			}
		}

		private void SchedularCallback(object e)
		{
			string archiveFile = BackupExtractor.GetBackupFile(ConfigurationManager.AppSettings["ArchiveFolder"]);
			bool isSuccess = BackupExtractor.Extract(archiveFile, ConfigurationManager.AppSettings["DBFolder"]);
			if (!isSuccess)
			{
				return;
			}
			try
			{

				var extract = new ReservationsExtract();
				extract.Extract(string.Format(ConfigurationManager.AppSettings["ReservationsFile"], DateTime.Now));
			}
			catch (Exception ex)
			{
				LOGGER.Error(ex, "Error in extracting reservations data");
			}
			try
			{
				var extract = new HistoryExtraction();
				extract.Extract(string.Format(ConfigurationManager.AppSettings["HistReservationFile"], DateTime.Now));
			}
			catch (Exception ex)
			{
				LOGGER.Error(ex, "Error in extracting history data");
			}
			try
			{
				var extract = new AvailabilityExtract();
				extract.Extract(string.Format(ConfigurationManager.AppSettings["AvailabilityFile"], DateTime.Now));
			}
			catch (Exception ex)
			{
				LOGGER.Error(ex, "Error in extracting availability data");
			}
			try
			{
				var extract = new GroupsExtract();
				extract.Extract(string.Format(ConfigurationManager.AppSettings["GroupsFile"], DateTime.Now));
			}

			catch (Exception ex)
			{
				LOGGER.Error(ex, "Error in extracting groups data");
			}
			DirectoryInfo di = new DirectoryInfo(ConfigurationManager.AppSettings["DBFolder"]);
			di.Empty();
			this.ScheduleService();
		}
	}
}

using NLog;
using System;
using System.IO;
using System.IO.Compression;

namespace DataExtracts
{
	public class BackupExtractor
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public static bool Extract(string srcfile, string destFolder)
		{
			DirectoryInfo di = new DirectoryInfo(destFolder);
			if (!di.Exists)
			{
				logger.Warn("{0} directory doesn't exist.", destFolder);
				return false;
			}
			logger.Debug("Emptying folder {0}", di.FullName);
			di.Empty();
			logger.Info("Extracting {0} to {1}", srcfile, destFolder);
			try { ZipFile.ExtractToDirectory(srcfile, destFolder); }
			catch (Exception e) {
				logger.Error(e, "Error in extraction zip file");
				return false;
			}
			return true;
		}
	}
}

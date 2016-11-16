using Dropbox.Api;
using Dropbox.Api.Files;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtracts
{
	class DropboxUploader
	{
		private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

		public static async Task UploadFile(string file)
		{
			using (var dbx = new DropboxClient(ConfigurationManager.AppSettings["DropboxToken"]))
			using (var stream = File.OpenRead(Path.Combine(Path.GetTempPath(), file)))
			{

				var folder = ConfigurationManager.AppSettings["DropboxFolder"];
				var updated = await dbx.Files.UploadAsync(
					folder + "/" + file,
					WriteMode.Overwrite.Instance,
					body: stream);
				LOGGER.Info("Saved {0}/{1} rev {2}", folder, file, updated.Rev);

				var full = await dbx.Users.GetCurrentAccountAsync();
				LOGGER.Info("{0} - {1} - {2}", full.Name.DisplayName, full.Email, full.Team.Name);
			}
		}
	}
}

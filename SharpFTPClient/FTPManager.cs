using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;

namespace SharpFTPClient
{
    class FTPManager
    {
        public FtpClient ftpClient
		{
			get; set;
		}
		public bool IsConnected
		{
			get; set;
		}

		public Logger logger
        {
			get; set;
        }

		public FTPManager(Logger _log)
        {
			ftpClient = null;
			IsConnected = false;
			logger = _log;
        }
        public async Task Connect(string host, string user, string password)
        {
			logger.AddToLog("Attempting connection to: " + host + " with username " + user, Color.Black);
			var token = new CancellationToken();
			ftpClient = new FtpClient(host, user, password);
            ftpClient.EncryptionMode = FtpEncryptionMode.Explicit;
            ftpClient.ValidateAnyCertificate = true;
			Task t = ftpClient.ConnectAsync(token);
			await t;
			if (ftpClient.IsAuthenticated)
			{
				IsConnected = true;
				logger.AddToLog("Connected to " + host + " with username " + user, Color.Green);
			}
			
        }

		public async Task<IEnumerable<FTPListingDetail>> GetDirectoryListing(string workingDir)
        {
            logger.AddToLog("Listing files of " + workingDir, Color.Black);
            var token = new CancellationToken();
			var result = new List<FTPListingDetail>();
			foreach (var item in await ftpClient.GetListingAsync(workingDir, FtpListOption.ForceList))
			{
				var fileOrDir = new FTPListingDetail();
				switch (item.Type)
                {
					case FtpFileSystemObjectType.Directory:
						fileOrDir.IsDirectory = true;
						break;
					case FtpFileSystemObjectType.File:
						fileOrDir.IsDirectory = false;
						break;
				}
				fileOrDir.Name = item.Name;
				fileOrDir.FullPath = item.FullName;
				fileOrDir.Permission = (await ftpClient.GetChmodAsync(item.FullName)).ToString();
				fileOrDir.Size = await ftpClient.GetFileSizeAsync(item.FullName, -1, token);
				fileOrDir.ModifiedTime = await ftpClient.GetModifiedTimeAsync(item.FullName, token);

				result.Add(fileOrDir);
			}
			return result;
        }

		public async Task<int> GetSubDirectoriesCount(string targetDir)
        {
			return (await ftpClient.GetListingAsync(targetDir, FtpListOption.ForceList)).Count();
		}

		public void DownloadFile(string remoteFileLocation, string localDestination)
        {
            logger.AddToLog("Downloading file: " + remoteFileLocation, Color.Black);
			Action<FtpProgress> progress = delegate(FtpProgress p){
				if (p.Progress == 1)
				{
					logger.AddToLog("Successfully downloaded file to: " + localDestination, Color.Black);
				}
				else
				{
					logger.AddToLog("Downloading file: " + localDestination + " : " + p.Progress + "%", Color.Black); 
				}
			};

			// download a file and ensure the local directory is created
			ftpClient.DownloadFile(localDestination, remoteFileLocation, FtpLocalExists.Resume, FtpVerify.None, progress);
			
        }

		public void DownloadDirectory(string remoteLocation, string localDestination)
		{

			logger.AddToLog("Downloading directory: " + remoteLocation, Color.Black);

			// download a file and ensure the local directory is created
			ftpClient.DownloadDirectory(localDestination, remoteLocation, FtpFolderSyncMode.Mirror);

		}

		public void RenameFileOrDirectory(string remoteLocation, string remoteDestination)
		{

			logger.AddToLog("Renaming: " + remoteLocation + " to " + remoteDestination, Color.Green);

			// rename a file or a directory
			// this could fail as renaming is server-side dependent.
			ftpClient.Rename(remoteLocation, remoteDestination);

		}

		public void Delete(string remoteLocation, bool isDirectory)
		{

			logger.AddToLog("Deleting: " + remoteLocation, Color.Black);

			// check if it is a directory
			if (isDirectory)
            {
				ftpClient.DeleteDirectory(remoteLocation);
				logger.AddToLog("Deleted directory: " + remoteLocation, Color.Green);
			}
			else
            {
				ftpClient.DeleteFile(remoteLocation);
				logger.AddToLog("Deleted file: " + remoteLocation, Color.Green);
			}

		}

		public void NewFolder(string remoteDestination)
		{
			logger.AddToLog("Creating new folder: " + remoteDestination, Color.Black);
			ftpClient.CreateDirectory(remoteDestination, true);

		}

		public void NewFile(string remoteDestination, string fileName)
		{

			logger.AddToLog("Creating new file: " + remoteDestination + "/" + fileName, Color.Black);

			DirectoryInfo di = Directory.CreateDirectory(@"C:\temp\");
			string localTempPath = @"C:\temp\" + fileName;
			try
			{
				// Create the file, or overwrite if the file exists.
				using (FileStream fs = File.Create(localTempPath))
				{
					byte[] info = new UTF8Encoding(true).GetBytes("");
					// Add some information to the file.
					fs.Write(info, 0, info.Length);
				}

				ftpClient.UploadFile(localTempPath, remoteDestination);

				// Delete the file.
				File.Delete(localTempPath);
				di.Delete();
			}

			catch (Exception ex)
			{
				logger.AddToLog("Failed creating new file: " + remoteDestination + "/" + fileName, Color.Red);
				Console.WriteLine(ex.ToString());
			}

		}
	}
}

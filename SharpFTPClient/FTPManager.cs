using System;
using System.Collections.Generic;
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

		public FTPManager()
        {
			ftpClient = null;
			IsConnected = false;
        }
        public async Task Connect(string host, string user, string password)
        {
			var token = new CancellationToken();

			ftpClient = new FtpClient(host, user, password);
            ftpClient.EncryptionMode = FtpEncryptionMode.Explicit;
            ftpClient.ValidateAnyCertificate = true;
			Task t = ftpClient.ConnectAsync(token);
			await t;
			if (ftpClient.IsAuthenticated)
				IsConnected = true;
			// get a recursive listing of the files & folders in a specific folder
			foreach (var item in ftpClient.GetListing(ftpClient.GetWorkingDirectory(), FtpListOption.Recursive))
			{
				switch (item.Type)
				{

					case FtpFileSystemObjectType.Directory:

						Console.WriteLine("Directory -> " + item.FullName);
						Console.WriteLine("Modified date:  " + await ftpClient.GetModifiedTimeAsync(item.FullName, token));

						break;

					case FtpFileSystemObjectType.File:

						Console.WriteLine("File ->  " + item.Name);
						Console.WriteLine("File size:  " + await ftpClient.GetFileSizeAsync(item.FullName, -1 , token));
						Console.WriteLine("Modified date:  " + await ftpClient.GetModifiedTimeAsync(item.FullName, token));
						Console.WriteLine("Chmod:  " + await ftpClient.GetChmodAsync(item.FullName));

						break;

					case FtpFileSystemObjectType.Link:
						break;
				}
			}
			
        }

		public async Task<IEnumerable<FTPListingDetail>> GetDirectoryListing(string workingDir)
        {
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

			Console.WriteLine("DownloadFile call");
			Action<FtpProgress> progress = delegate(FtpProgress p){
				if (p.Progress == 1)
				{
					Console.WriteLine("Successfully download file " + remoteFileLocation + " to dest: " + localDestination);
				}
				else
				{
					Console.WriteLine("Download of file " + remoteFileLocation + " ----> " + p.Progress + "%");
				}
			};

			// download a file and ensure the local directory is created
			ftpClient.DownloadFile(localDestination, remoteFileLocation, FtpLocalExists.Resume, FtpVerify.None, progress);
			
        }

		public void DownloadDirectory(string remoteLocation, string localDestination)
		{

			Console.WriteLine("DownloadDirectory call");
			
			// download a file and ensure the local directory is created
			ftpClient.DownloadDirectory(localDestination, remoteLocation, FtpFolderSyncMode.Mirror);

		}

		public void RenameFileOrDirectory(string remoteLocation, string remoteDestination)
		{

			Console.WriteLine("RenameFileOrDirectory call");

			// rename a file or a directory
			// this could fail as renaming is server-side dependent.
			ftpClient.Rename(remoteLocation, remoteDestination);

		}

		public void Delete(string remoteLocation, bool isDirectory)
		{

			Console.WriteLine("Delete call");

			// check if it is a directory
			if (isDirectory)
            {
				ftpClient.DeleteDirectory(remoteLocation);
            }
			else
            {
				ftpClient.DeleteFile(remoteLocation);
            }

		}
	}
}

using System.Net;
using System.IO;
using System;

namespace KBZ.Utils.Infrastructure
{
    public class FtpUpload
    {

        private string beforeSettlement = "BeforeSettlement";
        private string afterSettlement = "AfterSettlement";
        private string batchDetails = "BatchDetails";
        private string userName = "ftpuser";
        private string password = "ftp@123456";
        private string ftpPath = "ftp://180.234.222.40/";

        public void UploadBatchFileToFtp(string sourcePath, string fileName)
        {

            string fileDestination = ftpPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + beforeSettlement + "/" + fileName;
            //AfterSettlement
            var result = FtpDirectoryExists(fileDestination);

            if (!result)
            {
                CreateDirectorys(ftpPath);
            }

            FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(fileDestination);
            requestFTPUploader.Credentials = new NetworkCredential(userName, password);
            requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

            FileInfo fileInfo = new FileInfo(sourcePath);
            FileStream fileStream = fileInfo.OpenRead();

            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            Stream uploadStream = requestFTPUploader.GetRequestStream();
            int contentLength = fileStream.Read(buffer, 0, bufferLength);

            while (contentLength != 0)
            {
                uploadStream.Write(buffer, 0, contentLength);
                contentLength = fileStream.Read(buffer, 0, bufferLength);
            }

            uploadStream.Close();
            fileStream.Close();
            fileInfo = null;

            requestFTPUploader = null;
        }

        public void UploadBatchDetailsFileToFtp(string sourcePath, string fileName)
        {

            string fileDestination = ftpPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + batchDetails + "/" + fileName;
            //AfterSettlement
            var result = FtpDirectoryExists(fileDestination);

            //if (!result)
            //{
            //    CreateDirectorys(destPath);
            //}

            FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(fileDestination);
            requestFTPUploader.Credentials = new NetworkCredential(userName, password);
            requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

            FileInfo fileInfo = new FileInfo(sourcePath);
            FileStream fileStream = fileInfo.OpenRead();

            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            Stream uploadStream = requestFTPUploader.GetRequestStream();
            int contentLength = fileStream.Read(buffer, 0, bufferLength);

            while (contentLength != 0)
            {
                uploadStream.Write(buffer, 0, contentLength);
                contentLength = fileStream.Read(buffer, 0, bufferLength);
            }

            uploadStream.Close();
            fileStream.Close();
            fileInfo = null;

            requestFTPUploader = null;
        }

        private void CreateDirectorys(string destinationPath)
        {
            WebRequest dateRequest = WebRequest.Create(destinationPath + DateTime.Now.ToString("dd-MMM-yy"));
            dateRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            dateRequest.Credentials = new NetworkCredential(userName, password);
            var dateResp = (FtpWebResponse)dateRequest.GetResponse();

            WebRequest beforeRequest = WebRequest.Create(destinationPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + beforeSettlement);
            beforeRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            beforeRequest.Credentials = new NetworkCredential(userName, password);
            var beforeresp = (FtpWebResponse)beforeRequest.GetResponse();

            WebRequest AfterRequest = WebRequest.Create(destinationPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + afterSettlement);
            AfterRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            AfterRequest.Credentials = new NetworkCredential(userName, password);
            var AfterResp = (FtpWebResponse)AfterRequest.GetResponse();

            WebRequest BatchDaeatlsRequest = WebRequest.Create(destinationPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + batchDetails);
            BatchDaeatlsRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            BatchDaeatlsRequest.Credentials = new NetworkCredential(userName, password);
            var batchDetailsResp = (FtpWebResponse)BatchDaeatlsRequest.GetResponse();


        }

        public void DownloadFileFromFtp(string localPath, string fileName)
        {


            string fileDestination = ftpPath + DateTime.Now.ToString("dd-MMM-yy") + "/" + afterSettlement + "/" + fileName;
            //AfterSettlement
            var result = FtpDirectoryExists(fileDestination);
            if (!result)
            {
                fileDestination = ftpPath + DateTime.Now.AddDays(-1).ToString("dd-MMM-yy") + "/" + afterSettlement + "/" + fileName;
                result = FtpDirectoryExists(fileDestination);
            }
            if (result)
            {
                FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpPath + DateTime.Now.ToString("dd-MMM-yy") + "\\" + afterSettlement + "\\" + fileName);
                requestFileDownload.Credentials = new NetworkCredential(userName, password);
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

                Stream responseStream = responseFileDownload.GetResponseStream();
                FileStream writeStream = new FileStream(localPath, FileMode.Create);

                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }

                responseStream.Close();
                writeStream.Close();
                requestFileDownload = null;
                writeStream = null;
            }

        }

        private bool FtpDirectoryExists(string directory)
        {

            try
            {
                var request = (FtpWebRequest)WebRequest.Create(directory);
                request.Credentials = new NetworkCredential(userName, password);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
                else
                    return true;
            }
            return true;
        }
    }
}
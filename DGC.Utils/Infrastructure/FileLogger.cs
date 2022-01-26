using System;
using System.IO;

namespace KBZ.Utils.Infrastructure
{
    public class FileLogger
    {
        private static readonly object LockObj = new object();
        private static string FilePath = @"D:\Log\ChqueBookPrintingSystem\";
        public static void Info(string str)
        {
            try
            {
                object s = new object();
                lock (LockObj)
                {
                    FileStream fStream = null;
                    StreamWriter sWriter = null;
                    if (!Directory.Exists(FilePath))
                    {
                        try
                        {
                            Directory.CreateDirectory(FilePath);
                        }
                        catch (Exception ex)
                        {
                            // throw ex;
                        }
                    }
                    string filePath = FilePath + String.Format(@"ApplicationLogFile.txt");

                    if (!File.Exists(filePath))
                    {

                        try
                        {
                            fStream = File.Create(filePath);
                        }
                        catch (Exception ex)
                        {
                            // throw ex;
                        }

                    }
                    else
                    {
                        if (fStream == null)
                        {
                            fStream = File.Open(filePath, FileMode.Append);
                        }
                    }
                    if (sWriter == null)
                    {
                        sWriter = new StreamWriter(fStream);

                    }
                    sWriter.WriteLine("Time : " + DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + " || " + str);
                    sWriter.Close();
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                //
            }
            
            
        }
    }
}

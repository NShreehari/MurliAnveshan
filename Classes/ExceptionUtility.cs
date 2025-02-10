using System;
using System.IO;
using System.Configuration;

namespace MurliSearch.Classes
{
    // Create our own utility for exceptions
    public sealed class ExceptionUtility
    {
        // All methods are static, so this can be private
        private ExceptionUtility()
        { }

        // Log an Exception
        public static void LogException(Exception ex)
        {
            StreamWriter streamWriter = null;
            try
            {
                // Include logic for logging exceptions
                // Get the absolute path to the log file
                string logFile = ConfigurationManager.ConnectionStrings["LogFilePath"].ConnectionString + "\\ErrorLog.txt";
                //logFile = HttpContext.Current.Server.MapPath(logFile);

                // Open the log file for append and write the log
                streamWriter = new StreamWriter(logFile, true);

                streamWriter.WriteLine("\n\n\n******************************  {0} ******************************", DateTime.Now);
                
                if (ex.InnerException != null)
                {
                    streamWriter.Write("Inner Exception Type: ");
                    streamWriter.WriteLine(ex.InnerException.GetType().ToString());
                    streamWriter.Write("Inner Exception: ");
                    streamWriter.WriteLine(ex.InnerException.Message);
                    streamWriter.Write("Inner Source: ");
                    streamWriter.WriteLine(ex.InnerException.Source);
                
                    if (ex.InnerException.StackTrace != null)
                    {
                        streamWriter.WriteLine("Inner Stack Trace: ");
                        streamWriter.WriteLine(ex.InnerException.StackTrace);
                    }
                }
                
                streamWriter.Write("Exception Type: ");
                streamWriter.WriteLine(ex.GetType().ToString());
                streamWriter.WriteLine("Exception: " + ex.Message);
                streamWriter.WriteLine("Source: " );
                streamWriter.WriteLine(ex.Source);
                streamWriter.WriteLine("Stack Trace: ");
                
                if (ex.StackTrace != null)
                {
                    streamWriter.WriteLine(ex.StackTrace);
                    streamWriter.WriteLine();
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                streamWriter?.Close();
            }
        }
    }
}

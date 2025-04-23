using System;
using System.IO;

namespace ThothSystemVersion1.Utilities
{
    public static class WriteException
    {
        public static void WriteExceptionToFile(string exceptionMessage)
        {
            try
            {
                // Get the base application directory
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Create Exceptions directory if it doesn't exist
                string exceptionsRoot = Path.Combine(basePath, "Exceptions");
                Directory.CreateDirectory(exceptionsRoot);

                // Create timestamped folder
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                string exceptionFolder = Path.Combine(exceptionsRoot, timestamp);
                Directory.CreateDirectory(exceptionFolder);

                // Create log file path
                string filePath = Path.Combine(exceptionFolder, "ExceptionLog.txt");

                // Write exception to file
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine($"Date: {DateTime.Now}");
                    writer.WriteLine($"Exception: {exceptionMessage}");
                    writer.WriteLine(); // Add empty line for separation
                }
            }
            catch
            {
                // Consider adding fallback logging or notification here
            }
        }

        public static void WriteExceptionToFile(Exception ex)
        {
            WriteExceptionToFile(ex.ToString());
        }
    }
}
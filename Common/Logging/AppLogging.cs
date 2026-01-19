using System.Diagnostics;

namespace Common.Logging
{
    public class AppLogging
    {
        private static string CheckOrCreateLogsBaseFolder()
        {
            try
            {

                string FolderPath = @"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MyProjectApp-Exception\AppException";

                if (!System.IO.Directory.Exists(FolderPath))
                    System.IO.Directory.CreateDirectory(FolderPath);

                return FolderPath;

            }
            catch (Exception ex)
            {
                return "";

            }
        }
        public static void LogException(string message)
        {
            try
            {
                string folderPath = CheckOrCreateLogsBaseFolder();
                if (string.IsNullOrWhiteSpace(folderPath)) return;


                string filepath = @"" + CheckOrCreateLogsBaseFolder() + @"\" + "Log" + "Exception" + DateTime.Today.ToString("dd-MM-yy") + ".txt";

                if (!File.Exists(filepath))
                {
                    FileStream fs = File.Create(filepath);
                    fs.Close();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.Write(" ------------------ Exception Logs! ------------------");
                    sw.WriteLine("");
                    sw.Write("Date: " + System.DateTime.Now.ToString());
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("Log Msg: ");
                    sw.WriteLine(message);

                    sw.WriteLine("");
                    sw.Write("Class Name: " + new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name);
                    sw.WriteLine("");
                    sw.Write("Function Name: " + new StackTrace().GetFrame(1).GetMethod());

                    sw.WriteLine("");
                    sw.WriteLine(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {

                LogException(ex.Message);
            }
        }
    }
}

#region

using System;
using System.Diagnostics;
using System.IO;

#endregion

namespace FieryBlade.Util
{
    public enum LogLevel
    {
        Error,
        Warning,
        Information
    }

    public class Logger
    {
        [Conditional("DEBUG")]
        public static void Log(string message, LogLevel level = LogLevel.Information)
        {
            switch (level)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Information:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }

            var compiledMessage = string.Format("[{0}] {1}", DateTime.Now, message);
            Console.WriteLine(compiledMessage);
            AppendToFile(compiledMessage);
        }

        private static void AppendToFile(string compiledMessage)
        {
            try
            {
                var logFileName = String.Format("FieryBlades.{0}.log",
                    DateTime.Now.ToShortDateString().Replace("/", "_"));
                var logNameCombinded = Path.Combine("Logs", logFileName);

                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                File.AppendAllText(logNameCombinded, compiledMessage + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
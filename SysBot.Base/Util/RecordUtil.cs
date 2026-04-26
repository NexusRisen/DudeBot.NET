using System.IO;
using System;

namespace SysBot.Base.Util
{
    public static class RecordUtil<T>
    {
        private static readonly string LogPath = Path.Combine("records", $"{typeof(T).Name}.txt");

        public static void Record(string message)
        {
            try
            {
                var dir = Path.GetDirectoryName(LogPath);
                if (dir != null && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.AppendAllText(LogPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log: {ex.Message}");
            }
        }
    }
}

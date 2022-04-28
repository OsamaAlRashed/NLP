using System.Diagnostics;

namespace NLP.Helpers
{
    public static class Helpers
    {
        public static string[] SentenceToWords(string value)
            => value.Trim().Split(" ").Select(x => x.Trim()).Where(x => x != "" && x.Trim() != "" && x != " " && x.Trim() != " ").ToArray();

        public static List<SelectDto> EnumToList(Type enumType)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("enumType should describe enum");

            var names = Enum.GetNames(enumType).Cast<object>();
            var values = Enum.GetValues(enumType).Cast<int>();

            var list = names.Zip(values).ToList();

            return list.Select(x => new SelectDto
            {
                Id = x.Second,
                Name = x.First.ToString()
            }).ToList();
        }

        public class SelectDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public static string ExecuteCommandSync(object command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = @"C:\Users\Osama Al-Rashed";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            return result;
        }

        public static string TryUploadFile(this IFormFile file, string uploadsFolderName, string webRootPath)
        {
            string path = null;
            try
            {
                if (file != null)
                {
                    var documentsDirectory = Path.Combine("wwwroot", "audio", uploadsFolderName);
                    if (!Directory.Exists(documentsDirectory))
                    {
                        Directory.CreateDirectory(documentsDirectory);
                    }
                    path = Path.Combine("audio", uploadsFolderName, Guid.NewGuid().ToString() + "_" + file.FileName);
                    string filePath = Path.Combine(webRootPath, path);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(fileStream);
                }
                return path;
            }
            catch (Exception e) when (e is IOException || e is Exception)
            {
                return "";
            }
        }
    }
}

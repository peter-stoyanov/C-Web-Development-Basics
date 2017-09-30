using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace FlipImages
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            const string resultDir = "results";

            var imageDirectoryInfo = new DirectoryInfo(currentDirectory + "\\images");

            FileInfo[] files = imageDirectoryInfo.GetFiles();

            if (Directory.Exists(resultDir))
            {
                Directory.Delete(resultDir, true);
            }

            Directory.CreateDirectory(resultDir);

            var taskList = new List<Task>();
            foreach (var file in files)
            {
                var task = Task.Run(() =>
                {
                    var image = Image.FromFile(file.FullName); // Ex: Out of memory

                    image.RotateFlip(RotateFlipType.RotateNoneFlipXY);

                    image.Save($"{resultDir}\\flip-{file.Name}");
                });

                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());
        }
    }
}

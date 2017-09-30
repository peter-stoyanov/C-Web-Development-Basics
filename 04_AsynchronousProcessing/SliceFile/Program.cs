using System;
using System.IO;
using System.Threading.Tasks;

namespace SliceFile
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "quit")
                {
                    break;
                }

                string fileName = input;
                string destinationFolder = Console.ReadLine();
                int numberOfPieces = int.Parse(Console.ReadLine());

                SliceAsync(fileName, destinationFolder, numberOfPieces);
            }
        }

        public static void SliceAsync(string fileName, string destinationFolder, int numberOfParts)
        {
            Task.Run(() =>
                Slice(fileName, destinationFolder, numberOfParts)
            );
        }

        public static void Slice(string fileName, string destinationFolder, int numberOfParts)
        {
            if (Directory.Exists(destinationFolder))
            {
                Directory.Delete(destinationFolder, true);
            }

            Directory.CreateDirectory(destinationFolder);

            using (var source = new FileStream(fileName, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(fileName);

                long partLength = (source.Length / numberOfParts) + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart < numberOfParts; currentPart++)
                {
                    string filePath = String.Format("{0}/Part-{1}{2}", destinationFolder, currentPart, fileInfo.Extension);

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];
                        while (currentByte <= partLength * currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }
                }
            }
            Console.WriteLine("Slice complete.");
        }
    }
}

using System;
using System.IO;
using System.Text;

namespace Multithreading.Example.FileGeneration
{
    public sealed class FileGenerator
    {
        private const int MaxValue = 100;

        private readonly MersenneTwister _rng;
        private readonly int _lines;
        private readonly int _numbersPerLine;


        public FileGenerator(int lines, int numbersPerLine)
        {
            _rng = new MersenneTwister(Environment.TickCount);
            _lines = lines;
            _numbersPerLine = numbersPerLine;
        }

        public void Generate(string fileName)
        {

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    for (int i = 0; i < _lines; ++i)
                    {
                        string line = this.GenerateLine();
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }

        private string GenerateLine()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < _numbersPerLine; ++i)
            {
                stringBuilder.Append(_rng.Next(0, MaxValue)).Append(' ');
            }

            return stringBuilder.ToString();
        }
    }
}

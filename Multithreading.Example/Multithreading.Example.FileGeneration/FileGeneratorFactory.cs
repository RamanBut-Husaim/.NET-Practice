namespace Multithreading.Example.FileGeneration
{
    public sealed class FileGeneratorFactory
    {
        public FileGenerator Create(int lines, int numbersPerLine)
        {
            return new FileGenerator(lines, numbersPerLine);
        }
    }
}

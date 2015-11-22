using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Multithreading.Example.Core
{
    public sealed class FileContentProvider : IDisposable
    {
        private readonly StreamReader _streamReader;
        private readonly string _filePath;
        private bool _disposed;

        public FileContentProvider(string filePath)
        {
            _filePath = filePath;
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            _streamReader = new StreamReader(fileStream, Encoding.UTF8);
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        public IEnumerable<string> GetFileContent()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("FileContentProvider");
            }

            _streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            var strings = new List<string>();

            while (_streamReader.Peek() != -1)
            {
                string stringContent = _streamReader.ReadLine();
                strings.Add(stringContent);
            }

            return strings;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _streamReader.Dispose();
                }

                _disposed = true;
            }
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;

namespace AdvancedXml.Example.Atom
{
    public sealed class LibraryAtomTransformationManager : IDisposable
    {
        private readonly Lazy<Stream> _resourceStream;
        private bool _disposed;

        public LibraryAtomTransformationManager()
        {
            _resourceStream = new Lazy<Stream>(
                () => Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream("AdvancedXml.Example.Atom." + "LibraryAtomTransformation.xslt"));
        }

        public void Transform(string inputFile, string outputFile)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("manager");
            }

            var compiledTransform = new XslCompiledTransform();
            var settings = new XsltSettings {EnableScript = true};

            _resourceStream.Value.Seek(0, SeekOrigin.Begin);

            using (var transformation = XmlReader.Create(_resourceStream.Value))
            {
                compiledTransform.Load(transformation);

                compiledTransform.Transform(inputFile, outputFile);
            }
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
                    if (_resourceStream.IsValueCreated)
                    {
                        _resourceStream.Value.Dispose();
                    }

                    _disposed = true;
                }
            }
        }
    }
}

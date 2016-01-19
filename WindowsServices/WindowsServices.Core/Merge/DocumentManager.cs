using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsServices.Core.Merge
{
    public sealed class DocumentManager : IDocumentManager
    {
        private const string DocumentNameStartPart = "Doc";
        private const string PartExtension = "jpg";

        private readonly IDictionary<string, Document> _documents;

        public DocumentManager()
        {
            _documents = new Dictionary<string, Document>();
        }

        public event EventHandler<DocumentCompletionEventArgs> DocumentCompleted = delegate { };

        public bool CheckIfTheDocumentPart(string fullPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(fullPath);

            bool result = this.HasStartPart(fileName);
            if (!result)
            {
                return false;
            }

            string extension = Path.GetExtension(fullPath);
            result = PartExtension.Equals(extension, StringComparison.OrdinalIgnoreCase);

            if (!result)
            {
                return false;
            }

            int number;
            if (this.TryExtractPartNumber(fileName, out number))
            {
                return true;
            }

            return false;
        }

        public void AddDocumentPart(string fullPath)
        {
            if (!this.CheckIfTheDocumentPart(fullPath))
            {
                throw new ArgumentException("The file is not a document part.");
            }

            string fileName = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string documentName = fileName.Split('_')[0];
            string directoryPath = Path.GetDirectoryName(fullPath);
            string documentPath = Path.Combine(directoryPath, documentName + "." + extension);

            Document document;
            if (!_documents.TryGetValue(documentPath, out document))
            {
                var documentInfo = new DocumentInfo(documentName, extension);
                document = new Document(documentInfo);
                _documents.Add(documentPath, document);
            }

            int partNumber;
            this.TryExtractPartNumber(fileName, out partNumber);
            var part = new DocumentPart(document.Info, partNumber, directoryPath);
            document.AddPart(part);

            if (document.IsComplete())
            {
                this.DocumentCompleted.Invoke(this, new DocumentCompletionEventArgs(document.Info, document.ToList()));
                _documents.Remove(documentPath);
            }
        }

        private bool HasStartPart(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            return fileName.StartsWith(DocumentNameStartPart, StringComparison.OrdinalIgnoreCase);
        }

        private bool TryExtractPartNumber(string fileName, out int number)
        {
            string[] fileNameParts = fileName.Split('_');
            if (fileNameParts.Length <= 1 || fileNameParts.Length > 2)
            {
                number = default(int);
                return false;
            }

            if (int.TryParse(fileNameParts[1], out number))
            {
                if (number < 0)
                {
                    number = default(int);
                    return false;
                }

                return true;
            }

            return false;
        }

        public IEnumerator<Document> GetEnumerator()
        {
            return _documents.Values.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

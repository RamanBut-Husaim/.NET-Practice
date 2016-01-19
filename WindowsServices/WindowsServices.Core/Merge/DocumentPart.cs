using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServices.Core.Merge
{
    public sealed class DocumentPart : IEquatable<DocumentPart>
    {
        private readonly DocumentInfo _document;
        private readonly int _number;
        private readonly string _basePath;
        private readonly string _fileName;
        private readonly string _fullPath;

        public DocumentPart(DocumentInfo document, int number, string basePath)
        {
            _document = document;
            _number = number;
            _basePath = basePath;
            _fileName = document.Name + "_" + _number + "." + document.Type;
            _fullPath = Path.Combine(_basePath, _fileName);
        }

        public DocumentInfo Document
        {
            get { return _document; }
        }

        public int Number
        {
            get { return _number; }
        }

        public string Name
        {
            get { return _fileName; }
        }

        public string FullPath
        {
            get { return _fullPath; }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is DocumentPart && this.Equals((DocumentPart) obj);
        }

        public bool Equals(DocumentPart other)
        {
            return this.Document.Equals(other.Document) && this.Number.Equals(other.Number) && this.FullPath.Equals(other.FullPath);
        }

        public override int GetHashCode()
        {
            int hashCode = 17;

            hashCode += 31 * hashCode ^ this.Document.GetHashCode();
            hashCode += 31 * hashCode ^ this.Number.GetHashCode();
            hashCode += 31 * hashCode ^ this.FullPath.GetHashCode();

            return hashCode;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WindowsServices.Core.Merge
{
    public sealed class Document : IEnumerable<DocumentPart>
    {
        private readonly DocumentInfo _documentInfo;
        private readonly HashSet<DocumentPart> _parts;

        public Document(DocumentInfo documentInfo)
        {
            _documentInfo = documentInfo;
            _parts = new HashSet<DocumentPart>();
        }

        public DocumentInfo Info
        {
            get { return _documentInfo; }
        }

        public bool Contains(DocumentPart part)
        {
            return _parts.Contains(part);
        }

        public bool Add(DocumentPart part)
        {
            bool result = false;

            if (!this.Contains(part))
            {
                _parts.Add(part);
                result = true;
            }

            return result;
        }

        public IEnumerator<DocumentPart> GetEnumerator()
        {
            var copy = _parts.ToList().OrderBy(p => p.Number);

            return copy.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

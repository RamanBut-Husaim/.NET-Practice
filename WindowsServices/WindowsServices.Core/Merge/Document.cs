using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WindowsServices.Core.Merge
{
    public sealed class Document : IEnumerable<DocumentPart>
    {
        private readonly DocumentInfo _documentInfo;
        private readonly HashSet<DocumentPart> _parts;
        private readonly HashSet<int> _partNumbers;

        public Document(DocumentInfo documentInfo)
        {
            _documentInfo = documentInfo;
            _parts = new HashSet<DocumentPart>();
            _partNumbers = new HashSet<int>();
        }

        public DocumentInfo Info
        {
            get { return _documentInfo; }
        }

        public bool Contains(DocumentPart part)
        {
            return _parts.Contains(part);
        }

        public bool IsComplete()
        {
            if (_partNumbers.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < _partNumbers.Count; ++i)
            {
                if (!_partNumbers.Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        public bool AddPart(DocumentPart part)
        {
            bool result = false;

            if (!this.Contains(part))
            {
                _parts.Add(part);
                _partNumbers.Add(part.Number);
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

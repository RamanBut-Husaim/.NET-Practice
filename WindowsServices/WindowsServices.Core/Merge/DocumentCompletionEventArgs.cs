using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WindowsServices.Core.Merge
{
    public sealed class DocumentCompletionEventArgs : EventArgs
    {
        private readonly DocumentInfo _documentInfo;
        private readonly IReadOnlyCollection<DocumentPart> _documentParts;

        public DocumentCompletionEventArgs(
            DocumentInfo documentInfo,
            IList<DocumentPart> documentParts)
        {
            _documentInfo = documentInfo;
            _documentParts = new ReadOnlyCollection<DocumentPart>(documentParts);
        }

        public DocumentInfo DocumentInfo
        {
            get { return _documentInfo; }
        }

        public IReadOnlyCollection<DocumentPart> Parts
        {
            get { return _documentParts; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServices.Core.Merge
{
    public interface IDocumentManager : IEnumerable<Document>
    {
        event EventHandler<DocumentCompletionEventArgs> DocumentCompleted;

        bool CheckIfTheDocumentPart(string fullPath);

        void AddDocumentPart(string fullPath);
    }
}

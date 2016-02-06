using System.Collections.Generic;

namespace AdvancedXml.Example.Xsd
{
    public sealed class ValidationResult
    {
        private readonly IList<ValidationResultEntry> _errors;

        public ValidationResult()
        {
            _errors = new List<ValidationResultEntry>();
        }

        public bool Success
        {
            get { return _errors.Count == 0; }
        }

        public IList<ValidationResultEntry> Errors
        {
            get { return _errors; }
        }
    }
}

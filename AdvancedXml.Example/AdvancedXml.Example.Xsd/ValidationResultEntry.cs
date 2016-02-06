namespace AdvancedXml.Example.Xsd
{
    public sealed class ValidationResultEntry
    {
        private readonly string _message;
        private readonly string _markerElementValue;

        internal ValidationResultEntry(string message, string markerElementValue)
        {
            _message = message;
            _markerElementValue = markerElementValue;
        }

        public string Message
        {
            get { return _message; }
        }

        public string MarkerElementValue
        {
            get { return _markerElementValue; }
        }
    }
}

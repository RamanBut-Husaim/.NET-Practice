namespace AdvancedXml.Example.Xsd
{
    public sealed class ValidationResultEntry
    {
        private readonly string _message;

        internal ValidationResultEntry(string message)
        {
            _message = message;
        }

        public string Message
        {
            get { return _message; }
        }
    }
}

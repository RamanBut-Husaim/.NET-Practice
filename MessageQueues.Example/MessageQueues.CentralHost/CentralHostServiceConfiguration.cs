namespace MessageQueues.CentralHost
{
    public sealed class CentralHostServiceConfiguration
    {
        private readonly string _serviceName;
        private readonly string _resultFolder;

        public CentralHostServiceConfiguration(string serviceName, string resultFolder)
        {
            _serviceName = serviceName;
            _resultFolder = resultFolder;
        }

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public string ResultFolder
        {
            get { return _resultFolder; }
        }
    }
}

namespace MessageQueues.HarvesterHost
{
    public sealed class ServiceConfiguration
    {
        private readonly string _folderToMonitor;
        private readonly string _serviceName;
        private readonly string _harvesterName;

        public ServiceConfiguration(
            string folderToMonitor,
            string serviceName,
            string harvesterName)
        {
            this._folderToMonitor = folderToMonitor;
            this._serviceName = serviceName;
            this._harvesterName = harvesterName;
        }

        public string FolderToMonitor
        {
            get { return _folderToMonitor; }
        }

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public string HarvesterName
        {
            get { return _harvesterName; }
        }
    }
}

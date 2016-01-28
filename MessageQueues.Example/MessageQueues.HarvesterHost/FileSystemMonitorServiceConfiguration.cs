namespace MessageQueues.HarvesterHost
{
    public sealed class FileSystemMonitorServiceConfiguration
    {
        public FileSystemMonitorServiceConfiguration(
            string folderToMonitor,
            string serviceName)
        {
            this.FolderToMonitor = folderToMonitor;
            this.ServiceName = serviceName;
        }

        public string FolderToMonitor { get; private set; }

        public string ServiceName { get; private set; }
    }
}

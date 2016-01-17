namespace WindowsServices.Host
{
    public sealed class FileSystemMonitorServiceConfiguration
    {
        public FileSystemMonitorServiceConfiguration(
            string folderToMonitor,
            string targetFolder,
            string serviceName)
        {
            this.FolderToMonitor = folderToMonitor;
            this.TargetFolder = targetFolder;
            this.ServiceName = serviceName;
        }

        public string FolderToMonitor { get; private set; }

        public string TargetFolder { get; private set; }

        public string ServiceName { get; private set; }
    }
}

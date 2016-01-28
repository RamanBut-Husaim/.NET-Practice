namespace MessageQueues.HarvesterHost
{
    public interface IFileSystemMonitorServiceFactory
    {
        FileSystemMonitorService Create(FileSystemMonitorServiceConfiguration configuration);
    }
}

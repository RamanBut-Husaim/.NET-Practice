namespace MessageQueues.HarvesterHost
{
    public interface IFileSystemMonitorServiceFactory
    {
        FileSystemMonitorService Create(ServiceConfiguration configuration);
    }
}

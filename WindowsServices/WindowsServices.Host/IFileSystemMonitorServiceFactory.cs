using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServices.Host
{
    public interface IFileSystemMonitorServiceFactory
    {
        FileSystemMonitorService Create(FileSystemMonitorServiceConfiguration configuration);
    }
}

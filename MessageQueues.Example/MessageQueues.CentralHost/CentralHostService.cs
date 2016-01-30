using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NLog;

namespace MessageQueues.CentralHost
{
    public sealed class CentralHostService : ServiceBase
    {


        private readonly object _sync = new object();

        private readonly ILogger _logger;

        private readonly ManualResetEventSlim _shutdownEvent;
        private readonly CentralHostServiceConfiguration _centralHostServiceConfiguration;

    }
}

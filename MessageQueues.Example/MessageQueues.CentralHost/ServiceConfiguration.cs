﻿namespace MessageQueues.CentralHost
{
    public sealed class ServiceConfiguration
    {
        private readonly string _serviceName;
        private readonly string _resultFolder;

        public ServiceConfiguration(string serviceName, string resultFolder)
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

using AppDomains.Example.Maf.Plugin.HostViewAddIn;

namespace AppDomains.Example.Maf.Plugin.Host.Core
{
    public interface IPluginManager
    {
        TPlugin Load<TPlugin>() where TPlugin : class, IHostViewAddIn;
    }
}

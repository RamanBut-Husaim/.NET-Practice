using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public interface IPluginManager
    {
        TPlugin Load<TPlugin>() where TPlugin : PluginBase, new();

        void Unload<TPlugin>() where TPlugin : PluginBase, new();
    }
}

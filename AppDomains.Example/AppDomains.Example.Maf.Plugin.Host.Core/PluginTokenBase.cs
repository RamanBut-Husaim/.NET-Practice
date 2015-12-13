using System.AddIn.Hosting;

namespace AppDomains.Example.Maf.Plugin.Host.Core
{
    public abstract class PluginTokenBase
    {
        private readonly AddInToken _addInToken;

        protected PluginTokenBase(AddInToken addInToken)
        {
            _addInToken = addInToken;
        }

        protected AddInToken Token
        {
            get { return _addInToken; }
        }
    }
}

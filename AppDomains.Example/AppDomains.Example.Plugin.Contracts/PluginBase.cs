using System;
using System.Runtime.Remoting.Lifetime;

namespace AppDomains.Example.Plugin.Contracts
{
    public abstract class PluginBase : MarshalByRefObject
    {
        private readonly TimeSpan _initialLifeTime;
        private readonly TimeSpan _renewalOnCallTime;

        protected PluginBase(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime)
        {
            _initialLifeTime = initialLifeTime;
            _renewalOnCallTime = renewalOnCallTime;
        }

        public override object InitializeLifetimeService()
        {
            var lease = base.InitializeLifetimeService() as ILease;

            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = _initialLifeTime;
                lease.RenewOnCallTime = _renewalOnCallTime;
                lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
            }

            return lease;
        }
    }
}

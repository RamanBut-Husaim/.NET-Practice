using System;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;

namespace Debugging.Example.KeyGenerator
{
    public sealed class KeyGenerator
    {
        private readonly NetworkInterface _networkInterface;

        public KeyGenerator()
        {
            _networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
        }

        public string Generate()
        {
            if (_networkInterface == null)
            {
                throw new InvalidOperationException("The network interface is not specified");
            }

            byte[] addressBytes = _networkInterface.GetPhysicalAddress().GetAddressBytes();
            byte[] dateBytes = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
            var password = addressBytes
                .Select((value, index) => value ^ dateBytes[index])
                .Select(value =>
                {
                    if (value < 999)
                    {
                        return value * 10;
                    }

                    return value;
                })
                .Select(value => value.ToString(CultureInfo.InvariantCulture))
                .Aggregate(string.Empty, (accumulator, value) => accumulator + value + "-");

            return password.Substring(0, password.Length - 1);
        }
    }
}

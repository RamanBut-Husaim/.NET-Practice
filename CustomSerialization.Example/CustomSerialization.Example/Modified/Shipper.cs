using System;
using System.Runtime.Serialization;

using CustomSerialization.Example.TestHelpers;

namespace CustomSerialization.Example.DB
{
    [Serializable]
    public partial class Shipper : ISerializable
    {
        public Shipper(SerializationInfo info, StreamingContext context)
        {
            this.ShipperID = info.GetInt32(MemberNameHelper.GetPropertyName<Shipper, int>(p => p.ShipperID));
            this.CompanyName = info.GetString(MemberNameHelper.GetPropertyName<Shipper, string>(p => p.CompanyName));
            this.Phone = info.GetString(MemberNameHelper.GetPropertyName<Shipper, string>(p => p.Phone));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(MemberNameHelper.GetPropertyName<Shipper, int>(p => p.ShipperID), this.ShipperID);
            info.AddValue(MemberNameHelper.GetPropertyName<Shipper, string>(p => p.CompanyName), this.CompanyName);
            info.AddValue(MemberNameHelper.GetPropertyName<Shipper, string>(p => p.Phone), this.Phone);
        }
    }
}

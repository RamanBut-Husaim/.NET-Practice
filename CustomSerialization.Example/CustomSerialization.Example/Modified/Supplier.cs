using System;
using System.Runtime.Serialization;

using CustomSerialization.Example.TestHelpers;

namespace CustomSerialization.Example.DB
{
    [Serializable]
    public partial class Supplier : ISerializable
    {
        public Supplier(SerializationInfo info, StreamingContext context)
        {
            this.SupplierID = info.GetInt32(MemberNameHelper.GetPropertyName<Supplier, int>(p => p.SupplierID));
            this.CompanyName = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.CompanyName));
            this.ContactName = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.ContactName));
            this.ContactTitle = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.ContactTitle));
            this.Address = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Address));
            this.City = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.City));
            this.Region = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Region));
            this.PostalCode = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.PostalCode));
            this.Country = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Country));
            this.Phone = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Phone));
            this.Fax = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Fax));
            this.HomePage = info.GetString(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.HomePage));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, int>(p => p.SupplierID), this.SupplierID);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.CompanyName), this.CompanyName);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.ContactName), this.ContactName);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.ContactTitle), this.ContactTitle);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Address), this.Address);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.City), this.City);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Region), this.Region);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.PostalCode), this.PostalCode);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Country), this.Country);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Phone), this.Phone);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.Fax), this.Fax);
            info.AddValue(MemberNameHelper.GetPropertyName<Supplier, string>(p => p.HomePage), this.HomePage);
        }
    }
}

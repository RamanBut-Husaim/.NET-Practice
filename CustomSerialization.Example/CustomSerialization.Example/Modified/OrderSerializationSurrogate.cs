using System;
using System.Runtime.Serialization;

using CustomSerialization.Example.DB;
using CustomSerialization.Example.TestHelpers;

namespace CustomSerialization.Example.Modified
{
    public sealed class OrderSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var order = (Order)obj;

            info.AddValue(MemberNameHelper.GetPropertyName<Order, int>(p => p.OrderID), order.OrderID);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.CustomerID), order.CustomerID);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, int?>(p => p.EmployeeID), order.EmployeeID);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.OrderDate), order.OrderDate);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.RequiredDate), order.RequiredDate);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.ShippedDate), order.ShippedDate);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, int?>(p => p.ShipVia), order.ShipVia);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, decimal?>(p => p.Freight), order.Freight);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipAddress), order.ShipAddress);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipCity), order.ShipCity);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipRegion), order.ShipRegion);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipPostalCode), order.ShipPostalCode);
            info.AddValue(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipCountry), order.ShipCountry);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var order = (Order) obj;

            order.OrderID = info.GetInt32(MemberNameHelper.GetPropertyName<Order, int>(p => p.OrderID));
            order.CustomerID = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.CustomerID));
            order.EmployeeID = (int?) info.GetValue(MemberNameHelper.GetPropertyName<Order, int?>(p => p.EmployeeID), typeof (int?));
            order.OrderDate = (DateTime?)info.GetValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.OrderDate), typeof(DateTime?));
            order.RequiredDate = (DateTime?)info.GetValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.RequiredDate), typeof(DateTime?));
            order.ShippedDate = (DateTime?)info.GetValue(MemberNameHelper.GetPropertyName<Order, DateTime?>(p => p.ShippedDate), typeof(DateTime?));
            order.ShipVia = (int?)info.GetValue(MemberNameHelper.GetPropertyName<Order, int?>(p => p.ShipVia), typeof(int?));
            order.Freight = (decimal?)info.GetValue(MemberNameHelper.GetPropertyName<Order, decimal?>(p => p.Freight), typeof(decimal?));
            order.ShipAddress = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipAddress));
            order.ShipCity = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipCity));
            order.ShipRegion = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipRegion));
            order.ShipPostalCode = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipPostalCode));
            order.ShipCountry = info.GetString(MemberNameHelper.GetPropertyName<Order, string>(p => p.ShipCountry));

            return order;
        }
    }
}

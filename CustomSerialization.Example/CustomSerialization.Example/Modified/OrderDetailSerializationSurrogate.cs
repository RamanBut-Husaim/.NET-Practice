using System.Runtime.Serialization;

using CustomSerialization.Example.DB;
using CustomSerialization.Example.TestHelpers;

namespace CustomSerialization.Example.Modified
{
    public sealed class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, int>(p => p.OrderID), orderDetail.OrderID);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, int>(p => p.ProductID), orderDetail.ProductID);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, decimal>(p => p.UnitPrice), orderDetail.UnitPrice);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, short>(p => p.Quantity), orderDetail.Quantity);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, float>(p => p.Discount), orderDetail.Discount);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, Product>(p => p.Product), orderDetail.Product);
            info.AddValue(MemberNameHelper.GetPropertyName<Order_Detail, Order>(p => p.Order), orderDetail.Order);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail) obj;

            orderDetail.OrderID = info.GetInt32(MemberNameHelper.GetPropertyName<Order_Detail, int>(p => p.OrderID));
            orderDetail.ProductID = info.GetInt32(MemberNameHelper.GetPropertyName<Order_Detail, int>(p => p.ProductID));
            orderDetail.UnitPrice = info.GetDecimal(MemberNameHelper.GetPropertyName<Order_Detail, decimal>(p => p.UnitPrice));
            orderDetail.Quantity = info.GetInt16(MemberNameHelper.GetPropertyName<Order_Detail, short>(p => p.Quantity));
            orderDetail.Discount = info.GetSingle(MemberNameHelper.GetPropertyName<Order_Detail, float>(p => p.Discount));
            orderDetail.Product = (Product) info.GetValue(MemberNameHelper.GetPropertyName<Order_Detail, Product>(p => p.Product), typeof(Product));
            orderDetail.Order = (Order) info.GetValue(MemberNameHelper.GetPropertyName<Order_Detail, Order>(p => p.Order), typeof (Order));

            return orderDetail;
        }
    }
}

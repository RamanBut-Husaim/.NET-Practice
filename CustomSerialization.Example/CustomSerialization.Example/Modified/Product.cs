using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using CustomSerialization.Example.TestHelpers;

namespace CustomSerialization.Example.DB
{
    [Serializable]
    public partial class Product : ISerializable
    {
        public Product(SerializationInfo info, StreamingContext context)
        {
            this.ProductID = info.GetInt32(MemberNameHelper.GetPropertyName<Product, int>(p => p.ProductID));
            this.ProductName = info.GetString(MemberNameHelper.GetPropertyName<Product, string>(p => p.ProductName));
            this.SupplierID = (int?)info.GetValue(MemberNameHelper.GetPropertyName<Product, int?>(p => p.SupplierID), typeof(int?));
            this.CategoryID = (int?)info.GetValue(MemberNameHelper.GetPropertyName<Product, int?>(p => p.CategoryID), typeof(int?));
            this.QuantityPerUnit = info.GetString(MemberNameHelper.GetPropertyName<Product, string>(p => p.QuantityPerUnit));
            this.UnitPrice = (decimal?)info.GetValue(MemberNameHelper.GetPropertyName<Product, decimal?>(p => p.UnitPrice), typeof(decimal?));
            this.UnitsInStock = (short?)info.GetValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.UnitsInStock), typeof(short?));
            this.UnitsOnOrder = (short?)info.GetValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.UnitsOnOrder), typeof(short?));
            this.ReorderLevel = (short?)info.GetValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.ReorderLevel), typeof(short?));
            this.Discontinued = info.GetBoolean(MemberNameHelper.GetPropertyName<Product, bool>(p => p.Discontinued));
            this.Supplier = (Supplier) info.GetValue(MemberNameHelper.GetPropertyName<Product, Supplier>(p => p.Supplier), typeof (Supplier));

            this.Order_Details = new HashSet<Order_Detail>();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(MemberNameHelper.GetPropertyName<Product, int>(p => p.ProductID), this.ProductID);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, string>(p => p.ProductName), this.ProductName);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, int?>(p => p.SupplierID), this.SupplierID);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, int?>(p => p.CategoryID), this.CategoryID);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, string>(p => p.QuantityPerUnit), this.QuantityPerUnit);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, decimal?>(p => p.UnitPrice), this.UnitPrice);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.UnitsInStock), this.UnitsInStock);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.UnitsOnOrder), this.UnitsOnOrder);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, short?>(p => p.ReorderLevel), this.ReorderLevel);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, bool>(p => p.Discontinued), this.Discontinued);
            info.AddValue(MemberNameHelper.GetPropertyName<Product, Supplier>(p => p.Supplier), this.Supplier);
        }
    }
}

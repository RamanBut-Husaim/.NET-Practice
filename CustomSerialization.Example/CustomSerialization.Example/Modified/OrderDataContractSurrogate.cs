using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Runtime.Serialization;

using CustomSerialization.Example.DB;

namespace CustomSerialization.Example.Modified
{
    public sealed class OrderDataContractSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            Type[] types = new Type[] { typeof(Customer), typeof(Employee), typeof(Order_Detail), typeof(Shipper)};

            foreach (var typeToScan in types)
            {
                if (this.ShouldExclude(type, typeToScan))
                {
                    return typeToScan;
                }
            }
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj != null)
            {
                Type type = obj.GetType();

                if (this.ShouldExclude(type, typeof (Customer)) || type == typeof(Customer))
                {
                    return this.ClearCustomer(obj as Customer);
                }

                if (this.ShouldExclude(type, typeof (Employee)) || type == typeof(Employee))
                {
                    return this.ClearEmployee(obj as Employee);
                }

                if (this.ShouldExclude(type, typeof(Order_Detail)) || type == typeof(Order_Detail))
                {
                    return this.ClearOrderDetail(obj as Order_Detail);
                }

                Type actualType = ObjectContext.GetObjectType(type);
                if (actualType != type && actualType == typeof (Shipper))
                {
                    var shipper = obj as Shipper;

                    return shipper;
                }
            }

            return obj;
        }

        private Customer ClearCustomer(Customer customer)
        {
            return new Customer(customer);
        }

        private Employee ClearEmployee(Employee employee)
        {
            return new Employee(employee);
        }

        private Order_Detail ClearOrderDetail(Order_Detail orderDetail)
        {
            return new Order_Detail(orderDetail);
        }

        private bool ShouldExclude(Type type, Type typeToExclude)
        {
            Type actualType = ObjectContext.GetObjectType(type);
            if (type != actualType && actualType == typeToExclude)
            {
                return true;
            }

            return false;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CustomSerialization.Example.DB;
using CustomSerialization.Example.Modified;
using CustomSerialization.Example.TestHelpers;

using System.Data.Entity;

using Xunit;
using Xunit.Abstractions;

namespace CustomSerialization.Example
{
    public class SerializationSolutions
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Northwind _dbContext;

        public SerializationSolutions(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _dbContext = new Northwind();
        }

        [Fact]
        public void SerializationEvents()
        {
            var settings = new DataContractSerializerSettings
            {
                DataContractResolver = new ProxyDataContractResolver()
            };

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(
                _testOutputHelper,
                new DataContractSerializer(typeof (IEnumerable<Category>), settings),
                true);

            List<Category> categories = _dbContext.Categories.ToList();

            var deserializedCategories = tester.SerializeAndDeserialize(categories).ToList();
        }

        [Fact]
        public void ISerializable()
        {
            var settings = new DataContractSerializerSettings
            {
                DataContractResolver = new ProxyDataContractResolver(),
                KnownTypes = new List<Type>() {typeof (Supplier)}
            };

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(
                _testOutputHelper,
                new DataContractSerializer(typeof (IEnumerable<Product>), settings),
                true);

            var products = _dbContext.Products.ToList();

            var deserializedOrders = tester.SerializeAndDeserialize(products);
        }

        [Fact]
        public void ISerializationSurrogate()
        {
            bool oldSetting = _dbContext.Configuration.ProxyCreationEnabled;

            _dbContext.Configuration.ProxyCreationEnabled = false;
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Order_Detail), new StreamingContext(StreamingContextStates.All), new OrderDetailSerializationSurrogate());
            surrogateSelector.AddSurrogate(typeof(Order), new StreamingContext(StreamingContextStates.All), new OrderSerializationSurrogate());

            var tester = new BinarySerializerTester<IEnumerable<Order_Detail>>(
                _testOutputHelper,
                new BinaryFormatter(surrogateSelector, new StreamingContext()),
                false);

            var orderDetails = _dbContext.Order_Details
                .Include(p => p.Product)
                .Include(p => p.Order)
                .Take(100)
                .ToList();

            var deserializedOrderDetails = tester.SerializeAndDeserialize(orderDetails);

            _dbContext.Configuration.ProxyCreationEnabled = oldSetting;
        }

        [Fact]
        public void IDataContractSurrogate()
        {
            var settings = new DataContractSerializerSettings
            {
                DataContractResolver = new ProxyDataContractResolver(),
                DataContractSurrogate = new OrderDataContractSurrogate(),
            };

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(
                _testOutputHelper,
                new DataContractSerializer(typeof(IEnumerable<Order>), settings),
                true);

            var orders = _dbContext.Orders.Take(10).ToList();

            var deserializedOrders = tester.SerializeAndDeserialize(orders);
        }
    }
}

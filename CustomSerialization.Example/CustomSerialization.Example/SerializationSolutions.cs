using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using CustomSerialization.Example.DB;
using CustomSerialization.Example.TestHelpers;

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
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        [Fact]
        public void SerializationEvents()
        {
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(
                _testOutputHelper,
                new DataContractSerializer(typeof (IEnumerable<Category>)),
                true);

            var categories = _dbContext.Categories.ToList();

            tester.SerializeAndDeserialize(categories);
        }

        [Fact]
        public void ISerializable()
        {
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(
                _testOutputHelper,
                new DataContractSerializer(typeof(IEnumerable<Product>)),
                true);

            var products = _dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
        }


        [Fact]
        public void ISerializationSurrogate()
        {
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(
                _testOutputHelper,
                new DataContractSerializer(typeof(IEnumerable<Order_Detail>)),
                true);

            var orderDetails = _dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [Fact]
        public void IDataContractSurrogate()
        {
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(
                _testOutputHelper,
                new DataContractSerializer(typeof(IEnumerable<Order>)),
                true);

            var orders = _dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}

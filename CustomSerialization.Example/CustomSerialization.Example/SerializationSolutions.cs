﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

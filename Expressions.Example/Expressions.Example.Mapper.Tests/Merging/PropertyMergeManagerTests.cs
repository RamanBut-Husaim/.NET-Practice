﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Expressions.Example.Mapper.Merging;
using Expressions.Example.Mapper.Property;
using Moq;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Merging
{
    internal sealed class PropertySourceTest
    {
        public string PropString { get; set; }

        public int PropInt { get; set; }

        public int PropInt1 { get; set; }

        public double PropDouble { get; set; }

        public List<int> PropListInt { get; set; }

        public List<short> PropListShort { get; set; } 
    }

    internal sealed class PropertyDestinationTest
    {
        public string PropString { get; set; }

        public int PropInt1 { get; set; }

        public int PropInt2 { get; set; }

        public float PropFloat { get; set; }

        public short PropDouble { get; set; }

        public List<int> PropListInt { get; set; } 

        public List<long> PropListShort { get; set; }
    }

    public sealed class PropertyMergeManagerTests
    {
        private readonly IEqualityComparerFactory<PropertyInfo> _equalityComparerFactory;

        public PropertyMergeManagerTests()
        {
            _equalityComparerFactory = new PropertyEqualityComparerFactory();
        }

        [Fact]
        public void Merge_WhenClassesAreSpecified_PropertiesMergedCorrectly()
        {
            // arrange
            var sourcePropertyIterator = new Mock<IInstanceMemberIterator<PropertyInfo, PropertySourceTest>>();
            sourcePropertyIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSourceEnumerator);
            var destinationPropertyIterator = new Mock<IInstanceMemberIterator<PropertyInfo, PropertyDestinationTest>>();
            destinationPropertyIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateDestinationEnumerator);

            var iteratorFactory = new Mock<IIteratorFactory<PropertyInfo>>();
            iteratorFactory.Setup(p => p.CreateSourceIterator<PropertySourceTest>()).Returns(sourcePropertyIterator.Object);
            iteratorFactory.Setup(p => p.CreateDestinationIterator<PropertyDestinationTest>()).Returns(destinationPropertyIterator.Object);

            // act
            var mergeManager = new MemberMergeManager<PropertyInfo>(iteratorFactory.Object, _equalityComparerFactory);
            var mergeResult = mergeManager.Merge<PropertySourceTest, PropertyDestinationTest>().Select(p => p.MemberInfo).ToList();

            // assert
            Type type = typeof(PropertyDestinationTest);
            var expectedResult = new List<PropertyInfo>
            {
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, string>(p => p.PropString)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, int>(p => p.PropInt1)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, List<int>>(p => p.PropListInt))
            };

            Assert.Equal(expectedResult.OrderBy(p => p.Name), mergeResult.OrderBy(p => p.Name), _equalityComparerFactory.Create());
        }

        [Fact]
        public void Merge_WhenClassesHaveNoCommonProperties_TheResultIsMepy()
        {
            // arrange
            var sourcePropertyIterator = new Mock<IInstanceMemberIterator<PropertyInfo, PropertySourceTest>>();
            sourcePropertyIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSpecificSourceEnumerator);
            var destinationPropertyIterator = new Mock<IInstanceMemberIterator<PropertyInfo, PropertyDestinationTest>>();
            destinationPropertyIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSpecificDestinationEnumerator);

            var iteratorFactory = new Mock<IIteratorFactory<PropertyInfo>>();
            iteratorFactory.Setup(p => p.CreateSourceIterator<PropertySourceTest>()).Returns(sourcePropertyIterator.Object);
            iteratorFactory.Setup(p => p.CreateDestinationIterator<PropertyDestinationTest>()).Returns(destinationPropertyIterator.Object);

            // act
            var mergeManager = new MemberMergeManager<PropertyInfo>(iteratorFactory.Object, _equalityComparerFactory);
            var mergeResult = mergeManager.Merge<PropertySourceTest, PropertyDestinationTest>().Select(p => p.MemberInfo).ToList();

            // assert
            Assert.Equal(Enumerable.Empty<PropertyInfo>(), mergeResult);
        }

        private IEnumerator<PropertyInfo> CreateSourceEnumerator()
        {
            Type type = typeof (PropertySourceTest);
            return new List<PropertyInfo>
            {
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, string>(p => p.PropString)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, int>(p => p.PropInt)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, int>(p => p.PropInt1)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, double>(p => p.PropDouble)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, List<int>>(p => p.PropListInt)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, List<short>>(p => p.PropListShort))
            }.GetEnumerator();
        }

        private IEnumerator<PropertyInfo> CreateDestinationEnumerator()
        {
            Type type = typeof (PropertyDestinationTest);

            return new List<PropertyInfo>
            {
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, string>(p => p.PropString)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, int>(p => p.PropInt1)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, int>(p => p.PropInt2)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, float>(p => p.PropFloat)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, short>(p => p.PropDouble)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, List<int>>(p => p.PropListInt)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, List<long>>(p => p.PropListShort))
            }.GetEnumerator();
        }

        private IEnumerator<PropertyInfo> CreateSpecificSourceEnumerator()
        {
            Type type = typeof(PropertySourceTest);
            return new List<PropertyInfo>
            {
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, int>(p => p.PropInt)),
                type.GetProperty(Utils.GetPropertyName<PropertySourceTest, double>(p => p.PropDouble))
            }.GetEnumerator();
        }

        private IEnumerator<PropertyInfo> CreateSpecificDestinationEnumerator()
        {
            Type type = typeof(PropertyDestinationTest);

            return new List<PropertyInfo>
            {
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, int>(p => p.PropInt2)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, float>(p => p.PropFloat)),
                type.GetProperty(Utils.GetPropertyName<PropertyDestinationTest, short>(p => p.PropDouble))
            }.GetEnumerator();
        }
    }
}

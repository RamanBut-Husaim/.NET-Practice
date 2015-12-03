using System;
using System.Linq;
using Expressions.Example.Mapper.Property;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Property
{
    internal sealed class GetPropertyTest
    {
        private string _propSetterValue;
        private readonly int _propInt;
        private readonly double _propDouble;
        private readonly byte _propByte;
        private readonly short _propShort;
        private readonly string _propString;
        private readonly long _propLong;
        private readonly decimal _propDecimal;
        private readonly float _propFloat;
        private static readonly string PropStatic1;
        private static string PropStaticSetter1;

        public int PropInt
        {
            get { return _propInt; }
        }

        public double PropDouble
        {
            get { return _propDouble; }
        }

        public byte PropByte
        {
            get { return _propByte; }
        }

        public short PropShort
        {
            get { return _propShort; }
        }

        public string PropString
        {
            get { return _propString; }
        }

        public long PropLong
        {
            get { return _propLong; }
        }

        public decimal PropDecimal
        {
            get { return _propDecimal; }
        }

        public float PropFloat
        {
            get { return _propFloat; }
        }

        public string PropSetter
        {
            set { _propSetterValue = value; }
        }

        private int PropIntPrivate
        {
            get { return _propInt; }
        }

        public static string PropStatic
        {
            get { return PropStatic1; }
        }

        public int PropAuto { get; set; }

        public static string PropStaticSetter
        {
            set { PropStaticSetter1 = value; }
        }
    }

    internal struct GetPropertyTestStruct
    {
        private readonly int _propInt;
        private readonly string _propString;

        private double _propSetter;

        public int PropInt
        {
            get { return _propInt; }
        }

        public string PropString
        {
            get { return _propString; }
        }

        public long PropAuto { get; set; }

        public double PropSetter
        {
            set { _propSetter = value; }
        }
    }

    public sealed class GetInstancePropertyIteratorTests
    {
        [Fact]
        public void GetEnumerator_WhenTheClassHasInstanceProperties_TheyAreReturned()
        {
            var propertyIterator = new GetInstancePropertyIterator<GetPropertyTest>();

            string[] propertyNames = propertyIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedNames = new[] { "PropInt", "PropDouble", "PropByte", "PropShort", "PropString", "PropLong", "PropDecimal", "PropFloat", "PropAuto" }
               .OrderBy(p => p).ToArray();
            Assert.Equal(expectedNames.OrderBy(p => p), propertyNames.OrderBy(p => p));
        }

        [Theory]
        [InlineData("PropInt", typeof (int))]
        [InlineData("PropDouble", typeof(double))]
        [InlineData("PropString", typeof(string))]
        public void GetEnumerator_WhenClassPropertyIsReturned_ItsInfoIsCorrect(string propertyName, Type propertyType)
        {
            var propertyIterator = new GetInstancePropertyIterator<GetPropertyTest>();

            Type propertyReturnType = propertyIterator.First(p => p.Name.Equals(propertyName)).PropertyType;

            Assert.Equal(propertyType, propertyReturnType);
        }

        [Fact]
        public void GetEnumerator_WhenTheStructHasInstanceProperties_TheyAreReturned()
        {
            var propertyIterator = new GetInstancePropertyIterator<GetPropertyTestStruct>();

            string[] propertyNames = propertyIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedNames = new[] {"PropInt", "PropString", "PropAuto"}.OrderBy(p => p).ToArray();

            Assert.Equal(expectedNames, propertyNames);
        }

        [Theory]
        [InlineData("PropInt", typeof(int))]
        [InlineData("PropString", typeof(string))]
        public void GetEnumerator_WhenStructPropertyIsReturned_ItsInfoIsCorrect(string propertyName, Type propertyType)
        {
            var propertyIterator = new GetInstancePropertyIterator<GetPropertyTestStruct>();

            Type propertyReturnType = propertyIterator.First(p => p.Name.Equals(propertyName)).PropertyType;

            Assert.Equal(propertyType, propertyReturnType);
        }
    }
}

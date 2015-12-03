using System;
using System.Linq;
using Expressions.Example.Mapper.Property;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Property
{
    public sealed class SetPropertyTest
    {
        private int _propInt;
        private double _propDouble;
        private byte _propByte;
        private float _propFloat;
        private short _propShort;
        private decimal _propDecimal;
        private long _propLong;
        private string _propString;

        private readonly int _propGetterInt;

        private static int PropStaticSetter;

        public int PropInt
        {
            set { _propInt = value; }
        }

        public double PropDouble
        {
            set { _propDouble = value; }
        }

        public byte PropByte
        {
            set { _propByte = value; }
        }

        public float PropFloat
        {
            set { _propFloat = value; }
        }

        public short PropShort
        {
            set { _propShort = value; }
        }

        public decimal PropDecimal
        {
            set { _propDecimal = value; }
        }

        public long PropLong
        {
            set { _propLong = value; }
        }

        public string PropString
        {
            set { _propString = value; }
        }

        public static int PropStatic
        {
            set { PropStaticSetter = value; }
        }

        public int PropGetterInt
        {
            get { return _propGetterInt; }
        }

        public int PropAuto { get; set; }
    }

    public struct SetPropertyTestStruct
    {
        private readonly string _propGetter;

        private int _propInt;
        private double _propDouble;

        public int PropInt
        {
            set { _propInt = value; }
        }

        public double PropDouble
        {
            set { _propDouble = value; }
        }

        public string PropGetter
        {
            get { return _propGetter; }
        }

        public long PropAuto { get; set; }
    }

    public sealed class SetInstancePropertyIterator
    {
        [Fact]
        public void GetEnumerator_WhenTheClassHasInstanceProperties_TheyAreReturned()
        {
            var propertyIterator = new SetInstancePropertyIterator<SetPropertyTest>();

            string[] propertyNames = propertyIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedNames = new[] { "PropInt", "PropDouble", "PropByte", "PropShort", "PropString", "PropLong", "PropDecimal", "PropFloat", "PropAuto" }
               .OrderBy(p => p).ToArray();

            Assert.Equal(expectedNames.OrderBy(p => p), propertyNames.OrderBy(p => p));
        }

        [Theory]
        [InlineData("PropInt", typeof(int))]
        [InlineData("PropDouble", typeof(double))]
        [InlineData("PropString", typeof(string))]
        public void GetEnumerator_WhenClassPropertyIsReturned_ItsInfoIsCorrect(string propertyName, Type propertyType)
        {
            var propertyIterator = new SetInstancePropertyIterator<SetPropertyTest>();

            Type propertyReturnType = propertyIterator.First(p => p.Name.Equals(propertyName)).PropertyType;

            Assert.Equal(propertyType, propertyReturnType);
        }

        [Fact]
        public void GetEnumerator_WhenTheStructHasInstanceProperties_TheyAreReturned()
        {
            var propertyIterator = new SetInstancePropertyIterator<SetPropertyTestStruct>();

            string[] propertyNames = propertyIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedNames = new[] { "PropInt", "PropDouble", "PropAuto" }.OrderBy(p => p).ToArray();

            Assert.Equal(expectedNames, propertyNames);
        }

        [Theory]
        [InlineData("PropInt", typeof(int))]
        [InlineData("PropDouble", typeof(double))]
        public void GetEnumerator_WhenStructPropertyIsReturned_ItsInfoIsCorrect(string propertyName, Type propertyType)
        {
            var propertyIterator = new SetInstancePropertyIterator<SetPropertyTestStruct>();

            Type propertyReturnType = propertyIterator.First(p => p.Name.Equals(propertyName)).PropertyType;

            Assert.Equal(propertyType, propertyReturnType);
        }
    }
}

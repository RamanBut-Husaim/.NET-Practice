using System;
using System.Linq;
using Expressions.Example.Mapper.Field;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Field
{
    internal sealed class WritableFieldTest
    {
        public readonly int FieldInt;
        public readonly double FieldDouble;
        public readonly float FieldFloat;

        public byte FieldByte;
        public string FieldString;

        private readonly string _fieldString;
        private short _fieldShort;

        public static string FieldStaticString;
    }

    internal struct WritableFieldTestStruct
    {
        public readonly int FieldInt;
        public readonly double FieldDouble;

        public string FieldString;
    }

    public sealed class WritableInstanceFieldIteratorTests
    {
        [Fact]
        public void GetEnumerator_WhenTheClassHasInstanceFields_TheyAreReturned()
        {
            var fieldIterator = new WritableInstanceFieldIterator<WritableFieldTest>();

            string[] fieldNames = fieldIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedFieldNames = new[] { "FieldByte", "FieldString" }
                .OrderBy(p => p).ToArray();

            Assert.Equal(expectedFieldNames, fieldNames);
        }

        [Theory]
        [InlineData("FieldByte", typeof(byte))]
        [InlineData("FieldString", typeof(string))]
        public void GetEnumerator_WhenTheClassFieldIsReturned_ItsInfoIsCorrect(string fieldName, Type type)
        {
            var fieldIterator = new WritableInstanceFieldIterator<WritableFieldTest>();

            Type returnType = fieldIterator.First(p => p.Name.Equals(fieldName)).FieldType;

            Assert.Equal(type, returnType);
        }

        [Fact]
        public void GetEnumerator_WhenTheStructHasInstanceFields_TheyAreReturned()
        {
            var fieldIterator = new WritableInstanceFieldIterator<WritableFieldTestStruct>();

            string[] fieldNames = fieldIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedFieldNames = new[] { "FieldString" }.OrderBy(p => p).ToArray();

            Assert.Equal(expectedFieldNames, fieldNames);
        }

        [Theory]
        [InlineData("FieldString", typeof(string))]
        public void GetEnumerator_WhenTheStructFieldIsReturned_ItsInfoIsCorrect(string fieldName, Type type)
        {
            var fieldIterator = new WritableInstanceFieldIterator<WritableFieldTestStruct>();

            Type returnType = fieldIterator.First(p => p.Name.Equals(fieldName)).FieldType;

            Assert.Equal(type, returnType);
        }
    }
}

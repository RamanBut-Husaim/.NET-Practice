using System;
using System.Linq;
using Expressions.Example.Mapper.Field;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Field
{
    internal sealed class ReadableWritableTest
    {
        public readonly int FieldInt;
        public readonly double FieldDouble;
        public byte FieldByte;
        public long FieldLong;
        public float FieldFloat;
        public short FieldShort;
        public string FieldString;

        private string _fieldString;

        public static string FieldStringStatic;
    }

    internal struct ReadableWritableTestStruct
    {
        public readonly int FieldInt;

        public float FieldFloat;

        private string _fieldString;
    }

    public sealed class ReadableWritableInstanceFieldIteratorTests
    {
        [Fact]
        public void GetEnumerator_WhenTheClassHasInstanceFields_TheyAreReturned()
        {
            var fieldIterator = new ReadableWritableInstanceFieldIterator<ReadableWritableTest>();

            string[] fieldNames = fieldIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedFieldNames = new[] {"FieldInt", "FieldDouble", "FieldByte", "FieldLong", "FieldFloat", "FieldShort", "FieldString"}
                .OrderBy(p => p).ToArray();

            Assert.Equal(expectedFieldNames, fieldNames);
        }

        [Theory]
        [InlineData("FieldByte", typeof(byte))]
        [InlineData("FieldLong", typeof(long))]
        [InlineData("FieldDouble", typeof(double))]
        public void GetEnumerator_WhenTheClassFieldIsReturned_ItsInfoIsCorrect(string fieldName, Type type)
        {
            var fieldIterator = new ReadableWritableInstanceFieldIterator<ReadableWritableTest>();

            Type returnType = fieldIterator.First(p => p.Name.Equals(fieldName)).FieldType;

            Assert.Equal(type, returnType);
        }

        [Fact]
        public void GetEnumerator_WhenTheStructHasInstanceFields_TheyAreReturned()
        {
            var fieldIterator = new ReadableWritableInstanceFieldIterator<ReadableWritableTestStruct>();

            string[] fieldNames = fieldIterator.Select(p => p.Name).OrderBy(p => p).ToArray();

            var expectedFieldNames = new[] {"FieldInt", "FieldFloat"}.OrderBy(p => p).ToArray();

            Assert.Equal(expectedFieldNames, fieldNames);
        }

        [Theory]
        [InlineData("FieldInt", typeof(int))]
        [InlineData("FieldFloat", typeof(float))]
        public void GetEnumerator_WhenTheStructFieldIsReturned_ItsInfoIsCorrect(string fieldName, Type type)
        {
            var fieldIterator = new ReadableWritableInstanceFieldIterator<ReadableWritableTestStruct>();

            Type returnType = fieldIterator.First(p => p.Name.Equals(fieldName)).FieldType;

            Assert.Equal(type, returnType);
        }
    }
}

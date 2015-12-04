using System;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public sealed class FieldBlockExpressionBuilderTests : FieldExpressionBuilderTestsBase
    {
        [Fact]
        public void Create_WhenTheFieldsAreSpecified_TheAssignmentIsCorrect()
        {
            // arrange
            var stringField = typeof(FieldExpressionTestClass).GetField(Utils.GetFieldName<FieldExpressionTestClass, string>(p => p.FieldString));
            var mergeResultString = new MemberMergeResult<FieldInfo>(stringField);

            var intField = typeof(FieldExpressionTestClass).GetField(Utils.GetFieldName<FieldExpressionTestClass, int>(p => p.FieldInt));
            var mergeResultInt = new MemberMergeResult<FieldInfo>(intField);

            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var fields = new[] { mergeResultString, mergeResultInt };

            // act
            var fieldBlockExpressionBuilder = new MemberBlockExpressionBuilder<FieldInfo>(fieldExpressionBuilder);
            var blockExpression = fieldBlockExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(fields);

            // assert
            string fieldStringValue = "Thor";
            int fieldIntValue = int.MaxValue;
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldString = fieldStringValue, FieldInt = fieldIntValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldStringValue, destinationObject.FieldString);
            Assert.Equal(fieldIntValue, destinationObject.FieldInt);
        }

        [Theory]
        [InlineData(double.MinValue, float.MinValue)]
        [InlineData(0, 0)]
        [InlineData(double.MaxValue, float.MaxValue)]
        public void Create_WhenDoubleAndSingleFieldsAreSpecified_TheAssignmentIsCorrect(double doubleValue, float singleValue)
        {
            // arrange
            var doubleField = typeof(FieldExpressionTestClass).GetField(Utils.GetFieldName<FieldExpressionTestClass, double>(p => p.FieldDouble));
            var mergeResultDouble = new MemberMergeResult<FieldInfo>(doubleField);

            var floatField = typeof(FieldExpressionTestClass).GetField(Utils.GetFieldName<FieldExpressionTestClass, float>(p => p.FieldFloat));
            var mergeResultFloat = new MemberMergeResult<FieldInfo>(floatField);

            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var fields = new[] { mergeResultDouble, mergeResultFloat };

            // act
            var fieldBlockExpressionBuilder = new MemberBlockExpressionBuilder<FieldInfo>(fieldExpressionBuilder);
            var blockExpression = fieldBlockExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(fields);

            // assert
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldDouble = doubleValue, FieldFloat = singleValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(doubleValue, destinationObject.FieldDouble);
            Assert.Equal(singleValue, destinationObject.FieldFloat);
        }
    }
}

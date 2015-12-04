using System;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public sealed class FieldExpressionBuilderTests : FieldExpressionBuilderTestsBase
    {
        [Theory]
        [InlineData("Odin")]
        [InlineData("Thor")]
        [InlineData("")]
        public void Create_WhenTheFieldTypeIsString_TheAssignmentExpressionIsCorrect(string fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, string>(p => p.FieldString);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldString = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldString);
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData(0)]
        [InlineData(byte.MaxValue)]
        public void Create_WhenTheFieldTypeIsByte_TheAssignmentExpressionIsCorrect(byte fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, byte>(p => p.FieldByte);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldByte = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldByte);
        }

        [Theory]
        [InlineData(short.MinValue)]
        [InlineData(0)]
        [InlineData(short.MaxValue)]
        public void Create_WhenTheFieldTypeIsInt16_TheAssignmentExpressionIsCorrect(short fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, short>(p => p.FieldShort);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldShort = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldShort);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void Create_WhenTheFieldTypeIsInt32_TheAssignmentExpressionIsCorrect(int fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, int>(p => p.FieldInt);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldInt = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldInt);
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(0)]
        [InlineData(long.MaxValue)]
        public void Create_WhenTheFieldTypeIsInt64_TheAssignmentExpressionIsCorrect(long fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, long>(p => p.FieldLong);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldLong = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldLong);
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(0)]
        [InlineData(float.MaxValue)]
        public void Create_WhenTheFieldTypeIsSingle_TheAssignmentExpressionIsCorrect(float fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, float>(p => p.FieldFloat);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldFloat = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldFloat);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        [InlineData(double.MaxValue)]
        public void Create_WhenTheFieldTypeIsDouble_TheAssignmentExpressionIsCorrect(double fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, double>(p => p.FieldDouble);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldDouble = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldDouble);
        }

        [Theory]
        [InlineData(-789798123484)]
        [InlineData(0)]
        [InlineData(123409845712345)]
        public void Create_WhenTheFieldTypeIsDecimal_TheAssignmentExpressionIsCorrect(decimal fieldValue)
        {
            string fieldName = Utils.GetFieldName<FieldExpressionTestClass, decimal>(p => p.FieldDecimal);
            var fieldExpressionBuilder = new FieldExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var mergeResult = new MemberMergeResult<FieldInfo>(typeof(FieldExpressionTestClass).GetField(fieldName));
            Expression assignmentExpression = fieldExpressionBuilder.Create<FieldExpressionTestClass, FieldExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<FieldExpressionTestClass, FieldExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            FieldExpressionTestClass sourceObject = new FieldExpressionTestClass { FieldDecimal = fieldValue };
            var destinationObject = new FieldExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(fieldValue, destinationObject.FieldDecimal);
        }
    }
}

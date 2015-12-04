using System;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public sealed class PropertyExpressionBuilderTests
    {
        private static readonly string SourceParameterName = "source";
        private static readonly string DestinationParameterName = "destination";

        private readonly ParameterExpression _sourceParameterExpression;
        private readonly ParameterExpression _destinationParameterExpression;

        public PropertyExpressionBuilderTests()
        {
            _sourceParameterExpression = Expression.Parameter(typeof (PropertyExpressionTestClass), SourceParameterName);
            _destinationParameterExpression = Expression.Parameter(typeof (PropertyExpressionTestClass), DestinationParameterName);
        }

        [Theory]
        [InlineData("Odin")]
        [InlineData("Thor")]
        [InlineData("")]
        public void Create_WhenThePropertyTypeIsString_TheAssignmentExpressionIsCorrect(string propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, string>(p => p.PropString);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass {PropString = propertyValue};
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropString);
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData(0)]
        [InlineData(byte.MaxValue)]
        public void Create_WhenThePropertyTypeIsByte_TheAssignmentExpressionIsCorrect(byte propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, byte>(p => p.PropByte);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropByte = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropByte);
        }

        [Theory]
        [InlineData(short.MinValue)]
        [InlineData(0)]
        [InlineData(short.MaxValue)]
        public void Create_WhenThePropertyTypeIsInt16_TheAssignmentExpressionIsCorrect(short propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, short>(p => p.PropShort);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropShort = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropShort);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void Create_WhenThePropertyTypeIsInt32_TheAssignmentExpressionIsCorrect(int propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, int>(p => p.PropInt);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropInt = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropInt);
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(0)]
        [InlineData(long.MaxValue)]
        public void Create_WhenThePropertyTypeIsInt64_TheAssignmentExpressionIsCorrect(long propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, long>(p => p.PropLong);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropLong = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropLong);
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(0)]
        [InlineData(float.MaxValue)]
        public void Create_WhenThePropertyTypeIsSingle_TheAssignmentExpressionIsCorrect(float propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, float>(p => p.PropFloat);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropFloat = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropFloat);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        [InlineData(double.MaxValue)]
        public void Create_WhenThePropertyTypeIsDouble_TheAssignmentExpressionIsCorrect(double propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, double>(p => p.PropDouble);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropDouble = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropDouble);
        }

        [Theory]
        [InlineData(-1740958340972)]
        [InlineData(0)]
        [InlineData(1340932755493875)]
        public void Create_WhenThePropertyTypeIsDecimal_TheAssignmentExpressionIsCorrect(decimal propertyValue)
        {
            string propertyName = Utils.GetPropertyName<PropertyExpressionTestClass, decimal>(p => p.PropDecimal);
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            var mergeResult = new MemberMergeResult<PropertyInfo>(typeof(PropertyExpressionTestClass).GetProperty(propertyName));
            Expression assignmentExpression = propertyExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(mergeResult);

            BlockExpression blockExpression = Expression.Block(assignmentExpression);
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, _sourceParameterExpression, _destinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropDecimal = propertyValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propertyValue, destinationObject.PropDecimal);
        }
    }
}

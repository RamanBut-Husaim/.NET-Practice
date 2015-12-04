using System;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public sealed class PropertyBlockExpressionBuilderTests : PropertyExpressionBuilderTestsBase
    {
        [Fact]
        public void Create_WhenThePropertiesAreSpecified_TheAssignmentIsCorrect()
        {
            // arrange
            var stringProperty = typeof (PropertyExpressionTestClass).GetProperty(Utils.GetPropertyName<PropertyExpressionTestClass, string>(p => p.PropString));
            var mergeResultString = new MemberMergeResult<PropertyInfo>(stringProperty);

            var intProperty = typeof(PropertyExpressionTestClass).GetProperty(Utils.GetPropertyName<PropertyExpressionTestClass, int>(p => p.PropInt));
            var mergeResultInt = new MemberMergeResult<PropertyInfo>(intProperty);

            var propertyExpressionBuilder = new PropertyExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var properties = new[] {mergeResultString, mergeResultInt};

            // act
            var propertyBlockExpressionBuilder = new MemberBlockExpressionBuilder<PropertyInfo>(propertyExpressionBuilder);
            var blockExpression = propertyBlockExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(properties);

            // assert
            string propStringValue = "Odin";
            int propIntValue = int.MinValue;
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass {PropString = propStringValue, PropInt = propIntValue};
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(propStringValue, destinationObject.PropString);
            Assert.Equal(propIntValue, destinationObject.PropInt);
        }

        [Theory]
        [InlineData(double.MinValue, float.MinValue)]
        [InlineData(0, 0)]
        [InlineData(double.MaxValue, float.MaxValue)]
        public void Create_WhenDoubleAndSinglePropertiesAreSpecified_TheAssignmentIsCorrect(double doubleValue, float singleValue)
        {
            // arrange
            var doubleProperty = typeof(PropertyExpressionTestClass).GetProperty(Utils.GetPropertyName<PropertyExpressionTestClass, double>(p => p.PropDouble));
            var mergeResultDouble = new MemberMergeResult<PropertyInfo>(doubleProperty);

            var floatProperty = typeof(PropertyExpressionTestClass).GetProperty(Utils.GetPropertyName<PropertyExpressionTestClass, float>(p => p.PropFloat));
            var mergeResultFloat = new MemberMergeResult<PropertyInfo>(floatProperty);

            var propertyExpressionBuilder = new PropertyExpressionBuilder(this.SourceParameterExpression, this.DestinationParameterExpression);

            var properties = new[] { mergeResultDouble, mergeResultFloat };

            // act
            var propertyBlockExpressionBuilder = new MemberBlockExpressionBuilder<PropertyInfo>(propertyExpressionBuilder);
            var blockExpression = propertyBlockExpressionBuilder.Create<PropertyExpressionTestClass, PropertyExpressionTestClass>(properties);

            // assert
            var action = Expression.Lambda<Action<PropertyExpressionTestClass, PropertyExpressionTestClass>>(blockExpression, this.SourceParameterExpression, this.DestinationParameterExpression).Compile();
            PropertyExpressionTestClass sourceObject = new PropertyExpressionTestClass { PropDouble = doubleValue, PropFloat = singleValue };
            var destinationObject = new PropertyExpressionTestClass();
            action.Invoke(sourceObject, destinationObject);

            Assert.Equal(doubleValue, destinationObject.PropDouble);
            Assert.Equal(singleValue, destinationObject.PropFloat);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public sealed class PropertyBlockExpressionBuilder : IMemberBlockExpressionBuilder<PropertyInfo>
    {
        private readonly IMemberExpressionBuilder<PropertyInfo> _propertyExpressionBuilder;

        public PropertyBlockExpressionBuilder(IMemberExpressionBuilder<PropertyInfo> propertyExpressionBuilder)
        {
            _propertyExpressionBuilder = propertyExpressionBuilder;
        }

        public Expression Create<TSource, TDestination>(IEnumerable<MemberMergeResult<PropertyInfo>> properties)
        {
            Expression[] propertyExpressions = properties.Select(p => _propertyExpressionBuilder.Create<TSource, TDestination>(p)).ToArray();

            BlockExpression blockExpression = Expression.Block(propertyExpressions);

            return blockExpression;
        }
    }
}

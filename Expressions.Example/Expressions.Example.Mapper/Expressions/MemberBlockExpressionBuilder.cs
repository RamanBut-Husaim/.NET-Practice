using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public sealed class MemberBlockExpressionBuilder<TMemberInfo> : IMemberBlockExpressionBuilder<TMemberInfo> where TMemberInfo: MemberInfo
    {
        private readonly IMemberExpressionBuilder<TMemberInfo> _expressionBuilder;

        public MemberBlockExpressionBuilder(IMemberExpressionBuilder<TMemberInfo> expressionBuilder)
        {
            _expressionBuilder = expressionBuilder;
        }

        public Expression Create<TSource, TDestination>(IEnumerable<MemberMergeResult<TMemberInfo>> properties)
        {
            Expression[] propertyExpressions = properties.Select(p => _expressionBuilder.Create<TSource, TDestination>(p)).ToArray();

            BlockExpression blockExpression = Expression.Block(propertyExpressions);

            return blockExpression;
        }
    }
}

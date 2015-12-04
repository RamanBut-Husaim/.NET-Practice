using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public sealed class PropertyExpressionBuilder : IMemberExpressionBuilder<PropertyInfo>
    {
        private readonly Expression _sourceParameter;
        private readonly Expression _destinationParameter;

        public PropertyExpressionBuilder(Expression sourceParameter, Expression destinationParameter)
        {
            _sourceParameter = sourceParameter;
            _destinationParameter = destinationParameter;
        }

        public Expression Create<TSource, TDestination>(MemberMergeResult<PropertyInfo> memberMergeResult)
        {
            MemberExpression sourceProperty = Expression.Property(_sourceParameter, memberMergeResult.MemberInfo.Name);
            MemberExpression destinationProperty = Expression.Property(_destinationParameter, memberMergeResult.MemberInfo.Name);

            var assignExpression = Expression.Assign(destinationProperty, sourceProperty);

            return assignExpression;
        }
    }
}

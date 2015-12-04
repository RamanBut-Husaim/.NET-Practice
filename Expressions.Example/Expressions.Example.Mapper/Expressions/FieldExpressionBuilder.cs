using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public sealed class FieldExpressionBuilder : IMemberExpressionBuilder<FieldInfo>
    {
        private readonly Expression _sourceParameter;
        private readonly Expression _destinationParameter;

        public FieldExpressionBuilder(Expression sourceParameter, Expression destinationParameter)
        {
            _sourceParameter = sourceParameter;
            _destinationParameter = destinationParameter;
        }

        public Expression Create<TSource, TDestination>(MemberMergeResult<FieldInfo> memberMergeResult)
        {
            MemberExpression sourceProperty = Expression.Field(_sourceParameter, memberMergeResult.MemberInfo.Name);
            MemberExpression destinationProperty = Expression.Field(_destinationParameter, memberMergeResult.MemberInfo.Name);

            var assignExpression = Expression.Assign(destinationProperty, sourceProperty);

            return assignExpression;
        }
    }
}

using System.Linq.Expressions;
using System.Reflection;

namespace Expressions.Example.Mapper.Expressions
{
    public class MemberBlockExpressionBuilderFactory
    {
        public MemberBlockExpressionBuilderFactory<TSource, TDestination> Create<TSource, TDestination>() where TDestination : new()
        {
            return new MemberBlockExpressionBuilderFactory<TSource, TDestination>();
        }
    }

    public sealed class MemberBlockExpressionBuilderFactory<TSource, TDestination> where TDestination: new()
    {
        private static readonly string SourceParameterName = "source";
        private static readonly string DestinationParameterName = "destination";

        private readonly ParameterExpression _sourceParameterExpression;
        private readonly ParameterExpression _destinationParameterExpression;

        internal MemberBlockExpressionBuilderFactory()
        {
            _sourceParameterExpression = Expression.Parameter(typeof(TSource), SourceParameterName);
            _destinationParameterExpression = Expression.Parameter(typeof(TDestination), DestinationParameterName);
        }

        public ParameterExpression SourceParameterExpression
        {
            get { return _sourceParameterExpression; }
        }

        public ParameterExpression DestinationParameterExpression
        {
            get { return _destinationParameterExpression; }
        }

        public IMemberBlockExpressionBuilder<PropertyInfo> CreatePropertyBlockExpressionBuilder()
        {
            var propertyExpressionBuilder = new PropertyExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            return new MemberBlockExpressionBuilder<PropertyInfo>(propertyExpressionBuilder);
        }

        public IMemberBlockExpressionBuilder<FieldInfo> CreateFieldBlockExpressionBuilder()
        {
            var fieldExpressionBuilder = new FieldExpressionBuilder(_sourceParameterExpression, _destinationParameterExpression);

            return new MemberBlockExpressionBuilder<FieldInfo>(fieldExpressionBuilder);
        }
    }
}

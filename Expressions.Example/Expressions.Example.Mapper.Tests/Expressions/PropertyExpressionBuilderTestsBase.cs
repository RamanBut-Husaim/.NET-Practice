using System.Linq.Expressions;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public abstract class PropertyExpressionBuilderTestsBase
    {
        private static readonly string SourceParameterName = "source";
        private static readonly string DestinationParameterName = "destination";

        private readonly ParameterExpression _sourceParameterExpression;
        private readonly ParameterExpression _destinationParameterExpression;

        protected PropertyExpressionBuilderTestsBase()
        {
            _sourceParameterExpression = Expression.Parameter(typeof(PropertyExpressionTestClass), SourceParameterName);
            _destinationParameterExpression = Expression.Parameter(typeof(PropertyExpressionTestClass), DestinationParameterName);
        }

        protected ParameterExpression SourceParameterExpression
        {
            get { return _sourceParameterExpression; }
        }

        protected ParameterExpression DestinationParameterExpression
        {
            get { return _destinationParameterExpression; }
        }
    }
}

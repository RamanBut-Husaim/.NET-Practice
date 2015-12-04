using System.Linq.Expressions;

namespace Expressions.Example.Mapper.Tests.Expressions
{
    public abstract class FieldExpressionBuilderTestsBase
    {
        private static readonly string SourceParameterName = "source";
        private static readonly string DestinationParameterName = "destination";

        private readonly ParameterExpression _sourceParameterExpression;
        private readonly ParameterExpression _destinationParameterExpression;

        protected FieldExpressionBuilderTestsBase()
        {
            _sourceParameterExpression = Expression.Parameter(typeof(FieldExpressionTestClass), SourceParameterName);
            _destinationParameterExpression = Expression.Parameter(typeof(FieldExpressionTestClass), DestinationParameterName);
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

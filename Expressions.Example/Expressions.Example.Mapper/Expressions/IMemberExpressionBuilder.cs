using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public interface IMemberExpressionBuilder<TMemberInfo> where TMemberInfo: MemberInfo
    {
        Expression Create<TSource, TDestination>(MemberMergeResult<TMemberInfo> memberMergeResult);
    }
}

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper.Expressions
{
    public interface IMemberBlockExpressionBuilder<TMemberInfo> where TMemberInfo: MemberInfo
    {
        Expression Create<TSource, TDestination>(IEnumerable<MemberMergeResult<TMemberInfo>> properties);
    }
}

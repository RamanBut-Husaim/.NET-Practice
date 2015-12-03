using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper
{
    public interface IEqualityComparerFactory<in TMemberInfo> where TMemberInfo: MemberInfo
    {
        IEqualityComparer<TMemberInfo> Create();
    }
}

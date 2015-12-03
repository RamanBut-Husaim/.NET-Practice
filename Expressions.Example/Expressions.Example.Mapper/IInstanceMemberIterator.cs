using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper
{
    public interface IInstanceMemberIterator<out T, TTargetType> : IEnumerable<T> where T : MemberInfo
    {
    }
}

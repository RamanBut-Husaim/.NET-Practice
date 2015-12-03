using System.Reflection;

namespace Expressions.Example.Mapper
{
    public interface IIteratorFactory<out TMemberInfo> where TMemberInfo: MemberInfo
    {
        IInstanceMemberIterator<TMemberInfo, TTargetType> CreateSourceIterator<TTargetType>();

        IInstanceMemberIterator<TMemberInfo, TTargetType> CreateDestinationIterator<TTargetType>();
    }
}

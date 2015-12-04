using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public sealed class FieldIteratorFactory : IIteratorFactory<FieldInfo>
    {
        public IInstanceMemberIterator<FieldInfo, TTargetType> CreateSourceIterator<TTargetType>()
        {
            return new ReadableWritableInstanceFieldIterator<TTargetType>();
        }

        public IInstanceMemberIterator<FieldInfo, TTargetType> CreateDestinationIterator<TTargetType>()
        {
            return new WritableInstanceFieldIterator<TTargetType>();
        }
    }
}

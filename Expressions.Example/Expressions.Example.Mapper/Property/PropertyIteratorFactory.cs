using System.Reflection;

namespace Expressions.Example.Mapper.Property
{
    public sealed class PropertyIteratorFactory : IIteratorFactory<PropertyInfo>
    {
        public IInstanceMemberIterator<PropertyInfo, T> CreateSourceIterator<T>()
        {
            return new SetInstancePropertyIterator<T>();
        }

        public IInstanceMemberIterator<PropertyInfo, T> CreateDestinationIterator<T>()
        {
            return new GetInstancePropertyIterator<T>();
        }
    }
}

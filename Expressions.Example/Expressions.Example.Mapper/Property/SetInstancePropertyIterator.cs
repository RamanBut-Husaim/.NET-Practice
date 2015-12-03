using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Expressions.Example.Mapper.Property
{
    public sealed class SetInstancePropertyIterator<T> : InstancePropertyIterator<T>
    {
        protected override IEnumerable<PropertyInfo> Filter(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(p => p.CanWrite);
        }
    }
}

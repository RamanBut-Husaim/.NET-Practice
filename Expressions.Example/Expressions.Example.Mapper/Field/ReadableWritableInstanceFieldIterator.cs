using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public sealed class ReadableWritableInstanceFieldIterator<T> : InstanceFieldIterator<T>
    {
        protected override IEnumerable<FieldInfo> Filter(IEnumerable<FieldInfo> fields)
        {
            return fields;
        }
    }
}

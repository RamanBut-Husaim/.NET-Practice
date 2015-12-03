using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public sealed class WritableInstanceFieldIterator<T> : InstanceFieldIterator<T>
    {
        protected override IEnumerable<FieldInfo> Filter(IEnumerable<FieldInfo> fields)
        {
            return fields.Where(p => !p.IsInitOnly);
        }
    }
}

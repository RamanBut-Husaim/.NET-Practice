using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public sealed class FieldEqualityComparerFactory : IEqualityComparerFactory<FieldInfo>
    {
        public IEqualityComparer<FieldInfo> Create()
        {
            return new StrictFieldEqualityComparer();
        }
    }
}

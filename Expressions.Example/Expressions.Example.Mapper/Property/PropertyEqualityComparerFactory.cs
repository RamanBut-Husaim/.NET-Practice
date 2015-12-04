using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper.Property
{
    public sealed class PropertyEqualityComparerFactory : IEqualityComparerFactory<PropertyInfo>
    {
        public IEqualityComparer<PropertyInfo> Create()
        {
            return new StrictPropertyEqualityComparer();
        }
    }
}

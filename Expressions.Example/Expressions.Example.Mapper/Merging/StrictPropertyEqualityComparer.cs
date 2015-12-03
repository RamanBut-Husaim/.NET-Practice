using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper.Merging
{
    public sealed class StrictPropertyEqualityComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Name.Equals(y.Name) && x.PropertyType == y.PropertyType;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            int result = 17;

            result = 19 * result + obj.Name.GetHashCode();
            result = 19 * result + obj.PropertyType.GetHashCode();

            return result;
        }
    }
}

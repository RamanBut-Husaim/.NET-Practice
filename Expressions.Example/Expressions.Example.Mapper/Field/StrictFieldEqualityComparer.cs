using System.Collections.Generic;
using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public sealed class StrictFieldEqualityComparer : IEqualityComparer<FieldInfo>
    {
        public bool Equals(FieldInfo x, FieldInfo y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Name.Equals(y.Name) && x.FieldType == y.FieldType;
        }

        public int GetHashCode(FieldInfo obj)
        {
            int result = 17;

            result = 19 * result + obj.Name.GetHashCode();
            result = 19 * result + obj.FieldType.GetHashCode();

            return result;
        }
    }
}

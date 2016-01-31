using System;
using System.Linq.Expressions;

namespace CustomSerialization.Example.TestHelpers
{
    public static class MemberNameHelper
    {
        public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            return (propertySelector.Body as MemberExpression).Member.Name;
        }
    }
}

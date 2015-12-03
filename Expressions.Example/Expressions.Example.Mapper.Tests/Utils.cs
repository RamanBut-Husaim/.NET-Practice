using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Expressions.Example.Mapper.Tests
{
    public static class Utils
    {
        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
        {
            MemberExpression member = propertySelector.Body as MemberExpression;

            if (member != null)
            {
                return (member.Member as PropertyInfo).Name;
            }

            throw new ArgumentException("The selector should contain a property.");
        }
    }
}

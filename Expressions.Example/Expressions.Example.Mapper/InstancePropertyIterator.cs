using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Expressions.Example.Mapper
{
    public abstract class InstancePropertyIterator<T> : IEnumerable<PropertyInfo>
    {
        private readonly Lazy<IList<PropertyInfo>> _properties;

        protected InstancePropertyIterator()
        {
            _properties = new Lazy<IList<PropertyInfo>>(this.InitializeProperties);
        }

        private IList<PropertyInfo> Properties
        {
            get { return _properties.Value; }
        }

        private IList<PropertyInfo> InitializeProperties()
        {
            Type genericType = typeof (T);
            return this.Filter(genericType.GetProperties(BindingFlags.Instance | BindingFlags.Public)).ToList();
        }

        protected abstract IEnumerable<PropertyInfo> Filter(IEnumerable<PropertyInfo> properties);

        public IEnumerator<PropertyInfo> GetEnumerator()
        {
            return this.Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

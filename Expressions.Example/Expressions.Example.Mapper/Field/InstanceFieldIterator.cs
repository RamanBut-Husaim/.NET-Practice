using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Expressions.Example.Mapper.Field
{
    public abstract class InstanceFieldIterator<T> : IInstanceMemberIterator<FieldInfo, T>
    {
        private readonly Lazy<IList<FieldInfo>> _fields;

        protected InstanceFieldIterator()
        {
            _fields = new Lazy<IList<FieldInfo>>(this.InitializeFields);
        }

        private IList<FieldInfo> Fields
        {
            get { return _fields.Value; }
        }

        private IList<FieldInfo> InitializeFields()
        {
            var genericType = typeof (T);

            return this.Filter(genericType.GetFields(BindingFlags.Instance | BindingFlags.Public)).ToList();
        }

        protected abstract IEnumerable<FieldInfo> Filter(IEnumerable<FieldInfo> fields);

        public IEnumerator<FieldInfo> GetEnumerator()
        {
            return this.Fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

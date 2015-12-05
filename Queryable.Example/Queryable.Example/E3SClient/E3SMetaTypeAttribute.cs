using System;

namespace Queryable.Example.E3SClient
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class E3SMetaTypeAttribute : Attribute
    {
        public string Name { get; private set; }

        public E3SMetaTypeAttribute(string name)
        {
            Name = name;
        }
    }
}

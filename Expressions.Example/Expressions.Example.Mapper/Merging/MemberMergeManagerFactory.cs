﻿using System.Reflection;
using Expressions.Example.Mapper.Field;
using Expressions.Example.Mapper.Property;

namespace Expressions.Example.Mapper.Merging
{
    public sealed class MemberMergeManagerFactory
    {
        public MemberMergeManager<PropertyInfo> CreatePropertyMergeManager()
        {
            var iteratorFactory = new PropertyIteratorFactory();
            var equalityComparerFactory = new PropertyEqualityComparerFactory();

            return new MemberMergeManager<PropertyInfo>(iteratorFactory, equalityComparerFactory);
        }

        public MemberMergeManager<FieldInfo> CreateFieldMergeManager()
        {
            var iteratorFactory = new FieldIteratorFactory();
            var equalityComparerFactory = new FieldEqualityComparerFactory();

            return new MemberMergeManager<FieldInfo>(iteratorFactory, equalityComparerFactory);
        }
    }
}

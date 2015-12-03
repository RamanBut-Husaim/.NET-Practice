using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Expressions.Example.Mapper.Merging
{
    public sealed class MemberMergeManager<TMemberInfo> where TMemberInfo: MemberInfo
    {
        private readonly IIteratorFactory<TMemberInfo> _iteratorFactory;
        private readonly IEqualityComparerFactory<TMemberInfo> _equalityComparerFactory;

        public MemberMergeManager(IIteratorFactory<TMemberInfo> iteratorFactory, IEqualityComparerFactory<TMemberInfo> equalityComparerFactory)
        {
            _iteratorFactory = iteratorFactory;
            _equalityComparerFactory = equalityComparerFactory;
        }

        public IEnumerable<MemberMergeResult<TMemberInfo>> Merge<TSource, TDestination>()
        {
            var sourcePropertyIterator = _iteratorFactory.CreateSourceIterator<TSource>();
            var destinationPropertyIterator = _iteratorFactory.CreateDestinationIterator<TDestination>();

            var sourcePropertyHashSet = new HashSet<TMemberInfo>(sourcePropertyIterator, _equalityComparerFactory.Create());
            var destinationPropertyHashSet = new HashSet<TMemberInfo>(destinationPropertyIterator, _equalityComparerFactory.Create());

            destinationPropertyHashSet.IntersectWith(sourcePropertyHashSet);

            return destinationPropertyHashSet.Select(p => new MemberMergeResult<TMemberInfo>(p)).ToList();
        }
    }
}

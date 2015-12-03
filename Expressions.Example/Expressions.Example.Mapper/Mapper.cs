using System;

namespace Expressions.Example.Mapper
{
    public sealed class Mapper<TSource, TDestination> : IMapper<TSource, TDestination> where TDestination : new()
    {
        private readonly Func<TDestination> _builder;

        internal Mapper(Func<TDestination> builder)
        {
            _builder = builder;
        }

        public TDestination Map(TSource source)
        {
            return _builder.Invoke();
        }
    }
}

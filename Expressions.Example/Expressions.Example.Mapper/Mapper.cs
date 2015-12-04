using System;

namespace Expressions.Example.Mapper
{
    public sealed class Mapper<TSource, TDestination> : IMapper<TSource, TDestination> where TDestination : class, new()
    {
        private readonly Func<TDestination> _builder;
        private readonly Action<TSource, TDestination> _mapper;

        internal Mapper(Func<TDestination> builder, Action<TSource, TDestination> mapper)
        {
            _builder = builder;
            _mapper = mapper;
        }

        public TDestination Map(TSource source)
        {
            var destinationObject = _builder.Invoke();

            if (_mapper != null)
            {
                _mapper.Invoke(source, destinationObject);
            }

            return destinationObject;
        }
    }
}

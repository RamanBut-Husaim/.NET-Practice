using System;
using System.Linq.Expressions;

namespace Expressions.Example.Mapper
{
    public sealed class MapperGenerator : IMapperGenerator
    {
        public IMapper<TSource, TDestination> Generate<TSource, TDestination>() where TDestination : new()
        {
            Func<TDestination> builder = this.GenerateBuilder<TDestination>();

            return new Mapper<TSource, TDestination>(builder);
        }

        private Func<TDestination> GenerateBuilder<TDestination>() where TDestination : new()
        {
            var factoryExpression = Expression.Lambda<Func<TDestination>>(Expression.New(typeof (TDestination)));

            return factoryExpression.Compile();
        }
    }
}

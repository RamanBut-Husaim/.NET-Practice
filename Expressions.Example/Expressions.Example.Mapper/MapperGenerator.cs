namespace Expressions.Example.Mapper
{
    public sealed class MapperGenerator : IMapperGenerator
    {
        public IMapper<TSource, TDestination> Generate<TSource, TDestination>() where TDestination : new()
        {
            return new Mapper<TSource, TDestination>();
        }
    }
}

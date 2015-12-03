namespace Expressions.Example.Mapper
{
    public interface IMapperGenerator
    {
        IMapper<TSource, TDestination> Generate<TSource, TDestination>() where TDestination : new();
    }
}

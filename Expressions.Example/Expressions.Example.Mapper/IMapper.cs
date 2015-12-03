namespace Expressions.Example.Mapper
{
    public interface IMapper<TSource, TDestination> where TDestination : new()
    {
        TDestination Map(TSource source);
    }
}

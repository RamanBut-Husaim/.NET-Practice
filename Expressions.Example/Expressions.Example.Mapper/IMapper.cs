namespace Expressions.Example.Mapper
{
    public interface IMapper<in TSource, out TDestination> where TDestination : class, new()
    {
        TDestination Map(TSource source);
    }
}

using Xunit;

namespace Expressions.Example.Mapper.Tests
{
    public sealed class MapperGeneratorTests
    {
        [Fact]
        public void Generate_WhenClassesAreSpecified_TheMapperIsCreated()
        {
            var mapperGenerator = new MapperGenerator();

            IMapper<Foo, Bar> mapper = mapperGenerator.Generate<Foo, Bar>();

            Assert.NotNull(mapper);
        }
    }
}

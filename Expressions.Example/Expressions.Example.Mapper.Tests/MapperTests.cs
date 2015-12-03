using Xunit;

namespace Expressions.Example.Mapper.Tests
{
    public sealed class MapperTests
    {
        private readonly MapperGenerator _generator;

        public MapperTests()
        {
            _generator = new MapperGenerator();
        }

        [Fact]
        public void Map_WhenIsCalled_TheResultObjectCreated()
        {
            IMapper<Foo, Bar> mapper = _generator.Generate<Foo, Bar>();

            Bar resultObject = mapper.Map(new Foo());

            Assert.NotNull(resultObject);
        }
    }
}

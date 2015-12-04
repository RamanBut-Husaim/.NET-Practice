using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests
{
    public sealed class MapperGeneratorTests
    {
        [Fact]
        public void Generate_WhenClassesAreSpecified_TheMapperIsCreated()
        {
            var mapperGenerator = new MapperGenerator(new MemberMergeManagerFactory(), new MemberBlockExpressionBuilderFactory());

            IMapper<Foo, Bar> mapper = mapperGenerator.Generate<Foo, Bar>();

            Assert.NotNull(mapper);
        }
    }
}

using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;
using Xunit;

namespace Expressions.Example.Mapper.Tests
{
    public sealed class MapperTests
    {
        private readonly MapperGenerator _generator;

        public MapperTests()
        {
            _generator = new MapperGenerator(new MemberMergeManagerFactory(), new MemberBlockExpressionBuilderFactory());
        }

        [Fact]
        public void Map_WhenIsCalled_TheResultObjectCreated()
        {
            IMapper<Foo, Bar> mapper = _generator.Generate<Foo, Bar>();

            Bar resultObject = mapper.Map(new Foo());

            Assert.NotNull(resultObject);
        }

        [Fact]
        public void Map_WhenPropertiesAreSpecified_TheMappingIsCorrect()
        {
            IMapper<Foo, Bar> mapper = _generator.Generate<Foo, Bar>();
            var foo = new Foo
            {
                PropString = "Divide and Devour",
                PropInt = 42,
                PropDouble = double.MaxValue
            };

            Bar resultObject = mapper.Map(foo);

            Assert.Equal(foo.PropString, resultObject.PropString);
            Assert.Equal(foo.PropInt, resultObject.PropInt);
            Assert.Equal(foo.PropDouble, resultObject.PropDouble);
        }

        [Fact]
        public void Map_WhenFieldsAreSpecified_TheMappingIsCorrect()
        {
            IMapper<Foo, Bar> mapper = _generator.Generate<Foo, Bar>();
            var foo = new Foo
            {
                FieldString = "Number of the beast",
                FieldInt = 21,
                FieldDouble = double.MinValue
            };

            Bar resultObject = mapper.Map(foo);

            Assert.Equal(foo.FieldString, resultObject.FieldString);
            Assert.Equal(foo.FieldInt, resultObject.FieldInt);
            Assert.Equal(foo.FieldDouble, resultObject.FieldDouble);
        }

        [Fact]
        public void Map_WhenFieldsAndPropertiesAreSpecified_TheMappingIsCorrect()
        {
            IMapper<Foo, Bar> mapper = _generator.Generate<Foo, Bar>();
            var foo = new Foo
            {
                PropDecimal = 104m,
                PropFloat = float.MaxValue,
                FieldShort = 154,
                FieldLong = long.MaxValue
            };

            Bar resultObject = mapper.Map(foo);

            Assert.Equal(foo.PropDecimal, resultObject.PropDecimal);
            Assert.Equal(foo.PropFloat, resultObject.PropFloat);
            Assert.Equal(foo.FieldShort, resultObject.FieldShort);
            Assert.Equal(foo.FieldLong, resultObject.FieldLong);
        }
    }
}
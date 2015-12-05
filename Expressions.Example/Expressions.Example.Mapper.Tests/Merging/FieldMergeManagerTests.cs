using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Expressions.Example.Mapper.Field;
using Expressions.Example.Mapper.Merging;
using Moq;
using Xunit;

namespace Expressions.Example.Mapper.Tests.Merging
{
    internal sealed class FieldSourceTest
    {
        public readonly int FieldInt;

        public double FieldDouble;

        public string FieldString;

        public int FieldInt1;

        public int FieldInt2;

        public List<string> FieldListString;

        public List<float> FieldListFloat;  

        private long _fieldLong;
    }

    internal sealed class FieldDestinationTest
    {
        public readonly double FieldDouble;

        public string FieldString;

        public long FieldInt1;

        public int FieldInt2;

        public List<string> FieldListString;

        public List<double> FieldListDouble;  

        public decimal FieldDecimal;
    }

    public sealed class FieldMergeManagerTests
    {
        private readonly IEqualityComparerFactory<FieldInfo> _equalityComparerFactory;

        public FieldMergeManagerTests()
        {
            _equalityComparerFactory = new FieldEqualityComparerFactory();
        }

        [Fact]
        public void Merge_WhenClassesAreSpecified_FieldsMergedCorrectly()
        {
            // assign
            var sourceFieldIterator = new Mock<IInstanceMemberIterator<FieldInfo, FieldSourceTest>>();
            sourceFieldIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSourceEnumerator);
            var destinationFieldIterator = new Mock<IInstanceMemberIterator<FieldInfo, FieldDestinationTest>>();
            destinationFieldIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateDestinationEnumerator);

            var iteratorFactory = new Mock<IIteratorFactory<FieldInfo>>();
            iteratorFactory.Setup(p => p.CreateSourceIterator<FieldSourceTest>()).Returns(sourceFieldIterator.Object);
            iteratorFactory.Setup(p => p.CreateDestinationIterator<FieldDestinationTest>()).Returns(destinationFieldIterator.Object);

            var mergeManager = new MemberMergeManager<FieldInfo>(iteratorFactory.Object, _equalityComparerFactory);
            var mergeResult = mergeManager.Merge<FieldSourceTest, FieldDestinationTest>().Select(p => p.MemberInfo).ToList();

            // assert
            var type = typeof (FieldDestinationTest);
            var expectedResult = new List<FieldInfo>
            {
                type.GetField(Utils.GetFieldName<FieldDestinationTest, string>(p => p.FieldString)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, int>(p => p.FieldInt2)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, List<string>>(p => p.FieldListString))
            };

            Assert.Equal(expectedResult.OrderBy(p => p.Name), mergeResult.OrderBy(p => p.Name), _equalityComparerFactory.Create());
        }

        [Fact]
        public void Merge_WhenClassesHaveNoCommonFields_TheResultIsMepy()
        {
            // arrange
            var sourceFieldIterator = new Mock<IInstanceMemberIterator<FieldInfo, FieldSourceTest>>();
            sourceFieldIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSpecificSourceEnumerator);
            var destinationFieldIterator = new Mock<IInstanceMemberIterator<FieldInfo, FieldDestinationTest>>();
            destinationFieldIterator.Setup(p => p.GetEnumerator()).Returns(this.CreateSpecificDestinationEnumerator);

            var iteratorFactory = new Mock<IIteratorFactory<FieldInfo>>();
            iteratorFactory.Setup(p => p.CreateSourceIterator<FieldSourceTest>()).Returns(sourceFieldIterator.Object);
            iteratorFactory.Setup(p => p.CreateDestinationIterator<FieldDestinationTest>()).Returns(destinationFieldIterator.Object);

            // act
            var mergeManager = new MemberMergeManager<FieldInfo>(iteratorFactory.Object, _equalityComparerFactory);
            var mergeResult = mergeManager.Merge<FieldSourceTest, FieldDestinationTest>().Select(p => p.MemberInfo).ToList();
            
            // assert
            Assert.Equal(Enumerable.Empty<FieldInfo>(), mergeResult);
        }

        private IEnumerator<FieldInfo> CreateSourceEnumerator()
        {
            var type = typeof (FieldSourceTest);
            return new List<FieldInfo>
            {
                type.GetField(Utils.GetFieldName<FieldSourceTest, int>(p => p.FieldInt)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, double>(p => p.FieldDouble)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, string>(p => p.FieldString)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, int>(p => p.FieldInt1)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, int>(p => p.FieldInt2)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, List<string>>(p => p.FieldListString)),
                type.GetField(Utils.GetFieldName<FieldSourceTest, List<float>>(p => p.FieldListFloat))
            }.GetEnumerator();
        }

        private IEnumerator<FieldInfo> CreateDestinationEnumerator()
        {
            var type = typeof (FieldDestinationTest);

            return new List<FieldInfo>
            {
                type.GetField(Utils.GetFieldName<FieldDestinationTest, string>(p => p.FieldString)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, long>(p => p.FieldInt1)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, int>(p => p.FieldInt2)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, decimal>(p => p.FieldDecimal)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, List<string>>(p => p.FieldListString)),
                type.GetField(Utils.GetFieldName<FieldDestinationTest, List<double>>(p => p.FieldListDouble))
            }.GetEnumerator();
        }

        private IEnumerator<FieldInfo> CreateSpecificSourceEnumerator()
        {
            var type = typeof(FieldSourceTest);
            return new List<FieldInfo>
            {
                type.GetField(Utils.GetFieldName<FieldSourceTest, int>(p => p.FieldInt))

            }.GetEnumerator();
        }

        private IEnumerator<FieldInfo> CreateSpecificDestinationEnumerator()
        {
            var type = typeof(FieldDestinationTest);

            return new List<FieldInfo>
            {
                type.GetField(Utils.GetFieldName<FieldDestinationTest, decimal>(p => p.FieldDecimal))
            }.GetEnumerator();
        }
    }
}

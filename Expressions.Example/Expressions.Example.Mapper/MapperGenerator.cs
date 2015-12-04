using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Expressions.Example.Mapper.Expressions;
using Expressions.Example.Mapper.Merging;

namespace Expressions.Example.Mapper
{
    public sealed class MapperGenerator : IMapperGenerator
    {
        private readonly MemberMergeManager<PropertyInfo> _propertyMergeManager;
        private readonly MemberMergeManager<FieldInfo> _fieldMergeManager;
        private readonly MemberBlockExpressionBuilderFactory _blockExpressionBuilderFactory;

        public MapperGenerator(MemberMergeManagerFactory mergeManagerFactory, MemberBlockExpressionBuilderFactory blockExpressionBuilderFactory)
        {
            _propertyMergeManager = mergeManagerFactory.CreatePropertyMergeManager();
            _fieldMergeManager = mergeManagerFactory.CreateFieldMergeManager();
            _blockExpressionBuilderFactory = blockExpressionBuilderFactory;
        }

        public IMapper<TSource, TDestination> Generate<TSource, TDestination>() where TDestination : class, new()
        {
            Func<TDestination> builder = this.GenerateBuilder<TDestination>();
            Action<TSource, TDestination> memberMapper = this.GenerateMemberMapper<TSource, TDestination>();

            return new Mapper<TSource, TDestination>(builder, memberMapper);
        }

        private Func<TDestination> GenerateBuilder<TDestination>() where TDestination : class, new()
        {
            var factoryExpression = Expression.Lambda<Func<TDestination>>(Expression.New(typeof (TDestination)));

            return factoryExpression.Compile();
        }

        private Action<TSource, TDestination> GenerateMemberMapper<TSource, TDestination>() where TDestination : class, new()
        {
            var memberBlockExpressionFactory = _blockExpressionBuilderFactory.Create<TSource, TDestination>();
            IMemberBlockExpressionBuilder<FieldInfo> fieldBlockExpressionBuilder = memberBlockExpressionFactory.CreateFieldBlockExpressionBuilder();
            IMemberBlockExpressionBuilder<PropertyInfo> propertyBlockExpressionBuilder = memberBlockExpressionFactory.CreatePropertyBlockExpressionBuilder();

            IEnumerable<MemberMergeResult<PropertyInfo>> propertiesToMerge = _propertyMergeManager.Merge<TSource, TDestination>();
            IEnumerable<MemberMergeResult<FieldInfo>> fieldsToMerge = _fieldMergeManager.Merge<TSource, TDestination>();

            Expression propertyMapExpression = propertyBlockExpressionBuilder.Create<TSource, TDestination>(propertiesToMerge);
            Expression fieldMapExpression = fieldBlockExpressionBuilder.Create<TSource, TDestination>(fieldsToMerge);

            var expressionBlocks = new List<Expression>();
            if (propertyMapExpression != null)
            {
                expressionBlocks.Add(propertyMapExpression);
            }

            if (fieldMapExpression != null)
            {
                expressionBlocks.Add(fieldMapExpression);
            }

            if (expressionBlocks.Count > 0)
            {
                BlockExpression finalBlock = Expression.Block(expressionBlocks);
                var action = Expression.Lambda<Action<TSource, TDestination>>(finalBlock,
                    memberBlockExpressionFactory.SourceParameterExpression,
                    memberBlockExpressionFactory.DestinationParameterExpression)
                    .Compile();

                return action;
            }

            return null;
        }
    }
}

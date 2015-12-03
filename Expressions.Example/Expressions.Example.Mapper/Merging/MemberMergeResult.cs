using System.Reflection;

namespace Expressions.Example.Mapper.Merging
{
    public sealed class MemberMergeResult<TMemberInfo> where TMemberInfo: MemberInfo
    {
        public MemberMergeResult(TMemberInfo memberInfo)
        {
            this.MemberInfo = memberInfo;
        }

        public TMemberInfo MemberInfo { get; private set; }
    }
}

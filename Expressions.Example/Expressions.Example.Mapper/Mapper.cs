using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressions.Example.Mapper
{
    public sealed class Mapper<TSource, TDestination> : IMapper<TSource, TDestination> where TDestination : new()
    {


        public TDestination Map(TSource source)
        {
            return default(TDestination);
        }
    }
}

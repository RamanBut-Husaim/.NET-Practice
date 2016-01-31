using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomSerialization.Example.DB
{
    [Serializable]
    public partial class Product
    {
        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext context)
        {
        }

        [OnDeserializing]
        internal void OnDeserializing(StreamingContext context)
        {
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
        }
    }
}

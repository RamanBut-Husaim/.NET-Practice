using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CustomSerialization.Example.DB
{
    [Serializable]
    public partial class Category
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
            if (this.Products == null || (this.Products != null && this.Products.Count == 0))
            {
                this.Products = new HashSet<Product>();
            }
        }
    }
}

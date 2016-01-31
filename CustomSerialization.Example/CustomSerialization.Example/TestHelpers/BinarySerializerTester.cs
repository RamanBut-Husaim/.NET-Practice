using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Xunit.Abstractions;

namespace CustomSerialization.Example.TestHelpers
{
    public sealed class BinarySerializerTester<T> : SerializationTester<T, BinaryFormatter>
    {
        public BinarySerializerTester(
            ITestOutputHelper testOutputHelper,
            BinaryFormatter serializer,
            bool showResult = false) : base(testOutputHelper, serializer, showResult)
        {
        }

        internal override T Deserialization(MemoryStream stream)
        {
            return (T) this.Serializer.Deserialize(stream);
        }

        internal override void Serialization(T data, MemoryStream stream)
        {
            this.Serializer.Serialize(stream, data);
        }
    }
}

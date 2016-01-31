using System.IO;
using System.Runtime.Serialization;

using Xunit.Abstractions;

namespace CustomSerialization.Example.TestHelpers
{
    public class XmlDataContractSerializerTester<T> : SerializationTester<T, DataContractSerializer>
    {
        public XmlDataContractSerializerTester(
            ITestOutputHelper testOutputHelper,
            DataContractSerializer serializer,
            bool showResult = false) : base(testOutputHelper, serializer, showResult)
        {
        }

        internal override T Deserialization(MemoryStream stream)
        {
            return (T) this.Serializer.ReadObject(stream);
        }

        internal override void Serialization(T data, MemoryStream stream)
        {
            this.Serializer.WriteObject(stream, data);
        }
    }
}

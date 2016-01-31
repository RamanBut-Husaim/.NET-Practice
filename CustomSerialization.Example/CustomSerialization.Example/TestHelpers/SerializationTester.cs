using System.IO;
using System.Text;

using Xunit.Abstractions;

namespace CustomSerialization.Example.TestHelpers
{
    public abstract class SerializationTester<TData, TSerializer>
    {
        private readonly TSerializer _serializer;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly bool _showResult;

        protected SerializationTester(ITestOutputHelper testOutputHelper, TSerializer serializer, bool showResult = false)
        {
            _serializer = serializer;
            _showResult = showResult;
            _testOutputHelper = testOutputHelper;
        }

        protected TSerializer Serializer
        {
            get { return _serializer; }
        }

        public TData SerializeAndDeserialize(TData data)
        {
            using (var stream = new MemoryStream())
            {
                _testOutputHelper.WriteLine("Start serialization");
                this.Serialization(data, stream);
                _testOutputHelper.WriteLine("Serialization finished");

                if (_showResult)
                {
                    var r = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int) stream.Length);
                    _testOutputHelper.WriteLine(r);
                }

                stream.Seek(0, SeekOrigin.Begin);

                _testOutputHelper.WriteLine("Start deserialization");
                TData result = this.Deserialization(stream);
                _testOutputHelper.WriteLine("Deserialization finished");

                return result;
            }
        }

        internal abstract TData Deserialization(MemoryStream stream);

        internal abstract void Serialization(TData data, MemoryStream stream);
    }
}

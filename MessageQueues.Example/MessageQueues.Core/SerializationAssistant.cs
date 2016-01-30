using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MessageQueues.Core
{
    public sealed class SerializationAssistant : ISerializationAssistant
    {
        private readonly BinaryFormatter _binaryFormatter;

        public SerializationAssistant()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public byte[] Serialize<T>(T obj) where T : TransferableModel
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            byte[] objectBytes = new byte[0];

            using (var memoryStream = new MemoryStream())
            {
                _binaryFormatter.Serialize(memoryStream, obj);
                objectBytes = memoryStream.ToArray();
            }

            return objectBytes;
        }

        public T Deserialize<T>(byte[] objBytes) where T : TransferableModel
        {
            if (objBytes == null)
            {
                throw new ArgumentNullException("objBytes");
            }

            T resultObj = default(T);

            using (var memoryStream = new MemoryStream(objBytes))
            {
                resultObj = _binaryFormatter.Deserialize(memoryStream) as T;
            }

            return resultObj;
        }
    }
}

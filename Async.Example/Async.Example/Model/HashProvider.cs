using System;
using System.Security.Cryptography;

namespace Async.Example.Model
{
    public sealed class HashProvider : IDisposable
    {
        private readonly SHA512 _hashCalculator;
        private bool _disposed;

        public HashProvider()
        {
            _hashCalculator = new SHA512Managed();
        }

        public string ComputeHash(string value)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("hash provider");
            }

            byte[] stringBytes = this.GetBytes(value);

            byte[] hashValue = _hashCalculator.ComputeHash(stringBytes);

            return this.GetString(hashValue);
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _hashCalculator.Dispose();
                }

                _disposed = true;
            }
        }
    }
}

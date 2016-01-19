using System;

namespace WindowsServices.Core.Merge
{
    public sealed class DocumentInfo : IEquatable<DocumentInfo>
    {
        private readonly string _name;
        private readonly string _type;

        public DocumentInfo(string name, string type)
        {
            _name = name;
            _type = type;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Type
        {
            get { return _type; }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is DocumentInfo && this.Equals((DocumentInfo)obj);
        }

        public bool Equals(DocumentInfo other)
        {
            return this.Name.Equals(other.Name) && this.Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            int hashCode = 31;

            hashCode += 17 * hashCode ^ this.Name.GetHashCode();
            hashCode += 17 * hashCode ^ this.Type.GetHashCode();

            return hashCode;
        }
    }
}

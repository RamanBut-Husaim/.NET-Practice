using System.Xml;
using System.Xml.Schema;

namespace AdvancedXml.Example.Xsd
{
    public sealed class XmlValidator : IXmlValidator
    {
        private readonly string _schemaPath;
        private readonly string _targetNamespace;
        private readonly string _markerElement;
        private readonly string _markerAttribute;

        public XmlValidator(
            string schemaPath,
            string targetNamespace,
            string markerElement,
            string markerAttribute)
        {
            _schemaPath = schemaPath;
            _targetNamespace = targetNamespace;
            _markerElement = markerElement;
            _markerAttribute = markerAttribute;
        }

        public ValidationResult PerformValidation(string filePath)
        {
            XmlReaderSettings settings = this.CreateSettings();
            string markerAttributeValue = string.Empty;

            var validationResult = new ValidationResult();
            settings.ValidationEventHandler += (sender, args) =>
            {
                string attributeValue = new string(markerAttributeValue.ToCharArray());
                validationResult.Errors.Add(new ValidationResultEntry(args.Message, attributeValue));
            };

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while (reader.Read())
                {
                    string elementName = reader.Name;
                    if (_markerElement.Equals(elementName))
                    {
                        bool found = reader.MoveToAttribute(_markerAttribute);
                        if (found)
                        {
                            markerAttributeValue = reader.ReadContentAsString() ?? string.Empty;
                        }
                    }
                }
            }

            return validationResult;
        }

        private XmlReaderSettings CreateSettings()
        {
            var xmlReaderSettings = new XmlReaderSettings();

            xmlReaderSettings.Schemas.Add(_targetNamespace, _schemaPath);
            xmlReaderSettings.ValidationFlags = xmlReaderSettings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            xmlReaderSettings.ValidationType = ValidationType.Schema;

            return xmlReaderSettings;
        }
    }
}

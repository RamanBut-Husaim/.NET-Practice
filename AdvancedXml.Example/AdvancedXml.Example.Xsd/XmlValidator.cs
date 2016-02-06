using System.Xml;
using System.Xml.Schema;

namespace AdvancedXml.Example.Xsd
{
    public sealed class XmlValidator : IXmlValidator
    {
        private readonly string _schemaPath;
        private readonly string _targetNamespace;

        public XmlValidator(string schemaPath, string targetNamespace)
        {
            _schemaPath = schemaPath;
            _targetNamespace = targetNamespace;
        }

        public ValidationResult PerformValidation(string filePath)
        {
            XmlReaderSettings settings = this.CreateSettings();

            var validationResult = new ValidationResult();
            settings.ValidationEventHandler += (sender, args) =>
            {
                validationResult.Errors.Add(new ValidationResultEntry(args.Message));
            };

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while (reader.Read())
                {
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

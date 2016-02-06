using System;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(schemaPath), "The path to the schema could not be empty");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(targetNamespace), "The target namespace could not be empty");

            _schemaPath = schemaPath;
            _targetNamespace = targetNamespace;
        }

        public ValidationResult PerformValidation(string filePath)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(filePath), "The file path could not be empty.");

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

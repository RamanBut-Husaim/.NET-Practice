namespace AdvancedXml.Example.Xsd
{
    public interface IXmlValidator
    {
        ValidationResult PerformValidation(string filePath);
    }
}

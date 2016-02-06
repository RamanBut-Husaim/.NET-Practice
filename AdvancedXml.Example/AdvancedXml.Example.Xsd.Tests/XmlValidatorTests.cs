using Xunit;
using Xunit.Abstractions;

namespace AdvancedXml.Example.Xsd.Tests
{
    public sealed class XmlValidatorTests
    {
        private const string TargetNamespace = "http://library.by/catalog";
        private const string LibrarySchemaFileName = "LibrarySchema.xsd";

        private readonly ITestOutputHelper _textOutputHelper;

        public XmlValidatorTests(ITestOutputHelper textOutputHelper)
        {
            _textOutputHelper = textOutputHelper;
        }

        [Fact]
        public void PerformValidation_WhenTheFileIsValid_Succeeded()
        {
            var xmlValidator = this.CreateValidator();

            var validationResult = xmlValidator.PerformValidation("books.xml");

            Assert.True(validationResult.Success);
        }

        [Fact]
        public void PerformValidation_WhenTheFileIsInvalid_Failed()
        {
            var xmlValidator = this.CreateValidator();

            var validationResult = xmlValidator.PerformValidation("books_invalid.xml");

            foreach (var validationResultEntry in validationResult.Errors)
            {
                _textOutputHelper.WriteLine("Book: {0}", validationResultEntry.MarkerElementValue);
                _textOutputHelper.WriteLine("Message: {0}", validationResultEntry.Message);
            }

            Assert.False(validationResult.Success);
        }

        private IXmlValidator CreateValidator()
        {
            return new XmlValidator(LibrarySchemaFileName, TargetNamespace, "book", "id");
        }
    }
}

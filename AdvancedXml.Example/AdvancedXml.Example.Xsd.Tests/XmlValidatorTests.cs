using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        [Fact]
        public void PerformValidation_WhenTheFileIsValid_Succeeded()
        {

        }
    }
}

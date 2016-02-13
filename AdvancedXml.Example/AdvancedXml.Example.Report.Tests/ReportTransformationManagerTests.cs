using System;

using Xunit;

namespace AdvancedXml.Example.Report.Tests
{
    public sealed class ReportTransformationManagerTests : IDisposable
    {
        private readonly ReportTransformationManager _reportTransformationManager;

        public ReportTransformationManagerTests()
        {
            _reportTransformationManager = new ReportTransformationManager();
        }

        [Fact]
        public void Transform_WhenTheFileIsSpecified_Succeeded()
        {
            _reportTransformationManager.Transform("books.xml", "books-report.html");
        }

        public void Dispose()
        {
            _reportTransformationManager.Dispose();
        }
    }
}

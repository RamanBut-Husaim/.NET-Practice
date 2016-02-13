using System;

using Xunit;

namespace AdvancedXml.Example.Atom.Tests
{
    public sealed class LibraryAtomTransformationManagerTests : IDisposable
    {
        private readonly LibraryAtomTransformationManager _libraryAtomTransformationManager;

        public LibraryAtomTransformationManagerTests()
        {
            _libraryAtomTransformationManager = new LibraryAtomTransformationManager();
        }

        [Fact]
        public void Transform_WhenTheFileIsSpecified_Succeeded()
        {
            _libraryAtomTransformationManager.Transform("books.xml", "books-atom.xml");
        }

        public void Dispose()
        {
            _libraryAtomTransformationManager.Dispose();
        }
    }
}

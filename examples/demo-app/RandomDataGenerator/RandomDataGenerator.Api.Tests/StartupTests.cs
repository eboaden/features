using System;
using System.IO;
using Features.Providers;
using Xunit;

namespace RandomDataGenerator.Api.Tests
{
    public class StartupTests
    {
        private const string TestNamespace = "test-namespace";
        
        [Fact]
        public void WhenNamespaceFileExistsNamespaceContainedInThatFileIsReturned()
        {
            const string fileNamespace = "file-ns";
            const string namespaceFilePath = "namespace.txt";
            File.WriteAllText(namespaceFilePath, fileNamespace);
            var namespaceProvider = new NamespaceProvider(TestNamespace, namespaceFilePath);
            var ns = namespaceProvider.GetNamespace();
            Assert.Equal(fileNamespace, ns);
        }

        [Fact]
        public void WhenNamespaceFileDoesNotExistProvidedNamespaceIsReturned()
        {
            var namespaceProvider = new NamespaceProvider(TestNamespace, Guid.NewGuid().ToString());
            var ns = namespaceProvider.GetNamespace();
            Assert.Equal(TestNamespace, ns);
        }
    }
}
using System;
using System.IO;
using Features.Providers;
using Xunit;

namespace Features.Tests.Providers
{
    public class NamespaceProviderTests
    {
        private const string TestNamespace = "test-ns";
        [Fact]
        public void GetNamespaceFromNamespaceFile()
        {
            var fileNamespace = "file-ns";
            File.WriteAllText("namespace.txt", fileNamespace);
            var ns = new NamespaceProvider(namespacePath: "namespace.txt", ns: TestNamespace).GetNamespace();
            Assert.Equal(fileNamespace, ns);
        }

        [Fact]
        public void GetNamespaceFromConstructorArguments()
        {
            var ns = new NamespaceProvider(namespacePath: Guid.NewGuid().ToString(), ns: TestNamespace).GetNamespace();
            Assert.Equal(TestNamespace, ns);
        }
    }
}
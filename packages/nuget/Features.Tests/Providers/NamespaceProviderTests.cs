using System;
using System.IO;
using Features.Providers;
using Xunit;

namespace Features.Tests.Providers
{
    public class NamespaceProviderTests
    {
        [Fact]
        public void GetNamespaceFromNamespaceFile()
        {
            File.WriteAllText("namespace.txt", "file-ns");
            var ns = new NamespaceProvider(namespacePath: "namespace.txt", ns: "demo-app").GetNamespace();
            Assert.Equal("file-ns", ns);
        }

        [Fact]
        public void GetNamespaceFromConstructorArguments()
        {
            var ns = new NamespaceProvider(namespacePath: Guid.NewGuid().ToString(), ns: "demo-app").GetNamespace();
            Assert.Equal("demo-app", ns);
        }
    }
}
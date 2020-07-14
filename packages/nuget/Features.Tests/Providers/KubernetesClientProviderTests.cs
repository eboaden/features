using System;
using System.IO;
using Features.Providers;
using Xunit;

namespace Features.Tests.Providers
{
    public class KubernetesClientProviderTests
    {
        [Fact]
        public void BuildClientReturnsInClusterConfigWhenTokenFileExists()
        {
            var provider = new KubernetesClientProvider(Path.GetTempFileName());

            Assert.IsType<KubernetesClientProviderInCluster>(provider.InnerProvider);
        }
        
        [Fact]
        public void BuildClientReturnsConfigFileConfigWhenTokenFileDoesNotExist()
        {
            var provider = new KubernetesClientProvider(Guid.NewGuid().ToString());

            Assert.IsType<KubernetesClientProviderFromConfigFile>(provider.InnerProvider);
        }
    }
}
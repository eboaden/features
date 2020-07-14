using System.Collections.Generic;
using System.Threading;
using Features.Models;
using Features.Providers;
using k8s;
using Microsoft.Rest;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Features.Tests
{
    public class FeaturesClientTests
    {
        private readonly Mock<INamespaceProvider> _mockNamespaceProvider;
        private readonly Mock<IKubernetesClientProvider> _mockKubernetesClientProvider;
        private readonly Mock<IKubernetes> _mockKubernetesClient;
        private const string TestFeatureName = "test-feature";
        private const string TestNamespace = "test-ns";

        public FeaturesClientTests()
        {
            _mockNamespaceProvider = new Mock<INamespaceProvider>();
            _mockKubernetesClientProvider = new Mock<IKubernetesClientProvider>();
            _mockKubernetesClient = new Mock<IKubernetes>();
        }
        
        [Fact]
        public void CheckFeatureIsActiveWhenFeatureNameIsProvided()
        {
            
            MockClientCall(_mockNamespaceProvider, _mockKubernetesClient, TestNamespace, TestFeatureName, true);

            var featuresClient = new FeaturesClient(namespaceProvider: _mockNamespaceProvider.Object,
                clientProvider: _mockKubernetesClientProvider.Object);
            var featureResponse = featuresClient.CheckFeatureIsActive(TestFeatureName);
            _mockKubernetesClient.Verify();
            Assert.True(featureResponse);
        }

        [Fact]
        public void CheckFeatureIsNotActiveWhenFeatureNameIsProvided()
        {
            MockClientCall(_mockNamespaceProvider, _mockKubernetesClient, TestNamespace, TestFeatureName, false);

            var featuresClient = new FeaturesClient(namespaceProvider: _mockNamespaceProvider.Object,
                clientProvider: _mockKubernetesClientProvider.Object);
            var featureResponse = featuresClient.CheckFeatureIsActive(TestFeatureName);
            _mockKubernetesClient.Verify();
            Assert.False(featureResponse);
        }

        [Fact]
        public void CheckFeatureIsNotActiveWhenFeatureNameProvidedDoesNotExist()
        {
            MockClientCall(_mockNamespaceProvider, _mockKubernetesClient, TestNamespace, TestFeatureName, false);

            var featuresClient = new FeaturesClient(namespaceProvider: _mockNamespaceProvider.Object,
                clientProvider: _mockKubernetesClientProvider.Object);
            var featureResponse = featuresClient.CheckFeatureIsActive("magical-new-feature");
            _mockKubernetesClient.Verify();
            Assert.False(featureResponse);
        }

        private void MockClientCall(Mock<INamespaceProvider> mockNamespaceProvider,
            Mock<IKubernetes> mockKubernetesClient, string ns, string featureName, bool featureActive)
        {
            var stringJson = JsonConvert.SerializeObject(new
            {
                items = new[]
                {
                    new CustomObject
                    {
                        ApiVersion = "v1",
                        Kind = "Feature",
                        Spec = new CustomObjectSpec
                        {
                            Name = featureName,
                            Active = featureActive
                        }
                    }
                }
            });

            mockNamespaceProvider.Setup(s => s.GetNamespace()).Returns(ns);
            
            mockKubernetesClient.Setup(s => s.ListNamespacedCustomObjectWithHttpMessagesAsync(
                "edward.tech",
                "v1",
                ns,
                "features",
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<bool?>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string,List<string>>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<object> {Body = stringJson}).Verifiable();

            _mockKubernetesClientProvider.Setup(s => s.BuildClient()).Returns(_mockKubernetesClient.Object);
        }
    }
}
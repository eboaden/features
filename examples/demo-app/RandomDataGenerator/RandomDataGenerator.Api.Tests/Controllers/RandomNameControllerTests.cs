using Features;
using Moq;
using RandomDataGenerator.Api.Controllers;
using Xunit;

namespace RandomDataGenerator.Api.Tests.Controllers
{
    public class RandomNameControllerTests
    {
        private readonly Mock<IFeaturesClient> _featuresClient;
        private const string FeatureName = "change-name-to-city";
        public RandomNameControllerTests()
        {
            _featuresClient = new Mock<IFeaturesClient>();
        }
        [Fact]
        public void OnGetWhenFeatureIsActiveReturnsACity()
        {
            _featuresClient.Setup(s => s.CheckFeatureIsActive(FeatureName)).Returns(true).Verifiable();
            var randomNameController = new RandomNameController(_featuresClient.Object);
            var response = randomNameController.Get();
            _featuresClient.Verify();
            Assert.StartsWith("City:", response);
        }

        [Fact]
        public void OnGetWhenFeatureIsNotActiveReturnsAName()
        {
            _featuresClient.Setup(s => s.CheckFeatureIsActive(FeatureName)).Returns(false).Verifiable();
            var randomNameController = new RandomNameController(_featuresClient.Object);
            var response = randomNameController.Get();
            _featuresClient.Verify();
            Assert.StartsWith("Name:", response);
        }
    }
}
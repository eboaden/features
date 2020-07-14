using Bogus;
using Microsoft.AspNetCore.Mvc;
using Features;

namespace RandomDataGenerator.Api.Controllers
{
    [ApiController]
    [Route("random-name")]
    public class RandomNameController : ControllerBase
    {
        private readonly IFeaturesClient _featuresClient;
        private readonly Faker _faker;

        public RandomNameController(IFeaturesClient featuresClient)
        {
            _featuresClient = featuresClient;
            _faker = new Faker();
        }
        
        [HttpGet]
        public string Get()
        {
            return _featuresClient.CheckFeatureIsActive("change-name-to-city") ? $"City: {_faker.Address.City()}": $"Name: {_faker.Name.FullName()}";
        }
    }
}
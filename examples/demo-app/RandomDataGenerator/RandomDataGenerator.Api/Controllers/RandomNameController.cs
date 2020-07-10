using Bogus;
using Microsoft.AspNetCore.Mvc;
using Features;

namespace RandomDataGenerator.Api.Controllers
{
    [ApiController]
    [Route("random-name")]
    public class RandomNameController : ControllerBase
    {
        private readonly FeaturesClient _featuresClient;
        private readonly Faker _faker;

        public RandomNameController(FeaturesClient featuresClient)
        {
            _featuresClient = featuresClient;
            _faker = new Faker();
        }
        
        [HttpGet]
        public string Get()
        {
            return _featuresClient.GetFeature("change-name-to-city") ? $"City: {_faker.Address.City()}": $"Name: {_faker.Name.FullName()}";
        }
    }
}
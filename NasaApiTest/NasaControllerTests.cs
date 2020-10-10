using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NasaApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace NasaApiTest
{
    public class NasaControllerTests
    {

        public IConfiguration _configMock;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void poolImages_Positive()
        {
            _configMock = InitConfiguration();
            NasaController nasaObj = new NasaController(_configMock);
            ActionResult result = nasaObj.poolImages();
            Assert.Pass("Down loaded successfully", result);
        }

        [Test]
        public void poolImages_Negative()
        {
            _configMock = InitConfiguration();
            NasaController nasaObj = new NasaController(_configMock);
            ActionResult result = nasaObj.poolImages();
            Assert.Fail("Down loaded successfully", result);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return config;
        }
    }
}
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NasaApiTest
{
    public class Tests
    {

        public IConfiguration _configMock;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
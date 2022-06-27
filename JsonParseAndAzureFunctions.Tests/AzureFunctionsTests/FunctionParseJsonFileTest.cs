using JsonParseAndAzureFunctions.Functions;
using JsonParser.Menu.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace JsonParseAndAzureFunctions.Tests.AzureFunctionsTests
{
    [TestFixture]
    public class FunctionParseJsonFileTest 
    {
        private FunctionParseJsonFile functionParseJsonFile;
        private Mock<IParseStructureJson> parseMenu;
        private Mock<ILogger> logger;


        [SetUp]
        public void SetUp()
        {
            parseMenu = new Mock<IParseStructureJson>();
            functionParseJsonFile = new FunctionParseJsonFile(parseMenu.Object);
            logger = new Mock<ILogger>();
        }


        [TearDown]
        public void TearDown()
        {
            parseMenu = null;
            functionParseJsonFile = null;
            logger = null;
        }


        [Test]
        public void ParseJson_BlobTrigger_Success()
        {
            functionParseJsonFile.ParseJson("string", It.IsAny<string>(), logger.Object);

            parseMenu.Verify(service => service.Parsing(It.IsAny<string>()), Times.Once);
        }

    }
}

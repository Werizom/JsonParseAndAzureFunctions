using JsonParser.Models;
using JsonParser.Parse;
using NUnit.Framework;
using System;
using System.IO;

namespace JsonParseAndAzureFunctions.Tests.ParseJsonTests
{
    [TestFixture]
    public class JsonFileTest
    {
        private JsonFile jsonFile;
        private readonly string pathCompleteJsonFile = Path.Combine(Environment.CurrentDirectory, "jsonFile/CompleteJson.json");
        private readonly string pathFailJsonFile = Path.Combine(Environment.CurrentDirectory, "jsonFile/FailJson.json");

        [SetUp]
        public void SetUp()
        {
            jsonFile = new JsonFile();
        }

        [TearDown]
        public void TearDown()
        {
            jsonFile = null;
        }

        [Test]
        public void GetJsonDeserializeJsonFile_InputReadJsonString_ReturnDeserializeJsonModel()
        {
            var readJson = File.ReadAllText(pathCompleteJsonFile);

            var result = jsonFile.GetJsonDeserializeJsonFile(readJson);

            Assert.IsInstanceOf<DeserializedJsonModel>(result);
        } 
        
        [Test]
        public void GetJsonDeserializeJsonFile_InputReadFailJsonString_ReturnException()
        {
            var readJson = File.ReadAllText(pathFailJsonFile);

            Assert.Throws<Exception>(() => jsonFile.GetJsonDeserializeJsonFile(readJson));
        }

    }
}

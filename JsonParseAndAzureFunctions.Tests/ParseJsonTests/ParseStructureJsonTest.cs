using JsonParser.Menu;
using JsonParser.Models;
using JsonParser.Parse.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JsonParseAndAzureFunctions.Tests.ParseJsonTests
{
    [TestFixture]
    public class ParseStructureJsonTest
    {
        private DeserializedJsonModel deserializeValueType;
        private DeserializedJsonModel deserializeStructureType;
        private DeserializedJsonModel deserializeDependencyType;
        private DeserializedJsonModel deserializeFailType;

        private ValuesType valueType;
        private StructureType structureType;
        private DependencyType dependencyType;

        private ParseStructureJson parseMenu;
        private Mock<IJsonFile> jsonFile;
        private Mock<IValueTypeParse> valueStructureParse;
        private Mock<IStructureTypeParse> nodeStructureParse;
        private Mock<IDependencyTypeParse> dependencyStructureParse;


        [SetUp]
        public void SetUp()
        {
            jsonFile = new Mock<IJsonFile>();
            valueStructureParse = new Mock<IValueTypeParse>();
            nodeStructureParse = new Mock<IStructureTypeParse>();
            dependencyStructureParse = new Mock<IDependencyTypeParse>();
            parseMenu = new ParseStructureJson(jsonFile.Object, valueStructureParse.Object, nodeStructureParse.Object, dependencyStructureParse.Object);

            DeserializeModels();
            JsonStructureModels();
        }

        private void DeserializeModels()
        {
            deserializeValueType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "value",
                DefualtLanguagecode = "en"
            };

            deserializeStructureType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1"},
                VocabularyType = "structure",
                DefualtLanguagecode = "en"
            };

            deserializeDependencyType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "dependency",
                DefualtLanguagecode = "en"
            };

            deserializeFailType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "fail",
                DefualtLanguagecode = "en"
            };
        }
        private void JsonStructureModels()
        {
            valueType = new JsonParser.Models.ValuesType()
            {
                Description = new BaseDescription() { Name = "Name", Oid = "123", Version = "1" },
                DefualtLanguageCode = "eng",
                VocabularyType = "value",
                Values = new List<object>()
            };

            structureType = new StructureType()
            {
                Description = new BaseDescription() { Name = "Name", Oid = "123", Version = "1" },
                DefualtLanguageCode = "eng",
                VocabularyType = "structure",
                Nodes = new List<object>()
            };

            dependencyType = new DependencyType()
            {
                Description = new BaseDescription() { Name = "Name", Oid = "123", Version = "1" },
                DefualtLanguageCode = "eng",
                VocabularyType = "dependency",
                Values = new List<object>()
            };
        }


        [TearDown]
        public void TearDown()
        {
            jsonFile = null;
            valueStructureParse = null;
            nodeStructureParse = null;
            dependencyStructureParse = null;
            parseMenu = null;

            deserializeValueType = null;
            deserializeStructureType = null;
            deserializeDependencyType = null;
            deserializeFailType = null;

            valueType = null;
            structureType = null;
            dependencyType = null;
        }



        [Test]
        public void ParseStart_InputJsonValueType_ReturnValueType()
        {
            jsonFile.Setup(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>())).Returns(deserializeValueType);
            valueStructureParse.Setup(service => service.FillStructure(It.IsAny<DeserializedJsonModel>())).Returns(valueType);

            var result = parseMenu.Parsing(It.IsAny<string>());

            jsonFile.Verify(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>()), Times.Once);
            valueStructureParse.Verify(service => service.FillStructure(It.IsAny<DeserializedJsonModel>()), Times.Once);
            Assert.IsInstanceOf<JsonParser.Models.ValuesType>(result);
        }

        [Test]
        public void ParseStart_InputJsonStructureType_ReturnStructureType()
        {
            jsonFile.Setup(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>())).Returns(deserializeStructureType);
            nodeStructureParse.Setup(service => service.FillStructure(It.IsAny<DeserializedJsonModel>())).Returns(structureType);

            var result = parseMenu.Parsing(It.IsAny<string>());

            jsonFile.Verify(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>()), Times.Once);
            nodeStructureParse.Verify(service => service.FillStructure(It.IsAny<DeserializedJsonModel>()), Times.Once);
            Assert.IsInstanceOf<StructureType>(result);
        }

        [Test]
        public void ParseStart_InputJsonDependencyType_ReturnDependencyType()
        {
            jsonFile.Setup(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>())).Returns(deserializeDependencyType);
            dependencyStructureParse.Setup(service => service.FillStructure(It.IsAny<DeserializedJsonModel>())).Returns(dependencyType);

            var result = parseMenu.Parsing(It.IsAny<string>());

            jsonFile.Verify(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>()), Times.Once);
            dependencyStructureParse.Verify(service => service.FillStructure(It.IsAny<DeserializedJsonModel>()), Times.Once);
            Assert.IsInstanceOf<DependencyType>(result);
        }


        [Test]
        public void ParseStart_InputFailFieldVocabularyTypeInJsonFile_ReturnNull()
        {
            jsonFile.Setup(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>())).Returns(deserializeFailType);

            var result = parseMenu.Parsing(It.IsAny<string>());

            Assert.IsNull(result);
        }

        [Test]
        public void ParseStart_InputFailJsonFile_ReturnException()
        {
            jsonFile.Setup(service => service.GetJsonDeserializeJsonFile(It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => parseMenu.Parsing(It.IsAny<string>()));
        }

    }
}

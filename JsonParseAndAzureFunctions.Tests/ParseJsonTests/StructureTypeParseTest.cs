using JsonParser.Models;
using JsonParser.Parse;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace JsonParseAndAzureFunctions.Tests.ParseJsonTests
{
    [TestFixture]
    public class StructureTypeParseTest
    {
        private StructureTypeParse structureTypeParse;
        private DeserializedJsonModel deserializeStructureType;
        private const string vocolaburyType = "structure";

        [SetUp]
        public void SetUp()
        {
            structureTypeParse = new StructureTypeParse();
            DeserializeModels();
        }

        [TearDown]
        public void TearDown()
        {
            structureTypeParse = null;
            deserializeStructureType = null;
        }

        private void DeserializeModels()
        {
            deserializeStructureType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "structure",
                DefualtLanguagecode = "en",
                Node = new Dictionary<string, NodeTitle>()
                {
                    {
                        "NodeTitle", new NodeTitle()
                        {
                            FormType = new FormType()
                            {
                                 Fields = new Fields() { Link = "Link" }
                            },
                            ObjectLanguage = new Dictionary<string, ValueLanguage>()
                            {
                                {"languagecode", new ValueLanguage() { LanguageValue = "value" } }
                            } 
                        }
                    }
                }
            };
        }
 

        [Test]
        public void FillStructure_DeserializeJsonStructureType_ReturnStructureType()
        {
            var result = structureTypeParse.FillStructure(deserializeStructureType);

            Assert.IsInstanceOf<StructureType>(result);
            AssertAreEqual(result);
        }

        private void AssertAreEqual(StructureType result)
        {
            Assert.AreEqual(deserializeStructureType.Description.Name, result.Description.Name);
            Assert.AreEqual(deserializeStructureType.Description.Oid, result.Description.Oid);
            Assert.AreEqual(deserializeStructureType.Description.Version, result.Description.Version);
            Assert.AreEqual(deserializeStructureType.DefualtLanguagecode, result.DefualtLanguageCode);
            Assert.AreEqual(deserializeStructureType.VocabularyType, vocolaburyType);
            Assert.AreEqual(deserializeStructureType.Node.Values.Count, result.Nodes.Count);
            Assert.AreEqual(null, deserializeStructureType.Value);
            Assert.AreEqual(deserializeStructureType.Node.Values.LastOrDefault().FormType.Fields.Link, result.Link);
            Assert.AreEqual(deserializeStructureType.Node.Keys.FirstOrDefault(), result.Title);
            Assert.AreEqual(deserializeStructureType.Node.Values.FirstOrDefault().ObjectLanguage.Values.FirstOrDefault().LanguageValue, result.Value);
            Assert.AreEqual(deserializeStructureType.Node.Values.FirstOrDefault().ObjectLanguage.Keys.FirstOrDefault(), result.Language);
        }
    }
}

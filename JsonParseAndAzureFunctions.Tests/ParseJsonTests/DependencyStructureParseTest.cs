using JsonParser.Models;
using JsonParser.Parse;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace JsonParseAndAzureFunctions.Tests.ParseJsonTests
{
    [TestFixture]
    public class DependencyStructureParseTest
    {

        private DependencyTypeParse dependencyTypeParse;
        private DeserializedJsonModel deserializeDependencyType;
        private const string vocolaburyType = "dependency";

        [SetUp]
        public void SetUp()
        {
            dependencyTypeParse = new DependencyTypeParse();
            DeserializeModels();
        }

        [TearDown]
        public void TearDown()
        {
            dependencyTypeParse = null;
            deserializeDependencyType = null;
        }

        private void DeserializeModels()
        {
            deserializeDependencyType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "dependency",
                DefualtLanguagecode = "en",
                Value = new Dictionary<string, Dictionary<string, NodeTitle>>()
                {
                    {
                        "Oid", new Dictionary<string, NodeTitle>()
                        {
                            {
                                "NodeTitle", new NodeTitle()
                                 {
                                    DependsOf = new DependsOf()
                                    {
                                        KeysList = new List<string>(){ "first", "second" }
                                    },
                                    ObjectLanguage = new Dictionary<string, ValueLanguage>()
                                    {
                                       {"languagecode", new ValueLanguage() { LanguageValue = "value" } }
                                    }
                                 }
                            }
                        }
                    }
                }
            };
        }


        [Test]
        public void FillStructure_DeserializeJsonDependencyType_ReturnDependencyType()
        {
            var result = dependencyTypeParse.FillStructure(deserializeDependencyType);

            Assert.IsInstanceOf<DependencyType>(result);
            AssertAreEqual(result);
        }


        private void AssertAreEqual(DependencyType result)
        {
            Assert.AreEqual(deserializeDependencyType.Description.Name, result.Description.Name);
            Assert.AreEqual(deserializeDependencyType.Description.Oid, result.Description.Oid);
            Assert.AreEqual(deserializeDependencyType.Description.Version, result.Description.Version);
            Assert.AreEqual(deserializeDependencyType.DefualtLanguagecode, result.DefualtLanguageCode);
            Assert.AreEqual(deserializeDependencyType.VocabularyType, vocolaburyType);
            Assert.AreEqual(deserializeDependencyType.Value.Values.Count, result.Values.Count);
            Assert.AreEqual(null, deserializeDependencyType.Node);
            Assert.AreEqual(deserializeDependencyType.Value.Values.LastOrDefault().Keys.LastOrDefault(), result.NodeTitle);
            Assert.AreEqual(deserializeDependencyType.Value.Values.FirstOrDefault().Values.FirstOrDefault().ObjectLanguage.Values.FirstOrDefault().LanguageValue,
                result.ValueLanguage);
            Assert.AreEqual(deserializeDependencyType.Value.Values.FirstOrDefault().Values.FirstOrDefault().ObjectLanguage.Keys.FirstOrDefault(), result.Language);
        }

    }
}

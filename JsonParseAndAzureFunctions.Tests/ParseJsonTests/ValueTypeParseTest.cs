using JsonParser.Models;
using JsonParser.Parse;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace JsonParseAndAzureFunctions.Tests.ParseJsonTests
{
    [TestFixture]
    public class ValueTypeParseTest
    {
        private ValueTypeParse valueTypeParse;
        private DeserializedJsonModel deserializeValueType;
        private const string vocolaburyType = "value";

        [SetUp]
        public void SetUp()
        {
            valueTypeParse = new ValueTypeParse();
            DeserializeModels();
        }

        [TearDown]
        public void TearDown()
        {
            valueTypeParse = null;
            deserializeValueType = null;
        }

        private void DeserializeModels()
        {
            deserializeValueType = new DeserializedJsonModel()
            {
                Description = new Description() { Name = "Name", Oid = "001", Version = "1" },
                VocabularyType = "value",
                DefualtLanguagecode = "en",
                Value = new Dictionary<string, Dictionary<string, NodeTitle>>()
                {
                    {
                        "Oid", new Dictionary<string, NodeTitle>()
                        {
                            {
                                "NodeTitle", new NodeTitle()
                                 {
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
        public void FillStructure_DeserializeJsonValueType_ReturnValueType()
        {
            var result = valueTypeParse.FillStructure(deserializeValueType);

            Assert.IsInstanceOf<ValuesType>(result);
            AssertAreEqual(result);
        }


        private void AssertAreEqual(ValuesType result)
        {
            Assert.AreEqual(deserializeValueType.Description.Name, result.Description.Name);
            Assert.AreEqual(deserializeValueType.Description.Oid, result.Description.Oid);
            Assert.AreEqual(deserializeValueType.Description.Version, result.Description.Version);
            Assert.AreEqual(deserializeValueType.DefualtLanguagecode, result.DefualtLanguageCode);
            Assert.AreEqual(deserializeValueType.VocabularyType, vocolaburyType);
            Assert.AreEqual(deserializeValueType.Value.Values.Count, result.Values.Count);
            Assert.AreEqual(null, deserializeValueType.Node);
            Assert.AreEqual(deserializeValueType.Value.Values.LastOrDefault().Keys.LastOrDefault(), result.NodeTitle);
            Assert.AreEqual(deserializeValueType.Value.Values.FirstOrDefault().Values.FirstOrDefault().ObjectLanguage.Values.FirstOrDefault().LanguageValue,
                result.ValueLanguage);
            Assert.AreEqual(deserializeValueType.Value.Values.FirstOrDefault().Values.FirstOrDefault().ObjectLanguage.Keys.FirstOrDefault(), result.Language);
        }
    }
}

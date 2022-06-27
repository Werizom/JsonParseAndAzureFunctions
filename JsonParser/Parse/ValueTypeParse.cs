using JsonParser.Models;
using JsonParser.Parse.Interface;
using System.Collections.Generic;


namespace JsonParser.Parse
{
    public class ValueTypeParse : IValueTypeParse
    {
        public ValuesType FillStructure(DeserializedJsonModel deserializeFile)
        {
            ValuesType valueType = AddBaseInfo(deserializeFile);

            foreach (var oid in deserializeFile.Value)
            {
                valueType.ValueOid = oid.Key;
                
                foreach (var node in oid.Value)
                {
                    Dictionary<string, object> valuesOid = new Dictionary<string, object>();
                    valuesOid.Add("ValuesOid", valueType.ValueOid);

                    Dictionary<string, object> nodes = AddLanguageDescription(valueType, node);
                    valuesOid.Add("Description", nodes);
                    valueType.Values.Add(valuesOid);
                }
            }

            return valueType;
        }

        private ValuesType AddBaseInfo(DeserializedJsonModel deserializeFile)
        {
            ValuesType valueType = new ValuesType();
            valueType.DefualtLanguageCode = deserializeFile.DefualtLanguagecode;
            valueType.VocabularyType = deserializeFile.VocabularyType;

            valueType.Description = new BaseDescription();
            valueType.Description.Name = deserializeFile.Description.Name;
            valueType.Description.Oid = deserializeFile.Description.Oid;
            valueType.Description.Version = deserializeFile.Description.Version;

            valueType.Values = new List<object>();

            return valueType;
        }

        private Dictionary<string, object> AddLanguageDescription(ValuesType valueType, KeyValuePair<string, NodeTitle> node)
        {
            Dictionary<string, object> nodes = new Dictionary<string, object>();
            valueType.NodeTitle = node.Key;
            nodes.Add("NodeTitle", valueType.NodeTitle);

            List<object> languages = new List<object>();

            foreach (var descriptLanguage in node.Value.ObjectLanguage)
            {
                Dictionary<string, string> languageDescription = new Dictionary<string, string>();
               
                valueType.Language = descriptLanguage.Key;
                valueType.ValueLanguage = descriptLanguage.Value.LanguageValue;
               
                languageDescription.Add("Languagecode", valueType.Language);
                languageDescription.Add("Value", valueType.ValueLanguage);

                languages.Add(languageDescription); 
            }
            nodes.Add($"Languages", languages);

            return nodes;
        }

    }
}

using JsonParser.Models;
using JsonParser.Parse.Interface;
using System.Collections.Generic;
using System.Linq;


namespace JsonParser.Parse
{
    public class DependencyTypeParse : IDependencyTypeParse
    {
        public DependencyType FillStructure(DeserializedJsonModel deserializeFile)
        {
            DependencyType dependencyType = AddBaseInfo(deserializeFile);

            AddAllInfoAboutStructure(dependencyType, deserializeFile);

            return dependencyType;
        }

       private void AddAllInfoAboutStructure(DependencyType dependencyType, DeserializedJsonModel deserializeFile)
        {
            foreach (var node in deserializeFile.Value.FirstOrDefault().Value)
            {
                Dictionary<string, object> valuesOid = new Dictionary<string, object>();
                dependencyType.ValueOid = deserializeFile.Value.FirstOrDefault().Key;
                valuesOid.Add("ValuesOid", dependencyType.ValueOid);

                Dictionary<string, object> nodes = new Dictionary<string, object>();
                dependencyType.NodeTitle = node.Key;
                nodes.Add("NodeTitle", dependencyType.NodeTitle);

                AddDependsOptions(nodes, node, deserializeFile);
                AddDescriptionLanguage(dependencyType, nodes, node);

                valuesOid.Add("Description", nodes);
                dependencyType.Values.Add(valuesOid);
            }
        }

        private DependencyType AddBaseInfo(DeserializedJsonModel deserializeFile)
        {
            DependencyType dependencyType = new DependencyType();
            dependencyType.DefualtLanguageCode = deserializeFile.DefualtLanguagecode;
            dependencyType.VocabularyType = deserializeFile.VocabularyType;

            dependencyType.Description = new BaseDescription();
            dependencyType.Description.Name = deserializeFile.Description.Name;
            dependencyType.Description.Oid = deserializeFile.Description.Oid;
            dependencyType.Description.Version = deserializeFile.Description.Version;

            dependencyType.Values = new List<object>();

            return dependencyType;
        }

        private void AddDependsOptions(Dictionary<string, object> nodes, 
            KeyValuePair<string, NodeTitle> node,
            DeserializedJsonModel deserializeFile)
        {
            if (node.Value.DependsOf.KeysList != null)
            {
                var referenceDependsList = node.Value.DependsOf.KeysList.ToList();

                Dictionary<string, object> listOfDependency = new Dictionary<string, object>();
                foreach (var itemDepend in referenceDependsList)
                {
                    var dependencyDictionary = AddDescriptionToDependentOptions(deserializeFile, itemDepend);
                    listOfDependency.Add(itemDepend, dependencyDictionary);
                }

                nodes.Add("DependencyDictionary", listOfDependency);
            }

        }

       private  Dictionary<string, object> AddDescriptionToDependentOptions(DeserializedJsonModel deserialize, string findKey)
        {
            Dictionary<string, object> description = new Dictionary<string, object>();
            
            var dictionaryOfDependentOptions = deserialize.Value.LastOrDefault().Value;
            foreach (var option in dictionaryOfDependentOptions.Keys)
            { 
                if (findKey == option)
                {
                    AddLanguagesDependOfOptions(dictionaryOfDependentOptions, option, description);
                }
            }

            return description;
        }

        private void AddLanguagesDependOfOptions(Dictionary<string, NodeTitle> dictionaryOfDependentOptions, 
                                        string option, 
                                        Dictionary<string, object> description) 
        {
            var value = dictionaryOfDependentOptions[option];
            
            List<object> languages = new List<object>();
            foreach (var language in value.ObjectLanguage)
            {
                Dictionary<string, string> languageDescription = new Dictionary<string, string>();
                languageDescription.Add("Languagecode", language.Key);
                languageDescription.Add("Value", language.Value.LanguageValue);
                languages.Add(languageDescription);
            }
            description.Add($"Languages", languages);
        }

        private void AddDescriptionLanguage(DependencyType dependencyType,
            Dictionary<string, object> nodes, 
            KeyValuePair<string, NodeTitle> node)
        {

            List<object> languages = new List<object>();

            foreach (var descriptionLanguage in node.Value.ObjectLanguage)
            {
                Dictionary<string, string> languageDescription = new Dictionary<string, string>();

                dependencyType.Language = descriptionLanguage.Key;
                dependencyType.ValueLanguage = descriptionLanguage.Value.LanguageValue;

                languageDescription.Add("Languagecode", dependencyType.Language);
                languageDescription.Add("ValueLanguage", dependencyType.ValueLanguage);

                languages.Add(languageDescription);
            }
            nodes.Add($"Languages", languages);
        }

    }
}

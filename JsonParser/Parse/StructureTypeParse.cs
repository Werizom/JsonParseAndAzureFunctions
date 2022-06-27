using JsonParser.Models;
using JsonParser.Parse.Interface;
using System.Collections.Generic;


namespace JsonParser.Parse
{
    public class StructureTypeParse : IStructureTypeParse
    {
        public StructureType FillStructure(DeserializedJsonModel deserializeFile)
        {
            StructureType structureType = AddBaseInfo(deserializeFile);
            
            foreach (var node in deserializeFile.Node)
            {
                Dictionary<string, object> nodes = AddRegionTypeDocument(structureType, node);
                Dictionary<string, object> descriptionLanguage = AddDescriptionLanguage(structureType, node);

                nodes.Add("Description", descriptionLanguage);
                structureType.Nodes.Add(nodes);
            }

            return structureType;
        }

        private StructureType AddBaseInfo(DeserializedJsonModel deserializeFile)
        {
            StructureType structureType = new StructureType();
            structureType.DefualtLanguageCode = deserializeFile.DefualtLanguagecode;
            structureType.VocabularyType = deserializeFile.VocabularyType;

            structureType.Description = new BaseDescription();
            structureType.Description.Name = deserializeFile.Description.Name;
            structureType.Description.Oid = deserializeFile.Description.Oid;
            structureType.Description.Version = deserializeFile.Description.Version;

            structureType.Nodes = new List<object>();

            return structureType;
        }

        private Dictionary<string, object> AddRegionTypeDocument(StructureType structureType, KeyValuePair<string, NodeTitle> node)
        {
            Dictionary<string, object> nodes = new Dictionary<string, object>();

            structureType.Title = node.Key;
            nodes.Add("Title", structureType.Title);

            if (node.Value.FormType.Fields != null)
            {
                structureType.Link = node.Value.FormType.Fields.Link;
                nodes.Add("Link", structureType.Link);
            }

            return nodes;
        }

        private Dictionary<string, object> AddDescriptionLanguage(StructureType structureType, KeyValuePair<string, NodeTitle> node)
        {
            Dictionary<string, object> descriptionLanguage = new Dictionary<string, object>();
            
            List<object> languages = new List<object>();
            foreach (var language in node.Value.ObjectLanguage)
            {
                Dictionary<string, string> languagesDescription = new Dictionary<string, string>();

                structureType.Language = language.Key;
                structureType.Value = language.Value.LanguageValue;

                languagesDescription.Add("Languagecode", structureType.Language);
                languagesDescription.Add("Value", structureType.Value);

                languages.Add(languagesDescription);
            }
            descriptionLanguage.Add("Languages", languages);

            return descriptionLanguage;
        }
    }
}

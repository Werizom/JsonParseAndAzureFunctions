using Newtonsoft.Json;
using System.Collections.Generic;


namespace JsonParser.Models
{
    public class DeserializedJsonModel
    {
        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("default-language-code")]
        public string DefualtLanguagecode { get; set; }

        [JsonProperty("vocabulary-type")]
        public string VocabularyType { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]   // для структуры с Values
        public Dictionary<string, Dictionary<string, NodeTitle>> Value { get; set; }

        [JsonProperty("nodes", NullValueHandling = NullValueHandling.Ignore)]    // для структуры с Node
        public Dictionary<string, NodeTitle> Node { get; set; }

    }


    public class Description
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("oid")]
        public string Oid { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }


    public class NodeTitle
    {
        [JsonProperty("description")]
        public Dictionary<string, ValueLanguage> ObjectLanguage { get; set; } 

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]   // для структуры с Values
        public FormType FormType { get; set; }

        [JsonProperty("dependsOf", NullValueHandling = NullValueHandling.Ignore)]    // для ветки структуры Dependency
        public DependsOf DependsOf { get; set; }
    }


    public class ValueLanguage
    {
        [JsonProperty("value")]
        public string LanguageValue { get; set; }
    }


    public class FormType     // для структуры с Node
    {
        [JsonProperty("formType")]
        public Fields Fields { get; set; }
    }

    public class Fields     // для структуры с Node
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }


    public class DependsOf   // для структуры с Dependency
    {
        [JsonProperty("2.16.840.1.113883.3.989.5.1.2.2.1.13.1")]
        public List<string> KeysList { get; set; }
    }

}
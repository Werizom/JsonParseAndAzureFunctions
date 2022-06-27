using JsonParser.Menu.Interfaces;
using JsonParser.Models;
using JsonParser.Parse.Interface;


namespace JsonParser.Menu
{
    public class ParseStructureJson : IParseStructureJson
    {
        const string typeValueStructure = "value";
        const string typeNodeStructure = "structure";
        const string typeDependencyStructure = "dependency";

        private readonly IJsonFile jsonFile;
        private readonly IValueTypeParse valueTypeParse;
        private readonly IStructureTypeParse structureTypeParse;
        private readonly IDependencyTypeParse dependencyTypeParse;


        public ParseStructureJson(IJsonFile jsonFile, 
            IValueTypeParse valueTypeParse, 
            IStructureTypeParse structureTypeParse, 
            IDependencyTypeParse dependencyTypeParse)
        {
            this.jsonFile = jsonFile;
            this.valueTypeParse = valueTypeParse;
            this.structureTypeParse = structureTypeParse;
            this.dependencyTypeParse = dependencyTypeParse;
        }


        public BaseStructureJson Parsing(string inputJson)
        {
            var deserializeJson = jsonFile.GetJsonDeserializeJsonFile(inputJson);
            var modelStructure = ParsingTheSelectedStructure(deserializeJson);

            return modelStructure;
        }

        private BaseStructureJson ParsingTheSelectedStructure(DeserializedJsonModel deserializeJson)
        {
            var typeStructure = deserializeJson.VocabularyType;

            switch (typeStructure)
            {
                case typeValueStructure:
                    var valueType = valueTypeParse.FillStructure(deserializeJson);
                    return valueType;

                case typeNodeStructure:
                    var structureType = structureTypeParse.FillStructure(deserializeJson);
                    return structureType;

                case typeDependencyStructure:
                    var dependencyType = dependencyTypeParse.FillStructure(deserializeJson);
                    return dependencyType;

                default:
                    return null;
            }
        }

    }
}

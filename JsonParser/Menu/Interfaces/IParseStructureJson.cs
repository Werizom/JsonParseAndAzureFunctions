using JsonParser.Models;

namespace JsonParser.Menu.Interfaces
{
    public interface IParseStructureJson
    {
        public BaseStructureJson Parsing(string inputJson);
    }
}

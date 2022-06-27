using JsonParseAndAzureFunctions.Services.Interfaces;
using JsonParser.Menu.Interfaces;


namespace JsonParseAndAzureFunctions.Services
{
    public class ParseJsonFile : IParseJsonFile
    {
        private readonly IParseStructureJson parseMenu;

        public ParseJsonFile(IParseStructureJson parseMenu)
        {
            this.parseMenu = parseMenu;
        }


        public void ParseStart(string inputBlob)
        {
            parseMenu.Parsing(inputBlob);
        }
    }
}

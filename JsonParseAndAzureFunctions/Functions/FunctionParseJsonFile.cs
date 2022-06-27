using JsonParser.Menu.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;


namespace JsonParseAndAzureFunctions.Functions
{

    [StorageAccount("BlobConnectionString")]
    public class FunctionParseJsonFile
    {
        private readonly IParseStructureJson parseMenu;

        public FunctionParseJsonFile(IParseStructureJson parseMenu)
        {
            this.parseMenu = parseMenu;
        }


        [FunctionName("FunctionParseJsonFile")]
        public void ParseJson([BlobTrigger("folder-json/{name}")]string inputJson,
            string name, 
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name}");

            try
            {
                var structureJson = parseMenu.Parsing(inputJson);
                //var serialize = JsonConvert.SerializeObject(structureJson); // the serialization method is contained in "parseMenu"
                log.LogInformation("SUCCESS!!!");
            }
            catch (Exception e)
            {
                log.LogError("ERROR: " + e.Message);
            }
        }

    }
}

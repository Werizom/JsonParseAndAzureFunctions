using JsonParser.Models;
using JsonParser.Parse.Interface;
using Newtonsoft.Json;
using System;


namespace JsonParser.Parse
{
    public class JsonFile: IJsonFile
    {

        public DeserializedJsonModel GetJsonDeserializeJsonFile(string readJson)
        {
            var deserialize = DeserializeJsonFile(readJson);

            return deserialize;
        }

        public String SerializeObject(object seriazableObject)
        {
            var serialize = JsonConvert.SerializeObject(seriazableObject);

            return serialize;
        }

        private DeserializedJsonModel DeserializeJsonFile(string readJson)
        {
            try
            {
                var deserialize = JsonConvert.DeserializeObject<DeserializedJsonModel>(readJson);
                return deserialize;
            }
            catch (Exception e)
            {
                throw new Exception("Json Model isn't valid: " + e.Message);
            }
        }

    }
}
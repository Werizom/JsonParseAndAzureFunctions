using JsonParser.Models;
using System;


namespace JsonParser.Parse.Interface
{
    public interface IJsonFile
    {
       public DeserializedJsonModel GetJsonDeserializeJsonFile(string readJson);
       public String SerializeObject(object seriazableObject);
    }
}

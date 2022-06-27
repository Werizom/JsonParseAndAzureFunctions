using JsonParser.Models;


namespace JsonParser.Parse.Interface
{
    public interface IValueTypeParse
    {
        public ValuesType FillStructure(DeserializedJsonModel deserializeFile);
    }
}

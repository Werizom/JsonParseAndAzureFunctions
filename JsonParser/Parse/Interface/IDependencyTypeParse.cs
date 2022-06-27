using JsonParser.Models;

namespace JsonParser.Parse.Interface
{
    public interface IDependencyTypeParse
    {
        public DependencyType FillStructure(DeserializedJsonModel deserializeFile);
    }
}

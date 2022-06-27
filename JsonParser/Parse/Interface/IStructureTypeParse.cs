using JsonParser.Models;

namespace JsonParser.Parse.Interface
{
    public interface IStructureTypeParse
    {
        public StructureType FillStructure(DeserializedJsonModel deserializeFile);
    }
}

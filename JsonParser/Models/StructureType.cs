using System;
using System.Collections.Generic;

namespace JsonParser.Models
{
    [Serializable]
    public class StructureType : BaseStructureJson
    {
        [NonSerialized]
        public string Title;

        [NonSerialized]
        public string Link;

        [NonSerialized]
        public string Language;
        
        [NonSerialized]
        public string Value;

        public List<object> Nodes { get; set; }
       
    }
}

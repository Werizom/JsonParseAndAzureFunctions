using System;
using System.Collections.Generic;

namespace JsonParser.Models
{
    [Serializable]
    public class ValuesType : BaseStructureJson
    {
        [NonSerialized]
        public string ValueOid;

        [NonSerialized]
        public string NodeTitle;

        [NonSerialized]
        public string Language;

        [NonSerialized]
        public string ValueLanguage;

        public List<object> Values { get; set; }
    }
}



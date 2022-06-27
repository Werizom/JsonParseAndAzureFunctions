using System;
using System.Collections.Generic;

namespace JsonParser.Models
{
    [Serializable]
    public class DependencyType : BaseStructureJson
    {
        [NonSerialized]
        public string ValueOid;

        [NonSerialized]
        public string NodeTitle;

        [NonSerialized]
        public string Language;

        [NonSerialized]
        public string ValueLanguage;

        [NonSerialized]
        public string NumberDependsOf;

        public List<object> Values { get; set; }
    }
}

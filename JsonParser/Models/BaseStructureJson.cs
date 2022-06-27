using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParser.Models
{
    public class BaseStructureJson
    {
        public BaseDescription Description { get; set; }
        public string DefualtLanguageCode { get; set; } 
        public string VocabularyType { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParser.Models
{
    public class BaseDescription
    {
        public string Name { get; set; }
        public string Oid { get; set; }
        public string Version { get; set; }
    }
}

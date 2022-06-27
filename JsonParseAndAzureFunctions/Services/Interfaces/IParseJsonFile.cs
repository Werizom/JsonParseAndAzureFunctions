using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParseAndAzureFunctions.Services.Interfaces
{
    public interface IParseJsonFile
    {
        void ParseStart(string inputBlob);
    }
}

using JsonParseAndAzureFunctions.Services;
using JsonParseAndAzureFunctions.Services.Interfaces;
using JsonParser.Menu;
using JsonParser.Menu.Interfaces;
using JsonParser.Parse;
using JsonParser.Parse.Interface;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(JsonParseAndAzureFunctions.HostBuilder.Startup))]
namespace JsonParseAndAzureFunctions.HostBuilder
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddScoped<IParseJsonFile, ParseJsonFile>();
            builder.Services.AddScoped<IParseStructureJson, ParseStructureJson>();
            builder.Services.AddScoped<IJsonFile, JsonFile>();
            builder.Services.AddScoped<IValueTypeParse, ValueTypeParse>();
            builder.Services.AddScoped<IStructureTypeParse, StructureTypeParse>();
            builder.Services.AddScoped<IDependencyTypeParse, DependencyTypeParse>();

        }
    }
}

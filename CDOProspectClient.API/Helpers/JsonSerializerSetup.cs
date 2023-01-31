using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CDOProspectClient.API.Helpers;

public static class JsonSerializerSetup
{
    public static string Serialize(object value)
    {
        var json = JsonConvert.SerializeObject(
            value,
            new JsonSerializerSettings 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        
        return json;
    }

    public static string SerializePreserve(object value)
    {
        var json = JsonConvert.SerializeObject(
            value,
            new JsonSerializerSettings 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );
        
        return json;
    }
}
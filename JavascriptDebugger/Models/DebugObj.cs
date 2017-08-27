
using Newtonsoft.Json;

public class DebugObj
{
    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("value")]
    public string value { get; set; }

    [JsonProperty("isGlobal")]
    public bool isGlobal { get; set; }
}

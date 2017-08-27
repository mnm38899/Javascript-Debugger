using Newtonsoft.Json.Linq;
using System;
public class BasicRequest
{
    public int id { get; set; }
    public string time { get; set; }
    public string data { get; set; }

    public BasicRequest(string json)
    {
        try
        {
            JToken token = JObject.Parse(json);
            id = (int)token.SelectToken("id");
            time = (string)token.SelectToken("time");
            data = (string)token.SelectToken("data");
        }
        catch(Exception e)
        {
            Console.WriteLine("error - " + e.Message);
            Console.WriteLine(json);
        }


    }
}

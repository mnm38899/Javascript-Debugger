using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
public class TrackList
{
    public List<DebugObj> list { get; set; }


    public TrackList(string json)
    {
        //JToken token = JObject.Parse(json);
        if(json==null)
        {
            return;
        }
        list = JsonConvert.DeserializeObject<List<DebugObj>>(json);

    }
}

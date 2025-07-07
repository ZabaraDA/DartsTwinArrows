using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class JsonReaderService
{
    public static T ReadJsonInResources<T>(string path) where T : class
    {
        TextAsset text = Resources.Load<TextAsset>(path);
        var jsonContainer = JsonConvert.DeserializeObject<JsonContainer<T>>(text.text);
        return jsonContainer.Value;
    }
}

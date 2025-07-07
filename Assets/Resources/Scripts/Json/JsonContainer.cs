using Newtonsoft.Json;
public class JsonContainer<T> where T : class
{
    [JsonProperty("value")]
    public T Value { get; set; }
}

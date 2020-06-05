using Newtonsoft.Json;

namespace MarvelsoftConsole.models
{
    /// <summary>
    /// Model class for JSON data used by NewtonJson library.
    /// </summary>
    public class JsonProduct
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace DBClass
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ki")]
        public string Ki { get; set; }

        [JsonPropertyName("maxKi")]
        public string MaxKi { get; set; }

        [JsonPropertyName("race")]
        public string Race { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

    }
    
    public class Root
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
}


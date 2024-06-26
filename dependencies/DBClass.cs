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
    public class Links
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }
    public class Root
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }


        [JsonPropertyName("links")]
        public Links Links { get; set; }
    }
}


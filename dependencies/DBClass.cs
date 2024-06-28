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
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }  
    public class Root
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }


    public class Guerreros{
        private int id; private string name;private string race;private string description;private string ki;private string maxki;private int velocidad;private int destreza;private int fuerza;private int armadura;private int salud;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Race { get => race; set => race = value; }
        public string Description { get => description; set => description = value; }
        public string Ki { get => ki; set => ki = value; }
        public string Maxki { get => maxki; set => maxki = value; }
        
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }
    }
}

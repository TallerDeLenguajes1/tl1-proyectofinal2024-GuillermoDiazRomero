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


        //Propiedades agregadas manualmente para ahorra recursos
        /*          PREGUNTAR DE TODAS FORMAS       */	
        private int velocidad; private int destreza; private int fuerza; private int nivel; private int armadura; private int salud;
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; } //Esta variable Nivel será utilizada para el guardado de partida (ronda del torneo)
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }
    }
    
    public class Root
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
}

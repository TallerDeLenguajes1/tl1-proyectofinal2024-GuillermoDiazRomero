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


    public class Guerreros
    {
        private int id; private string name; private string race; private string description; private int salud; private string ki; private string maxki; private string status; private int kiCombate; private int velocidad; private int destreza; private int fuerza; private int armadura; 

        private bool ataqueEspecial1;
        private bool ataqueEspecial2;
        private bool eleccionUsuario;
        
        public bool EleccionUsuario { get => eleccionUsuario; set => eleccionUsuario = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Race { get => race; set => race = value; }
        public string Description { get => description; set => description = value; }
        public int Salud { get => salud; set => salud = value; }
        public string Ki { get => ki; set => ki = value; }
        public string Maxki { get => maxki; set => maxki = value; }
        public string Status { get => status; set => status = value; }
        public int KiCombate { get => kiCombate; set => kiCombate = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public bool AtaqueEspecial1 { get => ataqueEspecial1; set => ataqueEspecial1 = value; }
        public bool AtaqueEspecial2 { get => ataqueEspecial2; set => ataqueEspecial2 = value; }

    }
}

namespace CrearPersonajes
{
    
    public class FabricaDePersonajes
    {
        //Datos de los personajes

        private string Nombre;private string Apellido;private string Apodo;private DateTime FechaNac;private int Edad;
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Apellido1 { get => Apellido; set => Apellido = value; }
        public string Apodo1 { get => Apodo; set => Apodo = value; }

        //Características
        //Hacer la conversión del enum
        private string Raza;private int velocidad;private int destreza;private int fuerza;private int nivel;private int armadura;private int salud;private int ki;
        public string Raza1 { get => Raza; set => Raza = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }
        public int Ki { get => ki; set => ki = value; }
    }  
}

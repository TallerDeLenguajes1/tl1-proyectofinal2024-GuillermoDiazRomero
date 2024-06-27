using CrearPersonajes;
using DBClass;
using System.Text.Json;
using Mensajes;



internal class Program
{
    public static void Main(string[] args)
    {
        //Seccion de aleatoriedad de personajes
        string respaldoDB = @"resources\backup\Respaldo.JSON"; //Utilizo siempre el archivo de respaldo de la API para que el juego pueda correr sin problemas
        var texto = File.ReadAllText(respaldoDB); //Leo la API
        var datosDB = JsonSerializer.Deserialize<Root>(texto); //Doy formato legible a la API
        List<Item> Peleadores = new List<Item>();


        int numeroAleatorio;
        Random rand = new Random();
        //Utilizo HashSet ya que me permite almacenar aleatorios único (sin repetirse) y es más eficiente para la memoria
        HashSet<int> noRepetidos = new HashSet<int>();

        //Declaración de variables para el control del tamaño del torneo
        int tamaTorneo; bool respuesta; string ingresado;
        do
        {
            MensajesTerminal.TextoTiempo("\nIngrese el tamaño del torneo (4, 8 o 16): ", 1000,0);
            ingresado = Console.ReadLine();
            respuesta = int.TryParse(ingresado, out tamaTorneo);
        } while (!respuesta || (tamaTorneo != 4 && tamaTorneo != 8 && tamaTorneo != 16));

        while (noRepetidos.Count < tamaTorneo)
        { //Utilizo el .Count para saber la longitud del HashSet
            numeroAleatorio = rand.Next(1, 58);
            if (!noRepetidos.Contains(numeroAleatorio)) //Con .Contains pregunto si el numero aleatorio esta contenido adentro del HashSet
            {
                noRepetidos.Add(numeroAleatorio); //Añado el numero al HashSet
            }
        }
        for (int i = 0; i < tamaTorneo; i++)
        {
            var temp = datosDB.Items[noRepetidos.ElementAt(i)]; //Uso .ElementAt para usar el numero guardado en la posisión "i" del HashSet
            switch (temp.Race)
            {
                case "Human": temp.Velocidad = 3; temp.Destreza = 3; temp.Armadura = 2; temp.Fuerza = 4; temp.Armadura = 3; temp.Salud = 200; break;
                case "Saiyan": temp.Velocidad = 6; temp.Destreza = 6; temp.Armadura = 7; temp.Fuerza = 7; temp.Salud = 200; break;
                case "Namekian": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 6; temp.Fuerza = 5; temp.Salud = 200; break;
                case "Majin": temp.Velocidad = 4; temp.Destreza = 5; temp.Armadura = 3; temp.Fuerza = 5; temp.Salud = 200; break;
                case "Frieza Race": temp.Velocidad = 6; temp.Destreza = 6; temp.Armadura = 8; temp.Fuerza = 6; temp.Salud = 200; break;
                case "Android": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 5; temp.Fuerza = 6; temp.Salud = 200; break;
                case "Jiren Race": temp.Velocidad = 8; temp.Destreza = 7; temp.Armadura = 8; temp.Fuerza = 8; temp.Salud = 200; break;
                case "God": temp.Velocidad = 10; temp.Destreza = 10; temp.Armadura = 10; temp.Fuerza = 10; temp.Salud = 200; break;
                case "Angel": temp.Velocidad = 20; temp.Destreza = 20; temp.Armadura = 20; temp.Fuerza = 20; temp.Salud = 200; break;
                case "Evil": temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 6; temp.Fuerza = 6; temp.Salud = 200; break;
                case "Nucleico": temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 5; temp.Fuerza = 4; temp.Salud = 200; break; //Gran-Kaio-samas 
                case "Nucleico venigno": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 2; temp.Fuerza = 4; temp.Salud = 200; break; //Sumpremos Kaio-samas
                case "Unknown": temp.Velocidad = 100; temp.Destreza = 100; temp.Armadura = 200; temp.Fuerza = 10000; temp.Salud = 200; break; //Zen Oh sama, personaje más fuerte del juego con una probabilidad de 1/58 de que salga
            }
            Peleadores.Add(temp);
        }
        Console.WriteLine("Mostrando Personajes que tocaron");
        foreach (Item item in Peleadores){
            MensajesTerminal.mostrarPjs(item);
        }
        Console.ReadKey();
    }
}




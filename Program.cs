using CrearPersonajes;
using DBAPI;
using DBClass;
using System.Text.Json;


/*
internal class BackupAPI
{
    private static void Main(string[] args)
    {
        ConsumiendoAPI probando = new ConsumiendoAPI();
        probando.Probando();

        Console.WriteLine("Probando backup");
        Console.ReadLine();
    }
}
*/

internal class Program
{
    public static void Main(string[] args)
    {

        /*ConsumiendoAPI probando = new ConsumiendoAPI();
        probando.Probando();

        Console.WriteLine("Probando backup");
        Console.ReadLine();
*/

        //Seccion de aleatoriedad de personajes
        string respaldoDB = @"resources\backup\Respaldo.JSON";
        var texto = File.ReadAllText(respaldoDB);
        var datosDB = JsonSerializer.Deserialize<Root>(texto);

        List<Item> Peleadores = new List<Item>();

        int numeroAleatorio;
        Random rand = new Random();
        //Utilizo HashSet ya que me permite almacenar aleatorios único (sin repetirse) y es más eficiente para la memoria
        HashSet<int> noRepetidos = new HashSet<int>();

        int tamaTorneo;
        bool respuesta;
        string ingresado;
        do
        {
            Console.Write("\nIngrese el tamaño del torneo (4, 8 o 16): ");
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
            Peleadores.Add(temp);
        }
        Console.WriteLine("Mostrando Personajes que tocaron");
        foreach (Item item in Peleadores)
        {
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("|         Id:" + item.Id);
            Console.WriteLine("|         Nombre: " + item.Name);
            Console.WriteLine("|         Raza: " + item.Race);
            Console.WriteLine("|         Ki: " + item.Ki);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("\n");
        }


        Console.ReadKey();

    }
}







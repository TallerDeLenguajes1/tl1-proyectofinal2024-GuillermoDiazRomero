using CrearPersonajes;
using DBAPI;


internal class Program
{
    private static void Main(string[] args)
    {
        ConsumiendoAPI probando = new ConsumiendoAPI();
        probando.Probando();

        Console.WriteLine("Probando backup");
        Console.ReadLine();
    }
}
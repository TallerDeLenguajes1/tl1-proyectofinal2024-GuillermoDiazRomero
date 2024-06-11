using CrearPersonajes;



//Comienzo del juego, Parte en proceso
Console.WriteLine("Bienvenido a DRAGONBALLFIGHTERSTERMINAL");

//Declaracion de variables comunes
string respuesta;
int numero;
bool resultado;
do
{
    Console.WriteLine("Menú de Razas:");
    Console.WriteLine("1.Saiyajin");
    Console.WriteLine("2.Terrícola");
    Console.WriteLine("3.Androide");
    Console.WriteLine("4.Aleatorio");
    Console.WriteLine("Seleccione una raza: ");

    respuesta = Console.ReadLine();
    resultado = int.TryParse(respuesta, out numero);
} while (!resultado || numero < 1 || numero > 4);


using Mensajes;
using StartGame;



internal class Program
{
    public static void Main(string[] args)
    {
        GameStart.GameRun();
        

    }
}


/*
internal class Program
{

    private static string[] opcionesMenu = { "4 Rondas", "8 Rondas", "16 Rondas", "Salir del Juego" };

    private static int x;
    private static int y;

    private static void Main(string[] args)
    {
        Console.Clear(); //Limpia la consola para que se muestre solamente el juego
        bool estaSeleccionando = true;
        int indiceSeleccionado = 0;
        int tamaToreno=0; //Seteo en 0 para preguntar adentro del bucle del menu para usar operador ternario

        ConsoleKeyInfo tecla;
        Console.CursorVisible = false;
        MensajesTerminal.TituloJuego();

        MensajesTerminal.TextoTiempo("Seleccione un tamaño para el Torneo:",1000,1);

        x = Console.CursorLeft;
        y = Console.CursorTop;

        Menu(indiceSeleccionado);

        while (estaSeleccionando)
        {
            while ((tecla = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                switch (tecla.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (indiceSeleccionado == (opcionesMenu.Length - 1))
                            continue;
                        indiceSeleccionado++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (indiceSeleccionado == 0)
                            continue;
                        indiceSeleccionado--;
                        break;
                    default:
                        break;
                }

                Console.CursorLeft = x;
                Console.CursorTop = y;

                Menu(indiceSeleccionado);
            }
            
            switch (indiceSeleccionado)
            {
                case 0: 
                    tamaToreno = (tamaToreno == 0) ? 4 : tamaToreno; 
                    goto case 1; //goto sirve para que el case se cumpla y no use break para que siga con el resto del codigo
                case 1:
                    tamaToreno = (tamaToreno == 0) ? 8 : tamaToreno;
                    goto case 2;
                case 2:
                    tamaToreno = (tamaToreno == 0) ? 16 : tamaToreno;
                    Console.WriteLine("");
                    MensajesTerminal.TextoTiempo ("Preparando luchadores",1000,0);
                    MensajesTerminal.TextoTiempo("......",5000,1);
                    estaSeleccionando = false;
                    Thread.Sleep(2000);
                    break;
                case 3:
                    MensajesTerminal.TextoTiempo("Mr. Satan si se la banca...",2500,0);
                    Thread.Sleep (1000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no reconocida");
                    break;
            }
        }
        Console.WriteLine("Tamaño del toreno: "+tamaToreno);
    }

    private static void Menu(int indiceSeleccionado)
    {
        int destacado = 0;

        Array.ForEach(opcionesMenu, element =>
        {
            if (destacado == indiceSeleccionado)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" > " + element);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.WriteLine(element);
            }
            destacado++;
        });
    }
}
*/
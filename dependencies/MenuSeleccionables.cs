using Mensajes;
namespace MenusDelJuego
{
    public static class Menus
    {
        public static string[] opcionesMenu = { "4 Rondas", "8 Rondas", "16 Rondas", "Salir del Juego" };
        public static int x;
        public static int y;
        public static int MenuEsqueleto()
        {
            Console.Clear(); //Limpia la consola para que se muestre solamente el juego
            bool estaSeleccionando = true;
            int indiceSeleccionado = 0;
            int tamaToreno = 0; //Seteo en 0 para preguntar adentro del bucle del menu para usar operador ternario

            ConsoleKeyInfo tecla;
            Console.CursorVisible = false;
            MensajesTerminal.TituloJuego();


            Console.ForegroundColor = ConsoleColor.White;
            MensajesTerminal.CentradoSimple("Seleccione un tamaño para el Torneo:", 100, 1);

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
                        MensajesTerminal.TextoTiempo("      Preparando luchadores", 1000, 0);
                        MensajesTerminal.TextoTiempo("......", 5000, 1);
                        estaSeleccionando = false;
                        Thread.Sleep(2000);
                        break;
                    case 3:
                        MensajesTerminal.TextoTiempo("Mr. Satan si se la banca...", 2500, 0);
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            return tamaToreno;
        }





        private static void Menu(int indiceSeleccionado)
        {
            int destacado = 0;

            Array.ForEach(opcionesMenu, element =>
            {
                if (destacado == indiceSeleccionado)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                // Cálculo del padding para centrar el texto
                int padding = (Console.WindowWidth - element.Length - 3) / 2; // 3 extra characters for " > "
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine((destacado == indiceSeleccionado ? " > " : "   ") + element + (destacado == indiceSeleccionado ? " < " : "   "));

                if (destacado == indiceSeleccionado)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                destacado++;
            });
        }


    }
}































/* Menu para
internal class Program
{

    private static string[] opcionesMenu = {"Opcion 1", "Opcion 2", "Salir"};

    private static int x;
    private static int y;

    private static void Main(string[] args)
    {
        bool estaSeleccionando = true;
        int indiceSeleccionado = 0;

        ConsoleKeyInfo tecla;
        Console.CursorVisible = false;

        Console.WriteLine("Seleccione una opción de menú: \n");

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
                case 2:
                    System.Console.WriteLine("Chao");
                    estaSeleccionando = false;
                    break;
                default:
                    System.Console.WriteLine("asd");
                    break;
            }
        }
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
}*/
using Mensajes;
using CombateZ;
namespace MenusDelJuego
{
    public static class Menus
    {
        public static int x;
        public static int y;
        public static int MenuTorneo(string[] opcionesMenu, int tipo)
        {
            bool estaSeleccionando = true;
            int indiceSeleccionado = 0;
            int tamaToreno = 0; //Seteo en 0 para preguntar adentro del bucle del menu para usar operador ternario
            int tiempo = 0;

            Console.Clear(); //Limpia la consola para que se muestre solamente el juego  

            if (tipo == 1)
            {
                tiempo = 915;
            }


            ConsoleKeyInfo tecla;
            Console.CursorVisible = false;


            MensajesTerminal.TituloJuego(tiempo);
            Console.ForegroundColor = ConsoleColor.White;
            MensajesTerminal.CentradoSimple("Seleccione un tamaño para el Torneo:", 100, 1);
            //Ubicarlo a esta altura me permite tomar como tope todo lo que se esta mostrando en la terminal hasta este momento
            x = Console.CursorLeft;
            y = Console.CursorTop;

            MenuCentrado(indiceSeleccionado, opcionesMenu,"Listado");

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

                    MenuCentrado(indiceSeleccionado, opcionesMenu,"Listado");
                }

                //Este switch es importante ya que me permite seleccionar el tamaño del torneo y manejo 2 parametros (tamaTorneo y estaSeleccionando)
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
                        MensajesTerminal.TextoTiempo("\n      Preparando luchadores", 1000, 0);
                        MensajesTerminal.TextoTiempo("......", 3500, 1);
                        estaSeleccionando = false;
                        Thread.Sleep(2000);
                        break;
                    case 3:
                        estaSeleccionando = false;
                        break;
                    case 4:
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

        public static int MenuGuerreros(string[] opcionesMenu, string tipo)
        {

            bool estaSeleccionando = true;
            int indiceSeleccionado = 0;

            ConsoleKeyInfo tecla;
            Console.CursorVisible = false;

            //Defino si quiero mostrar un titulo o no antes de borrar el textp de la consola
            if (tipo == "Listado")
            {
                Console.Clear(); //Limpia la consola para que se muestre solamente el juego
                Console.WriteLine();
                MensajesTerminal.TextoTiempo("  \uD83D\uDCAA\uD83D\uDCAA\uD83D\uDCAA\uD83D\uDCAA Listado de Guerreros \uD83D\uDCAA\uD83D\uDCAA\uD83D\uDCAA\uD83D\uDCAA", 100, 1);

            }
            else if (tipo == "Combate")
            {
                Console.WriteLine();
                Console.WriteLine();
            }

            x = Console.CursorLeft;
            y = Console.CursorTop;

            if (tipo == "Listado")
            {
                MenuIzquierda(indiceSeleccionado, opcionesMenu);
            }
            else if (tipo == "Combate")
            {
                MenuCentrado(indiceSeleccionado, opcionesMenu, tipo);
            }

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
                    if (tipo == "Listado")
                    {
                        MenuIzquierda(indiceSeleccionado, opcionesMenu);
                    }
                    else if (tipo == "Combate")
                    {
                        MenuCentrado(indiceSeleccionado, opcionesMenu,tipo);
                    }

                }

                if (indiceSeleccionado >= 0 && indiceSeleccionado <= 16)
                {
                    estaSeleccionando = false;
                }

            }
            return indiceSeleccionado;
        }

        public static bool MenuDecision(string[] decision) //Creo esta función para evitar problemas de compatibilidad con el resto de menus
        {
            bool estaSeleccionando = true;
            int indiceSeleccionado = 0;

            ConsoleKeyInfo tecla;
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.White;
            x = Console.CursorLeft;
            y = Console.CursorTop;

            MenuIzquierda(indiceSeleccionado, decision);

            while (estaSeleccionando)
            {
                while ((tecla = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    switch (tecla.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (indiceSeleccionado == (decision.Length - 1))
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

                    MenuIzquierda(indiceSeleccionado, decision);
                }

                switch (indiceSeleccionado)
                {
                    case 0: return true;
                    case 1: return false;
                    default:
                        break;
                }
            }
            return false;
        }

        private static void MenuCentrado(int indiceSeleccionado, string[] opciones , string tipo)
        {
            int destacado = 0;

            Array.ForEach(opciones, element =>
            {
                if (destacado == indiceSeleccionado)
                {
                    if (tipo == "Listado"){
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (tipo == "Combate")
                    {
                        Console.ForegroundColor = InterfazCombate.seleccionPlayer;
                        
                    }
                }

                // Cálculo del padding para centrar el texto
                int padding = (Console.WindowWidth - element.Length - 3) / 2;
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine((destacado == indiceSeleccionado ? " > " : "   ") + element + (destacado == indiceSeleccionado ? " < " : "   "));

                if (destacado == indiceSeleccionado)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                destacado++;
            });
        }

        private static void MenuIzquierda(int indiceSeleccionado, string[] opciones)
        { //Menu traido desde un commit a causa de muchos errores durante la codeada
            int destacado = 0;

            Array.ForEach(opciones, element =>
            {
                if (destacado == indiceSeleccionado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
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
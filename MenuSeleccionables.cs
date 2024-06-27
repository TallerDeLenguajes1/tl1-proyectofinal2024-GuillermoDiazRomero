/*
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
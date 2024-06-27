using DBClass;
namespace Mensajes
{
    public static class MensajesTerminal
    {
        public static void TextoTiempo(string texto, int milisegundos, int tipo)
        {   //Uso el tipo para indicar si quiero que sea un texto con salto de linea (1) o sin salto de linea (0)
            if (tipo == 1)
            {
                foreach (char c in texto)
                {
                    Console.Write(c);
                    Thread.Sleep(milisegundos / texto.Length);
                }
                Console.WriteLine();
            }
            else if (tipo == 0){
                foreach (char c in texto)
                {
                    Console.Write(c);
                    Thread.Sleep(milisegundos / texto.Length);
                }
            }
            else{
                Console.WriteLine("Ingresó un tipo equivocado de texto");
            }
        }

        public static void mostrarPjs(Item item)
        {
            //Creo un arreglo de string con todos los datos que llegan desde item para poder obtener las longitudes de los datos que se muestran por pantalla para así poder centrarlos
            string[] lineas = {
                $"Id: {item.Id}",
                $"Nombre: {item.Name}",
                $"Raza: {item.Race}",
                $"Ki: {item.Ki}",
                $"Velocidad: {item.Velocidad}",
                $"Destreza: {item.Destreza}",
                $"Fuerza: {item.Fuerza}",
                $"Armadura: {item.Armadura}",
                $"Salud: {item.Salud}"
            };
            int ajusteDelAncho = 50;
            ColorTerminalRaza(item.Race);   //Invoco a la función que me permite cambiar los colores de la terminal segun la raza

            Console.WriteLine(new string('-', ajusteDelAncho));
            Console.WriteLine("|" + new string(' ', ajusteDelAncho - 2) + "|");
            foreach (var linea in lineas)
            {
                int padding = (ajusteDelAncho - 2 - linea.Length) / 2; //Calculo el padding que le dare al texto segun lo que ocupa con el ancho total de la terminal
                string paddedLinea = new string(' ', padding) + linea + new string(' ', ajusteDelAncho - 2 - linea.Length - padding);
                Console.WriteLine($"|{paddedLinea}|");
            }
            Console.WriteLine("|" + new string(' ', ajusteDelAncho - 2) + "|");
            Console.WriteLine(new string('-', ajusteDelAncho));
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White; //Me aseguro que el color de la consola siempre vuelva a blanco despues de mostrar los personajes
        }
        static void ColorTerminalRaza(string raza)
        {
            switch (raza)
            {
                case "Human": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Saiyan": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Namekian": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Majin": Console.ForegroundColor = ConsoleColor.Magenta; break;
                case "Frieza Race": Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case "Android": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Jiren Race": Console.ForegroundColor = ConsoleColor.Red; break;
                case "God": Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case "Angel": Console.ForegroundColor = ConsoleColor.White; break;
                case "Evil": Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "Nucleico": Console.ForegroundColor = ConsoleColor.Gray; break; //Gran-Kaio-samas
                case "Nucleico venigno": Console.ForegroundColor = ConsoleColor.Gray; break;  //Sumpremos Kaio-samas
                case "Unknown": Console.ForegroundColor = ConsoleColor.Blue; break;
            }
        }
    }
}

using System.Media;
using DBClass;
namespace Mensajes
{
    public static class MensajesTerminal
    {
        public static void TextoTiempo(string texto, int milisegundos, int tipo)
        {   //Uso el tipo para indicar si quiero que sea un texto con salto de linea (1) o sin salto de linea (0)
            if (tipo == 1) //pregunto antes de realizar el foreach para no hacer un mal uso de la memoria, ya que si fuera adentro el programa haria muchas comparaciones innecesarias
            {
                foreach (char c in texto)
                {
                    Console.Write(c);
                    Thread.Sleep(milisegundos / texto.Length);
                }
                Console.WriteLine();
            }
            else if (tipo == 0)
            {
                foreach (char c in texto)
                {
                    Console.Write(c);
                    Thread.Sleep(milisegundos / texto.Length);
                }
            }
            else
            {
                Console.WriteLine("Ingresó un tipo equivocado de texto");
            }
        }

        public static void CentradoSimple(string centrado, int tiempo, int tipo)
        {
            int anchoTerminal = Console.WindowWidth;
            int padding = (anchoTerminal - centrado.Length) / 2;
            centrado = new string(' ', padding) + centrado;
            TextoTiempo(centrado, tiempo, tipo);
        }
        public static void mostrarPjs(Guerreros item)
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
        public static ConsoleColor ColorTerminalRaza(string raza)
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
                case "Nucleico benigno": Console.ForegroundColor = ConsoleColor.Gray; break;  //Sumpremos Kaio-samas
                case "Unknown": Console.ForegroundColor = ConsoleColor.Blue; break;
            }
            return Console.ForegroundColor;
        }
        public static void TituloJuego(int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string[] letraASCII =
            [
            "<<<<<<>>>>>>------------------------------<<<<<<>>>>>>",
            @"|\         _ |)  ||",
            @"|/|`(|(|()|| |)(|||",
            @"      _|           ",
            "",
            @"  (`      | . _   |    _ |   ",
            @"  _)|)(||`|<|||(| |_L|(_ |<\/",
            @"    |          _|          / ",
            "<<<<<<>>>>>>------------------------------<<<<<<>>>>>>"
    ];

            int anchoTerminal = Console.WindowWidth;
            string centrado = "";

            foreach (string linea in letraASCII)
            {
                int padding = (anchoTerminal - linea.Length) / 2;
                centrado += new string(' ', padding) + linea + Environment.NewLine; //Enviroment.NewLine remplaza el uso de \r y \n y permite compatibilidad con distintos sistemas operativos
            }
            TextoTiempo(centrado, tiempo, 1);
        }

        public static void ErrorSalir()
        {
            TextoTiempo("Nos robaron el radar del dragon...", 1000, 1);
            TextoTiempo("Saliendo del juego...", 2000, 1);
            Environment.Exit(0);
        }

        public static void MostrarBloquesParalelos(Guerreros pjL, Guerreros pjR, ConsoleColor colorJ, ConsoleColor colorE)
        {
            int ajusteDelAncho = Console.WindowWidth;
            int anchoTotal = ajusteDelAncho / 2;
            //Defino los string adentro de esta función para poder crear el efecto de actualización de pantalla durante el combate
            string[] lineas1 = {
                $"Salud: {pjL.Salud}",
                $"Nombre: {pjL.Name}",
                $"Raza: {pjL.Race}",
                $"Ki: {pjL.KiCombate}",
                $"Velocidad: {pjL.Velocidad}",
                $"Fuerza: {pjL.Fuerza}",
                $"Armadura: {pjL.Armadura}",
            };
            string[] lineas2 = {
                $"Salud: {pjR.Salud}",
                $"Nombre: {pjR.Name}",
                $"Raza: {pjR.Race}",
                $"Ki: {pjR.KiCombate}",
                $"Velocidad: {pjR.Velocidad}",
                $"Fuerza: {pjR.Fuerza}",
                $"Armadura: {pjR.Armadura}",
            };
            Console.ForegroundColor = colorJ;
            Console.Write(new string('-', anchoTotal + 1));
            Console.ForegroundColor = colorE;
            Console.WriteLine(" " + new string('-', anchoTotal - 2));
            for (int i = 0; i < lineas1.Length; i++)
            {
                //Bloque Izquierda
                int padding1 = 1 + (anchoTotal - 2 - lineas1[i].Length) / 2;
                string paddedLinea1 = new string(' ', padding1) + lineas1[i] + new string(' ', anchoTotal - lineas1[i].Length - padding1) + "|";
                //Bloque Derecha
                int padding2 = (anchoTotal - 2 - lineas2[i].Length) / 2;
                string paddedLinea2 = "|" + new string(' ', padding2) + lineas2[i] + new string(' ', anchoTotal - 2 - lineas2[i].Length - padding2);
                Console.ForegroundColor = colorJ;
                Console.Write(paddedLinea1);
                Console.ForegroundColor = colorE;
                Console.WriteLine(" " + paddedLinea2);
            }
            Console.ForegroundColor = colorJ;
            Console.Write(new string('-', anchoTotal + 1));
            Console.ForegroundColor = colorE;
            Console.Write(" " + new string('-', anchoTotal - 2));
        }


        public static void Preparate()
        {
            Console.WriteLine("\n\n");
            TextoTiempo(@" ____,  ____,  ____,  ____,   ____, ____,   ____, ____,  ____, 
(-|__) (-|__) (-|_,  (-|__)  (-/_| (-|__)  (-/_| (-|    (-|_,  
 _|     _|  \, _|__,  _|     _/  |, _|  \, _/  |, _|,    _|__, 
(      (      (      (      (      (      (      (      (      
", 900, 1);
        }



        public static void CartelDerrota(int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string[] letraASCII =
            [
@"⠀⠀⠀⠀⠀⠀⠉⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⡟⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢡⠇⢸⣿⣿⣿⣿⠀⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢛⣿⣿⣿⣿⣿⡿⠁⠀⡞⠀⠸⣿⣿⣿⡏⠀⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⣸⣿⣿⣿⣿⠟⠀⠀⢰⠇⠀⠀⣿⣿⣿⠁⠀⡀⠀⠀⢸⣿⣿⣿⣿⣿⣿⡿⢋⠙⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⣿⣿⣿⣿⠏⠀⠀⠀⣿⠀⠀⠀⣿⣿⣿⣿⣷⣿⠷⠀⢸⣿⣿⣿⣿⣿⡿⢠⣟⢳⠘⣿⣿⣿⣿⣿⣿",
@"⠀⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠸⣿⣿⠃⠀⠀⠀⢀⡏⠀⢀⣴⣿⣿⠿⠛⣯⠀⠀⠀⣿⡿⢻⣿⣿⣿⠁⡜⣬⠁⠀⢹⣿⣿⣿⣿⣿",
@"⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣀⠀⣿⡏⠀⠀⠀⠀⢸⠁⣰⣾⠿⠋⠁⠀⠀⣿⠀⠀⠰⠟⠀⣸⣿⠟⠁⠀⠳⢾⡇⠀⢸⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠙⠛⠻⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡙⢿⠷⣿⣄⣀⢀⣀⢀⣼⡿⠛⣁⡀⠀⠀⠀⣀⡿⠀⠀⠀⠀⠀⡿⠃⠀⠀⠀⣴⣾⡇⠀⣼⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣷⡈⢧⡀⠹⡿⣦⣿⠻⠋⠻⣤⠭⠗⠒⢊⣉⣁⣤⣤⠄⠀⠀⠀⠁⠀⠀⠀⢰⡿⠉⣠⣾⣿⣿⣿⡟⣫⣵",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠙⠶⣗⡛⠀⠀⠀⠀⠤⠴⠒⠒⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡷⡾⠻⠿⠟⠋⠀⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⠉⠉⣸⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⠿⣷⠃⠀⠀⠀⠀⢠⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⠿⠿⠟⠛⠛⠋⠉⠉⠉⠉⠉⠉⠉⠉⠉⠛⠛⠻⣆⣼⣃⡤⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣾⣿⡷⡆⠀⠀⠀⣸⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣦⡀⠉⠁⠀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡾⠛⢯⣛⣿⠇⠀⠀⢠⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⡀⠀⠀⠈⠙⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡴⠫⡴⠂⠀⠉⡄⠀⠀⠀⣸⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⠶⠋⠉⠀⠀⠀⠀⠉⠉⠓⠦⣄⣀⣨⠷⢦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡴⠋⢤⠍⠁⠀⠀⠀⡇⠀⠀⢠⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣀⠀⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣇⠀⠀⠙⠳⣤⡀⠀⠀⠀⣀⣴⣞⠁⢠⢤⠏⠀⠀⠀⠀⠀⣇⣠⣶⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⠟⠁⡇⠈⠙⣾⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡴⠋⠀⠀⠀⠀⠀⠉⢻⠛⠋⠉⢹⣿⣷⣤⣨⣀⣀⠀⣀⣤⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡴⠋⢀⣤⡶⢤⣀⡋⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠁⠀⠀⠀⠀⠀⠀⠀⠸⡇⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⡏⠀⡴⠛⠁⡇⠀⠀⣹⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡏⣠⢾⡁⠀⠀⢱⡖⢶⡟⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡆⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⢠⡇⡞⠁⠰⠴⠯⣽⣄⣙⡇⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣧⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁",
@"⠀⠀⠀⠀⠀⠀⠀⣠⣼⡇⠀⣴⠇⠀⠀⠀⠀⠀⠈⣿⣇⠠⠖⠒⠒⠶⠤⠄⠀⣼⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⠀⠀",
@"⠀⠀⠀⠀⠀⢠⣾⣿⡿⠀⠀⢻⠆⠀⠛⠉⠉⠙⣾⠁⢈⣽⣶⣦⣤⣤⣤⣤⣾⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣧⠀⠀⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⠀⠀⠀",
@"⠀⠀⠀⠀⢀⣿⣿⣿⠃⠀⠀⢻⡄⣀⣠⣤⡤⠶⣣⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠠⠀⠀⠀⠀⠀⠈⢷⡀⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀",
@"⠀⠀⠀⠀⢸⣿⣿⠇⠀⠀⠀⠀⠀⠀⣺⣿⣷⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⢸⡄⠀⠀⠀⠀⠀⠈⢻⡄⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀⡀",
@"⠀⠀⠀⠀⣿⣿⣿⡀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⡈⣧⠀⠀⠀⠀⠀⠀⠀⠹⣶⣄⠀⠀⠈⢿⣿⣿⣿⡟⠁⠀⠀⠀⠀⡐⠀",
@"⠀⠀⠀⠀⣿⣿⣿⣷⣄⣀⣀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⢷⢸⣆⠀⠀⠀⠀⠀⠀⠀⠙⢿⣦⣀⠀⠀⠙⣿⠏⠀⠀⠀⢀⣀⠔⠀⠀"
    ];

            int anchoTerminal = Console.WindowWidth;
            string centrado = "";


            foreach (string linea in letraASCII)
            {
                int padding = (anchoTerminal - linea.Length) / 2;
                centrado += new string(' ', padding) + linea + Environment.NewLine; //Enviroment.NewLine remplaza el uso de \r y \n y permite compatibilidad con distintos sistemas operativos
            }


            TextoTiempo(centrado, tiempo, 1);
            Console.ForegroundColor = ConsoleColor.White;
            SoundPlayer player = new SoundPlayer(FraseDerrota);
            player.Play();
            Thread.Sleep(4500);
        }

        public static void CartelVictoria(int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            SoundPlayer player = new SoundPlayer(FraseVictoria);
            player.Play();
            Thread.Sleep(10250);

            string[] letraASCII =
            [
@"⠀⠀⠀⠀⠉⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⡀⠈⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⢠⣄⠀⠀⠀⠀⠀⠀⠀⠀",
@"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠁⣿⣿⣿⣿⣿⣿⣿⠟⠁⡞⠙⣿⣿⣿⣿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠘⣇⠉⠳⠦⣀⠀⠀⠀⠀",
@"⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⢸⣿⣿⣿⣿⣿⠟⠁⠀⡼⠀⢀⣿⣿⣿⠇⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡀⢸⡄⠀⠀⠀⠉⠓⠦⠄",
@"⠀⠀⠉⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣦⠀⠀⠀⣼⣿⣿⣿⠟⠁⠀⠀⣸⣣⣴⣾⣿⣿⣿⣶⣄⠀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⠈⢻⣆⠀⠀⣿⣿⣿⠋⠀⠀⢀⣴⣿⡿⢻⣿⣿⡏⠙⠻⠿⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⡴⠒⠶⣿⡄⠀⣿⡿⠁⠀⢀⣴⣿⡟⠉⠀⢀⣿⠟⢧⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢸⠁⠀⠀⠀⠉⠠⠿⠀⠀⣠⠞⠋⠈⠁⠀⠀⠈⠁⠀⢸⡄⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⡀⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⣁⣤⡄⠀⠀⠀⠀⣀⡀⠀⢸⠇⠀⠀⣠⣿⠿⣿⣿⣿⣿⡿⠛⠉⢻⡟⠿⣿⡟⠁⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣶⣾⣿⠿⣿⣿⣿⣿⣿⠷⠼⡄⠀⠀⢰⣿⡆⠀⠀⠀⠈⠿⠋⠀⠀⠀⢸⡋⠙⢦⣼⠀⠀⠐⠛⠉⣼⣿⣿⣿⠛⢠⡶⠀⠘⣷⠀⠈⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠲⢿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠹⣿⣿⣿⣿⡀⠀⣷⠀⢀⣀⠁⠀⠀⠀⠀⠀⠀⢴⣲⣾⣽⡟⠳⡀⠀⠙⣗⢦⡀⠀⣸⠟⠁⠟⠁⢠⣿⡀⠀⠀⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠈⠉⠛⠿⣿⣿⠃⠸⡄⠀⡏⢻⣿⣿⡇⠀⠘⢏⣩⣴⠾⠃⠀⠀⠰⠖⠚⠉⠁⠀⡾⢧⡀⠈⢦⡀⠘⢦⡙⢾⡁⠀⠀⠀⠀⣟⠀⠁⠀⠀⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⢠⡟⡇⠀⠀⡇⠀⡇⠀⣿⣿⣿⠐⠛⠉⠹⣄⠒⣶⠄⠀⠀⣀⡤⠚⠙⣆⢳⡀⠉⢧⡀⠙⢦⡀⠙⢌⠳⡀⠀⠀⠀⠻⢙⡦⠀⢀⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⢼⠁⣿⠀⠀⡇⠀⡇⠀⢸⣿⣿⡄⠀⠀⠀⠘⠋⣁⡠⠴⢛⣁⣤⣴⣶⣿⡀⠙⡄⠀⠙⢦⠀⠙⣆⠈⢳⣳⡄⠀⡠⢴⡞⠁⢀⣾⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⡟⠀⣿⠀⠘⡅⠀⡇⠀⢸⡏⠻⡇⠀⠀⠀⠀⠀⣏⠀⠚⢻⣿⣿⣿⡿⠛⣇⠀⠙⣆⠀⠈⢳⡀⠈⠓⠦⠙⣷⣴⠟⠉⢀⣴⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⢸⠀⠀⡇⠀⢳⠀⠀⣇⣠⡷⣄⠀⠀⠀⠀⠘⢷⣶⣿⣿⣿⠟⠁⠀⢸⠀⠀⠸⣆⠀⠀⠙⣄⠀⠀⠀⠈⢳⡶⠶⢿⠿⠿⠛⠉⠙⡇⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⢸⡀⠀⡇⠀⣺⣀⣀⣾⠁⠀⠈⣇⠀⠀⠀⠀⠈⢿⣿⡿⡏⠀⠀⠀⢸⠀⠀⠀⠹⡄⠀⠀⣮⢳⡀⠀⢆⠀⢷⠀⠀⠀⠀⠀⠀⢠⡇⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⢸⣆⣠⠟⠋⠀⠀⠀⢸⡄⠀⠀⢸⢦⣀⠀⠀⠀⠀⠹⣄⠹⢤⡤⢴⡟⠀⠀⠀⠀⢻⠀⠀⠘⠛⠙⠀⠀⠀⠘⣦⠀⠰⣦⠤⠖⠛⠁⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠾⠋⠁⣠⡴⠂⠀⠀⠀⡧⢶⠀⣿⠀⠙⠳⣄⡀⠀⠀⠈⠓⠤⠔⠚⠁⠀⠀⠀⠀⢸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠻⡄⠀⠘⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⣷⠀⠀⢠⠞⠁⠀⣀⣤⠴⢎⡇⠀⠀⣿⣰⡀⠀⣯⠙⠲⢤⡀⠀⠀⠀⠀⠀⠀⠀⣀⣤⠾⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⢳⡀⠀⠘⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⢹⡀⠀⠀⠠⠚⠉⠁⠀⢀⡾⠓⠂⠀⡏⢷⢧⠀⢸⡄⠀⠀⢻⠳⠦⠤⠤⢶⣶⠋⠉⠉⠉⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢷⣠⣤⣹⡄⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⠘⡇⠀⠶⣦⣀⠀⠀⢀⠟⠀⠀⠀⠀⡇⠈⢟⣆⢸⡇⠀⠀⠈⣇⠀⠀⠀⠀⠙⢳⣄⠀⠀⠀⢷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣹⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⠀⠀⠀⠈⠛⠒⠀⠀⠀⠀⢀⣼⠃⠀⠘⣟⠀⢷⠀⠀⠀⢹⡦⠤⠤⠤⢤⣀⣙⢦⡀⠀⠘⣧⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⢀⣤⡞⠁⠀⠀⠀⠀⠀⠸⡄⠀⠀⠈⢷⠀⠀⠀⠀⠀⠉⠙⠳⢤⣀⣘⣦⡀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣷⣤⡀⠀⢀⣤⣤⣶⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⢳⡀⠀⠀⠈⢣⡀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠀⠙⠲⢦⣄⣠⣾⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣭⣼⣷⣿⣿⣿⡏⠘⣆⠀⠀⠀⠀⠀⠀⠈⠳⣄⠀⠀⠀⠙⢦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⢿⣿⣿⣿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀",
@"⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠹⡄⠹⡄⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠀⠀⠀⠙⠦⣄⠀⠀⠀⠀⣶⠀⠀⠀⠀⠀⠀⣽⢿⣿⣿⣿⣇⣤⡄⠀⠀⠀⠀⠀⠀⠀",
@"⠀⣠⠜⠋⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⠀⠘⣄⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⢤⣀⡀⠀⠈⠳⢤⣀⠀⢹⡄⠀⠀⠀⢀⡼⠃⠀⠉⠻⣿⣿⣿⣧⡄⠀⠀⠀⠀⠀⠀",
    ];

            int anchoTerminal = Console.WindowWidth;
            string centrado = "";


            foreach (string linea in letraASCII)
            {
                int padding = (anchoTerminal - linea.Length) / 2;
                centrado += new string(' ', padding) + linea + Environment.NewLine; //Enviroment.NewLine remplaza el uso de \r y \n y permite compatibilidad con distintos sistemas operativos
            }


            TextoTiempo(centrado, tiempo, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(9000);

        }
        private static string FraseDerrota = @"resources\audio\Frase Final Derrota.wav";
        private static string FraseVictoria = @"resources\audio\Frase Final Victoria.wav";
    }
}


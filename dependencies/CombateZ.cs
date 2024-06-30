using DBClass;
using Mensajes;
using Torneo;
using System.Text.Json;
namespace CombateZ
{
    public class InterfazCombate
    {
        //public bool verCombates = 
        public static void ModuloDeCombate()
        {
            if (!File.Exists(TorneoSet.jugadorPJZ) || !File.Exists(TorneoSet.enemigosZ))
            {
                MensajesTerminal.ErrorSalir();
            }

            var texto1 = File.ReadAllText(TorneoSet.jugadorPJZ);
            Guerreros datosPlayer = JsonSerializer.Deserialize<Guerreros>(texto1);
            var texto2 = File.ReadAllText(TorneoSet.enemigosZ);
            List<Guerreros> datosEnemigos = JsonSerializer.Deserialize<List<Guerreros>>(texto2);

            List<Guerreros> OrdenAleatorio = [datosPlayer];

            for (int i = 0; i < datosEnemigos.Count; i++)
            {
                OrdenAleatorio.Add(datosEnemigos[i]);
            }
            //Randomizo el orden para el torneo
            Random random = new Random();
            OrdenAleatorio = OrdenAleatorio.OrderBy(x => random.Next()).ToList();

        

            for (int i = 0; i < OrdenAleatorio.Count; i+=2)
            {
                if (OrdenAleatorio[i] == datosPlayer || OrdenAleatorio[i+1] == datosPlayer)
                {
                    Combate1v1(OrdenAleatorio[i], OrdenAleatorio[i+1]);
                    
                }

            }

        }




        public static List<Guerreros> Combate1v1(Guerreros item1, Guerreros item2)
        {

            string[] lineas1 = {
                $"Salud: {item1.Salud}",
                $"Nombre: {item1.Name}",
                $"Raza: {item1.Race}",
                $"Ki: {item1.Ki}",
                $"Velocidad: {item1.Velocidad}",
                $"Fuerza: {item1.Fuerza}",
                $"Armadura: {item1.Armadura}",
            };

            string[] lineas2 = {
                $"Salud: {item2.Salud}",
                $"Nombre: {item2.Name}",
                $"Raza: {item2.Race}",
                $"Ki: {item2.Ki}",
                $"Velocidad: {item2.Velocidad}",
                $"Fuerza: {item2.Fuerza}",
                $"Armadura: {item2.Armadura}",
            };

            int ajusteDelAncho = 50;

            // Asignar colores a los bloques según la raza
            ConsoleColor colorItem1 = ObtenerColorPorRaza(item1.Race);
            ConsoleColor colorItem2 = ObtenerColorPorRaza(item2.Race);

            // Mostrar los datos de item1 e item2 en paralelo con colores diferentes
            MostrarBloquesParalelos(lineas1, lineas2, ajusteDelAncho, colorItem1, colorItem2);
            Console.ForegroundColor = ConsoleColor.White; // Me aseguro que el color de la consola siempre vuelva a blanco después de mostrar los personajes
            
        }

        private static void MostrarBloquesParalelos(string[] lineas1, string[] lineas2, int ajusteDelAncho, ConsoleColor colorItem1, ConsoleColor colorItem2)
        {
            // Calcular el ancho total de la consola necesario para mostrar ambos bloques
            int anchoTotal = (ajusteDelAncho * 2) + 2;

            // Imprimir la línea superior de ambos bloques
            Console.ForegroundColor = colorItem1;
            Console.Write(new string('-', ajusteDelAncho));
            Console.ForegroundColor = colorItem2;

            Console.WriteLine("  " + new string('-', ajusteDelAncho));

            // Imprimir los bloques en paralelo
            for (int i = 0; i < lineas1.Length; i++)
            {
                // Cambiar el color para el primer bloque
                int padding1 = (ajusteDelAncho - 2 - lineas1[i].Length) / 2;
                string paddedLinea1 = "|" + new string(' ', padding1) + lineas1[i] + new string(' ', ajusteDelAncho - 2 - lineas1[i].Length - padding1) + "|";

                // Cambiar el color para el segundo bloque
                int padding2 = (ajusteDelAncho - 2 - lineas2[i].Length) / 2;
                string paddedLinea2 = "|" + new string(' ', padding2) + lineas2[i] + new string(' ', ajusteDelAncho - 2 - lineas2[i].Length - padding2) + "|";

                // Imprimir ambas líneas en paralelo
                Console.ForegroundColor = colorItem1;
                Console.Write(paddedLinea1);
                Console.ForegroundColor = colorItem2;
                Console.WriteLine("  " + paddedLinea2);
            }

            // Imprimir la línea inferior de ambos bloques
            Console.ForegroundColor = colorItem1;
            Console.Write(new string('-', ajusteDelAncho));
            Console.ForegroundColor = colorItem2;
            Console.Write("  " + new string('-', ajusteDelAncho));
        }

        private static ConsoleColor ObtenerColorPorRaza(string raza)
        {
            switch (raza.ToLower())
            {
                case "saiyan":
                    return ConsoleColor.Yellow;
                case "android":
                    return ConsoleColor.Cyan;
                // Agregar más casos para otras razas si es necesario
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
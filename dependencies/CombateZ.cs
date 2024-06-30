using DBClass;
using Mensajes;
using Torneo;
using System.Text.Json;

namespace CombateZ
{
    public class InterfazCombate
    {
        //public bool verCombates = 
        public static ConsoleColor seleccionPlayer;
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



            for (int i = 0; i < OrdenAleatorio.Count; i += 2)
            {
                if (OrdenAleatorio[i] == datosPlayer || OrdenAleatorio[i + 1] == datosPlayer)
                {
                    Combate1v1(OrdenAleatorio[i], OrdenAleatorio[i + 1]);

                }

            }

        }




        public static void Combate1v1(Guerreros jugador, Guerreros item2)
        {

            
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.CursorVisible = false;
                // Asignar colores a los bloques según la raza
                ConsoleColor colorItem1 = MensajesTerminal.ColorTerminalRaza(jugador.Race);
                ConsoleColor colorItem2 = MensajesTerminal.ColorTerminalRaza(item2.Race);


                // Mostrar los datos de item1 e item2 en paralelo con colores diferentes
                MostrarBloquesParalelos(jugador, item2, colorItem1, colorItem2);

                jugador.Salud -= 20;
                item2.Salud -= 50;
                string[] probando = {"Atacar","Esquivar","Ataque","Especial","Rendirse"};
                seleccionPlayer = MensajesTerminal.ColorTerminalRaza(jugador.Race);
                MenusDelJuego.Menus.MenuGuerreros(probando,"Combate");

            Console.ForegroundColor = ConsoleColor.White; // Volver el color de la consola me soluciona un bug con los colores

            }





        }

        private static void MostrarBloquesParalelos(Guerreros item1, Guerreros item2, ConsoleColor colorItem1, ConsoleColor colorItem2)
        {
            int ajusteDelAncho = Console.WindowWidth;
            int anchoTotal = ajusteDelAncho /2;
            //Defino los string adentro de esta función para poder crear el efecto de actualización de pantalla durante el combate
            string[] lineas1 = {
                $"Salud: {item1.Salud}",
                $"Nombre: {item1.Name}",
                $"Raza: {item1.Race}",
                $"Ki: {item1.KiCombate}",
                $"Velocidad: {item1.Velocidad}",
                $"Fuerza: {item1.Fuerza}",
                $"Armadura: {item1.Armadura}",
            };
            string[] lineas2 = {
                $"Salud: {item2.Salud}",
                $"Nombre: {item2.Name}",
                $"Raza: {item2.Race}",
                $"Ki: {item2.KiCombate}",
                $"Velocidad: {item2.Velocidad}",
                $"Fuerza: {item2.Fuerza}",
                $"Armadura: {item2.Armadura}",
            };
            Console.ForegroundColor = colorItem1;
            Console.Write(new string('-', anchoTotal+1));
            Console.ForegroundColor = colorItem2;
            Console.WriteLine(" "+new string('-', anchoTotal-1));

            for (int i = 0; i < lineas1.Length; i++)
            {
                //Bloque Izquierda
                int padding1 = 1+ (anchoTotal - 2 - lineas1[i].Length) / 2;
                string paddedLinea1 = new string(' ', padding1) + lineas1[i] + new string(' ', anchoTotal  - lineas1[i].Length - padding1) + "|";
                //Bloque Derecha
                int padding2 = (anchoTotal - 2 - lineas2[i].Length) / 2;
                string paddedLinea2 = "|" + new string(' ', padding2) + lineas2[i] + new string(' ', anchoTotal - 2 - lineas2[i].Length - padding2);

                Console.ForegroundColor = colorItem1;
                Console.Write(paddedLinea1);
                Console.ForegroundColor = colorItem2;
                Console.WriteLine(" "+paddedLinea2);
            }

            Console.ForegroundColor = colorItem1;
            Console.Write(new string('-', anchoTotal+1));
            Console.ForegroundColor = colorItem2;
            Console.Write(" " +new string('-', anchoTotal-1));
        }


    }
}
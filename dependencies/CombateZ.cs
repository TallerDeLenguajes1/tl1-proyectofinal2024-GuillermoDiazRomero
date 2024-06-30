using DBClass;
using Mensajes;
using Torneo;
using System.Text.Json;

namespace CombateZ
{
    
    public class InterfazCombate
    {
        private static string GanadoresZ = @"resources\json\ganadores.json";
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

            List<Guerreros> Cuartos = new List<Guerreros>();
            List<Guerreros> Semis = new List<Guerreros>();
            List<Guerreros> Final = new List<Guerreros>();
            Guerreros GanadorTorneo = new Guerreros();
            if (OrdenAleatorio.Count == 16)
            {
                Cuartos = Simulador(OrdenAleatorio, datosPlayer);
                Semis = Simulador(OrdenAleatorio, datosPlayer);
                Final = Simulador(OrdenAleatorio, datosPlayer);
            }
            else if (OrdenAleatorio.Count == 8)
            {
                Semis = Simulador(OrdenAleatorio,datosPlayer);
                Final = Simulador(OrdenAleatorio, datosPlayer);
            } else if (OrdenAleatorio.Count == 4)
            {
                Final = Simulador(OrdenAleatorio, datosPlayer);
            }

            GanadorTorneo = Combate1v1(Final[0],Final[1],1);

            if (GanadorTorneo == datosPlayer)
            {
                GanadorTorneo.EleccionUsuario = true;
            }
            else{
                GanadorTorneo.EleccionUsuario = false;
            }

            if (!File.Exists(GanadoresZ))
            {
                using (File.Create(GanadoresZ)) {/*Creo y cierro el archivo*/}
            }

            string DatosPeleadoresJSON = JsonSerializer.Serialize(GanadorTorneo, new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.AppendAllText(GanadoresZ, DatosPeleadoresJSON + Environment.NewLine);


        }

        public static List<Guerreros> Simulador(List<Guerreros> ronda, Guerreros jugador)
        {
            List<Guerreros> Avanzan = new List<Guerreros>();
            Guerreros ganadorRonda;

            for (int i = 0; i < ronda.Count; i+=2)
            {
                if (ronda[i] == jugador || ronda[i + 1] == jugador)
                {
                    ganadorRonda = Combate1v1(ronda[i], ronda[i + 1], 1);
                    if (ganadorRonda.Name == jugador.Name)
                    {
                        Console.WriteLine("Avanzas a la siguiente ronda");
                    }
                    else
                    {
                        Console.WriteLine("Fuiste eliminado");
                    }
                }
                else
                {
                    ganadorRonda = Combate1v1(ronda[i], ronda[i + 1], 0);
                }
                Avanzan.Add(ganadorRonda);
            }
            return Avanzan;
        }


        public static Guerreros Combate1v1(Guerreros jugador, Guerreros enemigo, int tipo)
        {
            if (tipo == 1)
            {
                while (sigueVivo(jugador.Salud) && sigueVivo(enemigo.Salud))
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    // Asignar colores a los bloques según la raza
                    ConsoleColor colorL = MensajesTerminal.ColorTerminalRaza(jugador.Race);
                    ConsoleColor colorR = MensajesTerminal.ColorTerminalRaza(enemigo.Race);


                    // Mostrar los datos de jugador e enemigo en paralelo con colores diferentes
                    MostrarBloquesParalelos(jugador, enemigo, colorL, colorR);

                    jugador.Salud -= 50;
                    enemigo.Salud -= 10;
                    string[] probando = { "Atacar", "Esquivar", "Ataque", "Especial", "Rendirse" };
                    seleccionPlayer = MensajesTerminal.ColorTerminalRaza(jugador.Race);
                    MenusDelJuego.Menus.MenuGuerreros(probando, "Combate");
                    Console.ForegroundColor = ConsoleColor.White; // Volver el color de la consola me soluciona un bug con los colores

                }
            }
            else
            {
                //Aca tiene que ir el combate automatico del resto de los participantes en el cual no se encuentra el jugador
                jugador.Salud -= 50;
                enemigo.Salud -= 10;
            }
            if (sigueVivo(jugador.Salud))
            {
                return jugador;
            }
            else
            {
                return enemigo;

            }
        }

        private static void MostrarBloquesParalelos(Guerreros item1, Guerreros enemigo, ConsoleColor colorL, ConsoleColor colorR)
        {
            int ajusteDelAncho = Console.WindowWidth;
            int anchoTotal = ajusteDelAncho / 2;
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
                $"Salud: {enemigo.Salud}",
                $"Nombre: {enemigo.Name}",
                $"Raza: {enemigo.Race}",
                $"Ki: {enemigo.KiCombate}",
                $"Velocidad: {enemigo.Velocidad}",
                $"Fuerza: {enemigo.Fuerza}",
                $"Armadura: {enemigo.Armadura}",
            };
            Console.ForegroundColor = colorL;
            Console.Write(new string('-', anchoTotal + 1));
            Console.ForegroundColor = colorR;
            Console.WriteLine(" " + new string('-', anchoTotal - 2));
            for (int i = 0; i < lineas1.Length; i++)
            {
                //Bloque Izquierda
                int padding1 = 1 + (anchoTotal - 2 - lineas1[i].Length) / 2;
                string paddedLinea1 = new string(' ', padding1) + lineas1[i] + new string(' ', anchoTotal - lineas1[i].Length - padding1) + "|";
                //Bloque Derecha
                int padding2 = (anchoTotal - 2 - lineas2[i].Length) / 2;
                string paddedLinea2 = "|" + new string(' ', padding2) + lineas2[i] + new string(' ', anchoTotal - 2 - lineas2[i].Length - padding2);
                Console.ForegroundColor = colorL;
                Console.Write(paddedLinea1);
                Console.ForegroundColor = colorR;
                Console.WriteLine(" " + paddedLinea2);
            }
            Console.ForegroundColor = colorL;
            Console.Write(new string('-', anchoTotal + 1));
            Console.ForegroundColor = colorR;
            Console.Write(" " + new string('-', anchoTotal - 2));
        }



        public static bool sigueVivo(int hp)
        {
            return hp > 0;
        }

    }
}
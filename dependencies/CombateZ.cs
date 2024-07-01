using DBClass;
using Mensajes;
using Torneo;
using System.Text.Json;
using MenusDelJuego;
using MenusDelJuego;

namespace CombateZ
{

    public class InterfazCombate
    {

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

            //Decidí crear varias listas para poder tener un seguimiento más detallado de todo y que vaya decreciendo el tamaño de la misma
            List<Guerreros> Cuartos = new List<Guerreros>();
            List<Guerreros> Semis = new List<Guerreros>();
            List<Guerreros> Final = new List<Guerreros>();
            List<Guerreros> GanadorTorneo = new List<Guerreros>();
            if (OrdenAleatorio.Count == 16)
            {
                Cuartos = Simulador(OrdenAleatorio);
                Semis = Simulador(OrdenAleatorio);
                Final = Simulador(OrdenAleatorio);
            }
            else if (OrdenAleatorio.Count == 8)
            {
                Semis = Simulador(OrdenAleatorio);
                Final = Simulador(OrdenAleatorio);
            }
            else if (OrdenAleatorio.Count == 4)
            {
                Final = Simulador(OrdenAleatorio);
            }

            GanadorTorneo = Simulador(Final);
            

            if (GanadorTorneo[0].EleccionUsuario == true)
            {
                Console.Clear();
                MensajesTerminal.CartelVictoria(1);
                MensajesTerminal.CentradoSimple("FELIICDADES POR GANAR EL TORNEO DEL SPARKING LUCKY", 1000, 1);

            }
            else
            {
                Console.Clear();
                MensajesTerminal.CartelDerrota(1);
                MensajesTerminal.CentradoSimple("Suerte para la proxima...", 1000, 1);

            }

            if (!File.Exists(GanadoresZ))
            {
                using (File.Create(GanadoresZ)) {/*Creo y cierro el archivo*/}
            }

            string DatosPeleadoresJSON = JsonSerializer.Serialize(GanadorTorneo, new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.AppendAllText(GanadoresZ, DatosPeleadoresJSON + Environment.NewLine);


        }

        public static List<Guerreros> Simulador(List<Guerreros> ronda)
        {
            List<Guerreros> Avanzan = new List<Guerreros>();
            Guerreros ganadorRonda;

            for (int i = 0; i < ronda.Count; i += 2)
            {
                if (ronda[i].EleccionUsuario == true || ronda[i + 1].EleccionUsuario == true)
                {
                    ganadorRonda = Combate1v1(ronda[i], ronda[i + 1], 1);

                    if (ganadorRonda.EleccionUsuario == true)
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


        public static Guerreros Combate1v1(Guerreros pjL, Guerreros pjR, int tipo)
        {
            int seleccion;
            int AEAS = -1;
            int opcionAleatoria1;
            int opcionAleatoria2;
            string[] opciones1;
            string[] opciones2;

            KiOriginal1 = pjL.KiCombate;
            KiOriginal2 = pjR.KiCombate;

            Random numeroAleatorio = new Random();

            if (tipo == 1)
            {
                if (pjL.EleccionUsuario)
                {
                    //Selección del menú adecuado al personaje
                    if (pjL.AtaqueEspecial2)
                    {
                        opciones1 = OpcionesCombate1;
                    }
                    else if (pjL.AtaqueEspecial1)
                    {
                        opciones1 = OpcionesCombate2;
                    }
                    else
                    {
                        opciones1 = OpcionesCombate3;
                    }
                    if (pjR.AtaqueEspecial2)
                    {
                        opciones2 = OpcionesCombate4;
                    }
                    else if (pjR.AtaqueEspecial1)
                    {
                        opciones2 = OpcionesCombate5;
                    }
                    else
                    {
                        opciones2 = OpcionesCombate6;
                    }
                }
                else
                {
                    //Selección del menú adecuado al personaje
                    if (pjR.AtaqueEspecial2)
                    {
                        opciones1 = OpcionesCombate1;
                    }
                    else if (pjR.AtaqueEspecial1)
                    {
                        opciones1 = OpcionesCombate2;
                    }
                    else
                    {
                        opciones1 = OpcionesCombate3;
                    }
                    if (pjL.AtaqueEspecial2)
                    {
                        opciones2 = OpcionesCombate4;
                    }
                    else if (pjL.AtaqueEspecial1)
                    {
                        opciones2 = OpcionesCombate5;
                    }
                    else
                    {
                        opciones2 = OpcionesCombate6;
                    }
                }

                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    ConsoleColor colorIzquierda = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                    ConsoleColor colorDerecha = MensajesTerminal.ColorTerminalRaza(pjR.Race);

                    // Muestro los datos del jugador y del enemigo en paralelo con colores diferentes
                    MensajesTerminal.MostrarBloquesParalelos(pjL, pjR, colorIzquierda, colorDerecha);

                    //Sección de daño de los personajes
                    if (pjL.EleccionUsuario == true)
                    {
                        seleccionPlayer = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                        seleccion = Menus.MenuGuerreros(opciones1, "Combate");
                        switch (opciones1[seleccion])
                        {
                            case "Atacar":
                                AEAS = 1;
                                break;
                            case "Esquivar":
                                AEAS = 2;
                                break;
                            case "Ataque Especial":
                                AEAS = 3;
                                break;
                            case "Super Ataque":
                                AEAS = 4;
                                break;
                            case "Rendirse":
                                pjL.Salud = -9999;
                                break;
                            default:
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.White; // Volver el color de la consola me soluciona un bug con los colores
                        //Creo la opción elegida por el enemigo de forma aleatoria
                        opcionAleatoria1 = numeroAleatorio.Next(opciones2.Length) + 1;

                        if (pjL.Velocidad >= pjR.Velocidad)
                        {
                            salud1 = pjL.Salud;
                            ki1 = pjL.KiCombate;

                            salud2 = pjR.Salud;
                            ki2 = pjR.KiCombate;
                            DanioEnBatalla(pjL, pjR, AEAS, opcionAleatoria1);
                            pjL.Salud = salud1;
                            pjL.KiCombate = ki1;

                            pjR.Salud = salud2;
                            pjR.KiCombate = ki2;
                        }
                        else
                        {
                            salud1 = pjR.Salud;
                            ki1 = pjR.KiCombate;

                            salud2 = pjL.Salud;
                            ki2 = pjL.KiCombate;
                            DanioEnBatalla(pjR, pjL, opcionAleatoria1, AEAS);
                            pjR.Salud = salud1;
                            pjR.KiCombate = ki1;

                            pjL.Salud = salud2;
                            pjL.KiCombate = ki2;

                        }
                    }
                    else if (pjR.EleccionUsuario == true)
                    {
                        seleccionPlayer = MensajesTerminal.ColorTerminalRaza(pjR.Race);
                        seleccion = Menus.MenuGuerreros(opciones1, "Combate");
                        switch (opciones1[seleccion])
                        {
                            case "Atacar":
                                AEAS = 1;
                                break;
                            case "Esquivar":
                                AEAS = 2;
                                break;
                            case "Ataque Especial":
                                AEAS = 3;
                                break;
                            case "Super Ataque":
                                AEAS = 4;
                                break;
                            case "Rendirse":
                                pjR.Salud = -9999;
                                break;
                            default:
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.White; // Volver el color de la consola me soluciona un bug con los colores
                        //Creo la opción elegida por el enemigo de forma aleatoria
                        opcionAleatoria1 = numeroAleatorio.Next(opciones2.Length) + 1;

                        if (pjL.Velocidad >= pjR.Velocidad)
                        {
                            salud1 = pjL.Salud;
                            ki1 = pjL.KiCombate;

                            salud2 = pjR.Salud;
                            ki2 = pjR.KiCombate;
                            DanioEnBatalla(pjL, pjR, opcionAleatoria1, AEAS);
                            pjL.Salud = salud1;
                            pjL.KiCombate = ki1;

                            pjR.Salud = salud2;
                            pjR.KiCombate = ki2;
                        }
                        else
                        {
                            salud1 = pjR.Salud;
                            ki1 = pjR.KiCombate;

                            salud2 = pjL.Salud;
                            ki2 = pjL.KiCombate;
                            DanioEnBatalla(pjR, pjL, AEAS, opcionAleatoria1);
                            pjR.Salud = salud1;
                            pjR.KiCombate = ki1;

                            pjL.Salud = salud2;
                            pjL.KiCombate = ki2;

                        }
                    }
                }
            }
            else
            {

                if (pjL.AtaqueEspecial2)
                {
                    opciones1 = OpcionesCombate4;
                }
                else if (pjL.AtaqueEspecial1)
                {
                    opciones1 = OpcionesCombate5;
                }
                else
                {
                    opciones1 = OpcionesCombate6;
                }
                if (pjR.AtaqueEspecial2)
                {
                    opciones2 = OpcionesCombate4;
                }
                else if (pjR.AtaqueEspecial1)
                {
                    opciones2 = OpcionesCombate5;
                }
                else
                {
                    opciones2 = OpcionesCombate6;
                }
                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {
                    opcionAleatoria1 = numeroAleatorio.Next(opciones1.Length) + 1;
                    opcionAleatoria2 = numeroAleatorio.Next(opciones2.Length) + 1;

                    if (pjL.Velocidad >= pjR.Velocidad)
                    {
                        salud1 = pjL.Salud;
                        ki1 = pjL.KiCombate;

                        salud2 = pjR.Salud;
                        ki2 = pjR.KiCombate;
                        DanioEnBatalla(pjL, pjR, opcionAleatoria1, opcionAleatoria2);
                        pjL.Salud = salud1;
                        pjL.KiCombate = ki1;

                        pjR.Salud = salud2;
                        pjR.KiCombate = ki2;
                    }
                    else
                    {
                        salud1 = pjR.Salud;
                        ki1 = pjR.KiCombate;

                        salud2 = pjL.Salud;
                        ki2 = pjL.KiCombate;
                        DanioEnBatalla(pjR, pjL, opcionAleatoria2, opcionAleatoria1);
                        pjR.Salud = salud1;
                        pjR.KiCombate = ki1;

                        pjL.Salud = salud2;
                        pjL.KiCombate = ki2;
                    }
                }
            }

            if (sigueVivo(pjL.Salud))
            {
                pjL.Salud += 100;
                pjL.KiCombate = KiOriginal1;
                return pjL;
            }
            else
            {
                pjR.Salud += 100;
                pjR.KiCombate = KiOriginal2;
                return pjR;

            }
        }

        public static void DanioEnBatalla(Guerreros primero, Guerreros segundo, int movPrimero, int movSegundo)
        {
            //Todo lo que contiene un 1 es para el más rápido mientras que el 2 es para el otro
            double ataque1;
            double ataque2;
            double multiplicador1 = 1;
            double multiplicador2 = 1;
            bool exito1 = false;
            bool exito2 = false;
            bool fallo1 = true;
            bool fallo2 = true;
            Random random = new Random();
            int efectividad = random.Next(1, 101);

            ataque1 = (((primero.Destreza + primero.Fuerza) * efectividad) - segundo.Armadura) / AjusteAtaque;
            ataque2 = (((segundo.Destreza + segundo.Fuerza) * efectividad) - primero.Armadura) / AjusteAtaque;

            switch (primero.Status)
            {
                case "Guerrero Z":
                    multiplicador1 = 1.5;
                    break;
                case "Hakaishin":
                    multiplicador1 = 2;
                    break;
                case "Todo Poderoso":
                    multiplicador1 = 10;
                    break;
                default:
                    break;
            }
            switch (segundo.Status)
            {
                case "Guerrero Z":
                    multiplicador2 = 1.5;
                    break;
                case "Hakaishin":
                    multiplicador2 = 2;
                    break;
                case "Todo Poderoso":
                    multiplicador2 = 10;
                    break;
                default:
                    break;
            }


            switch (movPrimero)
            {
                //Atacar
                case 1:
                    break;
                //Esquivar
                case 2:
                    exito1 = Esquivar(primero, segundo);
                    break;
                //Ataque especial
                case 3:
                    if (ki1 > 0)
                    {
                        ataque1 *= multiplicador1;
                        ki1 -= 1;
                    }
                    else
                    {
                        fallo1 = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (ki1 > 0)
                    {
                        ataque1 *= multiplicador1;
                        ataque1 *= 1.2;
                        ki1 -= 2;
                    }
                    else
                    {
                        fallo1 = false;
                    }
                    break;
                //Rendirse
                case 5:
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:
                    break;
            }
            switch (movSegundo)
            {
                //Atacar
                case 1:
                    break;
                //Esquivar
                case 2:
                    exito2 = Esquivar(segundo, primero);
                    break;
                //Ataque especial
                case 3:
                    if (ki2 > 0)
                    {
                        ataque2 *= multiplicador2;
                        ki2 -= 1;
                    }
                    else
                    {
                        fallo2 = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (ki2 > 0)
                    {
                        ataque2 *= multiplicador2;
                        ataque2 *= 1.1;
                        ki2 -= 2;
                    }
                    else
                    {
                        fallo2 = false;
                    }

                    break;
                //Rendirse
                case 5:
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:
                    break;
            }


            if (!exito2 && fallo1 && movPrimero != 2)
            {
                salud2 -= ataque1;
                salud2 = Math.Round(salud2);
            }
            else
            {
                if (exito2)
                {
                    Console.WriteLine(primero.Name + " esquivó con éxito");
                    Thread.Sleep(2000);
                }
                if (!fallo1)
                {
                    Console.WriteLine(primero.Name + " no tiene Ki suficiente para realizar ese ataque");
                    Thread.Sleep(2000);
                }

            }
            if (!exito1 && fallo2 && movSegundo != 2)
            {
                if (sigueVivo(salud2))
                {
                    salud1 -= ataque1;
                    salud1 = Math.Round(salud1, 2);
                }
            }
            else
            {
                if (exito1)
                {
                    Console.WriteLine(segundo.Name + " esquivó con éxito");
                    Thread.Sleep(1000);
                }
                if (!fallo2)
                {
                    Console.WriteLine(segundo.Name + " no tiene Ki suficiente para realizar ese ataque");
                    Thread.Sleep(1000);
                }
            }

        }



        private static bool Esquivar(Guerreros primero, Guerreros segundo)
        {
            int probabilidadEsquivar = primero.Velocidad - segundo.Destreza;
            Random rnd = new Random();
            return rnd.Next(0, 20) < probabilidadEsquivar;
        }

        public static bool sigueVivo(double hp)
        {
            return hp > 0;
        }




        private static int AjusteAtaque = 20;
        private static string GanadoresZ = @"resources\json\ganadores.json";
        //public bool verCombates = 
        public static ConsoleColor seleccionPlayer;
        public static double salud1;
        public static double salud2;
        public static int ki1;
        public static int ki2;
        public static int KiOriginal1;
        public static int KiOriginal2;
        public static string[] OpcionesCombate1 = { "Atacar", "Esquivar", "Ataque Especial", "Super Ataque", "Rendirse" };
        public static string[] OpcionesCombate2 = { "Atacar", "Esquivar", "Ataque Especial", "Rendirse" };
        public static string[] OpcionesCombate3 = { "Atacar", "Esquivar", "Rendirse" };
        public static string[] OpcionesCombate4 = { "Atacar", "Esquivar", "Ataque Especial", "Super Ataque" };
        public static string[] OpcionesCombate5 = { "Atacar", "Esquivar", "Ataque Especial" };
        public static string[] OpcionesCombate6 = { "Atacar", "Esquivar" };
    }
}
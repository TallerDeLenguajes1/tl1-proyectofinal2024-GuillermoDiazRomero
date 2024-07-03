using DBClass;
using Mensajes;
using Torneo;
using System.Text.Json;
using MenusDelJuego;
using LogicaDeArchivos;

namespace CombateZ
{

    public class InterfazCombate
    {

        public static void ModuloDeCombate()
        {

            LecturaEscritura.ExisteError(TorneoSet.RutaFightersSelectedJSON);
            
            List<Guerreros> OrdenAleatorio = LecturaEscritura.ObtenerPeleadores(TorneoSet.RutaFightersSelectedJSON);


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
                OrdenAleatorio.Clear(); //Vacío las lista para no acumular memoria basura
                Semis = Simulador(Cuartos);
                Cuartos.Clear();
                Final = Simulador(Semis);
            }
            else if (OrdenAleatorio.Count == 8)
            {
                Semis = Simulador(OrdenAleatorio);
                Final = Simulador(Semis);
            }
            else if (OrdenAleatorio.Count == 4)
            {
                Final = Simulador(OrdenAleatorio);
            }

            GanadorTorneo = Simulador(Final);
            Final.Clear();

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
            GanadorTorneo.Clear();

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
            string[] opcionesUsuario;
            string[] opciones1;
            string[] opciones2;

            KiOriginal1 = pjL.KiCombate;
            KiOriginal2 = pjR.KiCombate;

            Random numeroAleatorio = new Random();

            //El tipo me define si la batalla será para el usuario o para la pc
            if (tipo == 1)
            {
                if (pjL.EleccionUsuario)
                {
                    //Selección del menú adecuado al personaje
                    opcionesUsuario = pjL.AtaqueEspecial2 ? OpcionesCombate1 : pjL.AtaqueEspecial1 ? OpcionesCombate2 : OpcionesCombate3;
                    opciones2 = pjR.AtaqueEspecial2 ? OpcionesCombate4 : pjR.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                }
                else
                {
                    //Selección del menú adecuado al personaje
                    opcionesUsuario = pjR.AtaqueEspecial2 ? OpcionesCombate4 : pjR.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                    opciones2 = pjL.AtaqueEspecial2 ? OpcionesCombate1 : pjL.AtaqueEspecial1 ? OpcionesCombate2 : OpcionesCombate3;
                }
                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {

                    Console.Clear();
                    Console.CursorVisible = false;

                    //Obtengo los colores los Peleadores para ser mostrados por pantalla
                    ConsoleColor colorIzquierda = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                    ConsoleColor colorDerecha = MensajesTerminal.ColorTerminalRaza(pjR.Race);


                    // Muestro los datos del jugador y del enemigo en paralelo con colores diferentes
                    MensajesTerminal.MostrarBloquesParalelos(pjL, pjR, colorIzquierda, colorDerecha);


                    //Creo la opción aleatoria de la PC
                    opcionAleatoria1 = numeroAleatorio.Next(opciones2.Length) + 1;


                    //Este if me indica si el usuario esta ubicado a la Izquierda o a la Derecha
                    if (pjL.EleccionUsuario == true)
                    {
                        seleccionPlayer = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                        seleccion = Menus.MenuGuerreros(opcionesUsuario, "Combate");
                        switch (opcionesUsuario[seleccion])
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


                        DanioEnBatalla(pjL, pjR, AEAS, opcionAleatoria1);

                    }

                    else {
                        seleccionPlayer = MensajesTerminal.ColorTerminalRaza(pjR.Race);
                        seleccion = Menus.MenuGuerreros(opcionesUsuario, "Combate");
                        switch (opcionesUsuario[seleccion])
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

                        DanioEnBatalla(pjL, pjR, opcionAleatoria1, AEAS);

                    }
                }
            }
            else
            {
                opciones1 = pjL.AtaqueEspecial2 ? OpcionesCombate4 : pjL.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                opciones2 = pjR.AtaqueEspecial2 ? OpcionesCombate4 : pjR.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;

                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {
                    opcionAleatoria1 = numeroAleatorio.Next(opciones1.Length) + 1;
                    opcionAleatoria2 = numeroAleatorio.Next(opciones2.Length) + 1;

                    DanioEnBatalla(pjL,pjR,opcionAleatoria1, opcionAleatoria2);
                    if (pjL.Velocidad >= pjR.Velocidad)
                    {
                        saludIzquierda = pjL.Salud;
                        ki1 = pjL.KiCombate;

                        saludDerecha = pjR.Salud;
                        ki2 = pjR.KiCombate;
                        DanioEnBatalla(pjL, pjR, opcionAleatoria1, opcionAleatoria2);
                        pjL.Salud = saludIzquierda;
                        pjL.KiCombate = ki1;

                        pjR.Salud = saludDerecha;
                        pjR.KiCombate = ki2;
                    }
                    else
                    {
                        saludIzquierda = pjR.Salud;
                        ki1 = pjR.KiCombate;

                        saludDerecha = pjL.Salud;
                        ki2 = pjL.KiCombate;
                        DanioEnBatalla(pjR, pjL, opcionAleatoria2, opcionAleatoria1);
                        pjR.Salud = saludIzquierda;
                        pjR.KiCombate = ki1;

                        pjL.Salud = saludDerecha;
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

        public static void DanioEnBatalla(Guerreros pjL, Guerreros pjR, int movpjL, int movpjR)
        {
            
            //Declaración de variables necesarias para el combate            
            double ataqueIzquierda;
            double ataqueDerecha;
            double multiplicador1 = 1;
            double multiplicador2 = 1;
            bool exitoIzquierda = false;
            bool exitoDerecha = false;
            bool falloIzquierda = true;
            bool falloDerecha = true;
            Random random = new Random();
            int efectividad = random.Next(1, 101);

            ataqueIzquierda = (((pjL.Destreza + pjL.Fuerza) * efectividad) - pjR.Armadura) / AjusteAtaque;
            ataqueDerecha = (((pjR.Destreza + pjR.Fuerza) * efectividad) - pjL.Armadura) / AjusteAtaque;


            if (pjR.Velocidad >= pjL.Velocidad)
            {
                saludIzquierda = pjR.Salud;
                ki1 = pjR.KiCombate;

                saludDerecha = pjL.Salud;
                ki2 = pjL.KiCombate;



                Console.WriteLine("Daño del pjR = " + ataqueIzquierda);
                Console.WriteLine("Daño del pjL = " + ataqueDerecha);

                Thread.Sleep(10000);

            }
            else
            {
                saludIzquierda = pjL.Salud;
                ki1 = pjL.KiCombate;

                saludDerecha = pjR.Salud;
                ki2 = pjR.KiCombate;

                Console.WriteLine("Daño del pjL = " + ataqueIzquierda);
                Console.WriteLine("Daño del pjR = " + ataqueDerecha);

                Thread.Sleep(10000);
            }



            switch (pjL.Status)
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
            switch (pjR.Status)
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

            switch (movpjL)
            {
                //Atacar
                case 1:
                    break;
                //Esquivar
                case 2:
                    exitoIzquierda = Esquivar(pjL, pjR);
                    break;
                //Ataque especial
                case 3:
                    if (ki1 > 0)
                    {
                        ataqueIzquierda *= multiplicador1;
                        ki1 -= 1;
                    }
                    else
                    {
                        falloIzquierda = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (ki1 > 0)
                    {
                        ataqueIzquierda *= multiplicador1;
                        ataqueIzquierda *= 1.2;
                        ki1 -= 2;
                    }
                    else
                    {
                        falloIzquierda = false;
                    }
                    break;
                //Rendirse
                case 5:
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:
                    break;
            }
            switch (movpjR)
            {
                //Atacar
                case 1:
                    break;
                //Esquivar
                case 2:
                    exitoDerecha = Esquivar(pjR, pjL);
                    break;
                //Ataque especial
                case 3:
                    if (ki2 > 0)
                    {
                        ataqueDerecha *= multiplicador2;
                        ki2 -= 1;
                    }
                    else
                    {
                        falloDerecha = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (ki2 > 0)
                    {
                        ataqueDerecha *= multiplicador2;
                        ataqueDerecha *= 1.1;
                        ki2 -= 2;
                    }
                    else
                    {
                        falloDerecha = false;
                    }

                    break;
                //Rendirse
                case 5:
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:
                    break;
            }


            if (!exitoDerecha && falloIzquierda && movpjL != 2)
            {
                saludDerecha -= ataqueIzquierda;
                saludDerecha = Math.Round(saludDerecha);
            }
            else
            {
                if (exitoDerecha)
                {
                    Console.WriteLine(pjR.Name + " esquivó con éxito");
                    Thread.Sleep(2000);
                }
                if (!falloIzquierda)
                {
                    Console.WriteLine(pjL.Name + " no tiene Ki suficiente para realizar ese ataque");
                    Thread.Sleep(2000);
                }

            }
            if (!exitoIzquierda && falloDerecha && movpjR != 2)
            {
                if (sigueVivo(saludDerecha))
                {
                    saludIzquierda -= ataqueDerecha;
                    saludIzquierda = Math.Round(saludIzquierda, 2);
                }
            }
            else
            {
                if (exitoIzquierda)
                {
                    Console.WriteLine(pjL.Name + " esquivó con éxito");
                    Thread.Sleep(1000);
                }
                if (!falloDerecha)
                {
                    Console.WriteLine(pjR.Name + " no tiene Ki suficiente para realizar ese ataque");
                    Thread.Sleep(1000);
                }
            }



        }



        private static bool Esquivar(Guerreros pjL, Guerreros pjR)
        {
            int probabilidadEsquivar = pjL.Velocidad - pjR.Destreza;
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
        public static double saludIzquierda;
        public static double saludDerecha;
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
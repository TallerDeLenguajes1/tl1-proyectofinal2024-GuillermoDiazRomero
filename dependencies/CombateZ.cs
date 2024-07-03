using DBClass;
using Mensajes;
using MenusDelJuego;
using LogicaArchivos;

namespace CombateZ
{
    public class InterfazCombate
    {
        public static void ModuloDeCombate()
        {

            LecturaEscritura.ExisteError(Rutas.FightersSelectedJSON);

            List<Guerreros> OrdenAleatorio = LecturaEscritura.ObtenerPeleadores(Rutas.FightersSelectedJSON);


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
                Semis = Simulador(Cuartos);
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

            LecturaEscritura.ExisteCrearRuta(Rutas.CarpetaJson, Rutas.GanadoresZ);

            string lecturaArchivoGanadores = File.ReadAllText(Rutas.GanadoresZ);

            //Me asegiró que el archivo no esté vacío para evitar un error al deserializar el json
            if (string.IsNullOrEmpty(lecturaArchivoGanadores))
            {
                LecturaEscritura.EscrituraJson(GanadorTorneo, Rutas.GanadoresZ);
            }
            else
            {
                List<Guerreros> ListaGanadores = LecturaEscritura.ObtenerPeleadores(Rutas.GanadoresZ);
                ListaGanadores.Add(GanadorTorneo[0]);
                LecturaEscritura.EscrituraJson(ListaGanadores, Rutas.GanadoresZ);
            }



            //Elimino todos los datos de las listas para evitar tener memoria basura
            Cuartos.Clear();
            Semis.Clear();
            Final.Clear();
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
                        MensajesTerminal.TextoTiempo("Avanzas a la siguiente ronda", 2000, 1);
                    }
                    else
                    {
                        MensajesTerminal.TextoTiempo("Fuiste eliminado", 2000, 1);
                    }
                }
                else
                {
                    ganadorRonda = Combate1v1(ronda[i], ronda[i + 1], 0);
                }
                Avanzan.Add(ganadorRonda);
                MensajesTerminal.TextoTiempo(Avanzan.Last().Name + " avanza a la siguiente ronda\n", 2000, 1);
                Thread.Sleep(2000);
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

            KiOriginalIzquierda = pjL.KiCombate;
            KiOriginalDerecha = pjR.KiCombate;


            Random numeroAleatorio = new Random();

            //El tipo me define si la batalla será para el usuario o para la pc
            if (tipo == 1)
            {
                //Selección del menú adecuado al personaje
                if (pjL.EleccionUsuario)
                {
                    opcionesUsuario = pjL.AtaqueEspecial2 ? OpcionesCombate1 : pjL.AtaqueEspecial1 ? OpcionesCombate2 : OpcionesCombate3;
                    opciones2 = pjR.AtaqueEspecial2 ? OpcionesCombate4 : pjR.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                }
                else
                {
                    opcionesUsuario = pjR.AtaqueEspecial2 ? OpcionesCombate1 : pjR.AtaqueEspecial1 ? OpcionesCombate2 : OpcionesCombate3;
                    opciones2 = pjL.AtaqueEspecial2 ? OpcionesCombate4 : pjL.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                }


                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {
                    Console.Clear();
                    Console.CursorVisible = false; //Puesto para evitar bugs

                    //Obtengo los colores los Peleadores para ser mostrados por pantalla
                    ConsoleColor colorIzquierda = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                    ConsoleColor colorDerecha = MensajesTerminal.ColorTerminalRaza(pjR.Race);


                    // Muestro los datos del jugador y del enemigo en paralelo con colores diferentes
                    MensajesTerminal.MostrarBloquesParalelos(pjL, pjR, colorIzquierda, colorDerecha);


                    //Creo la opción aleatoria de la PC
                    opcionAleatoria1 = numeroAleatorio.Next(opciones2.Length) + 1;


                    //Este if me indica si el usuario esta ubicado a la Izquierda o a la Derecha y obtengo su color para la terminal
                    if (pjL.EleccionUsuario == true) //Pregunto para evitar bugs
                    {
                        seleccionColorPlayer = MensajesTerminal.ColorTerminalRaza(pjL.Race);
                    }
                    else
                    {
                        seleccionColorPlayer = MensajesTerminal.ColorTerminalRaza(pjR.Race);
                    }

                    seleccion = Menus.MenuGuerreros(opcionesUsuario, "Combate");
                    //Uso un switch hermoso visualmente debido a que las opciones del menú de combate varian segun el personaje
                    AEAS = opcionesUsuario[seleccion] switch
                    {
                        "Atacar" => 1,
                        "Esquivar" => 2,
                        "Ataque Especial" => 3,
                        "Super Ataque" => 4,
                        "Rendirse" => 5,
                        _ => 0 // Default
                    };

                    Console.ForegroundColor = ConsoleColor.White; // Volver el color de la consola me soluciona un bug con los colores

                    if (pjL.EleccionUsuario == true)
                    {
                        DanioEnBatalla(pjL, pjR, AEAS, opcionAleatoria1);
                    }
                    else
                    {
                        DanioEnBatalla(pjL, pjR, opcionAleatoria1, AEAS);
                    }
                }
            }
            //Combate de la PC
            else
            {
                opciones1 = pjL.AtaqueEspecial2 ? OpcionesCombate4 : pjL.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                opciones2 = pjR.AtaqueEspecial2 ? OpcionesCombate4 : pjR.AtaqueEspecial1 ? OpcionesCombate5 : OpcionesCombate6;
                while (sigueVivo(pjL.Salud) && sigueVivo(pjR.Salud))
                {
                    opcionAleatoria1 = numeroAleatorio.Next(opciones1.Length) + 1;
                    opcionAleatoria2 = numeroAleatorio.Next(opciones2.Length) + 1;
                    DanioEnBatalla(pjL, pjR, opcionAleatoria1, opcionAleatoria2);
                }
            }

            if (sigueVivo(pjL.Salud))
            {
                pjL.Salud += 100;
                pjL.KiCombate = KiOriginalIzquierda;
                return pjL;
            }
            else
            {
                pjR.Salud += 100;
                pjR.KiCombate = KiOriginalDerecha;
                return pjR;
            }
        }

        public static void DanioEnBatalla(Guerreros pjL, Guerreros pjR, int movpjL, int movpjR)
        {

            //Declaración de variables necesarias para el combate            
            double ataqueIzquierda;
            double ataqueDerecha;
            double multiplicadorIzquierda;
            double multiplicadorDerecha;
            bool exitoIzquierda = false;
            bool exitoDerecha = false;
            bool falloIzquierda = true;
            bool falloDerecha = true;
            Random random = new Random();
            int efectividad = random.Next(1, 101);
            string mensajeIzquierda = "";
            string mensajeDerecha = "";

            ataqueIzquierda = (((pjL.Destreza + pjL.Fuerza) * efectividad) - pjR.Armadura) / AjusteAtaque;
            ataqueDerecha = (((pjR.Destreza + pjR.Fuerza) * efectividad) - pjL.Armadura) / AjusteAtaque;

            saludIzquierda = pjL.Salud;
            saludDerecha = pjR.Salud;

            kiIzquierda = pjL.KiCombate;
            kiDerecha = pjR.KiCombate;

            //Obtengo los multiplicadores segun el Status de cada peleador
            switch (pjL.Status)
            {
                case "Guerrero Z":
                    multiplicadorIzquierda = 1.5;
                    break;
                case "Hakaishin":
                    multiplicadorIzquierda = 2;
                    break;
                case "Todo Poderoso":
                    multiplicadorIzquierda = 10;
                    break;
                default:
                    multiplicadorIzquierda = 1;
                    break;
            }

            switch (pjR.Status)
            {
                case "Guerrero Z":
                    multiplicadorDerecha = 1.5;
                    break;
                case "Hakaishin":
                    multiplicadorDerecha = 2;
                    break;
                case "Todo Poderoso":
                    multiplicadorDerecha = 10;
                    break;
                default:
                    multiplicadorDerecha = 1;
                    break;
            }

            //MOVIMIENTO IZQUIERDA
            switch (movpjL)
            {
                //Atacar
                case 1:
                    mensajeIzquierda = pjL.Name + " realizó un ataque de " + ataqueIzquierda + " de daño";
                    break;
                //Esquivar
                case 2:
                    exitoIzquierda = Esquivar(pjL, pjR);
                    ataqueIzquierda = 0;
                    break;
                //Ataque especial
                case 3:
                    if (kiIzquierda > 0)
                    {
                        ataqueIzquierda *= multiplicadorIzquierda;
                        kiIzquierda -= 1;
                        mensajeIzquierda = pjL.Name + " realizó un Ataque Especial de " + ataqueIzquierda + " de daño";
                    }
                    else
                    {
                        falloIzquierda = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (kiIzquierda > 1)
                    {
                        ataqueIzquierda *= multiplicadorIzquierda;
                        ataqueIzquierda *= 1.2;
                        kiIzquierda -= 2;
                        mensajeIzquierda = pjL.Name + " realizó un Super Ataque de " + ataqueIzquierda + " de daño";
                    }
                    else
                    {
                        falloIzquierda = false;
                    }
                    break;
                //Rendirse
                case 5:
                    saludIzquierda = -99999;
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:
                    mensajeIzquierda = "Error";
                    break;
            }

            //MOVIMIENTO DERECHA
            switch (movpjR)
            {
                //Atacar
                case 1:
                    mensajeDerecha = pjR.Name + " realizó un ataque de " + ataqueDerecha + " de daño";
                    break;
                //Esquivar
                case 2:
                    exitoDerecha = Esquivar(pjR, pjL);
                    ataqueDerecha = 0;
                    break;
                //Ataque especial
                case 3:
                    if (kiDerecha > 0)
                    {
                        ataqueDerecha *= multiplicadorDerecha;
                        kiDerecha -= 1;
                        mensajeDerecha = pjR.Name + " realizó un Ataque Especial de " + ataqueDerecha + " de daño";
                    }
                    else
                    {
                        mensajeDerecha = pjR.Name + " no pudo realizar un Ataque Especial por falta de ki";
                        falloDerecha = false;
                    }
                    break;
                //Super Ataque
                case 4:
                    if (kiDerecha > 1)
                    {
                        ataqueDerecha *= multiplicadorDerecha;
                        ataqueDerecha *= 1.1;
                        kiDerecha -= 2;
                        mensajeDerecha = pjR.Name + " realizó un Super Ataque de " + ataqueDerecha + " de daño";
                    }
                    else
                    {
                        mensajeDerecha = pjR.Name + " no pudo realizar un Super Ataque por falta de ki";
                        falloDerecha = false;
                    }

                    break;
                //Rendirse
                case 5:
                    saludDerecha = -9999;
                    MensajesTerminal.TextoTiempo("Mr Satan si se la banca...", 1000, 1);
                    break;
                default:

                    break;
            }


            //El de la izquierda es más veloz
            if (pjL.Velocidad > pjR.Velocidad)
            {
                if (falloIzquierda && !exitoDerecha && movpjL != 2)
                {
                    saludDerecha -= ataqueIzquierda;
                    Console.WriteLine(mensajeIzquierda);
                }
                else if (!falloIzquierda)
                {
                    Console.WriteLine(pjL.Name + " no pudo realizar su movimiento por falta de ki");
                }
                else if (exitoDerecha)
                {
                    Console.WriteLine(pjR.Name + " esquivó con éxito el ataque de " + pjL.Name);
                }
                else
                {
                    Console.WriteLine(pjL.Name + " intentó esquivar y falló");
                }


                if (sigueVivo(saludDerecha))
                {
                    if (falloDerecha && !exitoIzquierda && movpjR != 2)
                    {
                        saludIzquierda -= ataqueDerecha;
                        Console.WriteLine(mensajeDerecha);
                    }
                    else if (!falloDerecha)
                    {
                        Console.WriteLine(pjR.Name + " no pudo realizar su movimiento por falta de ki");
                    }
                    else if (exitoIzquierda)
                    {
                        Console.WriteLine(pjL.Name + " esquivó con éxito el ataque de " + pjR.Name);
                    }
                    else
                    {
                        Console.WriteLine(pjR.Name + " intentó esquivar y falló");
                    }
                }
                else
                {
                    Console.WriteLine(pjL.Name + " noqueó a " + pjR.Name);
                }


            }
            //El de la derecha es más veloz
            else
            {
                if (falloDerecha && !exitoIzquierda && movpjR != 2)
                {
                    saludIzquierda -= ataqueDerecha;
                    Console.WriteLine(mensajeDerecha);
                }
                else if (!falloDerecha)
                {
                    Console.WriteLine(pjR.Name + " no pudo realizar su movimiento por falta de ki");
                }
                else if (exitoIzquierda)
                {
                    Console.WriteLine(pjL.Name + " esquivó con éxito el ataque de " + pjR.Name);
                }
                else
                {
                    Console.WriteLine(pjR.Name + " intentó esquivar y falló");
                }


                if (sigueVivo(saludIzquierda))
                {
                    if (falloIzquierda && !exitoDerecha && movpjL != 2)
                    {
                        saludDerecha -= ataqueIzquierda;
                        Console.WriteLine(mensajeIzquierda);
                    }
                    else if (!falloIzquierda)
                    {
                        Console.WriteLine(pjL.Name + " no pudo realizar su movimiento por falta de ki");
                    }
                    else if (exitoDerecha)
                    {
                        Console.WriteLine(pjR.Name + " esquivó con éxito el ataque de " + pjL.Name);
                    }
                    else
                    {
                        Console.WriteLine(pjL.Name + " intentó esquivar y falló");
                    }
                }
                else
                {
                    Console.WriteLine(pjR.Name + " noqueó a " + pjL.Name);
                }
            }
            Thread.Sleep(2500);

            pjL.Salud = saludIzquierda;
            pjL.KiCombate = kiIzquierda;
            pjR.Salud = saludDerecha;
            pjR.KiCombate = kiDerecha;

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
        //public bool verCombates = 
        public static ConsoleColor seleccionColorPlayer;
        public static double saludIzquierda;
        public static double saludDerecha;
        public static int kiIzquierda;
        public static int kiDerecha;
        public static int KiOriginalIzquierda;
        public static int KiOriginalDerecha;
        private static string[] OpcionesCombate1 = { "Atacar", "Esquivar", "Ataque Especial", "Super Ataque", "Rendirse" };
        private static string[] OpcionesCombate2 = { "Atacar", "Esquivar", "Ataque Especial", "Rendirse" };
        private static string[] OpcionesCombate3 = { "Atacar", "Esquivar", "Rendirse" };
        private static string[] OpcionesCombate4 = { "Atacar", "Esquivar", "Ataque Especial", "Super Ataque" };
        private static string[] OpcionesCombate5 = { "Atacar", "Esquivar", "Ataque Especial" };
        private static string[] OpcionesCombate6 = { "Atacar", "Esquivar" };
    }
}
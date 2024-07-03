using DBAPI;
using Torneo;
using Mensajes;
using MenusDelJuego;
using System.Media;
using CombateZ;
using LogicaArchivos;


namespace StartGame
{
    public class GameStart()
    {

        public static void GameRun()
        {
            /*      Cosas Necesarias    */
            Console.CursorVisible = false; //Deshabilita el cursor durante la ejecución del juego
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Permite mostrar emojis por consola
            Console.Clear(); //Borra todos los mensajes anteriores a la ejecución
            /*--------------------------*/

            /*       Control de la API     */
            ControlJSON.ControlAPI();
            Thread.Sleep(10000);
            LecturaEscritura.LimpiarBuffer();
            /*----------------------------*/

            /*      Sección Comienzo del opening del juego      */
            SoundPlayer Opening = new SoundPlayer(Rutas.menuSongs[0]);
            Opening.PlayLooping(); //Pongo el opening en loop
            Thread.Sleep(10);
            LecturaEscritura.LimpiarBuffer();
            /*--------------------------------------------------*/

            /*      Menú de selección del tamaño del Torneo     */
            int tama = Menus.MenuTorneo(Rutas.menuRondas, 1); //Función que ejecuta el menú principal del juego y me devuelve le tamaño del torneo elegido por el usuario
            /*--------------------------------------------------*/

            //Creo este while para poder navegar entre los menus
            while (IteracionesMenu)
            {
                if (tama != 0)
                {
                    /*  Creo la cantidad "tama" de personajes aleatorios    */
                    TorneoSet.AleatorioZ(tama); //Función que genera los personajes aleatorios contenidos por la API
                    /*------------------------------------------------------*/

                    /*      Mostrar el Menú con los personajes Aleatorios     */
                    IteracionesMenu = TorneoSet.SeleccionGuerrero(); //Función que inicia los combates o vuelve al menú principal
                    /*--------------------------------------------------------*/
                }
                else
                {
                    Console.Clear();
                    LogicaPersonajes.cargarGanadores();
                    Console.WriteLine("");
                    MensajesTerminal.TextoTiempo("Redirigiendo al menú principal",4000,0);
                    MensajesTerminal.TextoTiempo("...",5000,0);
                }

                if (IteracionesMenu)
                {
                    tama = Menus.MenuTorneo(Rutas.menuRondas, 0); //Muestro el menú principal con el titulo del juego sin la animación de inicio
                }
            }

            /*      Sección Cambio de Música      */
            Opening.Stop(); //Detengo el opening
            SoundPlayer CombatSong = new SoundPlayer(Rutas.menuSongs[1]);
            CombatSong.PlayLooping(); //Pongo play al soundtrak de combate
            Thread.Sleep(1800);
            LecturaEscritura.LimpiarBuffer();
            /*------------------------------------*/

            /*      Sección Inicio del Combate      */
            MensajesTerminal.Preparate(); //Mensaje para prepararse para el combate
            Thread.Sleep(10);
            LecturaEscritura.LimpiarBuffer();
            Console.Clear();
            InterfazCombate.ModuloDeCombate();
            /*--------------------------------------*/

            /*                 Finalización del Juego                 */
            CombatSong.Stop();
            Console.WriteLine();
            Thread.Sleep(3000);
            LecturaEscritura.LimpiarBuffer();
            MensajesTerminal.TextoTiempo("Gracias por jugar",3000,0);
            MensajesTerminal.TextoTiempo(".",2000,0);
            /*--------------------------------------------------------*/

        }

        private static bool iteracionesMenu = true;
        public static bool IteracionesMenu { get => iteracionesMenu; set => iteracionesMenu = value; }
    }
}

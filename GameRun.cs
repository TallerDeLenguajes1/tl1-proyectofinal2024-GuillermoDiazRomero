using DBAPI;
using DBClass;
using Torneo;
using Mensajes;
using MenusDelJuego;
using System.Media;
using CombateZ;


namespace StartGame
{
    public class GameStart()
    {
        private static string[] menuRondas = { "4 Rondas", "8 Rondas", "16 Rondas", "Salir del Juego" };

        private static string[] menuSongs = {@"resources\audio\LimitSurvivor.wav",@"resources\audio\SolidStateScouter.wav"};

         
        private static bool iteracionesMenu = true;
        public static bool IteracionesMenu { get => iteracionesMenu; set => iteracionesMenu = value; }
        public static void GameRun()
        {
            /*      Cosas Necesarias    */
            Console.CursorVisible = false; //Deshabilita el cursor durante la ejecución del juego
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Permite mostrar emojis por consola
            Console.Clear(); //Borra todos los mensajes anteriores a la ejecución
            /*--------------------------*/

            /*       Control de la API     */
            ControlJSON.ControlAPI();
            Thread.Sleep(8000);
            /*----------------------------*/

            /*      Sección Comienzo del opening del juego      */
            SoundPlayer Opening = new SoundPlayer(menuSongs[0]);
            Opening.PlayLooping(); //Pongo el opening en loop
            Thread.Sleep(10);
            /*--------------------------------------------------*/

            /*      Menú de selección del tamaño del Torneo     */
            int tama = Menus.MenuTorneo(menuRondas, 1); //Función que ejecuta el menú principal del juego y me devuelve le tamaño del torneo elegido por el usuario
            /*--------------------------------------------------*/

            //Creo este while para poder navegar entre los menus
            while (IteracionesMenu)
            {
                /*  Creo la cantidad "tama" de personajes aleatorios    */
                TorneoSet.AleatorioZ(tama); //Función que genera los personajes aleatorios contenidos por la API
                /*------------------------------------------------------*/

                /*      Mostrar el Menú con los personajes Aleatorios     */
                IteracionesMenu = TorneoSet.SeleccionGuerrero(); //Función que inicia los combates o vuelve al menú principal
                /*--------------------------------------------------------*/
                if (IteracionesMenu)
                {
                    tama = Menus.MenuTorneo(menuRondas, 0); //Muestro el menú principal con el titulo del juego sin la animación de inicio
                }
            }

            /*      Sección Cambio de Música      */ 
            Opening.Stop(); //Detengo el opening
            SoundPlayer CombatSong = new SoundPlayer(menuSongs[1]);
            CombatSong.PlayLooping(); //Pongo play al soundtrak de combate
            Thread.Sleep(1200);
            /*------------------------------------*/

            /*      Sección Inicio del Combate      */
            MensajesTerminal.Preparate(); //Mensaje para prepararse para el combate
            Thread.Sleep(10);
            Console.Clear();
            InterfazCombate.ModuloDeCombate();
            /*--------------------------------------*/

            /*                 Finalización del Juego                 */
            CombatSong.Stop();
            MensajesTerminal.TextoTiempo("Gracias por jugar.",3000,1);
            Environment.Exit(0);
            /*--------------------------------------------------------*/
        }
    }


}

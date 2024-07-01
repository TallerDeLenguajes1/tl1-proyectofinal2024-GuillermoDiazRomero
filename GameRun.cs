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

            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Permite mostrar emojis por consola
            Console.Clear();

            /*--------------------------*/



            /*       Control de la API     */

            ControlJSON.ControlAPI();
            Thread.Sleep(8000);

            /*----------------------------*/



            /*  SACAR EL COMENTADO CUANDO EL JUEGO ESTE CASI COMPLETO   */

            SoundPlayer Opening = new SoundPlayer(menuSongs[0]);
            Opening.PlayLooping();
            Thread.Sleep(10);

            /*----------------------------------------------------------*/




            /*      Menú de selección del tamaño del Torneo     */

            int tama = Menus.MenuTorneo(menuRondas, 1);

            /*--------------------------------------------------*/

            //Creo este while para poder navegar entre los menus
            while (IteracionesMenu)
            {


                /*  Creo la cantidad "tama" de personajes aleatorios    */

                TorneoSet.AleatorioZ(tama);

                /*------------------------------------------------------*/




                /*      Mostrar el Menú con los personajes Aleatorios     */

                IteracionesMenu = TorneoSet.SeleccionGuerrero();

                /*--------------------------------------------------------*/
                if (IteracionesMenu)
                {
                    tama = Menus.MenuTorneo(menuRondas, 0);
                }
            }


            /*      Sección de Combate      */ 
            Opening.Stop();
            SoundPlayer CombatSong = new SoundPlayer(menuSongs[1]);
            CombatSong.PlayLooping();

            Thread.Sleep(1000);

            MensajesTerminal.Preparate();
            Thread.Sleep(10);
            Console.Clear();

            InterfazCombate.ModuloDeCombate();


        }
    }


}

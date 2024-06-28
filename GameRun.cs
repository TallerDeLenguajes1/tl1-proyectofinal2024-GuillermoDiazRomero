using DBAPI;
using DBClass;
using Torneo;
using Mensajes;
using MenusDelJuego;
using System.Media;


namespace StartGame
{
    public class GameStart()
    {
        private static string[] menuRondas = { "4 Rondas", "8 Rondas", "16 Rondas", "Salir del Juego" };
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

            string soundtrack = @"resources\audio\LimitSurvivor.wav";
            SoundPlayer soundPLay = new SoundPlayer(soundtrack);
            soundPLay.PlayLooping();
            Thread.Sleep(10);

            /*----------------------------------------------------------*/




            /*      Menú de selección del tamaño del Torneo     */

            int tama = Menus.Menu(menuRondas,1);

            /*--------------------------------------------------*/




            /*  Creo la cantidad "tama" de personajes aleatorios    */

            TorneoSet.AleatorioZ(tama);

            /*------------------------------------------------------*/




            /*      Mostrar el Menú con los personajes Aleatorios     */

            TorneoSet.SeleccionGuerrero();

            /*--------------------------------------------------------*/








        }
    }


}
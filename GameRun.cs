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
        public static void GameRun()
        {

            /*      Cosas Necesarias    */
            Console.CursorVisible = false;
            Console.Clear();


            //ControlJSON.ControlAPI();
            //Thread.Sleep(8000);

            /*  SACAR EL COMENTADO CUANDO EL JUEGO ESTE CASI COMPLETO   */
            // string soundtrack = @"resources\audio\LimitSurvivor.wav";
            // SoundPlayer soundPLay = new SoundPlayer(soundtrack);
            // soundPLay.PlayLooping();
            //Thread.Sleep(10);

            int tama = Menus.MenuEsqueleto();

            
            Console.WriteLine("Tamaño: "+ tama);
            TorneoSet.AleatorioZ(tama);
        }
    }


}
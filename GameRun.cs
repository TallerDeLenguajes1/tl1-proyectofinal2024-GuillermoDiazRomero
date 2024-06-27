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
            ControlJSON.ControlAPI();
            Thread.Sleep(8000);



            // string soundtrack = @"resources\audio\LimitSurvivor.wav";
            // SoundPLayer soundPLay = new SoundPLayer();
            // soundPLay.Play();

            int tama = Menus.MenuEsqueleto();

            
            Console.WriteLine("Tamaño: "+ tama);
            MenuPersonajes.MenuPjs(tama);
        }
    }


}
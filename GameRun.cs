using DBAPI;
using ControlArchivosJSON;
using DBClass;
using Torneo;
using Mensajes;


namespace StartGame
{
    public class GameStart()
    {
        public static void GameRun()
        {
            Control.ControlAPI();
            Console.ReadKey();
            MenuPersonajes.MenuPjs();
            Console.ReadLine();
        }
    }


}
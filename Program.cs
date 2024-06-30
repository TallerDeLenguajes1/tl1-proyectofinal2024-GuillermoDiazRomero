using StartGame;
using DBClass;
using CombateZ;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        GameStart.GameRun();
        InterfazCombate.ModuloDeCombate();
        


    }
    
}

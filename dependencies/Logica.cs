using System.Text.Json;
using DBClass;
using Mensajes;

namespace LogicaArchivos
{
    public class LecturaEscritura()
    {   
        //Deserializo los datos guardados en el .json y los devuelvo en formato texto del tipo Root
        public static Root ObtenerListaAPI(string ruta)
        {
            var texto = File.ReadAllText(ruta);
            var datos = JsonSerializer.Deserialize<Root>(texto);
            return datos;
        }
     
        public static void EscrituraJson(List<Guerreros> lista, string ruta)
        {
            string datos = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.WriteAllText(ruta, datos);
        }
 
        //Si no existe la ruta cancelo la ejecución del juego
        public static void ExisteError(string ruta)
        {
            if (!File.Exists(ruta))
            {
                MensajesTerminal.ErrorSalir();
            }
        }
 
        public static void ExisteCrearRuta(string rutaCarpeta, string rutaArchivo)
        {
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            if (!File.Exists(rutaArchivo))
            {
                using (File.Create(rutaArchivo)) { }
            }
        }

        public static List<Guerreros> ObtenerPeleadores(string ruta)
        {
            var texto = File.ReadAllText(ruta);
            List<Guerreros> datos = JsonSerializer.Deserialize<List<Guerreros>>(texto);
            return datos;
        }

    }

    public class LogicaPersonajes()
    {
        
        public static List<Guerreros> CreacionListadoGuerreros(int tama, Root datosDB, HashSet<int> noRepetidos)
        {
            List<Guerreros> Peleadores = new List<Guerreros>();
            for (int i = 0; i < tama; i++)
            {
                Guerreros temp = new Guerreros //Simplifico el guardado de datos del tipo Item en la variable temp del tipo Guerreros
                {
                    //Uso .ElementAt para usar el numero guardado en la posisión "i" del HashSet
                    Id = datosDB.Items[noRepetidos.ElementAt(i)].Id,
                    Name = datosDB.Items[noRepetidos.ElementAt(i)].Name,
                    Ki = datosDB.Items[noRepetidos.ElementAt(i)].Ki,
                    Maxki = datosDB.Items[noRepetidos.ElementAt(i)].MaxKi,
                    Race = datosDB.Items[noRepetidos.ElementAt(i)].Race,
                    Description = datosDB.Items[noRepetidos.ElementAt(i)].Description
                };
                //Se añadió los parametros para identificar a Zeno-sama, el Gran Sacerdote y asignar el ki a los gran kaios
                switch (temp.Race)
                {
                    case "Human": temp.Velocidad = 3; temp.Destreza = 3; temp.Armadura = 2; temp.Fuerza = 4; temp.Salud = 200; break;
                    case "Saiyan": temp.Velocidad = 6; temp.Destreza = 6; temp.Armadura = 7; temp.Fuerza = 7; temp.Salud = 200; break;
                    case "Namekian": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 6; temp.Fuerza = 5; temp.Salud = 200; break;
                    case "Majin": temp.Velocidad = 4; temp.Destreza = 5; temp.Armadura = 3; temp.Fuerza = 5; temp.Salud = 200; break;
                    case "Frieza Race": temp.Velocidad = 6; temp.Destreza = 6; temp.Armadura = 8; temp.Fuerza = 6; temp.Salud = 200; break;
                    case "Android": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 5; temp.Fuerza = 6; temp.Salud = 200; break;
                    case "Jiren Race": temp.Velocidad = 8; temp.Destreza = 7; temp.Armadura = 8; temp.Fuerza = 8; temp.Salud = 200; break;
                    case "God": temp.Velocidad = 10; temp.Destreza = 10; temp.Armadura = 10; temp.Fuerza = 10; temp.Salud = 200; break;
                    case "Angel":
                        if (temp.Name.Contains("Grand Priest")) { temp.Velocidad = 9999; temp.Destreza = 9999; temp.Armadura = 9999; temp.Fuerza = 9999; temp.Salud = 9999; }
                        else { temp.Velocidad = 20; temp.Destreza = 20; temp.Armadura = 20; temp.Fuerza = 20; temp.Salud = 200; }
                        break;
                    case "Evil": temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 6; temp.Fuerza = 6; temp.Salud = 200; break;
                    case "Nucleico":
                        temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 5; temp.Fuerza = 4; temp.Salud = 200;
                        if (temp.Ki.Contains("unknown") || temp.Maxki.Contains("unknown")) { temp.Ki = "10000"; temp.Maxki = "10000"; }
                        break; //Gran-Kaio-samas 
                    case "Nucleico benigno": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 2; temp.Fuerza = 4; temp.Salud = 200; if (temp.Ki.Contains("unknown") || temp.Maxki.Contains("unknown")) { temp.Ki = "10000"; temp.Maxki = "10000"; } break; //Sumpremos Kaio-samas
                    case "Unknown": temp.Velocidad = 10000; temp.Destreza = 10000; temp.Armadura = 10000; temp.Fuerza = 10000; temp.Salud = 10000; temp.Ki = "Zeno-sama"; temp.Maxki = "Zeno-sama"; break; //Zen Oh sama, personaje más fuerte del juego con una probabilidad de 1/58 de que salga
                }

                temp.Status = MedicionDeKi(temp.Maxki);
                temp.KiCombate = BalanceoDeKi(temp.Ki);

                switch (temp.Status)
                {
                    case "Humano":
                        temp.AtaqueEspecial1 = false;
                        temp.AtaqueEspecial2 = false;
                        break;
                    case "SuperHumano":
                        temp.AtaqueEspecial1 = true;
                        temp.AtaqueEspecial2 = false;
                        break;
                    case "Guerrero Z":
                        temp.AtaqueEspecial1 = true;
                        temp.AtaqueEspecial2 = false;
                        break;
                    case "Hakaishin":
                        temp.AtaqueEspecial1 = true;
                        temp.AtaqueEspecial2 = true;
                        break;
                    case "Todo Poderoso":
                        temp.AtaqueEspecial1 = true;
                        temp.AtaqueEspecial2 = true;
                        break;
                    default:
                        break;
                }

                Peleadores.Add(temp);
            }

            return Peleadores;
        }

        
        public static string MedicionDeKi(string kimax)
        {
            //Esta función devolverá un string con el "Titulo" segun el kimaximo de los guerreros
            if (double.TryParse(kimax, out double resultado))
            {
                //"Humano"
                if (resultado == 0)
                {
                    return "Humano";
                }
                //"SuperHumano"
                if (resultado > 0 && resultado < 1000000)
                {
                    return "SuperHumano";
                }
                //"Guerrero Z"
                if (resultado >= 1000000)
                {
                    return "Guerrero Z";
                }
            }
            kimax = kimax.ToLower();
            //Guerrero Z
            if (kimax.Contains("billion") || kimax.Contains("trillion"))
            {
                return "Guerrero Z";
            }
            //Hakaishin
            if (kimax.Contains("quadrillion") || kimax.Contains("quintillion") || kimax.Contains("sextillion") || kimax.Contains("septillion"))
            {
                return "Hakaishin";
            }
            //Todo Poderoso (Zeno-sama y El Gran Sarcedote)
            if (kimax.Contains("zeno-sama") || kimax.Contains("googolplex"))
            {
                return "Todo Poderoso";
            }
            return "Error";
        }

        
        public static int BalanceoDeKi(string ki)
        {
            //Esta función devolverá un numero entero de ki que serán capaces de utilizar los peleadores durante la lucha
            if (double.TryParse(ki, out double resultado))
            {
                //"Humano"
                if (resultado == 0)
                {
                    return 0;
                }
                //"SuperHumano"
                else if (resultado > 0 && resultado < 1000000)
                {
                    return 4;
                }
                //"Guerrero Z"
                else
                {
                    return 6;
                }
            }
            ki = ki.ToLower();
            //Guerrero Z
            if (ki.Contains("billion") || ki.Contains("trillion"))
            {
                return 6;
            }
            //Hakaishin
            if (ki.Contains("quadrillion") || ki.Contains("quintillion") || ki.Contains("sextillion") || ki.Contains("septillion"))
            {
                return 10;
            }
            //Todo Poderoso (Zeno-sama y El Gran Sarcedote)
            if (ki.Contains("zeno-sama") || ki.Contains("googolplex"))
            {
                return 10000;
            }
            return -9999;
        }

        public static void cargarGanadores()
        {

            if (!File.Exists(Rutas.GanadoresZ))
            {
                MensajesTerminal.CentradoSimple("No se encuentran ganadores por todavía", 2000, 1);
                MensajesTerminal.CentradoSimple("Tu puedes ser el primero...", 1500, 1);
                Thread.Sleep(3000);
            }
            else
            {
                //Me aseguró que el archivo no esté vacío para evitar un error al deserializar el json
                string lecturaArchivoGanadores = File.ReadAllText(Rutas.GanadoresZ);
                if (string.IsNullOrEmpty(lecturaArchivoGanadores))
                {
                    MensajesTerminal.CentradoSimple("No se encontraron ganadores todavía", 2000, 1);
                    MensajesTerminal.CentradoSimple("Puedes ser el primero...", 1500, 1);
                    Thread.Sleep(3000);
                }
                else
                {
                    List<Guerreros> ListaGanadores = LecturaEscritura.ObtenerPeleadores(Rutas.GanadoresZ);
                    MensajesTerminal.CentradoSimple("Ganadores de torneos anteriores:",1000,1);
                    foreach (Guerreros Z in ListaGanadores)
                    {
                        MensajesTerminal.mostrarGanadores(Z);
                        Thread.Sleep(2000);
                    }
                    MensajesTerminal.TextoTiempo("Presiona cualquier tecla para salir",2000,1);
                    Console.ReadKey();
                }
            }
        }
    }

    public class Rutas()
    {
        public static string urlApi = "https://dragonball-api.com/api/characters?limit=58";
        public static string CarpetaBackup = @"resources\backup";
        public static string Backup = @"resources\backup\Respaldo.json"; //Utilizo siempre el archivo de respaldo de la API para que el juego pueda correr sin problemas
        public static string CarpetaJson = @"resources\json";
        public static string FightersJSON = @"resources\json\fighters.json";
        public static string FightersSelectedJSON = @"resources\json\fightersSelected.json";
        public static string FraseDerrota = @"resources\audio\Frase Final Derrota.wav";
        public static string FraseVictoria = @"resources\audio\Frase Final Victoria.wav";
        public static string GanadoresZ = @"resources\json\winners.json";
        public static string[] decision = { "Aceptar", "Volver" };
        public static string[] decision2 = { "Volver a jugar", "Salir del juego" };
        public static string[] menuRondas = { "4 Rondas", "8 Rondas", "16 Rondas", "Ver Ganadores", "Salir del Juego" };
        public static string[] menuSongs = { @"resources\audio\LimitSurvivor.wav", @"resources\audio\SolidStateScouter.wav" };

    }

}
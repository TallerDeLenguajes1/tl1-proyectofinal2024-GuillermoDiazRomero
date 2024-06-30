using System.Text.Json;
using DBClass;
using Mensajes;
using MenusDelJuego;

namespace Torneo
{
    public class TorneoSet()
    {
        //Seccion de aleatoriedad de personajes
        private static string respaldoDB = @"resources\backup\Respaldo.JSON"; //Utilizo siempre el archivo de respaldo de la API para que el juego pueda correr sin problemas
        private static string ArchivoPJZ = @"resources\json\characters.JSON";
        public static string jugadorPJZ = @"resources\json\player.JSON";
        public static string enemigosZ = @"resources\json\enemies.JSON";
        public static void AleatorioZ(int tama)
        {
            if (!File.Exists(respaldoDB))
            {
                MensajesTerminal.ErrorSalir();
            }
            var texto = File.ReadAllText(respaldoDB); //Leo la API
            var datosDB = JsonSerializer.Deserialize<Root>(texto); //Doy formato legible a la API
            List<Guerreros> Peleadores = new List<Guerreros>();


            int numeroAleatorio;
            Random rand = new Random();
            //Utilizo HashSet ya que me permite almacenar aleatorios único (sin repetirse) y es más eficiente para la memoria
            HashSet<int> noRepetidos = new HashSet<int>();


            while (noRepetidos.Count < tama)
            { //Utilizo el .Count para saber la longitud del HashSet
                numeroAleatorio = rand.Next(1, 58);
                if (!noRepetidos.Contains(numeroAleatorio)) //Con .Contains pregunto si el numero aleatorio esta contenido adentro del HashSet
                {
                    noRepetidos.Add(numeroAleatorio); //Añado el numero al HashSet
                }
            }

            //Declaro la variable temporal Guerrero;
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

                Peleadores.Add(temp);
            }

            //Guardado de personajes en un archivo Seleccion.JSON
            if (!File.Exists(ArchivoPJZ))
            {
                using (File.Create(ArchivoPJZ)) {/*Creo y cierro el archivo*/}
            }

            string DatosPeleadoresJSON = JsonSerializer.Serialize(Peleadores, new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.WriteAllText(ArchivoPJZ, DatosPeleadoresJSON);



        }
        public static bool SeleccionGuerrero()
        {
            int elegido = 0;
            bool seleccionado = false;
            if (!File.Exists(ArchivoPJZ))
            {
                MensajesTerminal.ErrorSalir();

            }
            var texto = File.ReadAllText(ArchivoPJZ);
            var datosZ = JsonSerializer.Deserialize<List<Guerreros>>(texto);
            int cantidadGuerreros = datosZ.Count; //Obtengo la cantidad de elementos de la lista

            string[] ListadoGuerreros = new string[cantidadGuerreros + 1];

            for (int i = 0; i < cantidadGuerreros; i++)
            {
                ListadoGuerreros[i] = datosZ[i].Name + " (" + datosZ[i].Race + ")";
            }
            ListadoGuerreros[cantidadGuerreros] = "Salir";

            while (!seleccionado)
            {
                elegido = Menus.MenuGuerreros(ListadoGuerreros);
                if (ListadoGuerreros[elegido] == "Salir")
                {
                    return true;
                }
                Console.Clear();
                MensajesTerminal.ColorTerminalRaza(datosZ[elegido].Race);
                MensajesTerminal.CentradoSimple("-------------->   " + datosZ[elegido].Name + "   <--------------", 200, 1);

                MensajesTerminal.TextoTiempo("Descripción: " + datosZ[elegido].Description, 1000, 1);
                Console.ForegroundColor = ConsoleColor.White;
                seleccionado = Menus.MenuDecision();

            }

            if (!File.Exists(jugadorPJZ))
            {
                using (File.Create(jugadorPJZ)) { }
            }
            if (!File.Exists(enemigosZ))
            {
                using (File.Create(enemigosZ)) { }
            }

            string DatosJugadorJSON = JsonSerializer.Serialize(datosZ[elegido], new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.WriteAllText(jugadorPJZ, DatosJugadorJSON);

            datosZ.Remove(datosZ[elegido]); //elimino un item de la lista
            string DatosEnemigosJSON = JsonSerializer.Serialize(datosZ, new JsonSerializerOptions { WriteIndented = true }); //Permito que sea legible dandole formato
            File.WriteAllText(enemigosZ, DatosEnemigosJSON);
            return false;


        }
        public static string MedicionDeKi(string kimax)
        {
            //Esta función devolverá un string con el "Titulo" segun el kimaximo de los guerreros
            string tipoKi = "";
            if (double.TryParse(kimax, out double resultado))
            {
                //"Humano"
                if (resultado == 0)
                {
                    tipoKi = "Humano";
                    return tipoKi;
                }
                //"SuperHumano"
                if (resultado > 0 && resultado < 1000000)
                {
                    tipoKi = "SuperHumano";
                    return tipoKi;
                }
                //"Guerrero Z"
                if (resultado >= 1000000)
                {
                    tipoKi = "Guerrero Z";
                    return tipoKi;
                }
            }
            kimax = kimax.ToLower();
            //Guerrero Z
            if (kimax.Contains("billion") || kimax.Contains("trillion"))
            {
                tipoKi = "Guerrero Z";
                return tipoKi;
            }
            //Hakaishin
            if (kimax.Contains("quadrillion") || kimax.Contains("quintillion") || kimax.Contains("sextillion") || kimax.Contains("septillion"))
            {
                tipoKi = "Hakaishin";
                return tipoKi;
            }
            //Todo Poderoso (Zeno-sama y El Gran Sarcedote)
            if (kimax.Contains("zeno-sama") || kimax.Contains("googolplex"))
            {
                tipoKi = "Todo Poderoso";
                return tipoKi;
            }
            return tipoKi = "Error";
        }
        public static int BalanceoDeKi(string ki)
        {
            //Esta función devolverá un numero entero ki que serán capaces de utilizar los peleadores durante la lucha
            int cantiadadKi = 0;
            if (double.TryParse(ki, out double resultado))
            {
                //"Humano"
                if (resultado == 0)
                {
                    cantiadadKi = 0;
                    return cantiadadKi;
                }
                //"SuperHumano"
                if (resultado > 0 && resultado < 1000000)
                {
                    cantiadadKi = 4;
                    return cantiadadKi;
                }
                //"Guerrero Z"
                if (resultado >= 1000000)
                {
                    cantiadadKi = 6;
                    return cantiadadKi;
                }
            }
            ki = ki.ToLower();
            //Guerrero Z
            if (ki.Contains("billion") || ki.Contains("trillion"))
            {
                cantiadadKi = 6;
                return cantiadadKi;
            }
            //Hakaishin
            if (ki.Contains("quadrillion") || ki.Contains("quintillion") || ki.Contains("sextillion") || ki.Contains("septillion"))
            {
                cantiadadKi = 10;
                return cantiadadKi;
            }
            //Todo Poderoso (Zeno-sama y El Gran Sarcedote)
            if (ki.Contains("zeno-sama") || ki.Contains("googolplex"))
            {
                cantiadadKi = 10000;
                return cantiadadKi;
            }
            return cantiadadKi = -9999;
        }

    }
}

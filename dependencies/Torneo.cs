using System.Text.Json;
using DBClass;
using Mensajes;
using MenusDelJuego;
using LogicaArchivos;

namespace Torneo
{
    public class TorneoSet()
    {

        /*      Función que crea el listado de "tama" guerreros de forma aleatoria traidos del respaldo de la API       */
        public static void AleatorioZ(int tama)
        {
            LecturaEscritura.ExisteError(Rutas.Backup);

            Root datosDB = LecturaEscritura.ObtenerListaAPI(Rutas.Backup);


            //Creación de variables para randomizar el orden
            int numeroAleatorio;
            Random rand = new Random();
            HashSet<int> noRepetidos = new HashSet<int>();//Utilizo HashSet ya que me permite almacenar aleatorios único (sin repetirse) y es más eficiente para la memoria

            while (noRepetidos.Count < tama)
            { //Utilizo el .Count para saber la longitud del HashSet
                numeroAleatorio = rand.Next(1, 58);
                if (!noRepetidos.Contains(numeroAleatorio)) //Con .Contains pregunto si el numero aleatorio esta contenido adentro del HashSet
                {
                    noRepetidos.Add(numeroAleatorio); //Añado el numero al HashSet
                }
            }

            List<Guerreros> Peleadores = LogicaPersonajes.CreacionListadoGuerreros(tama, datosDB, noRepetidos);

            LecturaEscritura.ExisteCrearRuta(Rutas.CarpetaJson, Rutas.FightersJSON);

            //Guardo los personajes que tocaron en un archivo characters para ser usado en la fase de ejecución

            LecturaEscritura.EscrituraJson(Peleadores, Rutas.FightersJSON);

            Peleadores.Clear(); //Libero datos para evitar memoria basura
        }

        public static bool SeleccionGuerrero()
        {
            int elegido = 0;
            bool seleccionado = false;

            LecturaEscritura.ExisteError(Rutas.FightersJSON);

            List<Guerreros> datosZ = LecturaEscritura.ObtenerPeleadores(Rutas.FightersJSON);

            int cantidadGuerreros = datosZ.Count; //Obtengo la cantidad de elementos de la lista

            string[] ListadoGuerreros = new string[cantidadGuerreros + 1]; //Sumo 1 para guardar el string de "Salir"

            for (int i = 0; i < cantidadGuerreros; i++)
            {
                ListadoGuerreros[i] = datosZ[i].Name + " (" + datosZ[i].Race + ")";
            }
            ListadoGuerreros[cantidadGuerreros] = "Salir";

            while (!seleccionado)
            {
                elegido = Menus.MenuGuerreros(ListadoGuerreros, "Listado");
                if (ListadoGuerreros[elegido] == "Salir")
                {
                    return true;
                }

                Console.Clear();

                MensajesTerminal.ColorTerminalRaza(datosZ[elegido].Race);

                MensajesTerminal.CentradoSimple("-------------->   " + datosZ[elegido].Name + "   <--------------", 200, 1);
                MensajesTerminal.TextoTiempo("Descripción: " + datosZ[elegido].Description, 1000, 1);
                MensajesTerminal.TextoTiempo("Raza: " + datosZ[elegido].Race, 1000, 1);
                MensajesTerminal.TextoTiempo("Nivel de Ki: " + datosZ[elegido].Status, 1000, 1);

                Console.ForegroundColor = ConsoleColor.White;

                MensajesTerminal.TextoTiempo("\n¿Quiere usar este guerrero?", 100, 1);
                seleccionado = Menus.MenuDecision(Rutas.decision);
            }

            LecturaEscritura.ExisteCrearRuta(Rutas.CarpetaJson, Rutas.FightersSelectedJSON);


            datosZ[elegido].EleccionUsuario = true;
            string datosRutaFightersJSON = JsonSerializer.Serialize(datosZ, new JsonSerializerOptions { WriteIndented = true });//Permito que sea legible dandole formato

            File.WriteAllText(Rutas.FightersSelectedJSON, datosRutaFightersJSON);

            datosZ.Clear();
            return false;
        }
    
    }
}

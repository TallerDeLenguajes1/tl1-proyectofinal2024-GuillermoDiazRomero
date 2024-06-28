using System.Text.Json;
using System.Text.Json.Serialization;
using DBClass;
using Mensajes;

namespace Torneo
{
    public class TorneoSet()
    {
        //Seccion de aleatoriedad de personajes
        private static string respaldoDB = @"resources\backup\Respaldo.JSON"; //Utilizo siempre el archivo de respaldo de la API para que el juego pueda correr sin problemas
        public static string ArchivoPJZ = @"resources\json\characters.JSON";
        public static void AleatorioZ(int tama)
        {
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
                    case "Angel": temp.Velocidad = 20; temp.Destreza = 20; temp.Armadura = 20; temp.Fuerza = 20; temp.Salud = 200; break;
                    case "Evil": temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 6; temp.Fuerza = 6; temp.Salud = 200; break;
                    case "Nucleico": temp.Velocidad = 2; temp.Destreza = 3; temp.Armadura = 5; temp.Fuerza = 4; temp.Salud = 200; break; //Gran-Kaio-samas 
                    case "Nucleico benigno": temp.Velocidad = 5; temp.Destreza = 5; temp.Armadura = 2; temp.Fuerza = 4; temp.Salud = 200; break; //Sumpremos Kaio-samas
                    case "Unknown": temp.Velocidad = 100; temp.Destreza = 100; temp.Armadura = 200; temp.Fuerza = 10000; temp.Salud = 200; break; //Zen Oh sama, personaje más fuerte del juego con una probabilidad de 1/58 de que salga
                }
                Peleadores.Add(temp);
            }

            //Guardado de personajes en un archivo Seleccion.JSON
            if (!File.Exists(ArchivoPJZ))
            {
                using (File.Create(ArchivoPJZ)){/*Creo y cierro el archivo*/}
            }

            string DatosPeleadoresJSON = JsonSerializer.Serialize(Peleadores, new JsonSerializerOptions{WriteIndented = true}); //Permito que sea legible dandole formato
            File.WriteAllText(ArchivoPJZ,DatosPeleadoresJSON);

            
            Console.WriteLine("Mostrando Personajes que tocaron");
            foreach (Guerreros datos in Peleadores)
            {
                MensajesTerminal.mostrarPjs(datos);
            }
            Console.ReadKey();
        }
    }
}
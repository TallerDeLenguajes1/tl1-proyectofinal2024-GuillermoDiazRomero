using System.Text.Json;
using DBClass;
using LogicaArchivos;
using Mensajes;

namespace DBAPI
{
    public class ConsumiendoAPI
    {
        public async Task<bool> TraerAPI() //Creo el Task de tipo bool para poder hacer un control de conexión con la API web
        {
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage respuesta = await cliente.GetAsync(Rutas.urlApi);
                respuesta.EnsureSuccessStatusCode();

                string responseBody = await respuesta.Content.ReadAsStringAsync();
                var contenidoAPI = JsonSerializer.Deserialize<Root>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                //Creo un archivo JSON que contenga toda la información de la API por si en algun momento el juego se ejecuta de forma offline
                LecturaEscritura.ExisteCrearRuta(Rutas.CarpetaBackup, Rutas.Backup);

                await File.WriteAllTextAsync(Rutas.Backup, JsonSerializer.Serialize(contenidoAPI));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }
    }
    public class ControlJSON()
    {
        public static async void ControlAPI() //Recordar agregar public al comienzo para poder utilizarlo en donde sea con Control.ControlApi()
        {
            ConsumiendoAPI serverON = new ConsumiendoAPI();
            bool control = await serverON.TraerAPI();
            if (control)
            { //Pregunto si se puede contactar con la API
                MensajesTerminal.TextoTiempo("Se logró contactar con Zeno-Sama...\nIniciando juego...", 2000, 1);
                Thread.Sleep(1000);
                LecturaEscritura.LimpiarBuffer();
            }
            else if (File.Exists(Rutas.Backup))
            { //Pregunto si existe un JSON de respaldo
                Console.Clear();
                MensajesTerminal.TextoTiempo("Imposible contactar con Zeno-Sama...\nUsando las esferas del dragon... \nShenlong consederá nuestro deseo: Iniciando Juego...", 2500, 1);
                Thread.Sleep(1000);
                LecturaEscritura.LimpiarBuffer();
            }
            else
            { //No inicio el juego por falta de API y de backup
                MensajesTerminal.TextoTiempo("Nos falta la esfera de 4 estrellas... Imposible iniciar el juego, presione una tecla para salir ", 1500, 1);
                Thread.Sleep(1000);
                LecturaEscritura.LimpiarBuffer();
                Environment.Exit(0);
            }
        }
    }
}
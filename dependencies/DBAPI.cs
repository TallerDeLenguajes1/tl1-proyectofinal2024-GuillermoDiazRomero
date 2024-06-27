using System.Text.Json;
using DBClass;
using DBAPI; //Uso este using que me permite usar el namespace DBAPI para Actualizar el juego
using Mensajes;

namespace DBAPI
{
    public class ConsumiendoAPI
    {
        public async Task<bool> TraerAPI() //Creo el Task de tipo bool para poder hacer un control de conexión con la API web
        {
            var url = "https://dragonball-api.com/api/characters?limit=58";
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage respuesta = await cliente.GetAsync(url);
                respuesta.EnsureSuccessStatusCode();
                string responseBody = await respuesta.Content.ReadAsStringAsync();
                var contenidoAPI = JsonSerializer.Deserialize<Root>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //Creo un archivo JSON que contenga toda la información de la API por si en algun momento el juego se ejecuta de forma offline
                string RespaldoJSON = @"resources\backup\Respaldo.JSON";
                //Pongo los foreach adentro de los ifs para evitar recorrer la API si ocurre algún error
                List<Item> LineasJSON = new List<Item>();
                if (!File.Exists(RespaldoJSON))
                {
                    using (File.Create(RespaldoJSON)) {/*Soluciona un error de acceso al archivo, porque al crearlo el FileStream deja al archivo abierto mientras que de esta forma se cierra el archivo*/};
                }
                string LineasConvertidasJSON = JsonSerializer.Serialize(LineasJSON);
                await File.WriteAllTextAsync(RespaldoJSON, JsonSerializer.Serialize(contenidoAPI));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }
    }
}

namespace ControlArchivosJSON
{
    public class Control()
    {
        public static async void ControlAPI() //Recordar agregar public al comienzo para poder utilizarlo en donde sea con Control.ControlApi()
        {
            ConsumiendoAPI serverON = new ConsumiendoAPI();
            bool control = await serverON.TraerAPI();
            string rutaRespaldo = @"resources\backup\Respaldo.JSON";
            if (control)
            { //Pregunto si se puede contactar con la API
                MensajesTerminal.TextoTiempo("Se logró contactar con Zeno-Sama...\nIniciando juego...", 2000, 1);
                Thread.Sleep(1000);
            }
            else if (File.Exists(rutaRespaldo))
            { //Pregunto si existe un JSON de respaldo
                MensajesTerminal.TextoTiempo("Imposible contactar con Zeno-Sama...\nUsando las esferas del dragon... \nShenlong consederá nuestro deseo: Iniciando Juego...", 2500, 1);
                Thread.Sleep(1000);
            }
            else
            { //No inicio el juego por falta de API y de backup
                MensajesTerminal.TextoTiempo("Nos falta la esfera de 4 estrellas... Imposible iniciar el juego, presione una tecla para salir ", 1500, 1);
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }
    }
}


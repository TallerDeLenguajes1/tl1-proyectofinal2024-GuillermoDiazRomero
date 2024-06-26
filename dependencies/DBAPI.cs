using System.Text.Json;
using DBClass;


namespace DBAPI
{
    public class ConsumiendoAPI
    {
        public async Task Probando()
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
                    File.Create(RespaldoJSON);
                }
                string LineasConvertidasJSON = JsonSerializer.Serialize(LineasJSON);
                await File.WriteAllTextAsync(RespaldoJSON, JsonSerializer.Serialize(contenidoAPI));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Imposible contactar con Bills-Sama por internet... Usando las esferas del dragon...");
            }
        }

    }
}

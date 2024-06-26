using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DBClass;

class ConsumiendoAPI
{
    private static async Task Probando(string[] args)
    {
        var url = "https://dragonball-api.com/api/characters?limit=58";
        try{
            HttpClient cliente = new HttpClient();
            HttpResponseMessage respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            string responseBody = await respuesta.Content.ReadAsStringAsync();
            var contenidoAPI = JsonSerializer.Deserialize<Root>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            string RespaldoJSON = @"assets\backup\Respaldo.JSON";
            //Pongo los foreach adentro de los ifs para evitar recorrer la API si ocurre algún error
            List<Item> LineasJSON = new List<Item>();

            if (!File.Exists(RespaldoJSON))
            {
                File.Create(RespaldoJSON);
                foreach (var personajes in contenidoAPI.Items){
                    LineasJSON.Add(personajes);
                }
                string LineasConvertidasJSON = JsonSerializer.Serialize(LineasJSON);
                File.WriteAllText(RespaldoJSON,LineasConvertidasJSON);
            }
            else{
                foreach (var personajes in contenidoAPI.Items){
                    LineasJSON.Add(personajes);
                }
                string LineasConvertidasJSON = JsonSerializer.Serialize(LineasJSON);
                File.WriteAllText(RespaldoJSON,LineasConvertidasJSON);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error #4, imposible contactar con Bills-Sama: {ex.Message}");
        }
    }

    static async Task Main(string[] args)
    {
        await Probando(args);
    }
}

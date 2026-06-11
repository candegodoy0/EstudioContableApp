using System.Net.Http.Json;
using EstudioContableApp.Models;

namespace EstudioContableApp.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient = new();

        // metodo que se encarga de ir a buscar los datos a la API

        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

                // si la respuesta fue correcta (200)
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<Cliente>>();

                    // si por alguna razon viene null, devuelvo lista vacia
                    return data ?? new List<Cliente>();
                }
                    // esto seria un error tipo 404, 500, etc.
                    throw new HttpRequestException($"Error HTTP: {response.StatusCode}");
                }
            }
}
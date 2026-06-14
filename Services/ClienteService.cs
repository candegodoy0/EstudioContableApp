using System.Net.Http.Json;
using EstudioContableApp.Models;

namespace EstudioContableApp.Services
{
    /// <summary>
    /// Servicio encargado de obtener clientes desde la API externa.
    /// </summary>
    public class ClienteService
    {
        private readonly HttpClient _httpClient = new();

        /// <summary>
        /// Obtiene la lista de clientes desde la API.
        /// </summary>
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